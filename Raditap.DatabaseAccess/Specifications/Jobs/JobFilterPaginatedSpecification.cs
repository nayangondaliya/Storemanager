using Raditap.DatabaseAccess.Entities;
using System;

namespace Raditap.DatabaseAccess.Specifications.Jobs
{
    public sealed class JobFilterPaginatedSpecification : BaseSpecification<Job>
    {
        public JobFilterPaginatedSpecification(int? skip, int? take, int statusId, string customerName, string manualNumber, string printerId, DateTime? delieveryDate, DateTime? billingDate)
            : base(x => (string.IsNullOrWhiteSpace(manualNumber) || x.ManualNumber == manualNumber) &&
                        (statusId == 0 || x.JobStatusId == statusId) &&
                        (string.IsNullOrWhiteSpace(customerName) || x.CustomerName.Contains(customerName)) &&
                        (string.IsNullOrWhiteSpace(printerId) || x.PrinterDevice == printerId) &&
                        (delieveryDate == null || (x.DelieveryDate >= delieveryDate && x.DelieveryDate <= delieveryDate)) &&
                        (billingDate == null || (x.BillingDate >= billingDate && x.BillingDate <= billingDate)))
        {
            if (skip.HasValue && take.HasValue) ApplyPaging(skip.GetValueOrDefault(), take.GetValueOrDefault());
            AddInclude(x => x.JobStatus);
            ApplyOrderByDescending(x => x.Id);
        }
    }
}
