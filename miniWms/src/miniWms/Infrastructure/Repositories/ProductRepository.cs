﻿using Microsoft.EntityFrameworkCore;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;
using miniWms.Domain.Models;
using miniWms.Infrastructure.Repositories.Common;
using Sieve.Models;
using Sieve.Services;

namespace miniWms.Infrastructure.Repositories
{
    public class ProductRepository : CrudBaseRepository<Product, Guid, ProductRepository>, IProductsRepository
    {
        private readonly MiniWmsDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        private readonly ISieveProcessor _sieveProcessor;

        public ProductRepository(
            MiniWmsDbContext context,
            ILogger<ProductRepository> logger,
            ISieveProcessor sieveProcessor) : base(context, logger)
        {
            _context = context;
            _logger = logger;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedResult<Product>> GetSortedAndFilteredProductsAsync(SieveModel sieveModel)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            var filteredProducts = await _sieveProcessor
                .Apply(sieveModel, products)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, products, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Product>(filteredProducts, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
