using EFCoreExampleForLnL.Data;
using EFCoreExampleForLnL.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreExampleForLnL;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register EF Core with InMemory provider
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("EFCoreExampleDb"));

        // Register ViewModels
        builder.Services.AddSingleton<MainPageViewModel>();

        // Register Pages
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
