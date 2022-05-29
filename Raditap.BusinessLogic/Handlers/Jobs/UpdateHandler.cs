using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using Raditap.BusinessLogic.Contexts;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Jobs
{
    public class UpdateHandler : IRequestHandler<EditJobRequest, EditJobResponse>
    {
        private readonly ILogger<UpdateHandler> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFileOperationHelper _fileOperationHelper;
        private readonly UserContext _userContext;
        private readonly AppSettings _appSettings;

        public UpdateHandler(ILogger<UpdateHandler> logger, IJobRepository jobRepository, IOrderRepository orderRepository,
                          IFileOperationHelper fileOperationHelper, UserContext userContext, AppSettings appSettings)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _fileOperationHelper = fileOperationHelper;
            _userContext = userContext;
            _orderRepository = orderRepository;
            _appSettings = appSettings;
        }

        public async Task<EditJobResponse> Handle(EditJobRequest request, CancellationToken cancellationToken)
        {
            var oldJob = await _jobRepository.GetByIdAsync(request.JobId);
            if (oldJob == null)
            {
                _logger.LogWarning($"Job is not exists with Id: {request.JobId}");
                return new EditJobResponse(Result.JobNotFound);
            }

            if (oldJob.ManualNumber != request.ManualNumber)
            {
                var manualNumberJob = await _jobRepository.GetByManualNumber(request.ManualNumber);
                if (manualNumberJob != null)
                {
                    _logger.LogWarning($"Job is already exists with Manual Number: {request.ManualNumber}");
                    return new EditJobResponse(Result.JobAlreadyExists);
                }
            }

            var fileName = Path.GetFileName(oldJob.ImagePath);
            if (!string.IsNullOrWhiteSpace(fileName))
                _fileOperationHelper.RemoveFile(fileName);

            oldJob.ImagePath = null;

            if (request.Image?.Length > 1)
            {
                fileName = $"{DateTime.Now.ToString(_appSettings.FileNameFormat)}{_appSettings.FileExtension}";
                _fileOperationHelper.WriteFile(fileName, request.Image);
                oldJob.ImagePath = _fileOperationHelper.GetFullPath(fileName);
            }

            oldJob.BillingDate = request.BillDate;
            oldJob.DelieveryDate = request.DelDate;
            oldJob.Description = request.Description;
            oldJob.FinalQty = request.FinalQty;
            oldJob.FinalUnit = request.FinalUnit;
            oldJob.JobStatusId = request.StatusId;
            oldJob.ManualNumber = request.ManualNumber;
            oldJob.Note = request.Note;
            oldJob.PrinterDevice = request.PrtId;
            oldJob.SystemUserId = _userContext.CustomerData.Id;
            oldJob.UpdatedAt = DateTime.Now;
            oldJob.MaterialType = request.MaterialType;
            oldJob.CustomerName = request.CustomerName;

            await _jobRepository.UpdateAsync(oldJob);
            return new EditJobResponse(Result.Success);
        }
    }
}
