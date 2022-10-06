using System.Windows;
using System.Windows.Controls;
using FrankThePOSsim.Helpers;
using FrankThePOSsim.observable;

namespace FrankThePOSsim.UserControls;

public partial class RunTransaction: ITransactionControl
{
    public RunTransaction()
    {
        InitializeComponent();

        ComboBoxCommand.ItemsSource = new CommandObservable(Commands.GetCommands);
        ComboBoxCommand.SelectedIndex = 0;
    }
        
    private void ComboBoxCommand_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var isGift = ((string)comboBox.SelectedItem).ToLower().StartsWith("gift");
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
        return "runTransaction";
    }

    public void SetUri(string? uri)
    {
        // safely ignore there
    }
    public Transaction GenerateTransaction(Terminal terminal)
    {
        Transaction transaction = new();
        if (CheckBoxCommand.IsChecked == true)
            transaction.Command = (string)ComboBoxCommand.SelectedValue;

        if (CheckBoxApiKey.IsChecked == true && terminal.ApiKey != null)
            transaction.Key = terminal.ApiKey;
        if (CheckBoxApiPassword.IsChecked == true && terminal.ApiPassword != null)
            transaction.Password = terminal.ApiPassword;
        if (CheckBoxAmount.IsChecked == true)
            transaction.Amount = TextBoxAmount.Text;

        if (CheckBoxTerminalId.IsChecked == true)
            transaction.TerminalId = terminal.Id.ToString();

        if (CheckBoxRefId.IsChecked == true)
            transaction.RefId = TextBoxRefId.Text;
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
        CheckBoxExpDate.IsChecked = transaction.ExpDate != null; 
        if(transaction.ExpDate != null)
            TextBoxExpDate.Text = transaction.ExpDate;
    }
    private void BtnGenerateRefId_Click(object sender, RoutedEventArgs e)
    {
        TextBoxRefId.Text = GenerateFieldValueHelper.GenerateRefId();
    }
    private void BtnGenerateAmount_Click(object sender, RoutedEventArgs e)
    {
        TextBoxAmount.Text = GenerateFieldValueHelper.GenerateAmount();
    }
    private void BtnGenerateExpDate_Click(object sender, RoutedEventArgs e)
    {
        TextBoxExpDate.Text = GenerateFieldValueHelper.GenerateDate();
    }
}