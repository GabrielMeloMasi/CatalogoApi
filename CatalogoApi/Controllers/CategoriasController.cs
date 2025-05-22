using CatalogoApi.Models;
using CatalogoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        //  private readonly IMeuServico _meuServico;

        //public CategoriasController(AppDbContext context, IMeuServico meuServico)

        public CategoriasController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            //_meuServico = meuServico;
        }

        [HttpGet("LerArquivoConfiguracao")]
        public string GetValores()
        {
            var valor1 = _configuration["chave1"];
            var valor2 = _configuration["chave2"];
            var secao1 = _configuration["secao1:chave1"];
            var secao2 = _configuration["secao1:chave2"];

            return $"Chave1 = {valor1} \n Chave2 = {valor2} \n Seção1 =>  Chave1 = {secao1} \n Seção1 => Chave2 = {secao2}";
        }


        //[HttpGet("UsandoFromServices/{nome}")]
        //public ActionResult<string> GetSaudacaoFromServices([FromServices] IMeuServico meuServico, string nome)
        //{
        //    return meuServico.Saudacao(nome);
        //}

        [HttpGet("SemFromServices/{nome}")]
        public ActionResult<string> GetSaudacaoSemFromServices(IMeuServico meuServico, string nome)
        {
            return meuServico.Saudacao(nome);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProduto()
        {
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> GetCategorias()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema para tratar sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetCategoria(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria is null)
                {
                    return NotFound("Categoria não encontrada!");
                }
                return categoria;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema para tratar sua solicitação");
            }
            
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
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if(categoria is null)
            {
                return NotFound("Categoria não encontrada!");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }

    }
}
