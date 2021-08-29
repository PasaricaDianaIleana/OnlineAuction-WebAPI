using AuctionWebApi.ModelsDTO.User;
using DataLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            var password = await _userManager.CheckPasswordAsync(user, userLogin.Password);
              if(user!=null && password)
            {
                
                var token = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim("UserId", user.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials=new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(token);
                var writeToken = tokenHandler.WriteToken(securityToken);
                return Ok(new { writeToken, user.Id });
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }
        [HttpGet]
        [Route("Profile")]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(id => id.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new UserProfileDTO
            {
                UserId = user.Id,
                UserName=user.UserName,
                Email=user.Email,
                Phone=user.PhoneNumber,
                Image=user.ImageUrl

            };
        }
    }
}
