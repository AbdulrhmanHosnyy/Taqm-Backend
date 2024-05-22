using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Users.Commands.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public CreateUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .MaximumLength(30).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength30]);

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .MaximumLength(30).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength30]);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .MinimumLength(6).WithMessage(_stringLocalizer[SharedResourcesKeys.MinLength6])
                .MaximumLength(16).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength16]);
            //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[a-zA-Z\d\W]+$")
            //.WithMessage(_stringLocalizer[SharedResourcesKeys.MatchPassword]);

            RuleFor(u => u.ConfirmPassword)
               .Equal(u => u.Password).WithMessage(_stringLocalizer[SharedResourcesKeys.ConfirmPasswordNotEqualPassword]);
        }
        #endregion
    }
}
