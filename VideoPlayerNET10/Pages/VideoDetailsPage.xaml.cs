using VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;

namespace VideoPlayerNET10.Pages;

public partial class VideoDetailsPage : ContentPage
{
    private VideoItem? _video;
    private bool _bgToggle = false;

    public VideoDetailsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        TagsLayout.Children.Clear();

        _video = CollectionSettings.SelectedVideo;
        if (_video == null)
            return;

        TitleLabel.Text = _video.Title;
        DescriptionLabel.Text = _video.Description;
        RatingLabel.Text = _video.Stars;

        ResolutionLabel.Text = _video.ResolutionLabel;
        ResolutionTextLabel.Text = _video.ResolutionText;

        ThumbnailImage.Source =
            ImageSource.FromFile(_video.ThumbnailPath);

        foreach (var tag in _video.Tags)
        {
            TagsLayout.Children.Add(
                new Border
                {
                    StrokeThickness = 0,
                    BackgroundColor = Color.FromArgb("#333333"),
                    Padding = 6,
                    Margin = 4,
                    Content = new Label
                    {
                        Text = tag,
                        TextColor = Colors.White
                    }
                });
        }

        CaptionInfoLabel.Text =
            _video.HasCaptions
                ? $"Captions Available ({_video.Captions.Count})"
                : "No Captions";

        CaptionsSwitch.IsEnabled = _video.HasCaptions;
        CaptionsSwitch.IsToggled = _video.HasCaptions;

        var firstTag = _video.Tags.FirstOrDefault();

        if (!string.IsNullOrEmpty(firstTag))
        {
            await ChangeBackgroundAsync(
                CollectionSettings.GetBackgroundForTag(firstTag)
            );
        }
    }

    private async Task ChangeBackgroundAsync(string newImage)
    {
        if (BgA == null || BgB == null)
            return;

        var current = _bgToggle ? BgB : BgA;
        var next = _bgToggle ? BgA : BgB;

        next.Source = newImage;
        next.Opacity = 0;

        await Task.Delay(20);

        await Task.WhenAll(
            current.FadeToAsync(0, 350),
            next.FadeToAsync(1, 350)
        );

        _bgToggle = !_bgToggle;
    }

    private async void SelectMusicClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(MusicPage));
    }

    private async void PlayVideoClicked(object sender, EventArgs e)
    {
        CollectionSettings.EnableCaptions =
            CaptionsSwitch.IsToggled;

        await DisplayAlertAsync(
            "Selected",
            _video?.Title ?? "",
            "OK");

        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void BackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
