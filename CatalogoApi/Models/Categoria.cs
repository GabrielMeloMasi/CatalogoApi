using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoApi.Models;

[Table("Categorias")]//Não necessário pois já atribuimos o atributo de tabela no Context.
public class Categoria
{
    public Categoria() { 
       Produtos = new Collection<Produto>();
    }
    [Key]//Não necessário pois já temos o sufixo Id no final do nome
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}
