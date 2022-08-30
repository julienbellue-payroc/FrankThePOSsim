using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrankThePOSsim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static readonly HttpClient HttpClient = new();
        public static readonly ObservableCollection<TransactionLogItem> LogTransaction = new();

        public static Environment Environment = new();
        public static Terminal Terminal = new();

        private IServiceProvider? ServiceProvider { get; set; }
        private IConfiguration? Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            //Preload the resources
            Current.Resources["LeftEye"] = Current.Resources["BlinkedLeftEye"];
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
}
