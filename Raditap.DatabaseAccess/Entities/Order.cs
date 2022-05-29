using System;
using System.Collections.Generic;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class Order
    {
        public long Id { get; set; }
        public string ManualNumber { get; set; }
        public int OrderStatusId { get; set; }
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public long? SystemUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CustomerMobileNumber { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
        public virtual User SystemUser { get; set; }
    }
}
