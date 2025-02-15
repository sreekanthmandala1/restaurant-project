using RestaurantMenuAPI.Models;

namespace RestaurantMenuAPI.Services
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem> GetByIdAsync(int id);
        Task<MenuItem> AddOrUpdateAsync(MenuItem menuItem);
        Task<MenuItem> UpdateAsync(MenuItem menuItem);
        Task<bool> DeleteAsync(int id);
    }
}
