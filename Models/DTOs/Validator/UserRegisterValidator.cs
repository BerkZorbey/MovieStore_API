using FluentValidation;

namespace MovieStore_API.Models.DTOs.Validator
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
        }
    }
}
