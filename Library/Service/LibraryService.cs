using System;
using System.Linq;
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

      public void PrintAllEntities()
      {
         _bookRepository.GetAllBooks().ToList().ForEach(Console.WriteLine);
         _authorRepository.GetAllAuthors().ToList().ForEach(Console.WriteLine);
         _magazineRepository.GetAllMagazines().ToList().ForEach(Console.WriteLine);
      }
   }
}