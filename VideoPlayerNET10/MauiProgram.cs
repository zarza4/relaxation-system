using CommunityToolkit.Maui;

using VideoPlayerNET10.Pages;
using VideoPlayerNET10.Services;

namespace VideoPlayerNET10;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement(false)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont(
                    "HanyiWenHei-85W.ttf",
                    "HanyiWenHei");
            });


        builder.Services.AddSingleton<JsonService>();

        builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<VideoPage>();
        builder.Services.AddTransient<VideoDetailsPage>();

        builder.Services.AddTransient<MusicPage>();
        builder.Services.AddTransient<MusicDetailsPage>();
        
        builder.Services.AddTransient<PlayerPage>();

        return builder.Build();
    }
}