using AutoMapper;
using BCP.ExchangeRate.BusinessLogic.Interfaces;
using BCP.ExchangeRate.Common;
using BCP.ExchangeRate.Domain;
using BCP.ExchangeRate.WebAPI.Models.Request;
using BCP.ExchangeRate.WebAPI.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.WebAPI.Controllers
{
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserBL userBL, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userBL = userBL;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        // POST api/<UsersController>
        [HttpPost("Auth")]
        [SwaggerOperation(Summary = "Servicio de autenticacion.", Description = "Servicio de tipo POST de autenticacion de credenciales. En caso las credenciales enviadas sean las correctas, se generara" +
            "un token de seguridad para el consumo de los demas servicios.")]
        [SwaggerResponse(200, "La solicitud se ha realizado correctamente.")]
        public async Task<AuthResponse> Autenticar([FromBody] AuthRequest model)
        {
            User auth = await _userBL.Authenticate(model.Username, model.Password);

            User user = _mapper.Map<User>(model);

            AuthResponse response = new AuthResponse();

            if (auth == null)
            {
                response.Message = "Usuario y/o contraseña incorrectos.";
                response.Fullname = string.Empty;
                response.Token = string.Empty;
            }
            else {
                response.Message = "Usuario y contraseña correctos.";
                response.Fullname = $"{auth.FirstName} {auth.LastName}";
                response.Token = GetToken(auth);
            }

            return response;
        }

        private string GetToken(User entity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, entity.Username),
                        new Claim(ClaimTypes.Name, $"{entity.FirstName} {entity.LastName}")
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
