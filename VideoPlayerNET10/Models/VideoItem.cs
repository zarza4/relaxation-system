using Microsoft.Maui.Graphics;

namespace VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;
public class VideoItem
{
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string VideoPath { get; set; } = string.Empty;

    public string ThumbnailPath { get; set; } = string.Empty;

    public int Width { get; set; }

    public int Height { get; set; }

    public int Rating { get; set; }

    public List<string> Tags { get; set; } = new();

    public List<CaptionItem> Captions { get; set; } = new();

    public string ResolutionLabel
    {
        get
        {
            return Height switch
            {
                720 => "720P",
                1080 => "1080P",
                2160 => "2160P",
                _ => $"{Height}P"
            };
        }
    }

    public Color ResolutionColor
    {
        get
        {
            return Height switch
            {
                720 => Colors.Red,
                1080 => Colors.LimeGreen,
                2160 => Colors.Gold,
                _ => Colors.White
            };
        }
    }

    public string Stars
    {
        get
        {
            int rating =
                Math.Clamp(Rating, 0, 5);

            string filled =
                new string('✦', rating);

            string empty =
                new string('✧', 5 - rating);

            return filled + empty;
        }
    }

    public string ResolutionText
    {
        get
        {
            return $"{Width}x{Height}";
        }
    }

    public bool LoopByDefault { get; set; }

    public bool HasCaptions
    {
        get
        {
            return Captions != null &&
                   Captions.Count > 0;
        }
    }
}