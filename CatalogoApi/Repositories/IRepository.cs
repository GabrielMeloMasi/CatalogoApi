using System.Linq.Expressions;

namespace CatalogoApi.Repositories
{
    public interface IRepository<T>
    {
        //Cuidado para não violar o principio SOLID ISP (Os clientes(da interface) não devem ser forçados a depender de interfaces que não utilizem.

        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T,bool>> predicate);
        T Create (T entity);
        T Update (T entity);
        T Delete (T entity);
    }
}
