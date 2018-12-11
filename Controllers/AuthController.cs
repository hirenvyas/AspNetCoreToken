using CityInfo.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //Generate Token based on Application user instance (Username,password)
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if(user!=null && await _userManager.CheckPasswordAsync(user,login.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                   new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecurekey"));

                //Set Token generation peramaters
                var token = new JwtSecurityToken(
                    issuer: "https://cityinfo.com",
                    audience: "https://cityinfo.com",
                    expires: DateTime.UtcNow.AddHours(1),
                    claims:claims,
                    signingCredentials:new Microsoft.IdentityModel.Tokens.SigningCredentials(signingkey,SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new { token=new JwtSecurityTokenHandler().WriteToken(token),
                    expiration=token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}