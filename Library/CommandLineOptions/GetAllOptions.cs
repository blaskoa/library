using CommandLine;

namespace Library.CommandLineOptions
{
   [Verb("get", HelpText = "Prints all print media")]
   public class GetAllOptions
   {
      [Option('s', Default = false, HelpText = "If set, sort the result by Title ascending")]
      public bool Sorted { get; set; }
   }
}
