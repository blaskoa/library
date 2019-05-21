using CsvHelper.Configuration;
using Library.Domain;

namespace Library.Repository.Csv.ClassMap
{
   public sealed class MagazineClassMap : ClassMap<Magazine>
   {
      public MagazineClassMap()
      {
         Map(m => m.Title).Name("Title");
         Map(m => m.ISBN).Name("ISBN-Nummber");
         //TODO map to array of strings
         Map(m => m.AuthorsString).Name("Author");
         Map(m => m.Released).Name("Released");
      }
   }
}