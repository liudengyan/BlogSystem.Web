using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class BaseService<T> : IDisposable where T : Models.BaseEntity, new()
    {
        public Models.EntityContext.EntityContext db = new Models.EntityContext.EntityContext();

        public async Task CreatAsync(T t)
        {
            db.Set<T>().Add(t);
            await db.SaveChangesAsync();
        }

        public async Task RemoveAsync(long id)
        {
            var data = await db.Set<T>().FindAsync(id);
            data.IsRemoved = true;
            await db.SaveChangesAsync();
        }

        public async Task EditAsync(T t)
        {
            db.Entry(t).State = System.Data.Entity.EntityState.Modified; 
            await db.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>().AsNoTracking().Where(m => !m.IsRemoved);
        }

        public async Task<T> GetOneAsync(long id)
        {
            return await GetAll().FirstAsync(m => m.Id == id);
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate,bool isAsc = true) 
        {
            if (isAsc)
            {
                return GetAllWhere(predicate).OrderBy(m => m.CreatTime);
            }
            else
            {
                return GetAllWhere(predicate).OrderByDescending(m => m.CreatTime);
            }
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate, int pageindex = 0, int pagesize = 10)
        {
            return GetAllWhere(predicate).Skip(pageindex * pagesize).Take(pagesize);
        }

        public IQueryable<T> GetAllWhere(Expression<Func<T, bool>> predicate, bool isAsc = true , int pageindex = 0, int pagesize = 10)
        {
            if (isAsc)
            {
                return GetAllWhere(predicate).OrderBy(m=>m.CreatTime).Skip(pageindex * pagesize).Take(pagesize);
            }
            else
            {
                return GetAllWhere(predicate).OrderByDescending(m => m.CreatTime).Skip(pageindex * pagesize).Take(pagesize);
            }
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().AnyAsync(predicate);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
