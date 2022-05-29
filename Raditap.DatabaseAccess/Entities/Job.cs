using System;
using System.Collections.Generic;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class Job
    {
        public long Id { get; set; }
        public string ManualNumber { get; set; }
        public int JobStatusId { get; set; }
        public string Description { get; set; }
        public string PrinterDevice { get; set; }
        public int? OrderQty { get; set; }
        public string OrderUnit { get; set; }
        public int? FinalQty { get; set; }
        public string FinalUnit { get; set; }
        public DateTime? DelieveryDate { get; set; }
        public DateTime? BillingDate { get; set; }
        public string Note { get; set; }
        public string ImagePath { get; set; }
        public long? SystemUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string MaterialType { get; set; }
        public string CustomerName { get; set; }

        public virtual JobStatus JobStatus { get; set; }
        public virtual User SystemUser { get; set; }
    }
}
