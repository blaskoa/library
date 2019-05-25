using System;
using System.IO;
using System.Linq;
using Library.Repository;
using Library.Repository.Csv;
using Library.Service;

namespace Library
{
   class Program
   {
      private const string DataFolder = "Data";
      private const string AuthorsFilePath = "authors.csv";
      private const string BooksFilePath = "books.csv";
      private const string MagazinesFilePath = "magazines.csv";

      static void Main(string[] args)
      {
         //TODO parse CLI arguments
         //TODO write tests
         
         IBookRepository bookRepository = new BookCsvRepository(Path.Combine(DataFolder, BooksFilePath));
         IAuthorRepository authorRepository = new AuthorCsvRepository(Path.Combine(DataFolder, AuthorsFilePath));
         IMagazineRepository magazineRepository = new MagazineCsvRepository(Path.Combine(DataFolder, MagazinesFilePath));

         LibraryService service = new LibraryService(bookRepository, authorRepository, magazineRepository);

         Console.WriteLine("--- 1 ---");
         var allPrintMediaString = string.Join(Environment.NewLine, service.GetAllPrintMedia());
         Console.WriteLine(allPrintMediaString);
         Console.WriteLine();

         Console.WriteLine("--- 2 ---");
         var printMedium = service.FindMediumWithIsbn("5454-5587-3210");
         Console.WriteLine(printMedium);
         Console.WriteLine();

         Console.WriteLine("--- 3 ---");
         var printMediaByAuthor = service.GetPrintMediaByAuthor("pr-gustafsson@optivo.de");
         var printMediaByAuthorString = string.Join(Environment.NewLine, printMediaByAuthor);
         Console.WriteLine(printMediaByAuthorString);
         Console.WriteLine();

         Console.WriteLine("--- 4 ---");
         var allPrintMediaSortedString = string.Join(Environment.NewLine, service.GetAllPrintMedia().OrderBy(x => x.Title));
         Console.WriteLine(allPrintMediaSortedString);
      }
   }
}
