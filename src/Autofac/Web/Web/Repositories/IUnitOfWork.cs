using System;

namespace Sop.Framework.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository>() where TRepository : class;
        void ExecuteProcedure(string procedureCommand, params object[] sqlParams);
        void SaveChanges();
    }
}
