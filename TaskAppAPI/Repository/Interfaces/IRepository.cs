
using System.Linq.Expressions;

namespace TaskAppAPI.Repository.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T newEntity);
        void Update(T entity);

        void Delete(T entity);
        Task<T> FindByIdAsync(Guid id);
        Task<List<T>> FindAll();
        Task<List<TDto>> FindAllAsync<TDto>(Expression<Func<T, TDto>> selector);

        Task<int> CommitAsync();
    }
}
