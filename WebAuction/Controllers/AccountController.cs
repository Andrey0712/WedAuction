
using Data.Entities.Identity;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAuction.Abstract;
using System.Data;
using WebAuction.Models;
using WebAuction.Constants;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace WebAuction.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    
    [ApiController]
    public class AccountController : ControllerBase
    {

       
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager; 
        private readonly RoleManager<AppRole> _roleManager;
            private readonly IJwtTokenService _jwtTokenService;
        private readonly AppEFContext _context;
            /*private readonly SignInManager<AppUser> _signInManager;
            
            private IHostEnvironment _host;
           
            private readonly ILogger<AccountController> _logger;*/

            public AccountController(UserManager<AppUser> userManeger, IMapper mapper, RoleManager<AppRole> roleManager,
                IJwtTokenService jwtTokenService,AppEFContext context
                /*SignInManager<AppUser> signInManager,
                 IHostEnvironment host,
                ILogger<AccountController> logger,*/
                )
        {
            _userManager = userManeger;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
           /* _signInManager = signInManager;
                        
            
            _host = host;
            _logger = logger;
            */
        }

            /// <summary>
            /// Реєстрація юзера
            /// </summary>
            /// <param name="model">Понель із даними</param>
            /// <returns>Повертає токен авторизації</returns>
            /// <remarks>Awesomeness!</remarks>
            /// <response code="200">Register user</response>
            /// <response code="400">Register has missing/invalid values</response>
            /// <response code="500">Oops! Can't Register right now</response>

            
                       /* [HttpPost("register")]
                        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
                        {

            String imageName = string.Empty;
            if (model.Avatar != null)
            {
                var fileExp = Path.GetExtension(model.Avatar.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
                imageName = Path.GetRandomFileName() + fileExp;
                using (var steam = System.IO.File.Create(Path.Combine(dirSave, imageName)))
                {
                    await model.Avatar.CopyToAsync(steam);
                }
            }
            var user = new AppUser()
            {
                Name = model.Name,
                Email = model.Email,
                Avatar = imageName,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, Roles.User);
                return Ok();
            }
            return BadRequest();
        }*/

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            

            string fileName = String.Empty;

            var rez = _roleManager.CreateAsync(new AppRole
            {
                Name = Roles.User
            }).Result;
            var user = _mapper.Map<AppUser>(model);

           
            if (model.Avatar != null)
            {
                var fileExp = Path.GetExtension(model.Avatar.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                fileName = Path.GetRandomFileName() + fileExp;
                using (var file = System.IO.File.Create(Path.Combine(dirSave, fileName)))
                {
                    await model.Avatar.CopyToAsync(file);
                }
                user.Avatar = fileName;
            }


            /* if(model.Avatar != null)
             {
                 string randomFileName = Path.GetRandomFileName()
                     + Path.GetExtension(model.Avatar.FileName);
                 string dirPath=Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                 fileName=Path.Combine(dirPath, randomFileName);

                 using(var file=System.IO.File.Create(fileName))
                 {
                     model.Avatar.CopyTo(file);
                 }
                 user.Avatar = randomFileName;

             }*/

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                result = _userManager.AddToRoleAsync(user, Roles.User).Result;
            }

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });


            return Ok(new { token = _jwtTokenService.CreateToken(user) });


        }

        /// <summary>
        /// Відображення юзерів
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає список юзерів</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Users</response>
        /// <response code="400">Users has missing/invalid values</response>
        /// <response code="500">Oops! Can't Users show now</response>
        [HttpGet]
        [Authorize]
        [Microsoft.AspNetCore.Mvc.Route("users")]
        public async Task<IActionResult> Users()
        {
            var list = await  _context.Users.Select(x => _mapper.Map<UserViewModel>(x))
                .AsQueryable().ToArrayAsync();
            return Ok(list);

        }

        /// <summary>
        /// Авторизація юзера
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає токен авторизації</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Avtorize user</response>
        /// <response code="400">Avtorize has missing/invalid values</response>
        /// <response code="500">Oops! Can't Avtorize right now</response>
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            try
            {
var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    return Ok(
                        new  { token = _jwtTokenService.CreateToken(user) }
                        //new TokenViewModel { token = _jwtTokenService.CreateToken(user) }
                        );
                }
            }
            return BadRequest(new { error = "User not found" });
            }
            catch(Exception ex) 
            {
                return BadRequest(new { error = "Server exception" });
            }
            
        }


    }
    
}
