using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsum.observable;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Typography.TextLayout;

namespace FrankThePOSsum.UserControls
{
    public partial class RunTransaction2: IGenerateRequests
    {
        public RunTransaction2()
        {
            InitializeComponent();

            ComboBoxCommand.ItemsSource = new CommandObservable(Commands.GetCommands);
            ComboBoxCommand.SelectedIndex = 0;
        }
        
        private void ComboBoxCommand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var isGift = ((string)comboBox.SelectedItem).ToLower().StartsWith("gift");
            CheckBoxToken.IsChecked = !isGift;
            CheckBoxExpDate.IsChecked = !isGift;
            CheckBoxInvoiceNumber.IsChecked = !isGift;
            CheckBoxMerchantId.IsChecked = !isGift;
            CheckBoxPaymentType.IsChecked = !isGift;

            CheckBoxCommand.IsChecked = true;
            CheckBoxAmount.IsChecked = true;
            CheckBoxRefId.IsChecked = true;
        }
        private string GenerateQueryString()
        {
            return string.Join("&", GenerateBody().Select(
                x => $"{x.Key}={x.Value}")
            );
        }
        private Transaction GenerateBody2()
        {
            Transaction transaction = new();
            if (CheckBoxCommand.IsChecked == true)
                transaction.Command = (string)ComboBoxCommand.SelectedValue;

            if (CheckBoxApiKey.IsChecked == true)
                transaction.Key = App.Terminal.ApiKey;
            if (CheckBoxApiPassword.IsChecked == true)
                transaction.Password = App.Terminal.ApiPassword;
            if (CheckBoxAmount.IsChecked == true)
                transaction.Amount = TextBoxAmount.Text;

            if (CheckBoxTerminalId.IsChecked == true)
                transaction.TerminalId = App.Terminal.Id.ToString();

            if (CheckBoxRefId.IsChecked == true)
                transaction.RefId = TextBoxRefId.Text;
            if (CheckBoxToken.IsChecked == true)
                transaction.Token = TextBoxToken.Text;
            if (CheckBoxExpDate.IsChecked == true)
                transaction.ExpDate = TextBoxExpDate.Text;
            if (CheckBoxMerchantId.IsChecked == true)
                transaction.MerchantId = TextBoxMerchantId.Text;
            if (CheckBoxInvoiceNumber.IsChecked == true)
                transaction.InvoiceNumber = TextBoxInvoiceNumber.Text;
            
            if (CheckBoxPaymentType.IsChecked == true)
                transaction.PaymentType = TextBoxPaymentType.Text;
            
            return transaction;
        }
        private ICollection<KeyValuePair<string, string>> GenerateBody()
        {
            Transaction transaction = new();
            var body = (ICollection<KeyValuePair<string, string>>)new List<KeyValuePair<string, string>>();
            if (CheckBoxCommand.IsChecked == true)
                transaction.Command = (string)ComboBoxCommand.SelectedValue;
                // body.Add(new KeyValuePair<string, string>("Command", (string)ComboBoxCommand.SelectedValue));

            if (CheckBoxApiKey.IsChecked == true)
                transaction.Key = App.Terminal.ApiKey;
                // body.Add(new KeyValuePair<string, string>("Key", App.Terminal.ApiKey));
            if (CheckBoxApiPassword.IsChecked == true)
                transaction.Password = App.Terminal.ApiPassword;
                // body.Add(new KeyValuePair<string, string>("Password", App.Terminal.ApiPassword));
            if (CheckBoxAmount.IsChecked == true)
                transaction.Amount = TextBoxAmount.Text;
                // body.Add(new KeyValuePair<string, string>("amount", TextBoxAmount.Text));

            if (CheckBoxTerminalId.IsChecked == true)
                transaction.TerminalId = App.Terminal.Id.ToString();
                // body.Add(new KeyValuePair<string, string>("terminalId", App.Terminal.Id.ToString()));

            if (CheckBoxRefId.IsChecked == true)
                transaction.RefId = TextBoxRefId.Text;
                // body.Add(new KeyValuePair<string, string>("refId", TextBoxRefId.Text));
            if (CheckBoxToken.IsChecked == true)
                transaction.Token = TextBoxToken.Text;
                // body.Add(new KeyValuePair<string, string>("token", TextBoxToken.Text));
            if (CheckBoxExpDate.IsChecked == true)
                transaction.ExpDate = TextBoxExpDate.Text;
                // body.Add(new KeyValuePair<string, string>("expDate", TextBoxExpDate.Text));
            if (CheckBoxMerchantId.IsChecked == true)
                transaction.MerchantId = TextBoxMerchantId.Text;
                // body.Add(new KeyValuePair<string, string>("merchantId", TextBoxMerchantId.Text));
            if (CheckBoxInvoiceNumber.IsChecked == true)
                transaction.InvoiceNumber = TextBoxInvoiceNumber.Text;
                // body.Add(new KeyValuePair<string, string>("invoiceNumber", TextBoxInvoiceNumber.Text));
            
            if (CheckBoxPaymentType.IsChecked == true)
                transaction.PaymentType = TextBoxPaymentType.Text;
                // body.Add(new KeyValuePair<string, string>("paymentType", TextBoxPaymentType.Text));
            
            return body;
        }
        
        private string GenerateRestBody()
        {
            var dictionary = new Dictionary<string, string>(GenerateBody().ToList());
            return JsonConvert.SerializeObject(dictionary);
        }
        
        private void BtnGenerateRefId_Click(object sender, RoutedEventArgs e)
        {
            TextBoxRefId.Text = Guid.NewGuid().ToString();
        }
        private void BtnGenerateAmount_Click(object sender, RoutedEventArgs e)
        {
            TextBoxAmount.Text = "1";
        }
        private void BtnGenerateExpDate_Click(object sender, RoutedEventArgs e)
        {
            TextBoxExpDate.Text = DateTime.Now.ToString("MMddyyyy");
        }

        public string GenerateSoapRequest()
        {
            return $"{App.Environment.SoapUrl}/RunTransaction2?{GenerateQueryString()}";
        }

        public StringContent GenerateRestRequest()
        {
            var restBody = GenerateRestBody();
            return new StringContent(restBody, Encoding.UTF8, "application/json");
        }
        public Transaction GenerateRestRequestBody()
        {
            return GenerateBody2();
        }
    }
}
