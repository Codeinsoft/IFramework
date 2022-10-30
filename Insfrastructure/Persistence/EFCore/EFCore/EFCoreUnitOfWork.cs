using IFramework.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace IFramework.Infrastructure.Persistence.EFCore
{
    public class EFCoreUnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// EFCoreUnitOfWork nesnesinin instance'ini alır. 
        /// Gelen işlemdeki geçerli nesneyi kullanır.
        /// </summary>
        public static EFCoreUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }

        [ThreadStatic]
        private static EFCoreUnitOfWork _current;

        /// <summary>
        /// Nhibernate session nesnesini verir.
        /// </summary>
        public EFDbContext Context { get; private set; }

        ///// <summary>
        ///// Session factory'yi referans eder.
        ///// </summary>
        //private readonly DBContext _sessionFactory;

        /// <summary>
        /// Aktif olarak verilen işlemler için çalışan transaction nesnesini verir.
        /// </summary>
        private IDbContextTransaction _transaction;

        /// <summary>
        /// FNhibernateUnitOfWork class'ından yeni bir instance alır.
        /// </summary>
        /// <param name="sessionFactory">Kullanılması istenilen SessionFactory nesnesi verilir.</param>
        public EFCoreUnitOfWork(EFDbContext dbContext)
        {
            //Current = new EFCoreUnitOfWork(dbContext);
            Context = dbContext;
        }

        /// <summary>
        /// Veri tabanı bağlantısını açar ve açılan bağlantıda transaction işlemi başlatır.
        /// </summary>
        public bool BeginTransaction()
        {
            try
            {
                _transaction = Context.Database.BeginTransaction();
                return true;
            }
#pragma warning disable CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            catch (Exception ex)
#pragma warning restore CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            {
                return false;
            }
        }

        /// <summary>
        /// Geçerli transaction'daki işlemleri commit(onaylar) eder ve veri tabanı bağlantısını kapatır.
        /// </summary>
        public bool Commit()
        {
            try
            {
                int result = Context.SaveChanges();
                //_transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Geçerli transaction'daki işlemleri geri alır ve veri tabanı bağlantısını kapatır.
        /// </summary>
        public bool Rollback()
        {
            try
            {
                _transaction.Rollback();
                _transaction = null;
                return true;
            }
#pragma warning disable CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            catch (Exception ex)
#pragma warning restore CS0168 // Değişken bildirildi ancak hiç kullanılmadı
            {
                return false;
            }
        }


        #region DisposingSection
        /// <summary>
        /// Context ile işimiz bittiğinde dispose edilmesini sağlıyoruz
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
