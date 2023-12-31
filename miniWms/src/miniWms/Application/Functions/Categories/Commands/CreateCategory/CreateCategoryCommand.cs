using MediatR;

namespace miniWms.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<ResponseBase>
    {
        public string CategoryName { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
