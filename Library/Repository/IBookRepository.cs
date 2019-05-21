using System.Linq;
using Library.Domain;

namespace Library.Repository
{
   public interface IBookRepository
   {
      IQueryable<Book> GetAllBooks();
   }
}