using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Jobs
{
    public class ListHandler : IRequestHandler<ListJobRequest, ListJobResponse>
    {
        private readonly ILogger<ListHandler> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly FormatSettings _formatSettings;

        public ListHandler(ILogger<ListHandler> logger, IJobRepository jobRepository, FormatSettings formatSettings)
        {
            this._logger = logger;
            this._jobRepository = jobRepository;
            this._formatSettings = formatSettings;
        }

        public async Task<ListJobResponse> Handle(ListJobRequest request, CancellationToken cancellationToken)
        {
            var jobs = await _jobRepository.List(request.GetSkip(), request.PageSize, request.StatusId, request.CustomerName, request.ManualNumber, request.PrtId, request.DelDate, request.BillDate);
            var totalJobs = await _jobRepository.Count(request.StatusId, request.CustomerName, request.ManualNumber, request.PrtId, request.DelDate, request.BillDate);

            var result = new ListJobResponse(Result.Success)
            {
                PageInfo = new PageInfo(totalJobs, request.PageSize),
                Jobs = jobs?.Select(x => new ListJobDto()
                {
                    BillingDate = x.BillingDate?.ToString(_formatSettings.DateTimeFormat),
                    DeliveryDate = x.DelieveryDate?.ToString(_formatSettings.DateTimeFormat),
                    JobId = x.Id,
                    ManualNumber = x.ManualNumber,
                    PrinterId = x.PrinterDevice,
                    Status = x.JobStatus.Status,
                    CustomerName = x.CustomerName,
                    Description = x.Description

                }).ToList()
            };

            return result;
        }
    }
}
