using ImersaoParaProjecao.Extensions;
using ImersaoParaProjecao.View;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace ImersaoParaProjecao;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(configureDelegate =>
            {
                configureDelegate.AddJsonFile("appsettings.json", true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddServices(hostContext.Configuration);
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startupForm = AppHost.Services.GetRequiredService<MainWindowView>();
        startupForm.Show();
        startupForm.Closed += (_, _) => Shutdown();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}