using Microsoft.EntityFrameworkCore;
using N5.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace N5.Core.Repository
{
    public class PostgresqlRepository<T, K> : IRepository<T, K> where T : class
                                                                where K : DbContext, new()
    {
        public PostgresqlRepository()
        {
        }

        public void Add(T item)
        {
            using (var db = new K())
            {
                db.Set<T>().Add(item);
                db.SaveChanges();
            }
        }

        public T Find(object id)
        {
            using (var db = new K())
            {
                return db.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> List()
        {
            using (var db = new K())
            {
                return db.Set<T>().ToList();
            }
        }

        public IEnumerable<T> ListIncludeMembers(params Expression<Func<T, object>>[] includes)
        {
            using (var db = new K())
            {
                return db.Set<T>().IncludeMultiple(includes).ToList();
            }
        }

        public IEnumerable<T> List(Func<T, bool> expression)
        {
            using (var db = new K())
            {
                return db.Set<T>().Where(expression);
            }
        }

        public void Truncate()
        {
            using (var db = new K())
            {
                var list = db.Set<T>().Select(x => x).ToList();
                foreach (var item in list)
                {
                    db.Remove(item);
                }
                db.SaveChanges();
            }
        }
    }
}
