using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ResponseBase<Product>>;
}
