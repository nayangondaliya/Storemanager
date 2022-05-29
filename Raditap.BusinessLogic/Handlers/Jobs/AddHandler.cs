using MediatR;
using Microsoft.Extensions.Logging;
using Raditap.BusinessLogic.ApiDataObjects;
using Raditap.BusinessLogic.ApiDataObjects.Jobs;
using Raditap.BusinessLogic.Contexts;
using Raditap.BusinessLogic.Interfaces.Helpers;
using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DataObjects.AppSettings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Raditap.BusinessLogic.Handlers.Jobs
{
    public class AddHandler : IRequestHandler<AddJobRequest, AddJobResponse>
    {
        private readonly ILogger<AddHandler> _logger;
        private readonly IJobRepository _jobRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IFileOperationHelper _fileOperationHelper;
        private readonly UserContext _userContext;
        private readonly AppSettings _appSettings;

        public AddHandler(ILogger<AddHandler> logger, IJobRepository jobRepository, IOrderRepository orderRepository,
                          IFileOperationHelper fileOperationHelper, UserContext userContext, AppSettings appSettings)
        {
            _logger = logger;
            _jobRepository = jobRepository;
            _fileOperationHelper = fileOperationHelper;
            _userContext = userContext;
            _orderRepository = orderRepository;
            _appSettings = appSettings;
        }

        public async Task<AddJobResponse> Handle(AddJobRequest request, CancellationToken cancellationToken)
        {
            var oldJob = await _jobRepository.GetByManualNumber(request.ManualNumber);

            if(oldJob != null)
            {
                _logger.LogWarning($"Job is already exists with manual number: {request.ManualNumber}");
                return new AddJobResponse(Result.JobAlreadyExists);
            }


            Job newJob = new Job();
            
            newJob.CreatedAt = DateTime.Now;
            newJob.Description = request.Description;
            newJob.JobStatusId = request.StatusId;
            newJob.ManualNumber = request.ManualNumber;
            newJob.Note = request.Note;
            newJob.PrinterDevice = request.PrtId;
            newJob.SystemUserId = _userContext.CustomerData.Id;
            newJob.OrderQty = request.OrderQty;
            newJob.OrderUnit = request.OrderUnit;
            newJob.FinalQty = request.FinalQty;
            newJob.FinalUnit = request.FinalUnit;
            newJob.DelieveryDate = request.DelDate;
            newJob.BillingDate = request.BillDate;
            newJob.MaterialType = request.MaterialType;
            newJob.CustomerName = request.CustomerName;

            if(request.Image != null)
            {
                var fileName = $"{DateTime.Now.ToString(_appSettings.FileNameFormat)}{_appSettings.FileExtension}";
                _fileOperationHelper.WriteFile(fileName, request.Image);
                newJob.ImagePath = _fileOperationHelper.GetFullPath(fileName);
            }

            await _jobRepository.AddAsync(newJob);
            return new AddJobResponse(Result.Success);
        }
    }
}
