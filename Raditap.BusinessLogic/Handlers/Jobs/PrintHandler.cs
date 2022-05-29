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
    public class PrintHandler : IRequestHandler<PrintJobRequest, PrintJobResponse>
    {
        private readonly ILogger<PrintHandler> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFileOperationHelper _fileOperationHelper;
        private readonly FormatSettings _formatSettings;

        public PrintHandler(ILogger<PrintHandler> logger, IJobRepository jobRepository,
            IFileOperationHelper fileOperationHelper, IOrderRepository orderRepository, FormatSettings formatSettings)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _fileOperationHelper = fileOperationHelper;
            _orderRepository = orderRepository;
            _formatSettings = formatSettings;
        }

        public async Task<PrintJobResponse> Handle(PrintJobRequest request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetJobById(request.JobId);

            if (job == null)
            {
                _logger.LogWarning($"Job is not found with Id : {request.JobId}");
                return new PrintJobResponse(Result.JobNotFound);
            }

            return new PrintJobResponse(Result.Success)
            {
                Job = new PrintJobDto()
                {
                    CreateDate = job.CreatedAt.ToString(_formatSettings.JobPrintDateFormat),
                    DeliveryDate = job.DelieveryDate?.ToString(_formatSettings.JobPrintDateFormat),
                    Description = job.Description,
                    FinalQty = job.FinalQty ?? 0,
                    Image = job.ImagePath != null ? _fileOperationHelper.GetFile(Path.GetFileName(job.ImagePath)) : null,
                    ManualNumber = job.ManualNumber,
                    CustomerName = job.CustomerName,
                    MaterialType = job.MaterialType,
                    PrtId = job.PrinterDevice
                }
            };
        }
    }
}
