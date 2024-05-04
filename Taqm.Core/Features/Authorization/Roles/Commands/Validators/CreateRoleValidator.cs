using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Authorization.Roles.Commands.Models;
using Taqm.Core.Resources;
using Taqm.Service.Abstracts;

namespace Taqm.Core.Features.Authorization.Roles.Commands.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public CreateRoleValidator(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(r => r.Name)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExistByNameAsync(Key))
                .WithMessage(_stringLocalizer[SharedResourcesKeys.ExistedRole]);
        }
        #endregion
    }
}
