using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Users.Commands.Validators
{
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ForgetPasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomRules()
        {
        }
        #endregion
    }
}
