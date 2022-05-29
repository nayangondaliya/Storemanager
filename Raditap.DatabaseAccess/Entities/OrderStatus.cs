using System;
using System.Collections.Generic;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
