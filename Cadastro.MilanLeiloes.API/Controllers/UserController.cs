using AutoMapper;
using Cadastro.MilanLeiloes.API.Dtos;
using Cadastro.MilanLeiloes.Domain.Model;
using Cadastro.MilanLeiloes.Domain.Models;
using Cadastro.MilanLeiloes.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cadastro.MilanLeiloes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext context,
                              IConfiguration config,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult> GetUser()
        {
            return Ok(new UserDto());
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);

                var result = await _userManager.CreateAsync(user, userDto.Password);

                var userToReturn = _mapper.Map<UserDto>(user);

                if (result.Succeeded)
                {
                    return Created("GetUser", userToReturn);
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados Falhou{ex.Message}");
            }
        }


        //[HttpPost("Documentos")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Documentos(List<Documentos> documentos, string email)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(email);
        //        //documentos.DocumentoId = user.Id;
        //        _context.Add(documentos);

        //        var result = await _context.SaveChangesAsync();


        //        if (result != null)
        //        {
        //            return Created("GetUser", "Teste");
        //        }

        //        return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {

        //        return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados Falhou{ex.Message}");
        //    }
        //}

        [HttpPost("SocialLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> SocialLogin(UserSocialLoginDto userSocialLogin)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userSocialLogin.Email);

                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedEmail == userSocialLogin.Email.ToUpper());

                var userToReturn = _mapper.Map<UserSocialLoginDto>(appUser);

                return Ok(new
                {
                    token = GenarateJwToken(appUser).Result,
                    user = userToReturn

                });
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados Falhou{ex.Message}");
            }

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);

                var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                        .FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    return Ok(new
                    {
                        token = GenarateJwToken(appUser).Result,
                        user = userToReturn

                    });
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de dados Falhou{ex.Message}");
            }

        }

        private async Task<string> GenarateJwToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
