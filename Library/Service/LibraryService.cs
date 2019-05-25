using System.Linq;
using Library.Domain;
using Library.Repository;

namespace Library.Service
{
   public class LibraryService
   {
      private readonly IBookRepository _bookRepository;
      private readonly IAuthorRepository _authorRepository;
      private readonly IMagazineRepository _magazineRepository;

      public LibraryService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMagazineRepository magazineRepository)
      {
         _bookRepository = bookRepository;
         _authorRepository = authorRepository;
         _magazineRepository = magazineRepository;
      }

      public PrintMedium FindMediumWithIsbn(string isbn)
      {
         return GetAllPrintMedia().FirstOrDefault(pm => pm.ISBN == isbn);
      }

      public IQueryable<PrintMedium> GetPrintMediaByAuthor(string authorEmail)
      {
         return GetAllPrintMedia().Where(pm => pm.Authors.Contains(authorEmail));
      }

      public IQueryable<PrintMedium> GetAllPrintMedia()
      {
         return _bookRepository.GetAllBooks().Cast<PrintMedium>()
            .Concat(_magazineRepository.GetAllMagazines().Cast<PrintMedium>());
      }

      public IQueryable<PrintMedium> GetAllPrintMediaSorted()
      {
         return GetAllPrintMedia().OrderBy(x => x.Title);
      }
   }
}