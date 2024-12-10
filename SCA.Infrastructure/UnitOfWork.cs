using SCA.Infrastructure;
using SCA.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SCAContext _context;
        private readonly Type _repositoryType;
        private bool disposedValue;

        public UnitOfWork(SCAContext context,Type repoType)
        {
            _context=context;
            _repositoryType=repoType;   
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            //return new GenericRepository<T>(_context);
            return (IGenericRepository<T>)
                Activator.
                CreateInstance(_repositoryType.MakeGenericType(typeof(T)), _context);
        }
        public int Save()
        {
           return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Dispose(disposing: false);
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
         ~UnitOfWork()
         {
             // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
             Dispose(disposing: false);
         }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
