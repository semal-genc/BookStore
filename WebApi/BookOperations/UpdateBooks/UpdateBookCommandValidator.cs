using FluentValidation;

namespace WebApi.BookOperations.UpdateBooks
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(4);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).LessThan(DateOnly.FromDateTime(DateTime.Today));
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}