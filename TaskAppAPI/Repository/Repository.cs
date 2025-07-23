using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskAppAPI.Data;
using TaskAppAPI.Repository.Interfaces;

namespace TaskAppAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<T> DbSet;
        public Repository(ApplicationDbContext applicationDbContext) 
        {
            this._applicationDbContext = applicationDbContext;
            DbSet = this._applicationDbContext.Set<T>();
        }
        public void Add(T newEntity)
        {
            DbSet.Add(newEntity);
        }
        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<List<TDto>> FindAllAsync<TDto>(Expression<Func<T, TDto>> selector)
        {
            return await DbSet.Select(selector).ToListAsync();
        }

        public async Task<T> FindByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> CommitAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> FindAll() 
        {
            return await DbSet.ToListAsync();
        }


    }
}
