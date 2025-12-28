using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.Name).MinimumLength(4).When(command => !string.IsNullOrWhiteSpace(command.Model.Name));
            RuleFor(command => command.Model.Surname).MinimumLength(4).When(command => !string.IsNullOrWhiteSpace(command.Model.Name));
            RuleFor(command => command.Model.BirthDate).LessThan(DateOnly.FromDateTime(DateTime.Today)).When(command => command.Model.BirthDate.HasValue);
        }
    }
}