using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using IFramework.Application.Contract.Core.Response;
using IFramework.Domain.Core.Entities;
using IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver;
using IFramework.Infrastructure.Transversal.Resources.Languages;
using IFramework.Infrastructure.Utility.Check;
using IFramework.Infrastructure.Utility.Cryptography;

namespace IFramework.Domain.User
{
    /// <summary>
    /// Sistemde kullanıcı, yönetici ve admin nesnelerinin oluşturulduğu sınıf
    /// </summary>
    public class User : AuditableAndActivableEntity<Guid>, IAggregateRoot
    {
        public virtual string Email { get; protected set; }
        public virtual string PasswordHash { get; protected set; }
        public virtual string SecurityStamp { get; protected set; }
        public virtual int UserInfoId { get; protected set; }
        public virtual bool EmailIsApproved { get; protected set; }
        public virtual Guid EmailApprovedCode { get; protected set; }
        public virtual DateTime EmailApprovedDatetime { get; protected set; }
        public virtual string EmailApprovedIpAddress { get; protected set; }
        public virtual Guid? RoleId { get; protected set; }

        public virtual UserInfo UserInfo { get; protected set; }
        public virtual Role Role { get; protected set; }

#pragma warning disable CS0649 // Field 'User._histories' is never assigned to, and will always have its default value null
        private IList<History.History> _histories;
#pragma warning restore CS0649 // Field 'User._histories' is never assigned to, and will always have its default value null
        public virtual IList<History.History> Histories
        {
            get { return _histories; }
        }

        /// <summary>
        /// ORM sistemlerinin gereksinim duymasından dolayı protected olarak default constructor tanımlandı.
        /// </summary>
        protected User()
        {

        }

        /// <summary>
        /// User sınıfından nesne oluşturulmak istenildiğin de email ve password parametreleri verilmelidir.
        /// </summary>
        /// <param name="email">Boş ve null olarak verilemez. Kullanıcının email adresi verilmelidir.</param>
        /// <param name="password">Boş ve null olarak verilemez. Kullanıcının şifresi verilmelidir. Complex olarak en az 8 karakter, en az bir sayı, harf ve özel karakter barındırmalıdır.</param>
        public User(string email, string password) : this()
        {
            Email = email;
            EmailApprovedCode = Guid.NewGuid();
            EmailApprovedDatetime = DateTime.MinValue;
            EmailApprovedIpAddress = string.Empty;
            EmailIsApproved = false;
            SecurityStamp = DateTime.Now.ToLongTimeString();
            PasswordHash = IoCResolver.Instance.ReleaseInstance<ICryptography>().Sha256(password, SecurityStamp);
        }


        #region User methods

        /// <summary>
        /// Kullanıcının şifresinin kontrol edilmesi işlemini yapmaktadır.
        /// </summary>
        /// <param name="password">Kontrol edilmesi istenilen şifre verilir.</param>
        /// <returns>Kullanıcının şifresi ile kontrol edilmesi için verilen şifre eşleşir ise true eşleşmez ise false değeri geri dönecektir.</returns>
        public virtual bool PasswordControl(string password)
        {
            string passwordHash = IoCResolver.Instance.ReleaseInstance<ICryptography>().Sha256(password, SecurityStamp);
            if (PasswordHash != passwordHash)
                throw new Exception("FailedLogin");
            return true;
        }

        /// <summary>
        /// Kullanıcının bilgilerinin girilmesi ve değiştirilmesi işlemini yapmaktadır. Geri dönüş değeri bulunmamaktadır.
        /// </summary>
        /// <param name="name">Kullanıcının adı verilmelidir.</param>
        /// <param name="lastName">Kullanıcının soyadı verilmelidir.</param>
        /// <param name="profileImage">Kullanıcının profil resminin path'i verilmelidir.</param>
        public virtual void ChangeUserInfo(string name, string lastName, string profileImage)
        {
            if (UserInfo == null)
                UserInfo = new UserInfo(name, lastName, profileImage);
            else
                UserInfo.Change(name, lastName, profileImage);
        }

        /// <summary>
        /// Kullanıcının email adresinin değiştirilmesi işlemini yapmaktadır.
        /// </summary>
        /// <param name="email">Yeni email adresi verilmelidir.</param>
        public virtual void ChangeEmail(string email)
        {
            Email = email;
            EmailUnApproved();
        }

        /// <summary>
        /// Kullanıcıya rol ekleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="role">Eklenmesi istenilen rol verilmelidir.</param>
        public virtual void ChangeRole(Role role)
        {
            Role = role;
        }

        /// <summary>
        /// Yapılan işlemi kullanıcının geçmiş işlemler bilgisine kayıt etme işlemini yapmaktadır.
        /// </summary>
        /// <param name="history">Yapılan işlem nesnesi yapılan işlemle ilgili bilgileri içerecek şekilde verilmelidir</param>
        public virtual void AddHistory(History.History history)
        {
            _histories.Add(history);
        }

        /// <summary>
        /// Email adresinin onaylanması işlemini yapmaktadır.
        /// </summary>
        /// <param name="approvedCode">Kullanıcının doğrulanması için gönderdiği aktivasyon kodudur.</param>
        /// <param name="ipAddress">Onaylama işlemi yapılan ip adresi gönderilmelidir.</param>
        public virtual bool EmailApproved(Guid approvedCode, string ipAddress)
        {
            if (approvedCode == EmailApprovedCode)
            {
                ParameterCheck.ThrowExceptionIsNullOrEmpty<string>(ipAddress, "ipAddress");
                if (!Regex.IsMatch(ipAddress, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"))
                {
                    throw new FormatException(String.Format(ErrorMessage.InvalidParameter, "ipAddress", "ipAddress"));
                }
                EmailApprovedDatetime = DateTime.Now;
                EmailApprovedIpAddress = ipAddress;
                EmailIsApproved = true;
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Email adresinin onaylanmamış olmasını sağlamaktadır.
        /// </summary>
        public virtual void EmailUnApproved()
        {
            EmailApprovedDatetime = DateTime.MinValue;
            EmailApprovedIpAddress = string.Empty;
            EmailIsApproved = false;
        }


        public virtual void NewEmailApprovedCode()
        {
            EmailApprovedCode = Guid.NewGuid();
        }


        public virtual ErrorMessageDto ChangePassword(string oldPassword, string newPassword, string newPasswordApproval)
        {
            if (PasswordControl(oldPassword))
                return new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Validation,
                    Message = ErrorMessage.InvalidPassword
                };

            if (newPassword != newPasswordApproval)
            {
                return new ErrorMessageDto()
                {
                    ErrorType = ErrorType.Validation,
                    Message = ErrorMessage.ParameterNotApproval,
                    PropertyName = "NewPassword-NewPasswordApproval"
                };
            }
            PasswordHash = IoCResolver.Instance.ReleaseInstance<ICryptography>().Sha256(newPassword, SecurityStamp);
            return null;
        }

        #endregion
    }
}
