namespace Library.Domain
{
   public class Book : PrintMedium
   {
      public string Summary { get; set; }

      public override string ToString()
      {
         return $"Book: {Title}; {ISBN}; {string.Join("|", Authors)}; {Summary}";
      }
   }
}