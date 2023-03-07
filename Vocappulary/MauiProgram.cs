using Microsoft.Extensions.Logging;
using Vocappulary.Persistence.Services;

namespace Vocappulary;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "vocapp.db3");
        builder.Services.AddSingleton<LearnItemRepository>(s => ActivatorUtilities.CreateInstance<LearnItemRepository>(s, dbPath));


        return builder.Build();
    }
}

