using FluentValidation;

namespace Application.CQRS.Products.Commands.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(r => r.Description).NotNull();
            RuleFor(r => r.CategoryId).NotNull().GreaterThan(0);
        }
    }
}
