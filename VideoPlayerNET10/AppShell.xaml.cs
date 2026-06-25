using VideoPlayerNET10.Pages;

namespace VideoPlayerNET10;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            nameof(VideoPage),
            typeof(VideoPage));

        Routing.RegisterRoute(
            nameof(VideoDetailsPage),
            typeof(VideoDetailsPage));

        Routing.RegisterRoute(
            nameof(MusicPage),
            typeof(MusicPage));

        Routing.RegisterRoute(
            nameof(MusicDetailsPage),
            typeof(MusicDetailsPage));

        Routing.RegisterRoute(
            nameof(PlayerPage),
            typeof(PlayerPage));
    }
}