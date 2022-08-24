﻿using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using ControlzEx.Theming;
using Microsoft.Extensions.Options;
using FrankThePOSsum.observable;

namespace FrankThePOSsum
{
    public partial class MainWindow
    {
        private bool _isDarkTheme;
        private readonly Config _configuration;

        public MainWindow(IOptions<Config> config)
        {
            _configuration = config.Value;

            InitializeComponent();
            _isDarkTheme = _configuration.DarkMode.HasValue && _configuration.DarkMode.Value;
            ThemeManager.Current.ChangeTheme(Application.Current, _isDarkTheme? "Dark.Blue":"Light.Blue");
            
            ComboBoxEnvironment.DisplayMemberPath = "Name";
            if (_configuration.Environments != null)
                ComboBoxEnvironment.ItemsSource = new EnvironmentObservable(_configuration.Environments);
            ComboBoxEnvironment.SelectedIndex = 0;

            ComboBoxTerminal.DisplayMemberPath = "SerialNumber";
            ComboBoxTerminal.ItemsSource = new TerminalObservable((Environment)ComboBoxEnvironment.SelectedItem);
            ComboBoxTerminal.SelectedIndex = 0;

            ListViewLogs.ItemsSource = App.LogTransaction;
        }

        private void comboBoxEnvironment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            App.Environment = (Environment)comboBox.SelectedItem;
            ComboBoxTerminal.ItemsSource = new TerminalObservable(App.Environment);
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
            ThemeManager.Current.ChangeTheme(Application.Current, _isDarkTheme? "Dark.Blue":"Light.Blue");
            _configuration.DarkMode = _isDarkTheme;
        }

        private void BtnRESTSend_Click(object sender, RoutedEventArgs e)
        {
            var page = (IGenerateRequests)((TabItem)TabControlMain.SelectedItem).Content;

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            var body = JsonSerializer.Serialize(page.GenerateRestRequestBody(), jsonOptions);
            HttpContent httpContent = new StringContent(body, Encoding.UTF8, "application/json"); 
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.econduitapps.uat.payroc.com/1.0/runTransaction"),
                Content = httpContent
            };
            SendTransaction(httpRequestMessage);
        }

        private void BtnSOAPSend_Click(object sender, RoutedEventArgs e)
        {
            var page = (IGenerateRequests)((TabItem)TabControlMain.SelectedItem).Content;
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(page.GenerateSoapRequest())
            };
            SendTransaction(httpRequestMessage);
        }

        private async void SendTransaction(HttpRequestMessage message)
        {
            var logItem = message.RequestUri?.ToString();
            if (message.Content != null)
            {
                logItem = $"{logItem}\n{message.Content.ReadAsStringAsync().Result}";
            }
            var transaction = new TransactionLogItem
            {
                Request = logItem
            };
            App.LogTransaction.Add(transaction);
            var response = await App.HttpClient.SendAsync(message);

            transaction.HttpStatusCode = (int)response.StatusCode;

            var responseBody = response.Content.ReadAsStringAsync().Result;
            transaction.Response = response.IsSuccessStatusCode ?
                responseBody :
                $"{response.ReasonPhrase}\n{responseBody}";

            ListViewLogs.Items.Refresh();
        }
    }
}