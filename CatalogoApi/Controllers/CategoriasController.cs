using CatalogoApi.DTOs;
using CatalogoApi.DTOs.Mappings;
using CatalogoApi.Filters;
using CatalogoApi.Models;
using CatalogoApi.Repositories;
using CatalogoApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;


namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetAll();
            var categoriasDTO = categorias.ToCategoriaDTOList();
            return Ok(categoriasDTO);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com id= {id} não encontrada...");
                return NotFound($"Categoria com id= {id} não encontrada...");
            }
            var categoriaDto = categoria.ToCategoriaDTO();
            return Ok(categoriaDto);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados inválidos");
            }
            var categoria = categoriaDto.ToCategoria(); 

            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();

            var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();
            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados inválidos");
            }
            var categoria = categoriaDTO.ToCategoria();

             var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();

            var categoriaAtualizadaDTO = categoriaAtualizada.ToCategoriaDTO();

            return Ok(categoriaAtualizadaDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound($"Categoria com id={id} não encontrada...");
            }

           var categoriaExcluida = _unitOfWork.CategoriaRepository.Delete(categoria);
            var categoriaExcluidaDTO = categoriaExcluida.ToCategoriaDTO();
            _unitOfWork.Commit();
            return Ok(categoriaExcluidaDTO);
        }
    }
}
