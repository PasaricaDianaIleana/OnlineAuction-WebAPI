using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary.Repository
{
    public  class CategoryRepository:ICategory
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
          await  _context.Categories.AddAsync(category);
          await  _context.SaveChangesAsync();
          
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
               _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
        }

        public async Task<IList<Category>> GetAllAsync()
        {
             return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await  _context.Categories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();

           
        }
    }
}
