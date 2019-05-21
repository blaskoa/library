using System;

namespace Library.Domain
{
   public class Magazine : PrintMedium
   {
      public DateTime Released { get; set; }
      public override string ToString()
      {
         return $"Magazine: {Title}; {ISBN}; {AuthorsString}; {Released:d}";
      }
   }
}