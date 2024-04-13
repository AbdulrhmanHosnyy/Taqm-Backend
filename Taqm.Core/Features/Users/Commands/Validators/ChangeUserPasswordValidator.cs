using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Users.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Functions
        public void ApplyValidationRules()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(u => u.CurrentPassword)
              .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
              .MinimumLength(6).WithMessage(_stringLocalizer[SharedResourcesKeys.MinLength6])
               .MaximumLength(16).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength16]);

            RuleFor(u => u.NewPassword)
              .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
              .MinimumLength(6).WithMessage(_stringLocalizer[SharedResourcesKeys.MinLength6])
              .MaximumLength(16).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength16]);

            RuleFor(u => u.ConfirmPassword)
               .Equal(u => u.NewPassword).WithMessage(_stringLocalizer[SharedResourcesKeys.ConfirmPasswordNotEqualPassword]);
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
