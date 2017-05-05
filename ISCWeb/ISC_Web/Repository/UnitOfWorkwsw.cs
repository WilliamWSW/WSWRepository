using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ISC_Web.DAL;
using System.Data.Entity;

namespace ISC_Web.Repository
{
    public class UnitOfWorkwsw:IUnitOfWork
    {
        private readonly DbContext _context;
        public UnitOfWorkwsw(DbContext context)
        {
            _context = context;
        }
        public DbContext Context
        {
            get { return _context; }
        }

        public event EventHandler Disposed;

        public bool IsDisposed { get; private set; }
        public void Dispose()
        {
            Dispose(true);
        }
        public virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                if (disposing && !IsDisposed)
                {
                    _context.Dispose();
                    var evt = Disposed;
                    if (evt != null) evt(this, EventArgs.Empty);
                    Disposed = null;
                    IsDisposed = true;
                    GC.SuppressFinalize(this);
                }
            }
        }

        

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        ~UnitOfWorkwsw()
        {
            Dispose(false);
        }
    }
}