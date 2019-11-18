using System.Linq;
using System.Threading.Tasks;
using WebShop.Data.NHibernate.Entities;

namespace WebShop.Data.NHibernate.Session
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(Product entity);
        Task Delete(Product entity);

        IQueryable<Product> Products { get; }
    }
}
