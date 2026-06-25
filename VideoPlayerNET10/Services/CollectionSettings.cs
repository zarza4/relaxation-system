using VideoPlayerNET10.Models;

namespace VideoPlayerNET10.Services;

public static class CollectionSettings
{
    public static string CurrentCollectionPath { get; set; } = string.Empty;

    public static VideoItem? SelectedVideo { get; set; }

    public static MusicItem? SelectedMusic { get; set; }

    public static bool EnableCaptions { get; set; }

    public static bool EnableLooping { get; set; }

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

    public static void ClearSelections()
    {
        SelectedVideo = null;
        SelectedMusic = null;

        EnableCaptions = false;
        EnableLooping = false;
    }
}