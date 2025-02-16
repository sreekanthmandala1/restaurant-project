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
        
        public async Task<MenuItem> AddOrUpdateAsync(MenuItem menuItem)
        {
            if ( menuItem.Id == null)
            {
                
                _context.MenuItems.Add(menuItem);
            }
            else
            {
                var existingItem = await _context.MenuItems
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(m => m.Id == menuItem.Id);

                if (existingItem == null)
                {
                    
                    menuItem.Id = 0;
                    _context.MenuItems.Add(menuItem);
                }
                else
                {
                   
                    _context.Entry(existingItem).State = EntityState.Detached;

                    
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
