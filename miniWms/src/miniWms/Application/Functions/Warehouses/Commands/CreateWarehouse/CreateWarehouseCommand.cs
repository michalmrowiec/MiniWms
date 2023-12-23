using MediatR;

namespace miniWms.Application.Functions.Warehouses.Commands.CreateWarehouse
{
    public class CreateWarehouseCommand : IRequest<WarehouseResponse>
    {
        public string WarehouseName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
