using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenSongToChordPro.Core.OpenSongFormats
{
    public static class Tools
    {
        public static string GetOpenSongLyricsWithoutChords(string lyrics)
        {
            var sb = new StringBuilder();
            foreach (var line in lyrics.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (line.StartsWith("."))
                    continue;
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        public static song ReadOpenSongXmlFromFile(string file, XmlSerializer serializer)
        {
            try
            {
                song song;

                using (var reader = new StreamReader(file))
                {
                    song = (song)serializer.Deserialize(reader);
                }

                return song;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Path.GetFileName(file)} - {ex.Message}");
                return null;
            }
        }

        public static song ReadOpenSongXmlFromFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(song));
            return ReadOpenSongXmlFromFile(file, serializer);
        }

        public static Task SaveOpenSongFileAsChord(string file)
        {
            try
            {
                var outputFile = $"{file.GetFullPathWithoutExtension()}.cho";
                var song = Tools.ReadOpenSongXmlFromFile(file);
                if (song == null)
                    return Task.CompletedTask;

                var chordFileContent = song.ToChordPro();

                return File.WriteAllTextAsync(outputFile, chordFileContent);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return Task.FromException(ex);
            }
        }

        public static string GetFullPathWithoutExtension(this string file) => $"{Path.Combine(new FileInfo(file).DirectoryName, Path.GetFileNameWithoutExtension(file))}";


        public static async Task ParseAndConvertFolder(string directory)
        {
            var di = new DirectoryInfo(directory);
            var files = di.GetFiles();
            var tasks = new List<Task>();
            foreach(var file in files)
            {
                tasks.Add(SaveOpenSongFileAsChord(file.FullName));
            }

            await Task.WhenAll(tasks.ToArray());

            var folders = di.GetDirectories();
            var dirTasks = new List<Task>();

            foreach(var folder in folders)
            {
                dirTasks.Add(ParseAndConvertFolder(folder.FullName));
            }

            await Task.WhenAll(dirTasks.ToArray());
        }
    }
}