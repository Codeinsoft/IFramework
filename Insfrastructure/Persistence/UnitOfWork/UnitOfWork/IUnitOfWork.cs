namespace IFramework.Infrastructure.Persistence.UnitOfWork
{
    /// <summary>
    /// Transaction olan bir işlemi temsil eder.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Veri tabanı bağlantısını açar ve transaction'ı başlatır.
        /// </summary>
        bool BeginTransaction();

        /// <summary>
        /// Transaction'ı onaylar ve veri tabanı bağlantısını kapatır.
        /// </summary>
        bool Commit();

        /// <summary>
        /// Transaction'da yapılan işlemleri iptal eder ve veri tabanı bağlantısını iptal eder.
        /// </summary>
        bool Rollback();
    }
}
