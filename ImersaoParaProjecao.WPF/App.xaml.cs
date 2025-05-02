using ImmersionToProjection.Extensions;
using ImmersionToProjection.Service.Configuration;
using ImmersionToProjection.Service.DynamicResources;
using ImmersionToProjection.Service.Language;
using ImmersionToProjection.View;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using IConfigurationManager = ImmersionToProjection.Service.Configuration.IConfigurationManager;

namespace ImmersionToProjection;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
#if DEBUG
        var configurationFilePath = "appsettings.json";
#else
        var configurationFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ImmersionToProjection", "appsettings.json");
#endif

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(configureDelegate => 
                configureDelegate.AddJsonFile(configurationFilePath, true))
            .ConfigureServices((hostContext, services) => 
                services.AddServices(hostContext.Configuration, configurationFilePath))
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        ApplyTheme();
        ApplyLanguage();
        
        var startupForm = AppHost.Services.GetRequiredService<MainWindowView>();
        startupForm.Show();
        startupForm.Closed += (_, _) => Shutdown();

        base.OnStartup(e);
    }

    private static void ApplyTheme()
    {
        var themeManager = AppHost!.Services.GetRequiredService<IThemeManager>();
        var theme = AppHost.Services.GetRequiredService<IConfiguration>().GetValue<string>("Theme");
        themeManager.ApplyTheme(theme);
    }

    private static void ApplyLanguage()
    {
        var configurationManager = AppHost!.Services.GetRequiredService<IConfigurationManager>();
        var language = configurationManager["Language"];
        if (string.IsNullOrEmpty(language))
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture.Name.Split('-').First();
            var languageKeys = AppHost!.Services.GetRequiredService<ILanguageKeys>();
            configurationManager["Language"] = languageKeys.AvailableLanguages.ContainsKey(currentCulture) ?
                                                currentCulture :
                                                languageKeys.DefaultLanguage;
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}