using System;
using IFramework.Infrastructure.Utility.CustomExceptions;
using IFramework.Infrastructure.Transversal.Resources.Languages;
using IFramework.Infrastructure.Constants.Exception;

namespace IFramework.Infrastructure.Utility.Check
{
    public static class ParameterCheck
    {

        public static void ThrowExceptionIsNullOrEmpty<T>(T obje, string parameterName)
        {
            ThrowExceptionIsNull(obje, parameterName);
            if (object.Equals(obje, default(T)) || (obje is string && string.IsNullOrEmpty(obje.ToString())) || (obje is Guid && new Guid(obje.ToString()) == Guid.Empty))
                throw new ValidationException(string.Format(ErrorMessage.EmptyParameterException, parameterName), ExceptionCodeConstants.EmptyParameterExceptionCode);
        }
        public static void ThrowExceptionIsNull<T>(T obje, string parameterName)
        {
            if (obje == null)
                throw new ValidationException(string.Format(ErrorMessage.NullParameterException, parameterName), ExceptionCodeConstants.NullExceptionCode);
        }

        public static bool CheckIsNullOrEmpty<T>(T obje)
        {
            CheckIsNull(obje);

            if (object.Equals(obje, default(T)) || (obje is string && string.IsNullOrEmpty(obje.ToString())) ||
                (obje is Guid && new Guid(obje.ToString()) == Guid.Empty)) return false;
            return true;
        }

        public static bool CheckIsNull<T>(T obje)
        {
            if (obje == null) return false;
            return true;
        }

        public static void ThrowExceptionCompare(object obje1, object obje2,string obje1Name,string obje2Name)
        {
            if(obje1!=obje2) 
                throw new ValidationException(string.Format(ErrorMessage.ParameterNotApproval, obje1Name,obje2Name),ExceptionCodeConstants.ParameterNotApproval);
        }

    }
}
