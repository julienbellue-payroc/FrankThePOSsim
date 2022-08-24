using System;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsum.observable;

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
        private Transaction GenerateBody()
        {
            Transaction transaction = new();
            if (CheckBoxCommand.IsChecked == true)
                transaction.Command = (string)ComboBoxCommand.SelectedValue;

            if (CheckBoxApiKey.IsChecked == true && App.Terminal.ApiKey != null)
                transaction.Key = App.Terminal.ApiKey;
            if (CheckBoxApiPassword.IsChecked == true && App.Terminal.ApiPassword != null)
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
            return $"{App.Environment.SoapUrl}/runTransaction2?{GenerateBody().ToQueryString()}";
        }
        public Transaction GenerateRestRequestBody()
        {
            return GenerateBody();
        }
    }
}
