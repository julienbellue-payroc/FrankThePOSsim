using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using ControlzEx.Theming;
using FrankThePOSsim.observable;
using FrankThePOSsim.UserControls;
using Microsoft.Extensions.Options;

namespace FrankThePOSsim
{
    public partial class MainWindow
    {
        private bool _isDarkTheme;
        private Config _configuration;
        public MainWindow(IOptionsMonitor<Config> configurationMonitor)
        {
            _= new EyeBlinker(3000, 5000);
            configurationMonitor.OnChange(config =>
            {
                _configuration = config;
                Dispatcher.Invoke(() =>
                {
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
            _isDarkTheme = _configuration.DarkMode.HasValue && _configuration.DarkMode.Value;
            SetTheme();

            ComboBoxEnvironment.DisplayMemberPath = "Name";
            if (_configuration.Environments != null)
                ComboBoxEnvironment.ItemsSource = new EnvironmentObservable(_configuration.Environments);
            ComboBoxEnvironment.SelectedIndex = 0;

            ComboBoxTerminal.DisplayMemberPath = "SerialNumber";
            ComboBoxTerminal.ItemsSource = new TerminalObservable((Environment)ComboBoxEnvironment.SelectedItem);
            ComboBoxTerminal.SelectedIndex = 0;

            DataGridLogs.ItemsSource = App.LogTransaction;
        }


        private void comboBoxEnvironment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            ComboBoxTerminal.ItemsSource = new TerminalObservable((Environment)comboBox.SelectedItem);
            ComboBoxTerminal.SelectedIndex = 0;
        }
        private void comboBoxTerminal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            App.Terminal = (Terminal)comboBox.SelectedItem;
        }

        private void btnClearLogs_Click(object sender, RoutedEventArgs e)
        {
            App.LogTransaction.Clear();
        }
        private void SwitchTheme(object sender, RoutedEventArgs e)
        {
            _isDarkTheme = !_isDarkTheme;
            _configuration.DarkMode = _isDarkTheme;
            SetTheme();
        }

        private void SetTheme()
        {
            ThemeManager.Current.ChangeTheme(Application.Current, _isDarkTheme? "Dark.Blue":"Light.Blue");
            Resources["IconSource"] = _isDarkTheme ? "☀" : "🌙";
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
                var page = (ITransactionControl)content;

                transaction = page.GenerateTransaction();
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

                httpRequestMessage.RequestUri = new Uri($"https://api.econduitapps.uat.payroc.com/1.0/{uri}");
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
                var page = (ITransactionControl)content;
                transaction = page.GenerateTransaction();
                uri = $"{((Environment)ComboBoxEnvironment.SelectedItem).SoapUrl}/{page.GetUri()}?{transaction.ToQueryString()}";
            }
            httpRequestMessage.RequestUri = new Uri(uri);
            SendTransaction(httpRequestMessage, transaction, uri);
        }

        private async void SendTransaction(HttpRequestMessage message, Transaction? transaction, string? uri)
        {
            var logItem = message.RequestUri?.ToString();
            if (message.Content != null)
            {
                logItem = $"{logItem}\n{message.Content.ReadAsStringAsync().Result}";
            }
            var transactionLogItem = new TransactionLogItem
            {
                Request = logItem,
                Transaction = transaction,
                Endpoint = uri
            };
            App.LogTransaction.Add(transactionLogItem);
            var response = await App.HttpClient.SendAsync(message);

            transactionLogItem.Response = new Response(response);

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
    }
}