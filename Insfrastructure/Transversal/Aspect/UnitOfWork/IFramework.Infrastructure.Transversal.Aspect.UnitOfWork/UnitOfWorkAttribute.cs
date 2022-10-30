using System;

namespace IFramework.Infrastructure.Transversal.Aspect.UnitOfWork
{
    /// <summary>
    /// Bu attribute ile method çalışmadan önce otomatik olarak bir transaction oluşturulur.
    /// Oluşturulan transaction method sonuna kadar eğer bir exception oluşmadı ise commit edilir ve exception oluştu ise transaction rollback yapılır.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {

    }
}
