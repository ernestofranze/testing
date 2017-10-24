using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace N5.Core.Repository
{
    public interface IRepository<T, K>
    {
        void Add(T item);

        IEnumerable<T> List();

        IEnumerable<T> ListIncludeMembers(params Expression<Func<T, object>>[] includes);

        IEnumerable<T> List(Func<T, bool> expression);

        T Find(object id);

        void Truncate();
    }
}
