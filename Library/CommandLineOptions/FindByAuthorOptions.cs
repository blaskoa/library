using CommandLine;

namespace Library.CommandLineOptions
{
   [Verb("afind", HelpText = "Find all print media by author email address")]
   class FindByAuthorOptions
   {
      [Option('e', Required = true, HelpText = "The Email of the author")]
      public string Email { get; set; }
   }
}
