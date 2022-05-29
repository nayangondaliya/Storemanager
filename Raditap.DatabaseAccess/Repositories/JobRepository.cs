using Raditap.DatabaseAccess.Entities;
using Raditap.DatabaseAccess.Interfaces.Repositories;
using Raditap.DatabaseAccess.Specifications.Jobs;
using Raditap.DataObjects.AppSettings;
using Raditap.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raditap.DatabaseAccess.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(RaditapContext writeContext, ReadRaditapContext readContext, DatabaseSettings databaseSettings, ProcessingTimeHelper timeHelper) :
               base(writeContext, readContext, databaseSettings, timeHelper)
        { }

        public async Task<int> Count(int statusId, string customerName, string manualNumber, string printerId, DateTime? delieveryDate, DateTime? billingDate)
        {
            var spec = new JobFilterPaginatedSpecification(null, null, statusId, customerName, manualNumber, printerId, delieveryDate, billingDate);
            var total = await CountAsync(spec);
            return total;
        }

        public async Task<Job> GetByManualNumber(string manualNumber)
        {
            var spec = new JobManualNumberFilterSpecification(manualNumber);
            var job = (await ListAsync(spec)).FirstOrDefault();
            return job;
        }

        public async Task<Job> GetJobById(long jobId)
        {
            var spec = new JobIdFilterSpecification(jobId);
            var job = (await ListAsync(spec)).FirstOrDefault();
            return job;
        }

        public async Task<List<Job>> GetJobsByUserId(long userId)
        {
            var spec = new JobsByUserIdFilterSpecification(userId);
            var jobs = await ListAsync(spec);
            return jobs?.ToList();
        }

        public async Task<Job> GetLastJob()
        {
            var spec = new JobManualNumberSpecification();
            var job = (await ListAsync(spec)).FirstOrDefault();
            return job;
        }

        public async Task<IReadOnlyList<Job>> List(int? skip, int? take, int statusId, string customerName, string manualNumber, string printerId, DateTime? delieveryDate, DateTime? billingDate)
        {
            var spec = new JobFilterPaginatedSpecification(skip, take, statusId, customerName, manualNumber, printerId, delieveryDate, billingDate);
            var jobs = await ListAsync(spec);
            return jobs;
        }
    }
}
