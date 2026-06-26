using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Core;
using VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;


namespace VideoPlayerNET10.Pages;

public partial class PlayerPage : ContentPage
{
    private IDispatcherTimer? _captionTimer;

    public PlayerPage()
    {
        InitializeComponent();
        VideoPlayerElement.MediaFailed += VideoPlayerElement_MediaFailed;
    }

    protected override void OnAppearing()
    {
        DisplayAlertAsync(
            "Debug",
            CollectionSettings.SelectedVideo?.Title ?? "NULL",
            "OK");

        base.OnAppearing();

        LoadVideo();

        LoadMusic();

        StartCaptionTimer();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _captionTimer?.Stop();

        VideoPlayerElement.Stop();

        MusicPlayerElement.Stop();
    }

    private void LoadVideo()
    {
        var video = CollectionSettings.SelectedVideo;

        if (video == null)
            return;

        if (!File.Exists(video.VideoPath))
            return;

        VideoPlayerElement.Source =
            MediaSource.FromFile(video.VideoPath);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            VideoPlayerElement.Play();
        });
    }

    private void LoadVideoClicked(
    object sender,
    EventArgs e)
    {
        LoadVideo();
    }

    private void LoadMusic()
    {
        var music =
            CollectionSettings.SelectedMusic;

        if (music == null)
            return;

        string fullPath =
            music.MusicPath;

        if (!File.Exists(fullPath))
            return;

        MusicPlayerElement.Source =
            MediaSource.FromFile(fullPath);
    }

    private void StartCaptionTimer()
    {
        if (!CollectionSettings.EnableCaptions)
            return;

        var video =
            CollectionSettings.SelectedVideo;

        if (video == null)
            return;

        if (video.Captions == null)
            return;

        if (video.Captions.Count == 0)
            return;

        CaptionLabel.IsVisible = true;

        _captionTimer =
            Dispatcher.CreateTimer();

        _captionTimer.Interval =
            TimeSpan.FromMilliseconds(250);

        _captionTimer.Tick +=
            CaptionTimer_Tick;

        _captionTimer.Start();
    }

    private void CaptionTimer_Tick(
        object? sender,
        EventArgs e)
    {
        var video =
            CollectionSettings.SelectedVideo;

        if (video == null)
            return;

        TimeSpan position =
            VideoPlayerElement.Position;

        CaptionItem? caption =
            video.Captions.FirstOrDefault(
                x =>
                    position >= x.Start &&
                    position <= x.End);

        CaptionLabel.Text =
            caption?.Text ?? "";
    }

    private void VideoPlayerElement_MediaFailed(
        object? sender,
        EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {

        });
    }
}
