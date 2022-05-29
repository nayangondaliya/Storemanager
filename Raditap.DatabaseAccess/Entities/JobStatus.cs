using System;
using System.Collections.Generic;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class JobStatus
    {
        public JobStatus()
        {
            Job = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Job> Job { get; set; }
    }
}
