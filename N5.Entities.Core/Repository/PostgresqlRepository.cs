using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Entities.Core.Repository
{
    public class PostgresqlRepository<T> : IRepository<T> where T : class
    {
        public void Add(T item)
        {
            using (var db = new EntityFrameworkCoreContext())
            {
                db.Set<T>().Add(item);
                db.SaveChanges();
            }
        }

        public T Find(object id)
        {
            using (var db = new EntityFrameworkCoreContext())
            {
                return db.Set<T>().Find(id);
            }
        }

        public IEnumerable<T> List()
        {
            using (var db = new EntityFrameworkCoreContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public IEnumerable<T> ListIncludeMembers(params string[] expressions)
        {
            IQueryable<T> data = null;

            using (var db = new EntityFrameworkCoreContext())
            {
                foreach (var expression in expressions)
                {
                    if (data == null)
                    {
                        data = db.Set<T>().Include(expression);
                    }
                    else
                    {
                        data = data.Include(expression);
                    }
                }
                return data.ToList();
            }
        }

        public IEnumerable<T> List(Func<T, bool> expression)
        {
            using (var db = new EntityFrameworkCoreContext())
            {
                return db.Set<T>().Where(expression);
            }
        }

        public void Truncate()
        {
            using (var db = new EntityFrameworkCoreContext())
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