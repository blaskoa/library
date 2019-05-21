using System.Linq;
using Library.Domain;
using Library.Repository.Csv.ClassMap;

namespace Library.Repository.Csv
{
   public class MagazineCsvRepository : BaseCsvRepository<Magazine, MagazineClassMap>, IMagazineRepository
   {
      public MagazineCsvRepository(string filePath) : base(filePath)
      {
      }

      public IQueryable<Magazine> GetAllMagazines()
      {
         return ReadFromCsv().AsQueryable();
      }
   }
}