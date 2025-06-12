using CatalogoApi.Models;

namespace CatalogoApi.Repositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        //IRepository<Produto> ProdutoRepository { get; }
        //IRepository<Categoria> CategoriasRepository { get; }
        void Commit();
    }
}
