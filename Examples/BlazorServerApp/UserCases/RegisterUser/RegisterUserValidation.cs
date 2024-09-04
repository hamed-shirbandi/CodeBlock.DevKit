using FluentValidation;

namespace BlazorServerApp.UserCases.RegisterUser;

public sealed class RegisterUserValidation : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidation()
    {
        ValidateUserName();
    }

    private void ValidateUserName()
    {
        RuleFor(o => o.UserName).NotEqual(o => "admin").WithMessage("admin username is resrved");
    }
}
