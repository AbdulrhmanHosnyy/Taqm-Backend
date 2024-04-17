using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Authentication.Queries.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Authentication.Queries.Validator
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ConfirmEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(u => u.UserId)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(u => u.Code)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
