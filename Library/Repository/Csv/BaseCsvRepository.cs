using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace Library.Repository.Csv
{
   public abstract class BaseCsvRepository<TEntity, TMap> where TMap : ClassMap<TEntity>
   {
      private readonly string _filePath;

      protected BaseCsvRepository(string filePath)
      {
         _filePath = filePath;
      }

      protected IEnumerable<TEntity> ReadFromCsv()
      {
         IEnumerable<TEntity> result;
         using (var reader = new StreamReader(_filePath, CodePagesEncodingProvider.Instance.GetEncoding(1252)))
         {
            using (var csv = new CsvReader(reader))
            {
               csv.Configuration.RegisterClassMap<TMap>();
               result = csv.GetRecords<TEntity>().ToList();
            }
         }
         
         return result;
      }
   }
}