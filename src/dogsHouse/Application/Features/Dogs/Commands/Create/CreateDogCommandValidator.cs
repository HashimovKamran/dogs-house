using FluentValidation;

namespace Application.Features.Dogs.Commands.Create;

public class CreateDogCommandValidator : AbstractValidator<CreateDogCommand>
{
    public CreateDogCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(30);
        RuleFor(c => c.Color).NotEmpty().MaximumLength(30);
        RuleFor(c => c.TailLength).GreaterThan((ushort)0);
        RuleFor(c => c.Weight).GreaterThan((ushort)0);
    }
}