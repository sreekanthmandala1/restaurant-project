using RestaurantMenuAPI.Models;
using RestaurantMenuAPI.Repositories;

namespace RestaurantMenuAPI.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _menuItemRepository.GetAllAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _menuItemRepository.GetByIdAsync(id);
        }

        //public async Task<MenuItem> AddAsync(MenuItem menuItem)
        //{
        //    // Example business logic before saving
        //    if (string.IsNullOrEmpty(menuItem.Name))
        //        throw new ArgumentException("Menu Item name cannot be empty");

        //    return await _menuItemRepository.AddAsync(menuItem);
        //}
        public async Task<MenuItem> AddOrUpdateAsync(MenuItem menuItem)
        {
            if (string.IsNullOrWhiteSpace(menuItem.Name))
                throw new ArgumentException("Menu item name cannot be empty.");

            return await _menuItemRepository.AddOrUpdateAsync(menuItem);
        }

        public async Task<MenuItem> UpdateAsync(MenuItem menuItem)
        {
            return await _menuItemRepository.UpdateAsync(menuItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _menuItemRepository.DeleteAsync(id);
        }
    }
}
