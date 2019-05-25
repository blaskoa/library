using Xunit;

namespace Library.Integration
{
   public class MainTests
   {
      [Theory]
      [InlineData("")]
      [InlineData("--help")]
      [InlineData("--version")]
      [InlineData("ifind")]
      [InlineData("ifind", "-i")]
      [InlineData("afind")]
      [InlineData("afind", "-e")]
      public void ExecuteMain_UseInvalidOrHelpArgs_ExitCodeIsNonSuccess(params string[] args)
      {
         int exitCode = Program.Main(args);
         Assert.NotEqual(0, exitCode);
      }

      [Theory]
      [InlineData("get")]
      [InlineData("get", "-s")]
      [InlineData("ifind", "-i", "3214-5698-7412")]
      [InlineData("afind", "-e", "pr-mueller@optivo.de")]
      [InlineData("afind", "-e", "x")]
      public void ExecuteMain_UseValidArgs_ExitCodeIsSuccess(params string[] args)
      {
         int exitCode = Program.Main(args);
         Assert.Equal(0, exitCode);
      }
   }
}
