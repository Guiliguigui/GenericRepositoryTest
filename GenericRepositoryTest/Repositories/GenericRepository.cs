using ExempleEFCore.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepositoryTest.Repositories
{
    internal class GenericRepository<TEntity> : BaseRepository, IRepository<TEntity> where TEntity : class, new()
    {
        private readonly Type typeEntity;
        public GenericRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            typeEntity = typeof(TEntity);
            if (typeEntity.GetProperty("Id") == null)
            {
                throw new Exception("The entity don't have an Id property");
            }
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            TEntity addedEntity = (await _db.Set<TEntity>().AddAsync(entity)).Entity;
            await _db.SaveChangesAsync();

            return addedEntity;
        }

        public virtual async Task<TEntity?> Find(int id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
                return null;
            TEntity entityFromDb = await _db.Set<TEntity>().FindAsync(typeEntity.GetProperty("Id").GetValue(entity));
            if (entityFromDb == null)
                return null;
            foreach (var property in typeEntity.GetProperties())
            {
                var valueFromDb = property.GetValue(entityFromDb);
                var valueNewEntity = property.GetValue(entity);
                if (property.Name != "Id" && property.CanWrite && valueFromDb != valueNewEntity)
                    property.SetValue(entityFromDb, valueNewEntity);
            }
            await _db.SaveChangesAsync();
            return await _db.Set<TEntity>().FindAsync(typeEntity.GetProperty("Id").GetValue(entity));
        }

        public virtual async Task<bool> Delete(int id)
        {
            TEntity entity = await _db.Set<TEntity>().FindAsync(id);
            _db.Set<TEntity>().Remove(entity);
            return await _db.SaveChangesAsync() == 1;
        }
    }
}
