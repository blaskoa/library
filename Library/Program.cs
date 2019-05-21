using System;
using System.Linq;
using Library.Repository;
using Library.Repository.Csv;

namespace Library
{
   class Program
   {
      private const string AuthorsFilePath = "authors.csv";
      private const string BooksFilePath = "books.csv";
      private const string MagazinesFilePath = "magazines.csv";
      static void Main(string[] args)
      {
         //TODO parse CLI arguments
         //TODO load CSV
         //TODO print all books/magazines
         //TODO search book/magazine by ISBN
         //TODO search book/magazine by author
         //TODO sort books/magazines by title and print

         IBookRepository bookRepository = new BookCsvRepository(BooksFilePath);
         IAuthorRepository authorRepository = new AuthorCsvRepository(AuthorsFilePath);
         IMagazineRepository magazineRepository = new MagazineCsvRepository(MagazinesFilePath);

         bookRepository.GetAllBooks().ToList().ForEach(Console.WriteLine);
         authorRepository.GetAllAuthors().ToList().ForEach(Console.WriteLine);
         magazineRepository.GetAllMagazines().ToList().ForEach(Console.WriteLine);
      }
   }
}
