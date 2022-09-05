using Exam.Data.Contexts;
using Exam.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exam.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly ExamDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository()
        {
            _context = new ExamDbContext();
            dbSet = _context.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var entityCreated = await dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entityCreated.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await dbSet.FirstOrDefaultAsync(expression);

            if (entity is null)
                return false;

            dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
            => expression is null ? dbSet : dbSet.Where(expression);

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
            => dbSet.FirstOrDefaultAsync(expression);

        public T Update(T entity)
        {
            var entityUpdated = dbSet.Update(entity);

            _context.SaveChanges();

            return entityUpdated.Entity;
        }
        public async Task<bool> DeleteRangeAsync(ICollection<T> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task SaveAsyn()
            => await _context.SaveChangesAsync();
    }
}