using CatalogoApi.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace CatalogoApi.Models;

[Table("Produtos")]//Não necessário pois já atribuimos o atributo de tabela no Context.
public class Produto
{
    [Key]//Não necessário pois já temos o sufixo Id no final do nome
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!" )]
    [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    [PrimeiraLetra]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres.")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public float? Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    [JsonIgnore]
    public Categoria? Categoria { get; set; }

}
