using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IList<Category>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public GetAllCategoriesQueryHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IList<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetAllAsync();
        }
    }
}
