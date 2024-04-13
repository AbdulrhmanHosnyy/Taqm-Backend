using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Users.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Users.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public UpdateUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
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
        }
        public void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
