using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Jobs
{
    public class SearchHandler : IRequestHandler<SearchJobRequest, SearchJobResponse>
    {
        private readonly ILogger<SearchHandler> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFileOperationHelper _fileOperationHelper;
        private readonly FormatSettings _formatSettings;

        public SearchHandler(ILogger<SearchHandler> logger, IJobRepository jobRepository,
                          IFileOperationHelper fileOperationHelper, IOrderRepository orderRepository, FormatSettings formatSettings)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _fileOperationHelper = fileOperationHelper;
            _orderRepository = orderRepository;
            _formatSettings = formatSettings;
        }

        public async Task<SearchJobResponse> Handle(SearchJobRequest request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetJobById(request.JobId);

            if (job == null)
            {
                _logger.LogWarning($"Job is not found with Id : {request.JobId}");
                return new SearchJobResponse(Result.JobNotFound);
            }

            return new SearchJobResponse(Result.Success)
            {
                Job = new JobDto()
                {
                    BillingDate = job.BillingDate?.ToString(_formatSettings.DateTimeFormat),
                    CreatedAt = job.CreatedAt.ToString(_formatSettings.DateTimeFormat),
                    DelieveryDate = job.DelieveryDate?.ToString(_formatSettings.DateTimeFormat),
                    Description = job.Description,
                    FinalQty = job.FinalQty ?? 0,
                    FinalUnit = job.FinalUnit,
                    Image = job.ImagePath != null ? _fileOperationHelper.GetFile(Path.GetFileName(job.ImagePath)) : null,
                    JobId = job.Id,
                    ManualNumber = job.ManualNumber,
                    Note = job.Note,
                    OrderQty = job.OrderQty ?? 0,
                    OrderUnit = job.OrderUnit,
                    PrinterId = job.PrinterDevice,
                    StatusId = job.JobStatusId,
                    SystemUser = job.SystemUser?.Fullname,
                    UpdatedAt = job.UpdatedAt?.ToString(_formatSettings.DateTimeFormat),
                    MaterialType = job.MaterialType,
                    CustomerName = job.CustomerName
                }
            };
        }
    }
}
