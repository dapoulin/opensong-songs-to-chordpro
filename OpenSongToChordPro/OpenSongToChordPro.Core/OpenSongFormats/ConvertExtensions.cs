using System;
using System.Text;

namespace OpenSongToChordPro.Core.OpenSongFormats
{
    public static class ConvertExtensions
    {
        public static string ToChordPro(this Core.OpenSongFormats.song song)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{{title:{song.title}}}");
            sb.AppendLine("{subtitle:}");
            sb.AppendLine($"{{key:{song.key}}}");
            sb.AppendLine($"{{d_ok:{song.key}}}");
            sb.AppendLine("{d_capo:}");
            sb.AppendLine("{d_tempo:}");
            sb.AppendLine($"{{tempo:{song.tempo}}}");
            sb.AppendLine($"{{metronome:{song.tempo}}}");
            sb.AppendLine("{time:}");
            sb.AppendLine($"{{ccli:{song.ccli}}}");
            sb.AppendLine("{artist:}");
            sb.AppendLine($"{{author:{song.author}}}");
            sb.AppendLine("{album:}");
            sb.AppendLine($"{{footer:© {song.copyright}. Reproduction et distribution interdite. Paroles et musique de {song.author}}}");
            sb.AppendLine(Tools.GetOpenSongLyricsWithoutChords(song.lyrics));

            return sb.ToString();
        }
    }
}
