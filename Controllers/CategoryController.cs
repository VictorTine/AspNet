using BlogX.Data;
using BlogX.Models;
using BlogX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogX.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> GetAsync([FromServices] BlogXDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Category>> ("05x04 - Falha interna no servidor"));
            }
            
        }


        [HttpGet("v1/categories/{id:int}")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] BlogXDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch
            {
                return StatusCode(500, "05X05 - Falha interna no servidor");
            }

        }

        [HttpPost("v1/categories")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogXDataContext context)
        {
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Posts = [],
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return Created($"v1/categories/{category.Id}", category);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



        }

        [HttpPut("v1/categories/{id:int}")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogXDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;
            category.Slug = model.Slug;

            context.Categories.Update(category);
            await context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("v1/categories/{id:int}")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogXDataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }
        
    }
}