﻿using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts.Common;
using static Sieve.Extensions.MethodInfoExtended;

namespace miniWms.Infrastructure.Repositories.Common
{
    public class CrudBaseRepository<TEntity, TId, TLogger> : ICrudRepository<TEntity, TId>
        where TEntity : class
        where TLogger : class
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<TLogger> _logger;

        public CrudBaseRepository(MiniWmsDbContext context, ILogger<TLogger> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await _context
                .Set<TEntity>()
                .AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context
                .Set<TEntity>()
                .Remove(entity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _context
                .Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var result =  await _context
                .Set<TEntity>()
                .FindAsync(id);

            return result ?? throw new Exception("The object with the given id was not found.");
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
