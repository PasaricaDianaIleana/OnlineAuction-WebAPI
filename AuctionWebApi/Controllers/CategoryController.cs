using AuctionWebApi.ModelsDTO;
using DataLibrary.Models;
using DataLibrary.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _repo;
        public CategoryController(ICategory repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IList<CategoryDTO>> GetCategoriesAsync()
        {
            var data =(await _repo.GetAllAsync())
                .Select(category => new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name=category.CategoryName
            }).ToList();
            return data;
        }
        [HttpPost]
        public async Task<ActionResult<CreateCategoryDTO>> AddCategoryAsync([FromBody] CreateCategoryDTO category)
        {
            if(category is null || !ModelState.IsValid)
            {
                return BadRequest();
            }
           
            Category data= new()
            {
               
                CategoryName=category.Name

            };
         await _repo.AddAsync(data);
            return CreatedAtAction(nameof(GetCategoryAsync), new { id = data.CategoryId }, data);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryAsync(int id)
        {
            var data =await  _repo.GetByIdAsync(id);

            if(data!=null)
            {
                var dataDTO = new CategoryDTO
                {
                    CategoryId = data.CategoryId,
                    Name = data.CategoryName
                };
                return Ok(dataDTO);
            }
            return NotFound();
            
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if(category is null)
            {
                return NotFound();
            }
           await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
