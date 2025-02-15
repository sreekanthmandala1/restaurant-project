using Microsoft.EntityFrameworkCore;
using RestaurantMenuAPI.Data;
using RestaurantMenuAPI.Models;

namespace RestaurantMenuAPI.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;
        public MenuItemRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<MenuItem>> GetAllAsync() => await _context.MenuItems.ToListAsync();
        public async Task<MenuItem> GetByIdAsync(int id) => await _context.MenuItems.FindAsync(id);
        //public async Task<MenuItem> AddAsync(MenuItem menuItem)
        //{
        //    _context.MenuItems.Add(menuItem);
        //    await _context.SaveChangesAsync();
        //    return menuItem;
        //}
        public async Task<MenuItem> AddOrUpdateAsync(MenuItem menuItem)
        {
            if (menuItem.Id == 0 || menuItem.Id == null)
            {
                // **Create a new menu item**
                _context.MenuItems.Add(menuItem);
            }
            else
            {
                var existingItem = await _context.MenuItems
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(m => m.Id == menuItem.Id);

                if (existingItem == null)
                {
                    // If ID is provided but not found in DB, treat it as a new entry
                    menuItem.Id = 0;
                    _context.MenuItems.Add(menuItem);
                }
                else
                {
                    // **Detach existing entity to prevent tracking conflicts**
                    _context.Entry(existingItem).State = EntityState.Detached;

                    // **Attach & update**
                    _context.MenuItems.Update(menuItem);
                }
            }

            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem> UpdateAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null) return false;
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
