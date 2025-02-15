using RestaurantMenuAPI;
using RestaurantMenuAPI.Models;
public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetAllAsync();
    Task<MenuItem> GetByIdAsync(int id);
    Task<MenuItem> AddOrUpdateAsync(MenuItem menuItem);
    Task<MenuItem> UpdateAsync(MenuItem menuItem);
    Task<bool> DeleteAsync(int id);
}
