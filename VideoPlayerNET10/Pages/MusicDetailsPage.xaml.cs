using VideoPlayerNET10.Services;

namespace VideoPlayerNET10.Pages;

public partial class MusicDetailsPage : ContentPage
{
    public MusicDetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        TagsLayout.Children.Clear();

        var music =
            CollectionSettings.SelectedMusic;

        if (music == null)
            return;

        TitleLabel.Text =
            music.Title;

        ArtistLabel.Text =
            music.Artist;

        RatingLabel.Text =
            music.Stars;

        DescriptionLabel.Text =
            music.Description;

        foreach (string tag in music.Tags)
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
    }

    private async void UseMusicClicked(
        object? sender,
        EventArgs e)
    {
        await DisplayAlertAsync(
            "Music Selected",
            CollectionSettings.SelectedMusic?.Title ?? "",
            "OK");

        await Shell.Current.GoToAsync("..");
    }
}