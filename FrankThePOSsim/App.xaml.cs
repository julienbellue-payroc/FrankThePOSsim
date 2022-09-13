﻿using System;
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

    public static Terminal Terminal = new();

    private IServiceProvider? ServiceProvider { get; set; }
    private IConfiguration? Configuration { get; set; }

    public const string ConfigFilePath = "appsettings.json";

    protected override void OnStartup(StartupEventArgs e)
    {
        if (!File.Exists(ConfigFilePath))
        {
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
            
        //Preload the resources
        Current.Resources["LeftEye"] = Current.Resources["ClosedLeftEye"];
        Current.Resources["LeftEye"] = Current.Resources["OpenLeftEye"];
            
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(typeof(MainWindow));
        services.Configure<Config>(Configuration?.GetSection(nameof(Config)));
    }
}
