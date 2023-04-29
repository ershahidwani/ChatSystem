using dot_Net_web_api.Data;
using dot_Net_web_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dot_Net_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly UserManager<Registermodel> _userManager;
        private readonly SignInManager<Registermodel> _signInManager;
        public ChatController(ApplicationDBContext applicationDBContext, UserManager<Registermodel> userManager, SignInManager<Registermodel> signInManager)
        {
            _applicationDBContext = applicationDBContext;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> signup(Registermodel registerModel)
        {
            if (_applicationDBContext != null)
            {
                _applicationDBContext.Registrations.Add(registerModel);
                await _applicationDBContext.SaveChangesAsync();
                return Ok("Employee Added Successfully!");
            }

            return BadRequest();
        }

        [HttpGet("login")]
        public async Task<IActionResult> login(Registermodel registerModel)
        {
            // Authenticate the user
            var user = await _userManager.FindByEmailAsync(registerModel.Email);
            if (user == null)
            {
                return BadRequest("Invalid login attempt.");
            }
            // more about jwt auth
            var result = await _signInManager.CheckPasswordSignInAsync(user, registerModel.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid login attempt.");
            }

            // Generate the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return Ok(new { Token = tokenString });
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> profile(Registermodel registerModel)
        {
            var user = await _userManager.FindByEmailAsync(registerModel.Email);
            if (user == null)
            {
                return BadRequest("Invalid login attempt.");
            }
            // more about jwt auth
            var result = await _signInManager.CheckPasswordSignInAsync(user, registerModel.Password, false);
            if (result.Succeeded)
            {
                Ok(new { Success = result });
            }
            return BadRequest();
        }

        [HttpPut("update email")]
        public async Task<IdentityResult> profile(string email)
        {
            Registermodel model = new Registermodel();
            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userEmail == null)
            {
                await _userManager.ChangeEmailAsync(userEmail, email);
            }

    }
    }
