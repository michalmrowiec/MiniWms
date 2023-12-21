using FluentValidation.Results;
using miniWms.Domain.Entities;

namespace miniWms.Application.Functions.Warehouses.Commands
{
    public class WarehouseResponse : ResponseBase
    {
        public Warehouse? Warehouse { get; set; }

        public WarehouseResponse(Warehouse warehouse) : base()
        {
            Warehouse = warehouse;
            Success = true;
            ValidationErrors = new();
        }

        public WarehouseResponse(bool status, string message) : base(status, message)
        { }

        public WarehouseResponse(bool success, string? message, ValidationResult validationResult) : base(success, message, validationResult)
        { }

        public WarehouseResponse(ValidationResult validationResult) : base(validationResult)
        { }
    }
}
