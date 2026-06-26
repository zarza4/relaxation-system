namespace VideoPlayerNET10.Models;

public class CaptionItem
{
    public TimeSpan Start { get; set; }

    public TimeSpan End { get; set; }

    public string Text { get; set; } = string.Empty;
}
