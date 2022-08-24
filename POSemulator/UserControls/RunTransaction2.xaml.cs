using System;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsum.observable;

namespace FrankThePOSsum.UserControls
{
    public partial class RunTransaction2: ITransactionControl
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

        public string GetUri()
        {
            //TODO get runTransaction2 for SOAP and runTransaction for REST
            return "runTransaction";
        }

        public void SetUri(string? uri)
        {
            // safely ignore there
        }
        public Transaction GenerateTransaction()
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

        public void SetControlsFromTransaction(Transaction transaction)
        {
            CheckBoxApiKey.IsChecked = transaction.Key != null;
            CheckBoxApiPassword.IsChecked = transaction.Password != null;
            CheckBoxTerminalId.IsChecked = transaction.TerminalId != null;

            CheckBoxCommand.IsChecked = transaction.Command != null;
            if (transaction.Command != null)
                ComboBoxCommand.SelectedValue = transaction.Command;

            CheckBoxRefId.IsChecked = transaction.RefId != null;
            if(transaction.RefId != null)
                TextBoxRefId.Text = transaction.RefId;
            CheckBoxMerchantId.IsChecked = transaction.MerchantId != null; 
            if(transaction.MerchantId != null)
                TextBoxMerchantId.Text = transaction.MerchantId;
            CheckBoxPaymentType.IsChecked = transaction.PaymentType != null; 
            if(transaction.PaymentType != null)
                TextBoxPaymentType.Text = transaction.PaymentType;
            CheckBoxAmount.IsChecked = transaction.Amount != null; 
            if(transaction.Amount != null)
                TextBoxAmount.Text = transaction.Amount;
            CheckBoxInvoiceNumber.IsChecked = transaction.InvoiceNumber != null; 
            if(transaction.InvoiceNumber != null)
                TextBoxInvoiceNumber.Text = transaction.InvoiceNumber;
            CheckBoxToken.IsChecked = transaction.Token != null; 
            if(transaction.Token != null)
                TextBoxToken.Text = transaction.Token;
            CheckBoxExpDate.IsChecked = transaction.ExpDate != null; 
            if(transaction.ExpDate != null)
                TextBoxExpDate.Text = transaction.ExpDate;
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
    }
}
