using FluentValidation;
using TodoApi.Application.DTOs;

namespace TodoApi.Application.Validators;

public class CreateTodoValidator : AbstractValidator<CreateTodoDto>
{
    public CreateTodoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(1).WithMessage("Title must be at least 1 character long");
    }
    
}