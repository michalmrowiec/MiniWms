﻿using MediatR;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ResponseBase<Product>>
    {
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string Unit { get; set; }
        public bool IsWeight { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
