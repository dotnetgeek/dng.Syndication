using System.IO;

namespace dng.Syndication.Tests
{
    internal static  class ExpectedContentLoader
    {
        private const string ExamplesPath = "Examples";

        internal static string BuildFilePath(
            string expectedFile)
        {
            return Path.Combine(ExamplesPath, expectedFile);
        }

        internal static string LoadFromFile(
            string expectedFile)
        {
            return File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
        }
    }
}
