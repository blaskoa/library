using CsvHelper.Configuration;
using Library.Domain;

namespace Library.Repository.Csv.ClassMap
{
   public sealed class AuthorClassMap : ClassMap<Author>
   {
      public AuthorClassMap()
      {
         Map(a => a.Email).Name("Email");
         Map(a => a.FirstName).Name("FirstName");
         Map(a => a.LastName).Name("LastName");
      }
   }
}