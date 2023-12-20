using miniWms.Domain.Entities;

namespace miniWms.Application.Contracts
{
    public interface IWarehouseRepository
    {
        Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse);
        Task<Warehouse> UpdateWarehouseAsync(Warehouse Warehouse);
        Task<bool> DeleteWarehouseAsync(Guid id);
        Task<Warehouse> GetWarehouseByIdAsync(Guid id); // with products?
        Task<Warehouse> GetAllWarehousesAsync();

    }
}
