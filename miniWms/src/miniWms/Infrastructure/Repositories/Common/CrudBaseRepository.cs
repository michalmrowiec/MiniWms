﻿using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts.Common;

namespace miniWms.Infrastructure.Repositories.Common
{
    public class CrudBaseRepository<TEntity, TId> : ICrudRepository<TEntity, TId> where TEntity : class, new()
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<TEntity> _logger;

        public CrudBaseRepository(MiniWmsDbContext context, ILogger<TEntity> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return await _context.SaveChangesAsync() == 0;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id) ?? new();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
