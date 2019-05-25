using CommandLine;

namespace Library.CommandLineOptions
{
   [Verb("ifind", HelpText = "Find print media by ISBN")]
   public class FindByISBNOptions
   {
      [Option('i', Required = true, HelpText = "ISBN of the print media to find")]
      public string ISBN { get; set; }
   }
}
