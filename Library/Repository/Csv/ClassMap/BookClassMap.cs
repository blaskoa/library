using CsvHelper.Configuration;
using Library.Domain;

namespace Library.Repository.Csv.ClassMap
{
   public sealed class BookClassMap : ClassMap<Book>
   {
      public BookClassMap()
      {
         Map(b => b.Title).Name("Title");
         Map(b => b.ISBN).Name("ISBN-Nummber");
         Map(b => b.AuthorsString).Name("Author");
         Map(b => b.Summary).Name("Summary");
      }
   }
}