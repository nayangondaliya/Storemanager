using System;
using System.Collections.Generic;

namespace Raditap.DatabaseAccess.Entities
{
    public partial class User
    {
        public User()
        {
            InverseSystemUser = new HashSet<User>();
            Job = new HashSet<Job>();
            Order = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Passcode { get; set; }
        public long? MobileNumber { get; set; }
        public long? SystemUserId { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? LastUpdated { get; set; }

        public virtual User SystemUser { get; set; }
        public virtual ICollection<User> InverseSystemUser { get; set; }
        public virtual ICollection<Job> Job { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
