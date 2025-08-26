using System.Linq.Expressions;
using BlogX.Data;
using BlogX.Extensions;
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
                return StatusCode(500, new ResultViewModel<List<Category>>("05x04 - Falha interna no servidor"));
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
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado."));
                }

                return Ok(new ResultViewModel<Category>(category));
            }
            catch
            {
                return StatusCode(500, "05X05 - Falha interna no servidor");
            }

        }

        [HttpPost("v1/categories")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model, [FromServices] BlogXDataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            }

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
                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            }

            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X09 - Não foi possível incluir a categoria."));
            }

            catch 
            {
                return StatusCode(500, new ResultViewModel<Category>("05X09 - Falha interna no servidor."));
            }



        }

        [HttpPut("v1/categories/{id:int}")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorCategoryViewModel model, [FromServices] BlogXDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                {
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado."));
                }

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch(DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X08 - Não foi possível"));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X11 - Falha interna do Servidor."));
            }
            
        }


        [HttpDelete("v1/categories/{id:int}")]  //localhost:PORTA/v1/categories
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BlogXDataContext context)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                {
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado."));
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X07 - Não foi póssível excluir a categoria."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05X12 - Falha interna no servidor."));
            }
            

        }
        
    }
}