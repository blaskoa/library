using System;
using System.IO;
using System.Linq;
using CommandLine;
using Library.CommandLineOptions;
using Library.Domain;
using Library.Repository;
using Library.Repository.Csv;
using Library.Service;

namespace Library
{
   public class Program
   {
      private const string DataFolder = "Data";
      private const string AuthorsFilePath = "authors.csv";
      private const string BooksFilePath = "books.csv";
      private const string MagazinesFilePath = "magazines.csv";

      public static int Main(string[] args)
      {
         IBookRepository bookRepository = new BookCsvRepository(Path.Combine(DataFolder, BooksFilePath));
         IAuthorRepository authorRepository = new AuthorCsvRepository(Path.Combine(DataFolder, AuthorsFilePath));
         IMagazineRepository magazineRepository = new MagazineCsvRepository(Path.Combine(DataFolder, MagazinesFilePath));

         LibraryService service = new LibraryService(bookRepository, authorRepository, magazineRepository);

         return Parser.Default.ParseArguments<GetAllOptions, FindByAuthorOptions, FindByISBNOptions>(args)
            .MapResult(
               (GetAllOptions opts) => RunGetAndReturnExitCode(opts, service),
               (FindByAuthorOptions opts) => RunFindByAuthorAndReturnExitCode(opts, service),
               (FindByISBNOptions opts) => RunFindByISBNAndReturnExitCode(opts, service),
               errs => 1);
      }

      private static int RunGetAndReturnExitCode(GetAllOptions options, LibraryService service)
      {
         IQueryable<PrintMedium> printMedia;

         if (options.Sorted)
         {
            printMedia = service.GetAllPrintMediaSorted();
         }
         else
         {
            printMedia = service.GetAllPrintMedia();
         }

         var allPrintMediaString = string.Join(Environment.NewLine, printMedia);
         Console.WriteLine(allPrintMediaString);
         return 0;
      }

      private static int RunFindByAuthorAndReturnExitCode(FindByAuthorOptions options, LibraryService service)
      {
         var printMediaByAuthor = service.GetPrintMediaByAuthor(options.Email);
         string printMediaByAuthorString;

         if (printMediaByAuthor.Any())
         {
            printMediaByAuthorString = string.Join(Environment.NewLine, printMediaByAuthor);
         }
         else
         {
            printMediaByAuthorString = $"Author {options.Email} has no print media";
         }
          
         Console.WriteLine(printMediaByAuthorString);
         return 0;
      }

      private static int RunFindByISBNAndReturnExitCode(FindByISBNOptions options, LibraryService service)
      {
         var printMedium = service.FindMediumWithIsbn(options.ISBN);
         if (printMedium != null)
         {
            Console.WriteLine(printMedium);
         }
         else
         {
            Console.WriteLine($"Cannot find print media with ISBN {options.ISBN}");
         }
         
         return 0;
      }
   }
}
