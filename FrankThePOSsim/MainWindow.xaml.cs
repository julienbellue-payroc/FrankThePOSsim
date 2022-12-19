using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ControlzEx.Theming;
using FrankThePOSsim.Helpers;
using FrankThePOSsim.observable;
using FrankThePOSsim.UserControls;
using Microsoft.Extensions.Options;

namespace FrankThePOSsim;

public partial class MainWindow
{
    private Config _configuration;
    public MainWindow(IOptionsMonitor<Config> configurationMonitor)
    {
        _= new Winker(3000, 5000);
        configurationMonitor.OnChange(config =>
        {
            _configuration = config;
            Dispatcher.Invoke(() =>
            {
                SetTheme();
                if (_configuration.Environments != null)
                    ComboBoxEnvironment.ItemsSource = new EnvironmentObservable(_configuration.Environments);
                ComboBoxEnvironment.SelectedIndex = 0;
                ComboBoxTerminal.ItemsSource =
                    new TerminalObservable((Environment)ComboBoxEnvironment.SelectedItem);
                ComboBoxTerminal.SelectedIndex = 0;
            });
        });
        _configuration = configurationMonitor.CurrentValue;

        InitializeComponent();
        SetTheme();


        Title = "Frank the POSsim 1.3.2";
        ComboBoxEnvironment.DisplayMemberPath = "Name";
        if (_configuration.Environments != null)
            ComboBoxEnvironment.ItemsSource = new EnvironmentObservable(_configuration.Environments);
        ComboBoxEnvironment.SelectedIndex = 0;
 
        ComboBoxTerminal.DisplayMemberPath = "SerialNumber";
        ComboBoxTerminal.ItemsSource = new TerminalObservable((Environment)ComboBoxEnvironment.SelectedItem);
        ComboBoxTerminal.SelectedIndex = 0;

        DataGridLogs.ItemsSource = App.LogTransaction;
    }

    private void UpdateSetting()
    {
        ConfigSaverHelper.SaveToFile(_configuration);
    }
        
