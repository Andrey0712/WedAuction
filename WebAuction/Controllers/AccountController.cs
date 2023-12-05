using AutoMapper;
using Data.Entities.Identity;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAuction.Abstract;
using System.Data;
using WebAuction.Models;

namespace WebAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppEFContext _context;
        private IHostEnvironment _host;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<AppUser> userManeger, SignInManager<AppUser> signInManager,
            AppEFContext context, IMapper mapper, IJwtTokenService jwtTokenService, IHostEnvironment host,
            ILogger<AccountController> logger, RoleManager<AppRole> roleManager)
        {
            _userManager = userManeger;
            _signInManager = signInManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _context = context;
            _host = host;
            _logger = logger;
            _roleManager = roleManager;
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

       /* [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            //string fileName = String.Empty;
            var rez = _roleManager.CreateAsync(new AppRole
            {
                Name = Roles.User
            }).Result;
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                result = _userManager.AddToRoleAsync(user, Roles.User).Result;
            }

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });


            return Ok(new { token = _jwtTokenService.CreateToken(user) });


        }*/


    }
}
