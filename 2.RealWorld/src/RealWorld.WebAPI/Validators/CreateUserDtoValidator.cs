using FluentValidation;
using RealWorld.WebAPI.DTOs;

namespace RealWorld.WebAPI.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(p => p.Name).MinimumLength(3);

        //18 yaşından küçük olamaz kontrolü
        //Cannot be under 18 years old control
        RuleFor(p => p.Age).GreaterThan(18);

        //18 yaşından küçük olamaz kontrolü
        //Cannot be under 18 years old control
        RuleFor(p => p.DateOfBirth).LessThanOrEqualTo(new DateOnly(2006, 01, 01));
    }
}
