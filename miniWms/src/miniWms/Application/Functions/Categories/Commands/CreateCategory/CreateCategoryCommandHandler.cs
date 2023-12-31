using MediatR;
using miniWms.Application.Contracts;
using miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ResponseBase>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CreateCategoryCommandHandler(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ResponseBase> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new CreateCategoryValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                return new ResponseBase(validationResult);
            }

            Category category = new()
            {
                CategoryName = request.CategoryName,
                CreatedBy = request.CreatedBy,
                ModifiedBy = request.CreatedBy,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            var addedCategory = await _categoriesRepository.CreateAsync(category);

            return new ResponseBase<Category>(addedCategory);
        }
    }
}
