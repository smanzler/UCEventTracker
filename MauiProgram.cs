using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UCEventTracker.ViewModels;

namespace UCEventTracker
{
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

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<NewEventPage>();
            builder.Services.AddTransient<NewEventViewModel>(sp =>
            {
                var mainViewModel = sp.GetRequiredService<MainPageViewModel>();
                return new NewEventViewModel(mainViewModel);
            });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
