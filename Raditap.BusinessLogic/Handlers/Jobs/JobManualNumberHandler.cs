using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Jobs
{
    public class JobManualNumberHandler : IRequestHandler<LastJobNumberRequest, LastJobNumberResponse>
    {
        private readonly ILogger<JobManualNumberHandler> _logger;
        private readonly IJobRepository _jobRepository;

        public JobManualNumberHandler(ILogger<JobManualNumberHandler> logger, IJobRepository jobRepository)
        {
            _logger = logger;
            _jobRepository = jobRepository;
        }

        public async Task<LastJobNumberResponse> Handle(LastJobNumberRequest request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetLastJob();
            var manualNumber = job?.ManualNumber == null ? 1 : Convert.ToInt64(job?.ManualNumber) + 1;

            var response = new LastJobNumberResponse(Result.Success)
            {
                ManualNumber = Convert.ToString(manualNumber)
            };

            _logger.LogWarning($"Last Job Manual Number is : {response.ManualNumber}");
            return response;
        }
    }
}
