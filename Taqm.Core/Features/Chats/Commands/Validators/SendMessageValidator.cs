using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Chats.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Chats.Commands.Validators
{
    public class SendMessageValidator : AbstractValidator<SendMessageCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public SendMessageValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(sm => sm.UserId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(sm => sm.RecipientId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(sm => sm.MessageContent)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);

            RuleFor(sm => sm.MessageType)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomRules()
        {
        }
        #endregion
    }
}
