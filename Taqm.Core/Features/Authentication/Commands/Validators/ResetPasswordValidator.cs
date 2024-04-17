using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Authentication.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Authentication.Commands.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ResetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .MinimumLength(6).WithMessage(_stringLocalizer[SharedResourcesKeys.MinLength6])
                .MaximumLength(16).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength16]);

            RuleFor(u => u.ConfirmPassword)
               .Equal(u => u.Password).WithMessage(_stringLocalizer[SharedResourcesKeys.ConfirmPasswordNotEqualPassword]);

            RuleFor(u => u.Email)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(u => u.Token)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
