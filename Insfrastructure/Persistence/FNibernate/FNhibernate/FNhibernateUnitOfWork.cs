using IFramework.Infrastructure.Persistence.UnitOfWork;
using NHibernate;
using System;

namespace IFramework.Infrastructure.Persistence.FNhibernate
{
    /// <summary>
    /// Nhibernate için unit of work katmanı .
    /// </summary>
    public class FNhibernateUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// FNhibernateUnitOfWork nesnesinin instance'ini alır. 
        /// Gelen işlemdeki geçerli nesneyi kullanır.
        /// </summary>
        public static FNhibernateUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }

        [ThreadStatic]
        private static FNhibernateUnitOfWork _current;

        /// <summary>
        /// Nhibernate session nesnesini verir.
        /// </summary>
        public ISession Session { get; private set; }

        /// <summary>
        /// Session factory'yi referans eder.
        /// </summary>
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Aktif olarak verilen işlemler için çalışan transaction nesnesini verir.
        /// </summary>
        private ITransaction _transaction;

        /// <summary>
        /// FNhibernateUnitOfWork class'ından yeni bir instance alır.
        /// </summary>
        /// <param name="sessionFactory">Kullanılması istenilen SessionFactory nesnesi verilir.</param>
        public FNhibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Veri tabanı bağlantısını açar ve açılan bağlantıda transaction işlemi başlatır.
        /// </summary>
        public bool BeginTransaction()
        {
            try
            {
                Session = _sessionFactory.OpenSession();
                _transaction = Session.BeginTransaction();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Geçerli transaction'daki işlemleri commit(onaylar) eder ve veri tabanı bağlantısını kapatır.
        /// </summary>
        public bool Commit()
        {
            bool result = false;
            try
            {
                _transaction.Commit();
                result = true;
            }
            finally
            {
                Session.Close();
            }
            return result;
        }

        /// <summary>
        /// Geçerli transaction'daki işlemleri geri alır ve veri tabanı bağlantısını kapatır.
        /// </summary>
        public bool Rollback()
        {
            bool result = false;
            try
            {
                _transaction.Rollback();
                result = true;
            }
            finally
            {
                Session.Close();
            }
            return result;
        }

    }
}