    private void comboBoxEnvironment_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        ComboBoxTerminal.ItemsSource = new TerminalObservable((Environment)comboBox.SelectedItem);
        ComboBoxTerminal.SelectedIndex = 0;
    }

    private void btnClearLogs_Click(object sender, RoutedEventArgs e)
    {
        App.LogTransaction.Clear();
    }
    private void SwitchTheme(object sender, RoutedEventArgs e)
    {
        _configuration.DarkMode = _configuration.DarkMode.HasValue && !_configuration.DarkMode.Value;
        UpdateSetting();
        SetTheme();
    }

    private void SetTheme()
    {
        var isDarkTheme = _configuration.DarkMode.HasValue && _configuration.DarkMode.Value;
        ThemeManager.Current.ChangeTheme(Application.Current, isDarkTheme ? "Dark.Blue":"Light.Blue");
        Resources["IconSource"] = isDarkTheme ? "☀" : "🌙";
        Resources["AlternateRowBackground"] = new SolidColorBrush(isDarkTheme ? Color.FromArgb(255,0x45,0x45,0x45) : Color.FromArgb(255,0xe0,0xe0,0xe0) );
        Resources["ForegroundColour"] = new SolidColorBrush(isDarkTheme ? Colors.White : Colors.Black);
    }

    private static void ShowTerminalEmptyErrorMessage()
    {
        MessageBox.Show("No valid terminal selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void BtnRESTSend_Click(object sender, RoutedEventArgs e)
    {
        var content = ((TabItem)TabControlMain.SelectedItem).Content;
        var httpRequestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Post
        };
        string? uri;
        Transaction? transaction = null;
        if ((UserControl)content is CustomTransaction customTransaction)
        {
            uri = customTransaction.TextBoxEndpoint.Text;
            httpRequestMessage.RequestUri = new Uri(uri);
            httpRequestMessage.Content = new StringContent(
                customTransaction.TextBoxBody.Text,
                Encoding.UTF8,
                "application/json");
        }
        else
        {
            var terminal = (Terminal)ComboBoxTerminal.SelectedItem;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (terminal is null)
            {
                ShowTerminalEmptyErrorMessage();
                return;
            }
            var page = (ITransactionControl)content;

            transaction = page.GenerateTransaction(terminal);
            uri = page.GetUri();

            HttpContent httpContent = new StringContent(
                JsonSerializer.Serialize(
                    transaction,
                    new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    }
                ),
                Encoding.UTF8,
                "application/json"
            );

            httpRequestMessage.RequestUri = new Uri($"{((Environment)ComboBoxEnvironment.SelectedItem).RestUrl}/1.0/{uri}");
            httpRequestMessage.Content = httpContent;
        }
        SendTransaction(httpRequestMessage, transaction, uri);
    }

    private void BtnSOAPSend_Click(object sender, RoutedEventArgs e)
    {
        var content = ((TabItem)TabControlMain.SelectedItem).Content;
        var httpRequestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Get
        };
        string? uri;
        Transaction? transaction = null;
        if ((UserControl)content is CustomTransaction customTransaction)
        {
            uri = customTransaction.TextBoxEndpoint.Text;
            if (!string.IsNullOrEmpty(customTransaction.TextBoxBody.Text))
            {
                httpRequestMessage.Content = new StringContent(
                    customTransaction.TextBoxBody.Text,
                    Encoding.UTF8,
                    "application/json");
            }
        }
        else
        {
            var terminal = (Terminal)ComboBoxTerminal.SelectedItem;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (terminal is null)
            {
                ShowTerminalEmptyErrorMessage();
                return;
            }
            var page = (ITransactionControl)content;
            transaction = page.GenerateTransaction(terminal);
            uri = $"{((Environment)ComboBoxEnvironment.SelectedItem).SoapUrl}/{page.GetUri()}?{transaction.ToQueryString()}";
        }
        httpRequestMessage.RequestUri = new Uri(uri);
        SendTransaction(httpRequestMessage, transaction, uri);
    }

    private async void SendTransaction(HttpRequestMessage message, Transaction? transaction, string? uri)
    {
        var now = DateTime.Now;
        var transactionLogItem = new TransactionLogItem
        {
            Timestamp = now.ToString(CultureInfo.InvariantCulture),
            Payload = message.Content != null ? message.Content.ReadAsStringAsync().Result : string.Empty,
            Url = message.RequestUri?.ToString(),
            Transaction = transaction,
            Endpoint = uri
        };
        App.LogTransaction.Add(transactionLogItem);
        DataGridLogs.ScrollIntoView(transactionLogItem);

        try
        {
            var response = await App.HttpClient.SendAsync(message);
            transactionLogItem.Response = new TransactionResponse(response, now);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            var errorMessage = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            errorMessage.ReasonPhrase = "Request took too long and Frank got bored of waiting";
            transactionLogItem.Response = new TransactionResponse(errorMessage, now);
        }
        catch (HttpRequestException e)
        {
            var errorMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
            errorMessage.ReasonPhrase = "Error - See full message";
            transactionLogItem.Response = new TransactionResponse(errorMessage, now, e.Message);
        }


        DataGridLogs.Items.Refresh();
    }

    private void BtnReuseTransaction_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var transactionLogItem = (TransactionLogItem)button.DataContext;
            
        var page = (ITransactionControl)((TabItem)TabControlMain.SelectedItem).Content;
        if (transactionLogItem.Transaction == null) return;

        page.SetControlsFromTransaction(transactionLogItem.Transaction);
        page.SetUri(transactionLogItem.Endpoint);
    }

    private void ButtonOpenConfig_OnClick(object sender, RoutedEventArgs e)
    {
        using var fileOpener = new Process();

        var appPath = App.FullConfigPath;
        fileOpener.StartInfo.FileName = "explorer";
        fileOpener.StartInfo.Arguments = $"\"{appPath}\"";
        fileOpener.Start();
    }
}
