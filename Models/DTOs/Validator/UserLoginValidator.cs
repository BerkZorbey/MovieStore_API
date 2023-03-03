using FluentValidation;

namespace MovieStore_API.Models.DTOs.Validator
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);

        }
    }
}
