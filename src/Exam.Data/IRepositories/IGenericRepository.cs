using System.Linq.Expressions;

namespace Exam.Data.IRepositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null);
        Task<bool> DeleteRangeAsync(ICollection<T> entities);
        Task SaveAsyn();
    }
}
