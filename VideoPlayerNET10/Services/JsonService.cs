using System.Text.Json;
using VideoPlayerNET10.Models;

namespace VideoPlayerNET10.Services;

public class JsonService
{
    private readonly JsonSerializerOptions _options =
        new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

    public List<VideoItem> LoadVideos()
    {
        try
        {
            string jsonPath =
                Path.Combine(
                    CollectionSettings.CurrentCollectionPath,
                    "videos.json");

            if (!File.Exists(jsonPath))
                return new List<VideoItem>();

            string json =
                File.ReadAllText(jsonPath);

            return JsonSerializer.Deserialize<List<VideoItem>>
                (json, _options)
                ?? new List<VideoItem>();
        }
        catch
        {
            return new List<VideoItem>();
        }
    }

    public List<MusicItem> LoadMusic()
    {
        try
        {
            string jsonPath =
                Path.Combine(
                    CollectionSettings.CurrentCollectionPath,
                    "music.json");

            if (!File.Exists(jsonPath))
                return new List<MusicItem>();

            string json =
                File.ReadAllText(jsonPath);

            return JsonSerializer.Deserialize<List<MusicItem>>
                (json, _options)
                ?? new List<MusicItem>();
        }
        catch
        {
            return new List<MusicItem>();
        }
    }

    public void SaveVideos(List<VideoItem> videos)
    {
        string jsonPath =
            Path.Combine(
                CollectionSettings.CurrentCollectionPath,
                "videos.json");

        string json =
            JsonSerializer.Serialize(
                videos,
                _options);

        File.WriteAllText(
            jsonPath,
            json);
    }

    public void SaveMusic(List<MusicItem> music)
    {
        string jsonPath =
            Path.Combine(
                CollectionSettings.CurrentCollectionPath,
                "music.json");

        string json =
            JsonSerializer.Serialize(
                music,
                _options);

        File.WriteAllText(
            jsonPath,
            json);
    }

    public bool CollectionLoaded()
    {
        return
            !string.IsNullOrWhiteSpace(
                CollectionSettings.CurrentCollectionPath)
            &&
            Directory.Exists(
                CollectionSettings.CurrentCollectionPath);
    }

    public string ResolveVideoPath(VideoItem video)
    {
        return video.VideoPath;
    }

    public string ResolveThumbnailPath(VideoItem video)
    {
        return video.ThumbnailPath;
    }

    public string ResolveMusicPath(MusicItem music)
    {
        return music.MusicPath;
    }
}