using VideoPlayerNET10.Models;
using VideoPlayerNET10.Services;

namespace VideoPlayerNET10.Pages;

public partial class VideoDetailsPage : ContentPage
{
    private VideoItem? _video;

    public VideoDetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        TagsLayout.Children.Clear();

        _video = CollectionSettings.SelectedVideo;

        if (_video == null)
            return;

        TitleLabel.Text = _video.Title;

        DescriptionLabel.Text = _video.Description;

        RatingLabel.Text = _video.Stars;

        ResolutionLabel.Text =
            _video.ResolutionLabel;

        ResolutionLabel.TextColor =
            _video.ResolutionColor;

        ResolutionTextLabel.Text =
            _video.ResolutionText;

        ThumbnailImage.Source =
            ImageSource.FromFile(
            _video.ThumbnailPath);

        foreach (string tag in _video.Tags)
        {
            TagsLayout.Children.Add(
                new Border
                {
                    Padding = 6,
                    Margin = 2,
                    BackgroundColor = Colors.DimGray,
                    Content = new Label
                    {
                        Text = tag
                    }
                });
        }

        CaptionInfoLabel.Text =
            _video.HasCaptions
            ? $"Captions Available ({_video.Captions.Count})"
            : "No Captions";

        CaptionsSwitch.IsEnabled =
            _video.HasCaptions;

        CaptionsSwitch.IsToggled =
            _video.HasCaptions;

        LoopSwitch.IsToggled =
            _video.LoopByDefault;
    }

    private async void SelectMusicClicked(
        object? sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync(
            nameof(MusicPage));
    }

    private async void PlayVideoClicked(
    object? sender,
    EventArgs e)
    {
        CollectionSettings.EnableCaptions =
            CaptionsSwitch.IsToggled;

        CollectionSettings.EnableLooping =
            LoopSwitch.IsToggled;

        await DisplayAlertAsync(
            "Selected",
            _video?.Title ?? "",
            "OK");

        await Shell.Current.GoToAsync("//MainPage");
    }
}