using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Name).Must(name => name == null || !string.IsNullOrWhiteSpace(name)).WithMessage("Kitap türü adı boşluklardan oluşamaz.");
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x => !string.IsNullOrWhiteSpace(x.Model.Name));
        }
    }
}