using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> set;
        public Repository(ApplicationDbContext dbContex) : base()
        {
            _dbContext = dbContex;
            set = _dbContext.Set<T>();
        }
        void IRepository<T>.Add(T Entity)
        {
            set.Add(Entity);
        }

        T IRepository<T>.Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = set;
            query= query.Where(filter);
            return query.FirstOrDefault();
        }

        IEnumerable<T> IRepository<T>.GetAll()
        {
            IQueryable<T> query = set;
            //query = query.Where(filter);
            return query.ToList();
        }

        void IRepository<T>.Remove(T Entity)
        {
            set.Remove(Entity);
        }

        void IRepository<T>.RemoveRange(IEnumerable<T> Entity)
        {
            set.RemoveRange(Entity);
        }
    }
}
