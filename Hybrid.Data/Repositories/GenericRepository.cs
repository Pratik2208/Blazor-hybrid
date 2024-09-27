using Hybrid.Shared;
using Microsoft.EntityFrameworkCore;

namespace Hybrid.Data.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        private readonly CosmosDbContext dbContext;
        private readonly DbSet<T> dbSet;
        protected GenericRepository(CosmosDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public async Task<MethodResult> AddEntity(T entity)
        {
            try
            {
                await dbContext.AddAsync(entity!);
                await dbContext.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (DbUpdateException ex)
            {
                return MethodResult.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MethodResult<T>> Get(object id)
        {
            try
            {
                var entity = await dbSet.FindAsync(id);
                return MethodResult<T>.Success(entity!);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MethodResult> Update(T entity)
        {
            try
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                dbSet.Update(entity);
                await dbContext.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MethodResult> Delete(object id, string className)
        {
            try
            {
                var entity = await Get(id);
                if (entity.Data == null)
                {
                    throw new Exception($"{className} with given Id {id} not found");
                }
                dbSet.Remove(entity.Data);
                await dbContext.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MethodResult<List<T>>> GetAll()
        {
            try
            {
                var entities = await dbSet.ToListAsync();
                return MethodResult<List<T>>.Success(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MethodResult> DeleteAll(IEnumerable<T> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);
                await dbContext.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
