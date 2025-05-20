using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = _context.Categorias.ToList();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas...");
            }
            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }
            return categoria;
        }

        [HttpPost]
        public ActionResult PostCategoria(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Categoria inválida!");
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria",
              new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id=int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id=int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if(categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }
            _context.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

    }
}
