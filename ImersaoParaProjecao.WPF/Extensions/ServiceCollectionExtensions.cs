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
using IConfigurationManager = ImmersionToProjection.Service.Configuration.IConfigurationManager;
using ConfigurationManager = ImmersionToProjection.Service.Configuration.ConfigurationManager;
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
            .AddSingleton<IConfigurationManager>(s => new ConfigurationManager(configuration, configurationFilePath))
            .AddSingleton<ILanguageKeys, LanguageKeys>()
            .AddSingleton<IThemeManager, ThemeManager>()
            .AddSingleton<IImmersionWeekViewFactory, ImmersionWeekViewFactory>()
            .AddSingleton<ICaseStringFormat, CaseStringFormat>()
            //Transients
            .AddTransient<IRegexHelper>(_ => new RegexHelper(RegexPatternsFactory.CreateFromConfiguration(configuration)))
            .AddTransient<IFormatProvider>(_ => CultureInfo.CreateSpecificCulture(configuration.GetValue<string>("Language") ?? "pt-BR"))
            .AddTransient<IImmersionExtractor>(s =>
                new ImmersionExtractor(
                    s.GetRequiredService<IRegexHelper>(),
                    s.GetRequiredService<IFormatProvider>(),
                    configuration.GetValueValidating("MessageTitleFormat"),
                    s.GetRequiredService<ICaseStringFormat>(),
                    s.GetRequiredService<ILanguageKeys>()))
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
