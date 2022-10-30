using FluentValidation;
using IFramework.Application.Contract.UserDto;
using IFramework.Infra.Transversal.Validation.Fluent.Validators.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace IFramework.Infra.Transversal.Validation.Fluent.Validators
{
    public class UserInfoDtoValidator : BaseValidator<UserInfoDto>
    {
        public UserInfoDtoValidator():base()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name cannot be null");
            RuleFor(x => x.LastName).NotNull().WithMessage("LastName cannot be null");
        }
    }
}
