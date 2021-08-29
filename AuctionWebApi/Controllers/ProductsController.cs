using AuctionWebApi.ModelsDTO.Product;
using DataLibrary.Models;
using DataLibrary.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IHostEnvironment _host;

        public ProductsController(IProductRepository repo,IHostEnvironment host)
        {
            _repo = repo;
            _host = host;
        }
             
       [HttpGet]
       public async Task<IList<ProductDTO>> GetProductsAsync()
        {

            var products = (await _repo.GetAll())
                .Select(prod => new ProductDTO
                {
                    ProductId = prod.ProductId,
                    Description = prod.Description,
                    Name = prod.Name,
                    Price = prod.Price,
                    CategoryName=prod.Category.CategoryName,
                    CategoryId = prod.Category.CategoryId,
                    CreatedDate = prod.CreatedDate,
                    ImageUrl = String.Format("{0}/{1}", "https://localhost:44300/api/Products/image", prod.ProductId),
                    InProcess = prod.InProcess,
                    BidNr = _repo.GetBidNumber(prod.ProductId)
                }).ToList();
            return products;
        }
        [HttpPost]
        public async Task<ActionResult<CreateProductDTO>> CreateProductAsync([FromBody] CreateProductDTO product)
        {
            if(product is null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Product prod = new()
            {
                
                CreatedDate = product.CreatedDate,
                Description = product.Description,
                Price = product.Price,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                UserId = product.UserId,
                InProcess = product.InProcess

            };
            await _repo.AddProduct(prod);
            return CreatedAtAction(nameof(GetProductsAsync), new { id = prod.ProductId }, prod);
        }

        [HttpPost]
        [Route("image/{id}")]
        public async Task<IActionResult> AddImage([FromRoute] int id, [FromForm] IFormFile file)
        {
            var imagePath = Path.Combine(_host.ContentRootPath, "Images", file.FileName);
            using (var streamImg=new FileStream(imagePath, FileMode.Create))
            {
                file.CopyTo(streamImg);
            };
            var product = await _repo.getProductById(id);
            product.ImageUrl = file.FileName;
            await _repo.EditProduct(product);
            return Ok();
        }
        [HttpGet]
        [Route("image/{id}")]
        public async Task<ActionResult> GetImage([FromRoute]int id)
        {
            var product = await _repo.getProductById(id);
            var image = product.ImageUrl;
            var path = Path.Combine(_host.ContentRootPath, "images", image);
            var imageFile = System.IO.File.OpenRead(path);
            return File(imageFile, "image/jpeg");

        }
    }
}
