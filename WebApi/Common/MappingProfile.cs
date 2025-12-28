using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*BOOK*/
            CreateMap<CreateBookModel, Book>();

            CreateMap<UpdateBookModel, Book>()
                .ForMember(dest => dest.Title, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Title));
                    opt.MapFrom(src => src.Title!.Trim());
                })
                .ForMember(dest => dest.PageCount, opt =>
                {
                    opt.PreCondition(src => src.PageCount.HasValue);
                    opt.MapFrom(src => src.PageCount!.Value);
                })
                .ForMember(dest => dest.PublishDate, opt =>
                {
                    opt.PreCondition(src => src.PublishDate.HasValue);
                    opt.MapFrom(src => src.PublishDate!.Value);
                })
                 .ForMember(dest => dest.GenreId, opt =>
                {
                    opt.PreCondition(src => src.GenreId.HasValue);
                    opt.MapFrom(src => src.GenreId!.Value);
                })
                .ForMember(dest => dest.AuthorId, opt =>
                {
                    opt.PreCondition(src => src.AuthorId.HasValue);
                    opt.MapFrom(src => src.AuthorId!.Value);
                });

            CreateMap<Book, GetBookDetailViewModel>()
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null)
                    )
                .ForMember(
                    dest => dest.AuthorFullName,
                    opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname)
                    );
            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null)
                    )
                .ForMember(
                    dest => dest.AuthorFullName,
                    opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname)
                    );


            /* GENRE */
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            /* AUTHOR */
            CreateMap<Author, GetAuthorsModel>();
            CreateMap<Author, GetAuthorDetailModel>();

            CreateMap<CreateAuthorModel, Author>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name.Trim())
                    )
                .ForMember(
                    dest => dest.Surname,
                    opt => opt.MapFrom(src => src.Surname.Trim())
                    );
            CreateMap<UpdateAuthorModel, Author>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Name));
                    opt.MapFrom(src => src.Name!.Trim());
                })
                .ForMember(dest => dest.Surname, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Surname));
                    opt.MapFrom(src => src.Surname!.Trim());
                })
                .ForMember(dest => dest.BirthDate, opt =>
                {
                    opt.PreCondition(src => src.BirthDate.HasValue);
                    opt.MapFrom(src => src.BirthDate!.Value);
                });

        }
    }
}