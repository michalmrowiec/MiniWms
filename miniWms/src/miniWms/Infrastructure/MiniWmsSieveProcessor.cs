using Microsoft.Extensions.Options;
using miniWms.Domain.Entities;
using Sieve.Models;
using Sieve.Services;

namespace miniWms.Infrastructure
{
    public class MiniWmsSieveProcessor : SieveProcessor
    {
        public MiniWmsSieveProcessor(IOptions<SieveOptions> options) : base(options)
        { }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Product>(p => p.ProductName)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(p => p.ProductDescription)
                .CanSort()
                .CanFilter();

            mapper.Property<Product>(p => p.Category.CategoryName)
                .CanSort()
                .CanFilter()
                .HasName("categoryName");

            return mapper;
        }
    }
}
