using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Authorization.Roles.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Authorization.Roles.Commands.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public DeleteRoleValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Functions
        public void ApplyValidationRules()
        {
            RuleFor(r => r.Id)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        private void ApplyCustomValidationRules()
        {
        }
        #endregion
    }
}
