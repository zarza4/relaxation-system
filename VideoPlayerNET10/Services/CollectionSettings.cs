using VideoPlayerNET10.Models;

namespace VideoPlayerNET10.Services;

public static class CollectionSettings
{
    public static string CurrentCollectionPath { get; set; } = string.Empty;

    public static VideoItem? SelectedVideo { get; set; }

    public static MusicItem? SelectedMusic { get; set; }

    public static bool EnableCaptions { get; set; }

    public static bool EnableLooping { get; set; }

    public static string SelectedTagBackground { get; set; } = "background_default.png";

    public static string GetFullVideoPath(string path)
    {
        return path;
    }

    public static string GetFullMusicPath(string path)
    {
        return path;
    }

    public static string GetFullThumbnailPath(string path)
    {
        return path;
    }

    public static string GetTagIconPath(string tagName)
    {
        return Path.Combine(
            CurrentCollectionPath,
            "TagIcons",
            tagName + ".webp");
    }

    public static string GetTagCardPath(
    string tag)
    {
        return Path.Combine(
            CurrentCollectionPath,
            "TagNamecards",
            $"{tag}.webp");
    }

    public static void ClearSelections()
    {
        SelectedVideo = null;
        SelectedMusic = null;

        EnableCaptions = false;
        EnableLooping = false;
    }

    private static readonly Dictionary<string, string> TagBackgroundMap = new()
    {
        { "test", "background_pyro.png" },
        { "nigger", "background_hydro.png" }
    };

    public static string GetBackgroundForTag(string tag)
    {
        return TagBackgroundMap.TryGetValue(tag.ToLower(), out var bg)
            ? Path.Combine(CurrentCollectionPath, bg)
            : Path.Combine(CurrentCollectionPath, "background_default.png");
    }
}
