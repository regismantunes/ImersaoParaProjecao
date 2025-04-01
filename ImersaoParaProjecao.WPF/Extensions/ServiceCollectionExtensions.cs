using ImersaoParaProjecao.Helper.Interfaces;
using ImersaoParaProjecao.Helper;
using ImersaoParaProjecao.View;
using ImersaoParaProjecao.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using ImersaoParaProjecao.Service;
using ImersaoParaProjecao.Service.Interfaces;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace ImersaoParaProjecao.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IRegexHelper>(_ => new RegexHelper(RegexHelperPatternsFactory.CreateFromConfiguration(configuration)))
            .AddSingleton<IFormatProvider>(CultureInfo.CreateSpecificCulture(configuration.GetValue<string>("Language") ?? "pt-BR"))
            .AddSingleton<IImmersionExtractor, ImmersionExtractor>()
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
