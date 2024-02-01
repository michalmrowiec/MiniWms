using MediatR;
using miniWms.Application.Contracts;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ResponseBase<Category>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public GetCategoryByIdQueryHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ResponseBase<Category>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            Category category;
            try
            {
                category = await _categoriesRepository.GetByIdAsync(request.CategoryId);

            }
            catch (Exception ex)
            {
                return new ResponseBase<Category>(false, "Something went wrong." + ex.Message);
            }

            return new ResponseBase<Category>(category);
        }
    }
}
