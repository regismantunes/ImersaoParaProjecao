using ImmersionToProjection.Helper.Interfaces;
using ImmersionToProjection.Helper;
using ImmersionToProjection.View;
using ImmersionToProjection.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using ImmersionToProjection.Service;
using ImmersionToProjection.Service.Interfaces;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace ImmersionToProjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IRegexHelper>(_ => new RegexHelper(RegexHelperPatternsFactory.CreateFromConfiguration(configuration)))
            .AddSingleton<IFormatProvider>(CultureInfo.CreateSpecificCulture(configuration.GetValue<string>("Language") ?? "pt-BR"))
            .AddSingleton<IImmersionExtractor>(s =>
                new ImmersionExtractor(
                    s.GetRequiredService<IRegexHelper>(),
                    s.GetRequiredService<IFormatProvider>(),
                    configuration.GetValueValidating("MessageTitleFormat")))
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
            ;

        return services;
    }
}
