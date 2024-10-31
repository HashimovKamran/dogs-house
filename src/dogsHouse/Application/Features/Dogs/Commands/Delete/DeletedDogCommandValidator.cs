using FluentValidation;

namespace Application.Features.Dogs.Commands.Delete;

public class DeleteDogCommandValidator : AbstractValidator<DeleteDogCommand>
{
    public DeleteDogCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}