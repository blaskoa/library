using System.Linq;
using Library.Domain;
using Library.Repository.Csv.ClassMap;

namespace Library.Repository.Csv
{
   public class BookCsvRepository : BaseCsvRepository<Book, BookClassMap>, IBookRepository
   {
      public BookCsvRepository(string filePath) : base(filePath)
      {
      }

      public IQueryable<Book> GetAllBooks()
      {
         return ReadFromCsv().AsQueryable();
      }
   }
}