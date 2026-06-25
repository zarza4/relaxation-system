using VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;

namespace VideoPlayerNET10.Pages;

public partial class VideoPage : ContentPage
{
    private readonly JsonService _json;

    private List<VideoItem> _videos = new();

    public VideoPage(JsonService json)
    {
        InitializeComponent();

        _json = json;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _videos = _json.LoadVideos();

        VideoCollection.ItemsSource = _videos;

        BuildTags();
    }

    private void BuildTags()
    {
        var tags =
            _videos
            .SelectMany(x => x.Tags)
            .GroupBy(x => x)
            .Select(x => new Models.TagItem
            {
                Name = x.Key,
                Count = x.Count(),
                IconPath =
                    CollectionSettings.GetTagIconPath(
                        x.Key)
            })
            .OrderBy(x => x.Name)
            .ToList();

        TagView.ItemsSource = tags;
    }

    private void TagSelected(
        object? sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var tag =
            (TagItem)e.CurrentSelection[0];

        VideoCollection.ItemsSource =
            _videos
            .Where(x =>
                x.Tags.Contains(tag.Name))
                .ToList();

        if (sender is CollectionView collection)
        {
            collection.SelectedItem = null;
        }
    }

    private void SearchChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        string search =
            e.NewTextValue ?? "";

        if (string.IsNullOrWhiteSpace(search))
        {
            VideoCollection.ItemsSource =
                _videos;

            return;
        }

        VideoCollection.ItemsSource =
            _videos
            .Where(x =>
                x.Title.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase)
                ||
                x.Tags.Any(t =>
                    t.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private async void VideoSelected(
        object? sender,
        SelectionChangedEventArgs e)
    {
        await DisplayAlertAsync(
        "Debug",
        "Video clicked",
        "OK");
        if (e.CurrentSelection.Count == 0)
            return;

        var video =
            (VideoItem)e.CurrentSelection[0];

        CollectionSettings.SelectedVideo =
            video;

        if (sender is CollectionView collection)
        {
            collection.SelectedItem = null;
        }

        await Shell.Current.GoToAsync(
            nameof(VideoDetailsPage));
    }
}