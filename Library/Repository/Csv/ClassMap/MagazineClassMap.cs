using System.Linq;
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
         Map(b => b.Authors).Name("Author").ConvertUsing(row => row.GetField<string>("Author").Split(",").ToList());
         Map(m => m.Released).Name("Released");
      }
   }
}