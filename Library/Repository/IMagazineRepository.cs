using System.Linq;
using Library.Domain;

namespace Library.Repository
{
   public interface IMagazineRepository
   {
      IQueryable<Magazine> GetAllMagazines();
   }
}