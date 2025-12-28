using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(4).When(command => !string.IsNullOrWhiteSpace(command.Model.Title));
            RuleFor(command => command.Model.GenreId).GreaterThan(0).When(command => command.Model.GenreId.HasValue);
            RuleFor(command => command.Model.AuthorId).GreaterThan(0).When(command => command.Model.GenreId.HasValue);
            RuleFor(command => command.Model.PublishDate).LessThan(DateOnly.FromDateTime(DateTime.Today)).When(command => command.Model.PublishDate.HasValue);
            RuleFor(command => command.Model.PageCount).GreaterThan(0).When(command => command.Model.PageCount.HasValue);
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}