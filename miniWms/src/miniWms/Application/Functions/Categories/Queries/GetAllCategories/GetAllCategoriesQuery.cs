using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Categories.Queries.GetAllCategories
{
    public record GetAllCategoriesQuery : IRequest<IList<Category>>;
}
