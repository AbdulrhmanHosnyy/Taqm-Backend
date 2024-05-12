using FluentValidation;
using Microsoft.Extensions.Localization;
using Taqm.Core.Features.Posts.Commands.Models;
using Taqm.Core.Resources;

namespace Taqm.Core.Features.Posts.Commands.Validators
{
    public class UpdatePostValidator : AbstractValidator<UpdatePostCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region Constructors
        public UpdatePostValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationRules();
            ApplyCustomRules();
        }
        #endregion

        #region Methods
        public void ApplyValidationRules()
        {
            RuleFor(p => p.PostId)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(p => p.ProductImage)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(p => p.ProductCategory)
                .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull])
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength50]);
            RuleFor(p => p.ProductPrice)
                .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull]);
            RuleFor(p => p.ProductCondition)
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength50]);
            RuleFor(p => p.ProductGender)
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength50]);
            RuleFor(p => p.ProductColor)
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength50]);
            RuleFor(p => p.ProductSeason)
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength50]);
            RuleFor(p => p.ProductSize)
                .MaximumLength(50).WithMessage(_stringLocalizer[SharedResourcesKeys.MaxLength50]);
        }
        public void ApplyCustomRules()
        {
        }
        #endregion
    }
}
