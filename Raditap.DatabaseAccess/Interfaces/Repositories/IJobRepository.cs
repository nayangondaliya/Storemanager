using Raditap.DatabaseAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Interfaces.Repositories
{
    public interface IJobRepository : IAsyncRepository<Job>
    {
        Task<Job> GetByManualNumber(string manualNumber);
        Task<Job> GetJobById(long jobId);
        Task<IReadOnlyList<Job>> List(int? skip, int? take, int statusId, string customerName, string manualNumber, string printerId, DateTime? delieveryDate, DateTime? billingDate);
        Task<int> Count(int statusId, string customerName, string manualNumber, string printerId, DateTime? delieveryDate, DateTime? billingDate);
        Task<Job> GetLastJob();
        Task<List<Job>> GetJobsByUserId(long userId);
    }
}
