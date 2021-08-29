using DataLibrary.Models;
using DataLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddProduct(Product product)
        {
           await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product =await getProductById(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

        }

        public async Task<Product> EditProduct(Product product)
        {
            var data = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (data != null)
            {
                data.Category.CategoryId = product.Category.CategoryId;
                data.CreatedDate = product.CreatedDate;
                data.Description = product.Description;
                data.ImageUrl = product.ImageUrl;
                data.Name = product.Name;
                data.Price = product.Price;
                data.ProductId = product.ProductId;
                data.UserId = product.UserId;
                data.InProcess = product.InProcess;
                await _context.SaveChangesAsync();
                return data;
            }
            return null;
        }
       
        public async Task<IList<Product>> GetAll()
        {
            return await _context.Products.Include(bid=>bid.Bids).Include(c=>c.Category).
                ToListAsync();
        }

        public int GetBidNumber(int id)
        {
          return _context.Bids.Count(d => d.ProductId == id);
           
        }

        public async Task<Product> getProductById(int id)
        {
            return await _context.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();
            
        }

        public Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
