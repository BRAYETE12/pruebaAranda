using Application.CQRS.Products.Commands.Update;
using FluentValidation;

namespace Application.CQRS.Products.Commands.Create
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(r => r.Id).NotNull().GreaterThan(0);
            RuleFor(r => r.Description).NotNull();
            RuleFor(r => r.CategoryId).NotNull().GreaterThan(0);
        }
    }
}
