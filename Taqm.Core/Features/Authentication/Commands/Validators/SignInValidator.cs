using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Authentication.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Authentication.Commands.Validators
{
    internal class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public SignInValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(u => u.Email)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .MinimumLength(6).WithMessage(_stringLocalizer[SharedResourcesKeys.MinLength6])
                .MaximumLength(16).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength16]);
        }
        #endregion
    }
}
