using AutoMapper;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAuction.Models;

namespace WebAuction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;

        public CategoryController(IMapper mapper, AppEFContext context)
        {
            mapper = _mapper;
            context = _context;
        }

        /// <summary>
        /// Список категорій
        /// </summary>
        /// <returns>Повертає лист</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">List categorys</response>
        /// <response code="400">List categorys has missing/invalid values</response>
        /// <response code="500">Oops! Can't get  categorys list right now</response>

        [HttpGet("list")]
        public async Task<IActionResult> GetCategory()
        {

            var list = await _context.Categories.Select(x => _mapper.Map<ItemCategoryViewModel>(x))
                .AsQueryable().ToListAsync();

            return Ok(list);
        }


        /// <summary>
        /// Видалення категорії
        /// </summary>
        /// <param name="model">Понель із даними</param>
        /// <returns>Повертає messege</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Delete category</response>
        /// <response code="400">Delete category has missing/invalid values</response>
        /// <response code="500">Oops! Can't delete category now</response>
        /// 





    }
}
