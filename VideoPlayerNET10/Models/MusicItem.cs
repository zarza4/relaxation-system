namespace VideoPlayerNET10.Models;

public class MusicItem
{
    public string Title { get; set; } = string.Empty;

    public string Artist { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string MusicPath { get; set; } = string.Empty;

    public int Rating { get; set; }

    public List<string> Tags { get; set; } = new();

    public string Stars
    {
        get
        {
            string filled = new string('✦', Rating);
            string empty = new string('✧', 5 - Rating);

            return filled + empty;
        }
    }
}
