using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid CategoryId) : IRequest<ResponseBase<Category>>;
}
