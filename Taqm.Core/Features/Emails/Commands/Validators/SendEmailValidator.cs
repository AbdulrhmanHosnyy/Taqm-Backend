using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Emails.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public SendEmailValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Email)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                 .MaximumLength(100).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength100]);

            RuleFor(s => s.Subject)
                 .MaximumLength(30).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength30]);

            RuleFor(s => s.Message)
                 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        #endregion
    }
}
