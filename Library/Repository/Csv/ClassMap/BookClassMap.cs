using System.Linq;
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
         Map(b => b.Authors).Name("Author").ConvertUsing(row => row.GetField<string>("Author").Split(",").ToList());
         Map(b => b.Summary).Name("Summary");
      }
   }
}