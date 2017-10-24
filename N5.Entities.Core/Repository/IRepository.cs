using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Entities.Core.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);

        IEnumerable<T> List();

        IEnumerable<T> ListIncludeMembers(params string[] expressions);

        IEnumerable<T> List(Func<T, bool> expression);

        T Find(object id);

        void Truncate();
    }
}
