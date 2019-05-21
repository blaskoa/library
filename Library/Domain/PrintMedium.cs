using System.Collections.Generic;
using System.Linq;

namespace Library.Domain
{
   public abstract class PrintMedium
   {
      public string ISBN { get; set; }
      public string Title { get; set; }
      public List<string> Authors => AuthorsString != null ? AuthorsString.Split(",").ToList() : new List<string>();
      public string AuthorsString { get; set; }
   }
}