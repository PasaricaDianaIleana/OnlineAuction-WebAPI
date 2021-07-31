using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Repository
{
   public interface ICategory
    {
       Task<IList<Category>> GetAllAsync();
        Task AddAsync(Category category);
       Task<Category> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
