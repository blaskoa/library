using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain;
using Library.Repository;
using Library.Service;
using Moq;
using Xunit;

namespace Library.Tests
{
   public class LibraryServiceTests
   {
      [Fact]
      public void RepositoriesHaveNoPrintMedia_GetAllPrintMedia_EmptyListIsReturned()
      {
         var mockBookRepository = new Mock<IBookRepository>();
         var mockAuthorRepository = new Mock<IAuthorRepository>();
         var mockMagazineRepository = new Mock<IMagazineRepository>();

         mockBookRepository.Setup(x => x.GetAllBooks()).Returns(new List<Book>().AsQueryable);
         mockMagazineRepository.Setup(x => x.GetAllMagazines()).Returns(new List<Magazine>().AsQueryable);

         var service = new LibraryService(mockBookRepository.Object, mockAuthorRepository.Object,
            mockMagazineRepository.Object);

         var result = service.GetAllPrintMedia();
         
         Assert.Empty(result);
      }

      [Fact]
      public void RepositoriesHaveSomePrintMedia_GetAllPrintMedia_NonEmptyListIsReturned()
      {
         var mockBookRepository = new Mock<IBookRepository>();
         var mockAuthorRepository = new Mock<IAuthorRepository>();
         var mockMagazineRepository = new Mock<IMagazineRepository>();

         mockBookRepository.Setup(x => x.GetAllBooks()).Returns(new List<Book>{new Book()}.AsQueryable);
         mockMagazineRepository.Setup(x => x.GetAllMagazines()).Returns(new List<Magazine>().AsQueryable);

         var service = new LibraryService(mockBookRepository.Object, mockAuthorRepository.Object,
            mockMagazineRepository.Object);

         var result = service.GetAllPrintMedia();
         
         Assert.NotEmpty(result);
      }

      [Theory]
      [InlineData(0, 0)]
      [InlineData(1, 0)]
      [InlineData(0, 1)]
      [InlineData(1, 1)]
      [InlineData(7, 15)]
      public void BookAndMagazineRepositoriesHaveData_GetAllPrintMedia_DataFromBothRepositoriesAreReturned(int bookCount, int magazineCount)
      {
         var mockBookRepository = new Mock<IBookRepository>();
         var mockAuthorRepository = new Mock<IAuthorRepository>();
         var mockMagazineRepository = new Mock<IMagazineRepository>();


         var bookList = new List<Book>(bookCount);

         for (int i = 0; i < bookCount; i++)
         {
            bookList.Add(new Book());
         }

         var magazineList = new List<Magazine>(magazineCount);

         for (int i = 0; i < magazineCount; i++)
         {
            magazineList.Add(new Magazine());
         }

         mockBookRepository.Setup(x => x.GetAllBooks()).Returns(bookList.AsQueryable);
         mockMagazineRepository.Setup(x => x.GetAllMagazines()).Returns(magazineList.AsQueryable);

         var service = new LibraryService(mockBookRepository.Object, mockAuthorRepository.Object,
            mockMagazineRepository.Object);

         var result = service.GetAllPrintMedia();
         
         Assert.Equal(bookCount + magazineCount, result.Count());
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForBookISBN_BookIsReturned()
      {
         var service = PrepareServiceWithData();
         var ISBN = "1";

         var result = service.FindMediumWithIsbn(ISBN);

         Assert.IsType<Book>(result);
         Assert.Equal(ISBN, result.ISBN);
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForMagazineISBN_MagazineIsReturned()
      {
         var service = PrepareServiceWithData();
         var ISBN = "3";

         var result = service.FindMediumWithIsbn(ISBN);

         Assert.IsType<Magazine>(result);
         Assert.Equal(ISBN, result.ISBN);
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForNonExistingISBN_NullIsReturned()
      {
         var service = PrepareServiceWithData();
         var ISBN = "nonExistingISBN";

         var result = service.FindMediumWithIsbn(ISBN);

         Assert.Null(result);
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForNonExistingAuthor_EmptyResultIsReturned()
      {
         var service = PrepareServiceWithData();
         var authorEmail = "nonExistentAuthorEmail";

         var result = service.GetPrintMediaByAuthor(authorEmail);

         Assert.Empty(result);
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForAuthorWithOneMedium_OneResultIsReturned()
      {
         var service = PrepareServiceWithData();
         var authorEmail = "jkr@hp.com";

         var result = service.GetPrintMediaByAuthor(authorEmail);

         Assert.Single(result);
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForAuthorWithMultipleMedia_MultipleResultsAreReturned()
      {
         var service = PrepareServiceWithData();
         var authorEmail = "grrm@got.com";

         var result = service.GetPrintMediaByAuthor(authorEmail);

         Assert.True(result.Count() > 1);
      }

      [Fact]
      public void UseServiceWithPreparedData_SearchForAuthorThatCoauthored_SomeResultsAreReturned()
      {
         var service = PrepareServiceWithData();
         var authorEmail = "hkuschel@IEEE.org";

         var result = service.GetPrintMediaByAuthor(authorEmail);

         Assert.NotEmpty(result);
      }

      [Fact]
      public void UseServiceWithPreparedData_GetAllPrintMediaSorted_TheResultIsSorthedAlphabetically()
      {
         var service = PrepareServiceWithData();

         var result = service.GetAllPrintMediaSorted().ToList();

         Assert.Equal("1", result[0].ISBN);
         Assert.Equal("3", result[1].ISBN);
         Assert.Equal("2", result[2].ISBN);
         Assert.Equal("4", result[3].ISBN);
      }

      private static LibraryService PrepareServiceWithData()
      {
         var mockBookRepository = new Mock<IBookRepository>();
         var mockAuthorRepository = new Mock<IAuthorRepository>();
         var mockMagazineRepository = new Mock<IMagazineRepository>();

         mockBookRepository.Setup(x => x.GetAllBooks()).Returns(new List<Book>
         {
            new Book
            {
               ISBN = "1",
               Authors = new List<string>
               {
                  "grrm@got.com"
               },
               Summary = "That book from the series",
               Title = "A Song of Ice and Fire"
            },
            new Book
            {
               ISBN = "2",
               Authors = new List<string>
               {
                  "jkr@hp.com"
               },
               Summary = "You're a wizard, Harry!",
               Title = "Harry Potter"
            }
         }.AsQueryable());

         mockAuthorRepository.Setup(x => x.GetAllAuthors()).Returns(new List<Author>
         {
            new Author
            {
               Email = "grrm@got.com",
               FirstName = "George R. R.",
               LastName = "Martin"
            },
            new Author
            {
               Email = "jkr@hp.com",
               FirstName = "J. K.",
               LastName = "Rowling"
            }
         }.AsQueryable);

         mockMagazineRepository.Setup(x => x.GetAllMagazines()).Returns(new List<Magazine>
         {
            new Magazine
            {
               ISBN = "3",
               Authors = new List<string>()
               {
                  "hkuschel@IEEE.org",
                  "rappel@IEEE.org"
               },
               Title = "Aerospace and Electronic Systems Magazine",
               Released = DateTime.UnixEpoch
            },
            new Magazine
            {
               ISBN = "4",
               Authors = new List<string>()
               {
                  "grrm@got.com"
               },
               Title = "How to sell right to a fantasy universe",
               Released = DateTime.UnixEpoch
            }
         }.AsQueryable());


         var service = new LibraryService(mockBookRepository.Object, mockAuthorRepository.Object,
            mockMagazineRepository.Object);
         return service;
      }
   }
}
