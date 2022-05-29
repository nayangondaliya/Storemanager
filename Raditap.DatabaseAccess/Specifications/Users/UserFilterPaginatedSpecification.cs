using Raditap.DatabaseAccess.Entities;
using System;

namespace Raditap.DatabaseAccess.Specifications.Users
{
    public sealed class UserFilterPaginatedSpecification : BaseSpecification<User>
    {
        public UserFilterPaginatedSpecification(int? skip, int? take, string firstName, string lastName, string userName, string mobileNumber)
            : base(x => (string.IsNullOrWhiteSpace(firstName) || x.FirstName == firstName) &&
                         (string.IsNullOrWhiteSpace(lastName) || x.LastName == lastName) &&
                        (string.IsNullOrWhiteSpace(userName) || x.Email == userName) &&
            (string.IsNullOrWhiteSpace(mobileNumber) || x.MobileNumber == Convert.ToInt64(mobileNumber)))
        {
            if (skip.HasValue && take.HasValue) ApplyPaging(skip.GetValueOrDefault(), take.GetValueOrDefault());
            ApplyOrderByDescending(x => x.Id);
        }
    }
}
