namespace Library.Domain
{
   public class Author
   {
      public string Email { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }

      public override string ToString()
      {
         return $"Author: {FirstName} {LastName} ({Email})";
      }
   }
}