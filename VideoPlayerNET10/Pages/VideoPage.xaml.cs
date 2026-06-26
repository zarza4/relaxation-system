using VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;

namespace VideoPlayerNET10.Pages;

public partial class VideoPage : ContentPage
{
    private readonly JsonService _json;

    private List<VideoItem> _videos = new();
    private List<TagItem> _allTags = new();

    private bool _bgToggle = false;

    public VideoPage(JsonService json)
    {
        InitializeComponent();
        _json = json;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        TransitionOverlay.Opacity = 1;

        _videos = _json.LoadVideos();
        VideoCollection.ItemsSource = _videos;

        BuildTags();

        await Task.Delay(50);

        await TransitionOverlay.FadeToAsync(0, 500, Easing.CubicOut);
    }

    private void BuildTags()
    {
        _allTags =
            _videos
            .SelectMany(v => v.Tags)
            .Distinct()
            .OrderBy(t => t)
            .Select(tag => new TagItem
            {
                Name = tag,
                Count = _videos.Count(v => v.Tags.Contains(tag)),
                IconPath = CollectionSettings.GetTagIconPath(tag),
                BackgroundPath = CollectionSettings.GetTagCardPath(tag)
            })
            .ToList();

        TagView.ItemsSource = _allTags;
    }

    // ✅ ONLY RELIABLE EVENT
    private async void TagSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var tag = (TagItem)e.CurrentSelection[0];

        // filter videos
        VideoCollection.ItemsSource =
            _videos.Where(v => v.Tags.Contains(tag.Name)).ToList();

        // background transition
        await ChangeBackgroundAsync(
            CollectionSettings.GetBackgroundForTag(tag.Name)
        );

        ((CollectionView)sender).SelectedItem = null;
    }

    // ✅ CROSSFADE BACKGROUND
    private async Task ChangeBackgroundAsync(string newImage)
    {
        var current = _bgToggle ? BgB : BgA;
        var next = _bgToggle ? BgA : BgB;

        next.Source = newImage;
        next.Opacity = 0;

        await Task.Delay(20);

        await Task.WhenAll(
            current.FadeToAsync(0, 400),
            next.FadeToAsync(1, 400)
        );

        _bgToggle = !_bgToggle;
    }

    private void SearchChanged(object sender, TextChangedEventArgs e)
    {
        string search = e.NewTextValue ?? "";

        if (string.IsNullOrWhiteSpace(search))
        {
            TagView.ItemsSource = _allTags;
            return;
        }

        TagView.ItemsSource =
            _allTags.Where(t =>
                t.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private async void VideoSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var video = (VideoItem)e.CurrentSelection[0];

        CollectionSettings.SelectedVideo = video;

        ((CollectionView)sender).SelectedItem = null;

        await Shell.Current.GoToAsync(nameof(VideoDetailsPage));
    }

    private async void BackClicked(object sender, EventArgs e)
    {
        TransitionOverlay.Opacity = 0;

        await TransitionOverlay.FadeToAsync(1, 400, Easing.CubicIn);

        await Shell.Current.GoToAsync("..");
    }
}
