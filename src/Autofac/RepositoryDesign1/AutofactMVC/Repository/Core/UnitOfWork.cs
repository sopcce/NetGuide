using System.Data.Entity;
using Autofac;

namespace AutofactMVC.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IComponentContext _componentContext;
        protected readonly DbContext Context;

        public UnitOfWork(DbContext context, IComponentContext componentContext)
        {
            Context = context;
            _componentContext = componentContext;
        }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            return _componentContext.Resolve<TRepository>();
        }

        public void ExecuteProcedure(string procedureCommand, params object[] sqlParams)
        {
            Context.Database.ExecuteSqlCommand(procedureCommand, sqlParams);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}
