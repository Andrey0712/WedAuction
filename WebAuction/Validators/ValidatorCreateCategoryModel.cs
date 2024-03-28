using FluentValidation;
using WebAuction.Models;

namespace WebAuction.Validators
{
    public class ValidatorCreateCategoryModel : AbstractValidator<CreateCategoryViewModel>
    {
        public ValidatorCreateCategoryModel()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Поле не може бути порожнім")
                .MinimumLength(2).WithMessage("Мінімум два символи");
        }
    }
}
