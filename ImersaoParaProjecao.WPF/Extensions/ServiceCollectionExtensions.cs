using ImmersionToProjection.View;
using ImmersionToProjection.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using ImmersionToProjection.Service;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using ImmersionToProjection.Service.Extraction.Patterns;
using ImmersionToProjection.Service.Extraction;
using ImmersionToProjection.Service.Configuration;
using ImmersionToProjection.Service.DynamicResources;
using ImmersionToProjection.Service.Language;
using ImmersionToProjection.Service.ViewFactory;
using ImmersionToProjection.Service.Formatter;

namespace ImmersionToProjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, string configurationFilePath)
    {
        services
            //Services
            //Singletons
            .AddSingleton<IAppConfiguration>(s => new AppConfiguration(configurationFilePath))
            .AddSingleton<IPatternsFactory, PatternsFactory>()
            .AddSingleton<ILanguageKeys, LanguageKeys>()
            .AddSingleton<IThemeManager, ThemeManager>()
            .AddSingleton<IImmersionWeekViewFactory, ImmersionWeekViewFactory>()
            //Transients
            .AddTransient<ICaseStringFormat, CaseStringFormat>()
            .AddTransient<IPatternsHelper, PatternsHelper>()
            .AddTransient<IFormatProvider>(_ => CultureInfo.CreateSpecificCulture(configuration.GetValue<string>("Language") ?? "en-US"))
            .AddTransient<IImmersionExtractor, ImmersionExtractor>()
            //MainWindow
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton(s => new MainWindowView()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            })
            //ImmersionWeek
            .AddTransient<ImmersionWeekViewModel>()
            .AddTransient(s => new ImmersionWeekView()
            {
                DataContext = s.GetRequiredService<ImmersionWeekViewModel>()
            })
            //Configuration
            .AddSingleton<ConfigurationViewModel>()
            .AddSingleton(s => new ConfigurationView()
            {
                DataContext = s.GetRequiredService<ConfigurationViewModel>()
            })
            ;

        return services;
    }
}
