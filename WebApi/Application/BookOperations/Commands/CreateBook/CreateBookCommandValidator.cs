using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.AuthorId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateOnly.FromDateTime(DateTime.Today));
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4).Must(title => title.Trim().Length >= 4).WithMessage("Kitap adı geçersiz.");
        }
    }
}