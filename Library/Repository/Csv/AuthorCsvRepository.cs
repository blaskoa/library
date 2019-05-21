using System.Linq;
using Library.Domain;
using Library.Repository.Csv.ClassMap;

namespace Library.Repository.Csv
{
   public class AuthorCsvRepository : BaseCsvRepository<Author, AuthorClassMap>, IAuthorRepository
   {
      public AuthorCsvRepository(string filePath) : base(filePath)
      {
      }

      public IQueryable<Author> GetAllAuthors()
      {
         return ReadFromCsv().AsQueryable();
      }
   }
}