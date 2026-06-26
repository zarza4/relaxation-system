using VideoPlayerNET10.Pages;
using VideoPlayerNET10.Services;
using CommunityToolkit.Maui.Storage;

namespace VideoPlayerNET10;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        CollectionLabel.Text =
            string.IsNullOrWhiteSpace(CollectionSettings.CurrentCollectionPath)
            ? "No Collection Selected"
            : CollectionSettings.CurrentCollectionPath;
    }

    private async void SelectCollectionClicked(
    object? sender,
    EventArgs e)
    {
        var result =
            await FolderPicker.Default.PickAsync(
                CancellationToken.None);

        if (!result.IsSuccessful)
            return;

        string path =
            result.Folder.Path;

        CollectionSettings.CurrentCollectionPath =
            path;

        CollectionLabel.Text =
            path;

        await DisplayAlertAsync(
            "Success",
            "Collection loaded.",
            "OK");
    }

    private async void VideosClicked(object? sender, EventArgs e)
    {
        if (!ValidateCollection())
            return;

        TransitionOverlay.Opacity = 0;

        await TransitionOverlay.FadeToAsync(
            1,
            350,
            Easing.CubicIn);

        await Shell.Current.GoToAsync(nameof(VideoPage));

        TransitionOverlay.Opacity = 0;
    }

    private async void MusicClicked(object? sender, EventArgs e)
    {
        if (!ValidateCollection())
            return;

        await Shell.Current.GoToAsync(nameof(MusicPage));
    }

    private async void PlayerClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PlayerPage));
    }

    private bool ValidateCollection()
    {
        if (string.IsNullOrWhiteSpace(
                CollectionSettings.CurrentCollectionPath))
        {
            DisplayAlertAsync(
                "Collection Missing",
                "Select a collection first.",
                "OK");

            return false;
        }

        return true;
    }
}
