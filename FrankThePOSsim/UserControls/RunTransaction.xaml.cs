using System.Windows;
using System.Windows.Controls;
using FrankThePOSsim.Helpers;

namespace FrankThePOSsim.UserControls;

public partial class RunTransaction: ITransactionControl
{
    public RunTransaction()
    {
        InitializeComponent();
        CheckBoxCommandControl.SelectedIndex = 0;
    }
        
    private void ComboBoxCommand_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Ensure that the sender is a CheckBoxCommand
        if (sender is not CheckBoxCommand checkBoxCommand) return;

        // Find the ComboBox inside the CheckBoxCommand control
        var comboBox = checkBoxCommand.FindName("ThisComboBox") as ComboBox;

        // Ensure ComboBox is found and SelectedItem is valid
        if (comboBox?.SelectedItem is not string selectedItem) return;
        
        // Check if the selected item starts with "gift" (case-insensitive)
        var isGift = selectedItem.StartsWith("gift", System.StringComparison.CurrentCultureIgnoreCase);

        // Update CheckBox states based on the selection
        CheckBoxTextBoxControlExpDate.IsChecked = !isGift;
        CheckBoxTextBoxControlInvoiceNumber.IsChecked = !isGift;
        CheckBoxTextBoxControlMerchantId.IsChecked = !isGift;
        CheckBoxTextBoxControlPaymentType.IsChecked = !isGift;

        CheckBoxCommandControl.IsChecked = true;
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
        if (CheckBoxCommandControl.IsChecked)
            transaction.Command = CheckBoxCommandControl.SelectedValue;

        if (ApiKeyPasswordTerminalIdCheckboxesControl.IsApiKeyChecked && terminal.ApiKey != null)
            transaction.Key = terminal.ApiKey;
        if (ApiKeyPasswordTerminalIdCheckboxesControl.IsApiPasswordChecked && terminal.ApiPassword != null)
            transaction.Password = terminal.ApiPassword;
        if (CheckBoxTextBoxControlAmount.IsChecked)
            transaction.Amount = CheckBoxTextBoxControlAmount.TextValue;

        if (ApiKeyPasswordTerminalIdCheckboxesControl.IsTerminalIdChecked)
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
        ApiKeyPasswordTerminalIdCheckboxesControl.IsApiKeyChecked = transaction.Key != null;
        ApiKeyPasswordTerminalIdCheckboxesControl.IsApiPasswordChecked = transaction.Password != null;
        ApiKeyPasswordTerminalIdCheckboxesControl.IsTerminalIdChecked = transaction.TerminalId != null;

        CheckBoxCommandControl.IsChecked = transaction.Command != null;
        if (transaction.Command != null)
            CheckBoxCommandControl.SelectedValue = transaction.Command;

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
