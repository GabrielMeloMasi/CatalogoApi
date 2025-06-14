﻿using System.ComponentModel.DataAnnotations;

namespace CatalogoApi.DTOs
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string Descricao { get; set; }
        public decimal Preco {  get; set; }
        [Required]
        [StringLength(300)]
        public string ImagemUrl { get; set; }
        public int CategoriaId { get; set; }
    }
}
