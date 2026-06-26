using VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;

namespace VideoPlayerNET10.Pages;

public partial class MusicPage : ContentPage
{
    private readonly JsonService _json;

    private List<MusicItem> _music = new();

    public MusicPage(JsonService json)
    {
        InitializeComponent();

        _json = json;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _music = _json.LoadMusic();

        MusicCollection.ItemsSource = _music;

        BuildTags();
    }

    private void BuildTags()
    {
        var tags =
            _music
            .SelectMany(x => x.Tags)
            .GroupBy(x => x)
            .Select(x => new TagItem
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

    private void SearchChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        string search =
            e.NewTextValue ?? "";

        if (string.IsNullOrWhiteSpace(search))
        {
            MusicCollection.ItemsSource =
                _music;

            return;
        }

        MusicCollection.ItemsSource =
            _music
            .Where(x =>
                x.Title.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase)
                ||
                x.Artist.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase)
                ||
                x.Tags.Any(t =>
                    t.Contains(
                        search,
                        StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private void TagSelected(
        object? sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var tag =
            (TagItem)e.CurrentSelection[0];

        MusicCollection.ItemsSource =
            _music
            .Where(x =>
                x.Tags.Contains(tag.Name))
            .ToList();

        if (sender is CollectionView collection)
        {
            collection.SelectedItem = null;
        }
    }

    private async void MusicSelected(
        object? sender,
        SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var music =
            (MusicItem)e.CurrentSelection[0];

        CollectionSettings.SelectedMusic =
            music;

        if (sender is CollectionView collection)
        {
            collection.SelectedItem = null;
        }

        await Shell.Current.GoToAsync(
            nameof(MusicDetailsPage));
    }
}
