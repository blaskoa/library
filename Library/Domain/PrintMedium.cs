namespace Library.Domain
{
   public abstract class PrintMedium
   {
      public string ISBN { get; set; }
      public string Title { get; set; }
      public string AuthorsString { get; set; }
   }
}