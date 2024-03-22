
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
            private readonly SignInManager<AppUser> _signInManager;
            
            private IHostEnvironment _host;
           
            private readonly ILogger<AccountController> _logger;

            public AccountController(UserManager<AppUser> userManeger, IMapper mapper, RoleManager<AppRole> roleManager,
                IJwtTokenService jwtTokenService,AppEFContext context,
                SignInManager<AppUser> signInManager,
                 IHostEnvironment host,
                ILogger<AccountController> logger
                )
        {
            _userManager = userManeger;
            _mapper = mapper;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _signInManager = signInManager;
                        
            
            _host = host;
            _logger = logger;
            
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            _logger.LogInformation("login user");
           /* try
            {*/
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
           /* }
            catch(Exception ex) 
            {
                _logger.LogError("Problem login user"+model.Email, ex.Message);
                return BadRequest(new { error = "Server exception" });
            }*/
            
        }

        /// <summary>
        ///Видаленя юзера
        /// </summary>
        /// <param name="model">id users</param>
        /// <returns>ok</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Delete user</response>
        /// <response code="400">Delete has missing/invalid values</response>
        /// <response code="500">Oops! Can't Delete your user right now</response>
        [HttpPost]
        [Authorize]
        [Microsoft.AspNetCore.Mvc.Route("delete")]
        public IActionResult Delete([FromBody] UserDelModel model)
        {
            var res=_context.Users.FirstOrDefault(x=>x.Id == model.Id);
            if(res==null)
            {
                return BadRequest(new { message = "Chek Id" });
            }
            _context.Remove(res);
            _context.SaveChanges();

            var oldImage = res.Avatar;


            //delete avatar
            string folder = "\\uploads\\";
            string contentRootPath = _host.ContentRootPath + folder + oldImage;

            if (System.IO.File.Exists(contentRootPath))
            {
                System.IO.File.Delete(contentRootPath);
            }
            return Ok(new {messege="User delete"});


        }

        /// <summary>
        /// Редагувння юзера
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає токен авторизації</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Update user</response>
        /// <response code="400">Update has missing/invalid values</response>
        /// <response code="500">Oops! Can't Update right now</response>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Microsoft.AspNetCore.Mvc.Route("edit")]

        public IActionResult EditUser([FromForm]  EditUserModel model)
        {
            var user=_context.Users.FirstOrDefault(x=>x.Id==model.Id);
            if(model==null) {
                return BadRequest(new { messege = "empty array" });
                    }
            if (model.Name != null)
            {
                user.Name= model.Name;
            }
            if(model.Email != null)
            {
                user.Email= model.Email;
            }

            string fileName = string.Empty;

            if (model.Avatar != null)
            {


                string img = Path.GetRandomFileName() +
                    Path.GetExtension(model.Avatar.FileName);

                string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                fileName = Path.Combine(dirPath, img);
                using (var file = System.IO.File.Create(fileName))
                {
                    model.Avatar.CopyTo(file);
                }
               


                var oldImage = user.Avatar;



                string folder = "\\uploads\\";
                string contentRootPath = _host.ContentRootPath + folder + oldImage;

                if (System.IO.File.Exists(contentRootPath))
                {
                    System.IO.File.Delete(contentRootPath);
                }
                user.Avatar = img;

            }

            /* string avatarNew = string.Empty;
             if(model.Avatar != null)
             {
                 string foto=Path.GetRandomFileName()+Path.ChangeExtension(model.Avatar.FileName);
                 string dirPath = Path.Combine(Dictionary.GetCurrentDirectory(), "uploads");
                 avatarNew=Path.Combine(dirPath, foto);
                 using(var file=System.IO.File.Create(avatarNew))
                 {
                     model.Avatar.CopyTo(file);
                 }

                 var oldAvatar=user.Avatar;
                 string contentRootPath = _host.ContentRootPath + "\\uploads\\" + oldAvatar;
                 if(System.IO.File.Exists(contentRootPath))
                 {
                     System.IO.File.Delete(contentRootPath);
                 }
                 user.Avatar = avatarNew;

             }*/

            _context.SaveChanges();
            return Ok(new { token = _jwtTokenService.CreateToken(user) });
            
        }

    }

}
