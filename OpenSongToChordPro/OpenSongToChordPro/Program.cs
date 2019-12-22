using System;
using System.IO;

namespace OpenSongToChordPro
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location);
            Console.WriteLine($"Input directory => {currentDirectory}");
            var task = OpenSongToChordPro.Core.OpenSongFormats.Tools.ParseAndConvertFolder(currentDirectory);
            task.Wait();
            Console.WriteLine("Task ended!");
        }        
    }
}