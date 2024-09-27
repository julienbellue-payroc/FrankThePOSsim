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
        CheckBoxTextBoxControlExpDate.IsChecked = !isGift;
        CheckBoxTextBoxControlInvoiceNumber.IsChecked = !isGift;
        CheckBoxTextBoxControlMerchantId.IsChecked = !isGift;
        CheckBoxTextBoxControlPaymentType.IsChecked = !isGift;

        CheckBoxCommand.IsChecked = true;
        CheckBoxTextBoxControlAmount.IsChecked = true;
        CheckBoxTextBoxControlRefId.IsChecked = true;
    }

    public string GetUri()
    {
        return "runTransaction";
    }

    public void SetUri(Environment environment)
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
        if (CheckBoxTextBoxControlAmount.IsChecked)
            transaction.Amount = CheckBoxTextBoxControlAmount.TextValue;

        if (CheckBoxTerminalId.IsChecked == true)
            transaction.TerminalId = terminal.Id.ToString();

        if (CheckBoxTextBoxControlRefId.IsChecked)
            transaction.RefId = CheckBoxTextBoxControlRefId.TextValue;
        if (CheckBoxTextBoxControlExpDate.IsChecked)
            transaction.ExpDate = CheckBoxTextBoxControlExpDate.TextValue;
        if (CheckBoxTextBoxControlMerchantId.IsChecked)
            transaction.MerchantId = CheckBoxTextBoxControlMerchantId.TextValue;
        if (CheckBoxTextBoxControlInvoiceNumber.IsChecked)
            transaction.InvoiceNumber = CheckBoxTextBoxControlInvoiceNumber.TextValue;
            
        if (CheckBoxTextBoxControlPaymentType.IsChecked)
            transaction.PaymentType = CheckBoxTextBoxControlPaymentType.TextValue;
            
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

        CheckBoxTextBoxControlRefId.IsChecked = transaction.RefId != null;
        if(transaction.RefId != null)
            CheckBoxTextBoxControlRefId.TextValue = transaction.RefId;
        CheckBoxTextBoxControlMerchantId.IsChecked = transaction.MerchantId != null; 
        if(transaction.MerchantId != null)
            CheckBoxTextBoxControlMerchantId.TextValue = transaction.MerchantId;
        CheckBoxTextBoxControlPaymentType.IsChecked = transaction.PaymentType != null; 
        if(transaction.PaymentType != null)
            CheckBoxTextBoxControlPaymentType.TextValue = transaction.PaymentType;
        CheckBoxTextBoxControlAmount.IsChecked = transaction.Amount != null; 
        if(transaction.Amount != null)
            CheckBoxTextBoxControlAmount.TextValue = transaction.Amount;
        CheckBoxTextBoxControlInvoiceNumber.IsChecked = transaction.InvoiceNumber != null; 
        if(transaction.InvoiceNumber != null)
            CheckBoxTextBoxControlInvoiceNumber.TextValue = transaction.InvoiceNumber;
        CheckBoxTextBoxControlExpDate.IsChecked = transaction.ExpDate != null; 
        if(transaction.ExpDate != null)
            CheckBoxTextBoxControlExpDate.TextValue = transaction.ExpDate;
    }
    private void BtnGenerateRefId_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlRefId.TextValue = GenerateFieldValueHelper.GenerateRefId();
    }
    private void BtnGenerateAmount_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlAmount.TextValue = GenerateFieldValueHelper.GenerateAmount();
    }
    private void BtnGenerateExpDate_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlExpDate.TextValue = GenerateFieldValueHelper.GenerateDate();
    }

    private void BtnChangeRefIdRefId_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlRefId.TextValue = GenerateFieldValueHelper.ChangeRefIdCase(CheckBoxTextBoxControlRefId.TextValue);
    }
}
