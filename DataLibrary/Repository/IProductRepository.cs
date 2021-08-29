using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Repository
{
   public interface IProductRepository
    {
       Task<IList<Product>> GetAll();
        Task AddProduct(Product product);
       Task<Product> getProductById(int id);
        int GetBidNumber(int id);
       Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<Product> EditProduct(Product product);
    }
}
