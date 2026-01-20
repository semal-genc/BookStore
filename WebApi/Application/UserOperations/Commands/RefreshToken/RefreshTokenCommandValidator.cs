using FluentValidation;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(command => command.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token boş olamaz.");
        }
    }
}