using System.Linq;
using Library.Domain;

namespace Library.Repository
{
   public interface IAuthorRepository
   {
      IQueryable<Author> GetAllAuthors();
   }
}