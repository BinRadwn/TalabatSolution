using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Services;

namespace Talabat.Service
{
    public class TokenServices : ITokenServices
    {
        //Prorerty
        public IConfiguration Configuration { get; }


        //Constructor
        public TokenServices(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {

            //Private Claims (User-Defined)
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.GivenName ,user.DisplayName)
            };
            var Rolles = await userManager.GetRolesAsync(user);
            foreach (var role in Rolles)
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));

            //Key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));

            //
            var Token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(Configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token); 
        }
    }
}
