using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Users.Queries.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Users.Queries.Validators
{
    public class ConfirmCreateUserEmailValidator : AbstractValidator<ConfirmCreateUserEmailQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public ConfirmCreateUserEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(u => u.UserId)
               .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty]);

            RuleFor(u => u.EmailConfirmationToken)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        #endregion
    }
}
