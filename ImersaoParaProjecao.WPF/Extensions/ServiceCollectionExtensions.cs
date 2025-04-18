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

namespace ImmersionToProjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IRegexHelper>(_ => new RegexHelper(RegexPatternsFactory.CreateFromConfiguration(configuration)))
            .AddSingleton<IConfigurationUpdater>(_ = new ConfigurationUpdater("appsettings.json"))
            .AddSingleton<IFormatProvider>(CultureInfo.CreateSpecificCulture(configuration.GetValue<string>("Language") ?? "pt-BR"))
            .AddSingleton<IImmersionExtractor>(s =>
                new ImmersionExtractor(
                    s.GetRequiredService<IRegexHelper>(),
                    s.GetRequiredService<IFormatProvider>(),
                    configuration.GetValueValidating("MessageTitleFormat")))
            .AddSingleton<IThemeManager, ThemeManager>()
            //MainWindow
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton(s => new MainWindowView()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            })
            //ImmersionWeek
            .AddSingleton<ImmersionWeekViewModel>()
            .AddSingleton(s => new ImmersionWeekView()
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
