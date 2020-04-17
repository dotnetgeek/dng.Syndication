using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dng.Syndication.Tests
{
    internal static  class ExpectedContentLoader
    {
        private const string ExamplesPath = "Examples";

        internal static string LoadFromFile(
            string expectedFile)
        {
            return File.ReadAllText(Path.Combine(ExamplesPath, expectedFile));
        }
    }
}
