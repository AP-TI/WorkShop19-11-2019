using NHibernate;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data.NHibernate.Entities;

namespace WebShop.Data.NHibernate.Session
{
    public class NHibernateMapperSession : IMapperSession
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHibernateMapperSession(ISession session)
        {
            _session = session;
        }

        public IQueryable<Product> Products => _session.Query<Product>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task Save(Product entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(Product entity)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
