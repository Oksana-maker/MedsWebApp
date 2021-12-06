using MedsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MedsWebApp.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : BaseModel
    {
        private readonly ApplicationContext _applicationContext;
        private readonly DbSet<TEntity> _dbSet;
        protected BaseRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            var props = typeof(ApplicationContext).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var property = props.FirstOrDefault(p => p.PropertyType == typeof(DbSet<TEntity>));
            if (property is null) throw new ArgumentException($"Current context do not contains DbSet of type {typeof(TEntity)}");
            _dbSet = property.GetValue(applicationContext) as DbSet<TEntity>;
        }
        protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where = null, bool noTracking = false)
        {
            IQueryable<TEntity> query = noTracking ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
            if (where == null) return query.Where(model => model.Active);
            else
            {
                var parameter = where.Parameters[0];
                var property = Expression.Property(parameter, nameof(BaseModel.Active));
                Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(where.Body, property), parameter);
                return query.Where(lambda);
            }
        }
        public async Task<TEntity> Insert(TEntity entity)
        {
            var insertedEntity = await _dbSet.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
            return insertedEntity.Entity;
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            var dbEntity = await Get(en => en.Id == entity.Id).FirstOrDefaultAsync();
            if (dbEntity == null) return null; //throw new ArgumentException($"{entity.GetType().Name} with Id = {entity.Id} not found");
            dbEntity.CopyFrom(entity);
            await _applicationContext.SaveChangesAsync();
            return dbEntity;
        }

        public async Task<TEntity> Delete(int entityId)
        {
            var dbEntity = await Get(en => en.Id == entityId).FirstOrDefaultAsync();
            if (dbEntity == null) return null; // throw new ArgumentException($"{typeof(TEntity).Name} with Id = {entityId} not found");
            dbEntity.Active = false;
            await _applicationContext.SaveChangesAsync();
            return dbEntity;
        }
    }
}
