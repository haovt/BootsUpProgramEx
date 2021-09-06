using BookStorePersistence.Domain;
using BookStoreService;
using BookStoreService.Dto;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreServiceTest
{
    // Run test and export result to xml
    // C:\Users\vha\AppData\Local\Apps\OpenCover
    // OpenCover.Console.exe -register:user
    // -target:"c:\Program Files\dotnet\dotnet.exe"
    // -targetargs:"test D:\Boots_Up_Program\Exampleprj\BookStoreServiceTest\bin\Debug\net5.0\BookStoreTest.dll"
    // -output:"D:\Boots_Up_Program\Exampleprj\BookStoreServiceTest\coverage.xml"

    // Report generator
    // dotnet C:\Users\vha\.nuget\packages\reportgenerator\4.8.12\tools\net5.0\ReportGenerator.dll
    // "-reports:D:\Boots_Up_Program\Exampleprj\BookStoreServiceTest\coverage.xml"
    // "-targetdir:D:\Boots_Up_Program\Exampleprj\BookStoreServiceTest\coveragereport"
    // -reporttypes:Html

    [TestFixture]
    public class BSServiceTest
    {
        private BookStorePersistence.IBSRepository _repo;
        private BSService _service;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _repo = Substitute.For<BookStorePersistence.IBSRepository>();
            _service = new BSService(_repo);
        }

        [Test]
        public void GetAll__ExistSomeBooks__ShouldReturnCorrectly()
        {
            _repo.GetAll().Returns(new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Toy story 1",
                    Author = "Disney",
                    Price = 100
                },
                new Book
                {
                    Id = 1,
                    Title = "Opera",
                    Author = "Hamilton",
                    Price = 150
                }
            });

            var result = _repo.GetAll();

            result.Should().HaveCount(2);
            var firstItem = result.First(x => x.Id == 1);
            firstItem.Title.Should().Be("Toy story 1");
            firstItem.Author.Should().Be("Disney");
            firstItem.Price.Should().Be(100);
        }

        [Test]
        public void AddBook__NoDuplicatedBookTitle__ShouldAddSuccessfully()
        {
            var newBook = new BookDto
            {
                Title = "Mona",
                Author = "Vnex",
                Price = 50
            };

            _repo.ExistBook(newBook.Title).Returns(false);
            _service.AddBook(newBook);

            _repo.Received().AddBook(Arg.Any<BookStorePersistence.Domain.Book>());
        }

        [Test]
        public void AddBook__ExistBookWithSameTitle__ShouldReturnException()
        {
            var newBook = new BookDto
            {
                Title = "Mona",
                Author = "Vnex",
                Price = 50
            };

            _repo.ExistBook(newBook.Title).Returns(true);

            Assert.Throws<Exception>(() => _service.AddBook(newBook));      
        }

        [Test]
        public void DeleteBook__ExistBookToDelete__ShouldDeleteSuccessully()
        {
            _service.DeleteBook(1);

            _repo.Received().DeleteBook(1);
        }

        [Test]
        public void UpdateBook__ExistBookToUpdate__ShouldUpdateSuccessully()
        {
            _service.UpdateBook(new BookDto
            {
                Id = 1,
                Author = "Disney",
                Price = 100,
                Title = "Windflow"
            });

            _repo.Received().UpdateBook(Arg.Any<Book>());
        }
    }
}
