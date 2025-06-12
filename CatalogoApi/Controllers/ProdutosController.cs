using AutoMapper;
using CatalogoApi.DTOs;
using CatalogoApi.Models;
using CatalogoApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_produtoRepository = produtoRepository;
            //_repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos == null)
            {
                return NotFound();
            }
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }
        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
        {
           var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(id);
            if (produtos == null)
            {
                return NotFound();
            }
            // var destino = _mapper.Map<Destino>(origem);
            var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDto);
        }

        [HttpGet("{id=int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get([FromQuery] int id)
        {
            var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound($"Produto com id= {id} não encontrado...");
            }
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDto);
        }

        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
            {
                return BadRequest("Dados inválidos");
            }
            var produto = _mapper.Map<Produto>(produtoDto);

            var newProduto = _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();

            var newProdutoDto = _mapper.Map<ProdutoDTO>(newProduto); 
           
            return new CreatedAtRouteResult("ObterProduto", new { id = newProdutoDto.ProdutoId }, newProdutoDto);

        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
        {   
                if (id != produtoDto.CategoriaId)
                    return BadRequest("Dados inválidos");

                var produto = _mapper.Map<Produto>(produtoDto);
                var produtoAtualizado = _unitOfWork.ProdutoRepository.Update(produto);
                _unitOfWork.Commit();
                var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);
                return Ok(produtoAtualizadoDto);

        }
            [HttpDelete("{id:int}")]
            public ActionResult<ProdutoDTO> Delete(int id)
            {
                var produto = _unitOfWork.ProdutoRepository.Get(p => p.ProdutoId == id);

                if (produto == null)
                {
                    return NotFound($"Produto com id={id} não encontrado...");
                }

                var produtoExcuido = _unitOfWork.ProdutoRepository.Delete(produto);
                _unitOfWork.Commit();
               var produtoExcuidoDto = _mapper.Map<ProdutoDTO>(produtoExcuido);
                return Ok(produtoExcuidoDto);
            }

        }
}
