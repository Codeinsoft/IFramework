using System;
using System.Reflection;

namespace IFramework.Infrastructure.Persistence.UnitOfWork
{
    public static class UnitOfWorkHelper
    {
        public static bool IsRepositoryMethod(MethodInfo methodInfo)
        {
            return IsRepositoryClass(methodInfo.DeclaringType);
        }

        public static bool IsRepositoryClass(Type type)
        {
            return type.Name.Contains("Repository");
            //return typeof(IRepository<,>).IsAssignableFrom(type);
        }

        //public static bool HasUnitOfWorkAttribute(MethodInfo methodInfo)
        //{
        //    return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        //}
    }
}
