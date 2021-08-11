using AuctionWebApi.ModelsDTO.User;
using DataLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuctionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public UserController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest();
                }
                var newUser = new Users()
                {
                    Email = user.Email,
                    UserName = user.UserName
                };
                var register = await _userManager.CreateAsync(newUser, user.Password);
                if (register.Succeeded)
                {
                    return Ok(register);
                }
                else
                {
                    return BadRequest();
                }
          
            }
              return BadRequest();
        }
    }
}
