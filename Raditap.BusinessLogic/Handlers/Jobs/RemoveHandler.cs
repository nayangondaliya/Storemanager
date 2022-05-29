using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Jobs
{
    public class RemoveHandler : IRequestHandler<RemoveJobRequest, RemoveJobResponse>
    {
        private readonly ILogger<RemoveHandler> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IFileOperationHelper _fileOperationHelper;
        
        public RemoveHandler(ILogger<RemoveHandler> logger, IJobRepository jobRepository,
                          IFileOperationHelper fileOperationHelper)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _fileOperationHelper = fileOperationHelper;
        }

        public async Task<RemoveJobResponse> Handle(RemoveJobRequest request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);

            if (job == null)
            {
                _logger.LogWarning($"Job is not found with Id : {request.JobId}");
                return new RemoveJobResponse(Result.JobNotFound);
            }

            if (job.ImagePath != null)
                _fileOperationHelper.RemoveFile(Path.GetFileName(job.ImagePath));

            await _jobRepository.DeleteAsync(job);
            return new RemoveJobResponse(Result.Success);
        }
    }
}
