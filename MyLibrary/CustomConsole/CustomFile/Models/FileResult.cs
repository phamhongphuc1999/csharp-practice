using System.Diagnostics.CodeAnalysis;

namespace MyLibrary.CustomConsole.CustomFile.Models
{
    public class FileResult
    {
        public string message { get; set; }

        [NotNull]
        public bool result { get; set; }
    }
}
