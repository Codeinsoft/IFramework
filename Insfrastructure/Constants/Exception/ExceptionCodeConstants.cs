using System;

namespace IFramework.Infrastructure.Constants.Exception
{
    public class ExceptionCodeConstants
    {
        private const string ExceptionCodeTitle = "Err-";
        public const string EmailNotRegisterCode = ExceptionCodeTitle + "00001";
        public const string EmailRegisteredAlreadyCode = ExceptionCodeTitle + "00002";
        public const string EmailApprovedCodeExceptionCode = ExceptionCodeTitle + "00003";
        public const string UserNotFoundCode = ExceptionCodeTitle + "00004";
        public const string NotFoundCode = ExceptionCodeTitle + "00005";
        public const string AlreadyDeletedExceptionCode = ExceptionCodeTitle + "00006";
        public const string FailedLoginCode = ExceptionCodeTitle + "00007";
        public const string NullExceptionCode = ExceptionCodeTitle + "00008";
        public const string EmptyParameterExceptionCode = ExceptionCodeTitle + "00009";
        public const string ParameterNotApproval = ExceptionCodeTitle + "00009";
    }
}
