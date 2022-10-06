using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using FrankThePOSsim.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrankThePOSsim;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    public static readonly HttpClient HttpClient = new();
    public static readonly ObservableCollection<TransactionLogItem> LogTransaction = new();

    private IServiceProvider? ServiceProvider { get; set; }
    private IConfiguration? Configuration { get; set; }

    public const string ConfigFilePath = "appsettings.json";

    protected override void OnStartup(StartupEventArgs e)
    {
        CreateDefaultConfigFileIfNeeded();

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(ConfigFilePath, optional: false, reloadOnChange: true);

        try
        {
            Configuration = builder.Build();
        }
        catch (InvalidDataException ex)
        {
            MessageBox.Show($"Invalid configuration file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Current.Shutdown();
        }

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        PreloadResources();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private static void PreloadResources()
    {
        Current.Resources["LeftEye"] = Current.Resources["ClosedLeftEye"];
        Current.Resources["LeftEye"] = Current.Resources["OpenLeftEye"];
    }

    private static void CreateDefaultConfigFileIfNeeded()
    {
        if (File.Exists(ConfigFilePath)) return;
        if (MessageBox.Show(
                @$"Configuration file {ConfigFilePath} not found.
Create a default one?
(Frank won't run if you press no)",
                "Error", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
        {
            Current.Shutdown();
        }
        else
        {
            ConfigSaverHelper.SaveToFile(new Config().SetDefault());
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(typeof(MainWindow));
        services.Configure<Config>(Configuration?.GetSection(nameof(Config)));
    }
}
