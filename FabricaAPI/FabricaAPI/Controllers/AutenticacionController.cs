using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FabricaAPI.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FabricaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly FabricaDBContext _context;

        private readonly string secretKey;
     
        public AutenticacionController(IConfiguration config, FabricaDBContext context)
        {
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _context = context;
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Usuario request)
        {

            var dbEntry = _context.Usuarios.FirstOrDefault(acc => acc.UsuNombre == request.UsuNombre);
            if(dbEntry == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
            else if (request.UsuNombre== dbEntry.UsuNombre && request.UsuPass == dbEntry.UsuPass)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.UsuNombre));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new { token =tokencreado});

            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = ""});
            }
        }
    }
}
