using CatalogoApi.Models;
using CatalogoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        //private readonly IRepository<Produto> _repository;
        private readonly IProdutoRepository _produtoRepository;


        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
            //_repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = _produtoRepository.GetAll();
            return Ok(produtos);
        }
        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
           var produtos = _produtoRepository.GetProdutosPorCategoria(id);
            if (produtos == null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }

        [HttpGet("{id=int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get([FromQuery] int id)
        {
            var produto = _produtoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound($"Produto com id= {id} não encontrado...");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest("Dados inválidos");
            }

            var newProduto = _produtoRepository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = newProduto.ProdutoId }, newProduto);

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            {
                if (id != produto.CategoriaId)
                {
                    return BadRequest("Dados inválidos");
                }

                _produtoRepository.Update(produto);
                return Ok(produto);
            }

        }
            [HttpDelete("{id:int}")]
            public ActionResult Delete(int id)
            {
                var produto = _produtoRepository.Get(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound($"Produto com id={id} não encontrado...");
                }

                var produtoExcuido = _produtoRepository.Delete(produto);
                return Ok(produtoExcuido);
            }

        }
}
