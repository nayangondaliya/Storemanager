using System.Collections.Generic;
using Raditap.DatabaseAccess.Helpers.Query;

namespace Raditap.DatabaseAccess.Interfaces
{
    public interface IIncludeQuery
    {
        Dictionary<IIncludeQuery, string> PathMap { get; }
        IncludeVisitor Visitor { get; }
        HashSet<string> Paths { get; }
    }

    public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery { }
}
