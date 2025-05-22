using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
       private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        //[HttpGet("{valor:alpha:length(5)}")] restrições em roteamento!
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Produto>>> GetProdutos()
        { 
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync(); //AsNoTracking é usado para otimizar Get que não tem a necessidade de realizar o rastreamento e acompanhar seus estados.
     
            return produtos;
        }

        [HttpGet("{id=int:min(1)}", Name = "ObterProduto")]
        public async Task <ActionResult<Produto>> Get([FromQuery]int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Produto inválido!");
            }   

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
               new { id = produto.ProdutoId }, produto);
                
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id) 
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
           // var produto = _context.Produtos.Find(id); 

            if(produto is null)
            {
                return NotFound("Não encontrado!");
            }
            
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }


    }

}
