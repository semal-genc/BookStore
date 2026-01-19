using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorDoesNotExist_InvalidOperationException_ShouldBeThrown()
        {
            var query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 999;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Yazar bulunamadı!");
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_AuthorDetail_ShouldBeReturned()
        {
            var author = new Author
            {
                Name = "Ahmet",
                Surname = "Yılmaz",
                BirthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-40))
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            var query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = author.Id;

            var result = query.Handle();

            result.Should().NotBeNull();
            result.Name.Should().Be(author.Name);
            result.Surname.Should().Be(author.Surname);
            result.BirthDate.Should().Be(author.BirthDate);
        }
    }
}