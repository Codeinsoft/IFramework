using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infrastructure.Constants.Exception
{
    public static class IoCContainerExceptionConstants
    {
        public const string VALIDATOR_NOT_IMPLEMENTED = "Methoda parametre olarak geçilen objenin validasyondan geçmesi için sınıfa ait validator geliştirilmeli ve IoC'da tanımlanmalıdır.";
    }
}
