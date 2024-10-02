using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsim.observable;
using FrankThePOSsim.Helpers;
using FrankThePOSsim.Models;
using Environment = FrankThePOSsim.Models.Environment;

namespace FrankThePOSsim.UserControls;

public partial class FullRequest: ITransactionControl
{
    public FullRequest()
    {
        InitializeComponent();

        ComboBoxEndpoints.DisplayMemberPath = "Uri";
        ComboBoxEndpoints.ItemsSource = new EndPointObservable(new Endpoints());
        ComboBoxEndpoints.SelectedIndex = 0;
    }

    private void SwitchAllCheckboxesOff()
    {
        ApiKeyPasswordTerminalIdCheckboxesControl.IsApiPasswordChecked = false;
        ApiKeyPasswordTerminalIdCheckboxesControl.IsApiKeyChecked = false;
        ApiKeyPasswordTerminalIdCheckboxesControl.IsTerminalIdChecked = false;
        CheckBoxCommandControl.IsChecked = false;
        CheckBoxTextBoxControlRefId.IsChecked = false;
        CheckBoxTextBoxControlDate.IsChecked = false;
        CheckBoxTextBoxControlMerchantId.IsChecked = false;
        CheckBoxTextBoxControlPaymentType.IsChecked = false;
        CheckBoxTextBoxControlPrompt.IsChecked = false;
        CheckBoxTextBoxControlCountry.IsChecked = false;
        CheckBoxTextBoxControlBusinessName.IsChecked = false;
        CheckBoxTextBoxControlContactName.IsChecked = false;
        CheckBoxTextBoxControlAddress.IsChecked = false;
        CheckBoxTextBoxControlCity.IsChecked = false;
        CheckBoxTextBoxControlState.IsChecked = false;
        CheckBoxTextBoxControlZipCode.IsChecked = false;
        CheckBoxTextBoxControlEmail.IsChecked = false;
        CheckBoxTextBoxControlPhone.IsChecked = false;
        CheckBoxTextBoxControlResellerName.IsChecked = false;
        CheckBoxTextBoxControlReferenceId.IsChecked = false;
        CheckBoxTextBoxControlTerminalSerialNumber.IsChecked = false;
        CheckBoxTextBoxControlAmount.IsChecked = false;
        CheckBoxTextBoxControlInvoiceNumber.IsChecked = false;
        CheckBoxTextBoxControlToken.IsChecked = false;
        CheckBoxTextBoxControlExpDate.IsChecked = false;
        CheckBoxTextBoxControlType.IsChecked = false;
        CheckBoxTextBoxControlData.IsChecked = false;
        CheckBoxTextBoxControlUrl.IsChecked = false;
        CheckBoxTextBoxControlIsDefault.IsChecked = false;
        CheckBoxTextBoxControlOptionName.IsChecked = false;
        CheckBoxTextBoxControlOptionValue.IsChecked = false;
        CheckBoxTextBoxControlTitle.IsChecked = false;
        CheckBoxTextBoxControlMaxLength.IsChecked = false;
        CheckBoxTextBoxControlOptions.IsChecked = false;
    }
    private void SelectEndpointAvailableControls(Endpoint endpoint)
    {
        SwitchAllCheckboxesOff();
        if (endpoint.RequiredFields == null) return;
        foreach (var requiredField in endpoint.RequiredFields)
        {
            switch (requiredField)
            {
                case RequestFields.Key:
                    ApiKeyPasswordTerminalIdCheckboxesControl.IsApiKeyChecked = true;
                    break;
                case RequestFields.Password:
                    ApiKeyPasswordTerminalIdCheckboxesControl.IsApiPasswordChecked = true;
                    break;
                case RequestFields.TerminalId:
                    ApiKeyPasswordTerminalIdCheckboxesControl.IsTerminalIdChecked = true;
                    break;
                case RequestFields.Command:
                    CheckBoxCommandControl.IsChecked = true;
                    break;
                case RequestFields.RefId:
                    CheckBoxTextBoxControlRefId.IsChecked = true;
                    break;
                case RequestFields.Date:
                    CheckBoxTextBoxControlDate.IsChecked = true;
                    break;
                case RequestFields.MerchantId:
                    CheckBoxTextBoxControlMerchantId.IsChecked = true;
                    break;
                case RequestFields.PaymentType:
                    CheckBoxTextBoxControlPaymentType.IsChecked = true;
                    break;
                case RequestFields.Prompt:
                    CheckBoxTextBoxControlPrompt.IsChecked = true;
                    break;
                case RequestFields.Country:
                    CheckBoxTextBoxControlCountry.IsChecked = true;
                    break;
                case RequestFields.BusinessName:
                    CheckBoxTextBoxControlBusinessName.IsChecked = true;
                    break;
                case RequestFields.ContactName:
                    CheckBoxTextBoxControlContactName.IsChecked = true;
                    break;
                case RequestFields.Address:
                    CheckBoxTextBoxControlAddress.IsChecked = true;
                    break;
                case RequestFields.City:
                    CheckBoxTextBoxControlCity.IsChecked = true;
                    break;
                case RequestFields.State:
                    CheckBoxTextBoxControlState.IsChecked = true;
                    break;
                case RequestFields.ZipCode:
                    CheckBoxTextBoxControlZipCode.IsChecked = true;
                    break;
                case RequestFields.Email:
                    CheckBoxTextBoxControlEmail.IsChecked = true;
                    break;
                case RequestFields.Phone:
                    CheckBoxTextBoxControlPhone.IsChecked = true;
                    break;
                case RequestFields.ResellerName:
                    CheckBoxTextBoxControlResellerName.IsChecked = true;
                    break;
                case RequestFields.ReferenceId:
                    CheckBoxTextBoxControlReferenceId.IsChecked = true;
                    break;
                case RequestFields.TerminalSerialNumber:
                    CheckBoxTextBoxControlTerminalSerialNumber.IsChecked = true;
                    break;
                case RequestFields.Amount:
                    CheckBoxTextBoxControlAmount.IsChecked = true;
                    break;
                case RequestFields.InvoiceNumber:
                    CheckBoxTextBoxControlInvoiceNumber.IsChecked = true;
                    break;
                case RequestFields.Token:
                    CheckBoxTextBoxControlToken.IsChecked = true;
                    break;
                case RequestFields.ExpDate:
                    CheckBoxTextBoxControlExpDate.IsChecked = true;
                    break;
                case RequestFields.Type:
                    CheckBoxTextBoxControlType.IsChecked = true;
                    break;
                case RequestFields.Data:
                    CheckBoxTextBoxControlData.IsChecked = true;
                    break;
                case RequestFields.Url:
                    CheckBoxTextBoxControlUrl.IsChecked = true;
                    break;
                case RequestFields.IsDefault:
                    CheckBoxTextBoxControlIsDefault.IsChecked = true;
                    break;
                case RequestFields.OptionName:
                    CheckBoxTextBoxControlOptionName.IsChecked = true;
                    break;
                case RequestFields.OptionValue:
                    CheckBoxTextBoxControlOptionValue.IsChecked = true;
                    break;
                case RequestFields.Title:
                    CheckBoxTextBoxControlTitle.IsChecked = true;
                    break;
                case RequestFields.MaxLength:
                    CheckBoxTextBoxControlMaxLength.IsChecked = true;
                    break;
                case RequestFields.Options:
                    CheckBoxTextBoxControlOptions.IsChecked = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Transaction GenerateTransaction(Terminal terminal)
    {
        Transaction transaction = new();

        if(ApiKeyPasswordTerminalIdCheckboxesControl.IsApiKeyChecked && terminal.ApiKey != null)
            transaction.Key = terminal.ApiKey;
        if(ApiKeyPasswordTerminalIdCheckboxesControl.IsApiPasswordChecked && terminal.ApiPassword != null)
            transaction.Password = terminal.ApiPassword;
        if(ApiKeyPasswordTerminalIdCheckboxesControl.IsTerminalIdChecked)
            transaction.TerminalId = terminal.Id.ToString();
        if(CheckBoxCommandControl.IsChecked)
            transaction.Command = CheckBoxCommandControl.SelectedValue;
        if(CheckBoxTextBoxControlRefId.IsChecked)
            transaction.RefId = CheckBoxTextBoxControlRefId.TextValue;
        if(CheckBoxTextBoxControlDate.IsChecked)
            transaction.Date = CheckBoxTextBoxControlDate.TextValue;
        if(CheckBoxTextBoxControlMerchantId.IsChecked)
            transaction.MerchantId = CheckBoxTextBoxControlMerchantId.TextValue;
        if(CheckBoxTextBoxControlPaymentType.IsChecked)
            transaction.PaymentType = CheckBoxTextBoxControlPaymentType.TextValue;
        if(CheckBoxTextBoxControlPrompt.IsChecked)
            transaction.Prompt = CheckBoxTextBoxControlPrompt.TextValue;
        if(CheckBoxTextBoxControlCountry.IsChecked)
            transaction.Country = CheckBoxTextBoxControlCountry.TextValue;
        if(CheckBoxTextBoxControlBusinessName.IsChecked)
            transaction.BusinessName = CheckBoxTextBoxControlBusinessName.TextValue;
        if(CheckBoxTextBoxControlContactName.IsChecked)
            transaction.ContactName = CheckBoxTextBoxControlContactName.TextValue;
        if(CheckBoxTextBoxControlAddress.IsChecked)
            transaction.Address = CheckBoxTextBoxControlAddress.TextValue;
        if(CheckBoxTextBoxControlCity.IsChecked)
            transaction.City = CheckBoxTextBoxControlCity.TextValue;
        if(CheckBoxTextBoxControlState.IsChecked)
            transaction.State = CheckBoxTextBoxControlState.TextValue;
        if(CheckBoxTextBoxControlZipCode.IsChecked)
            transaction.ZipCode = CheckBoxTextBoxControlZipCode.TextValue;
        if(CheckBoxTextBoxControlEmail.IsChecked)
            transaction.Email = CheckBoxTextBoxControlEmail.TextValue;
        if(CheckBoxTextBoxControlPhone.IsChecked)
            transaction.Phone = CheckBoxTextBoxControlPhone.TextValue;
        if(CheckBoxTextBoxControlResellerName.IsChecked)
            transaction.ResellerName = CheckBoxTextBoxControlResellerName.TextValue;
        if(CheckBoxTextBoxControlReferenceId.IsChecked)
            transaction.ReferenceId = CheckBoxTextBoxControlReferenceId.TextValue;
        if(CheckBoxTextBoxControlTerminalSerialNumber.IsChecked)
            transaction.TerminalSerialNumber = CheckBoxTextBoxControlTerminalSerialNumber.TextValue;
        if(CheckBoxTextBoxControlAmount.IsChecked)
            transaction.Amount = CheckBoxTextBoxControlAmount.TextValue;
        if(CheckBoxTextBoxControlInvoiceNumber.IsChecked)
            transaction.InvoiceNumber = CheckBoxTextBoxControlInvoiceNumber.TextValue;
        if(CheckBoxTextBoxControlToken.IsChecked)
            transaction.Token = CheckBoxTextBoxControlToken.TextValue;
        if(CheckBoxTextBoxControlExpDate.IsChecked)
            transaction.ExpDate = CheckBoxTextBoxControlExpDate.TextValue;
        if(CheckBoxTextBoxControlType.IsChecked)
            transaction.Type = CheckBoxTextBoxControlType.TextValue;
        if(CheckBoxTextBoxControlData.IsChecked)
            transaction.Data = CheckBoxTextBoxControlData.TextValue;
        if(CheckBoxTextBoxControlUrl.IsChecked)
            transaction.Url = CheckBoxTextBoxControlUrl.TextValue;
        if(CheckBoxTextBoxControlIsDefault.IsChecked)
            transaction.IsDefault = CheckBoxTextBoxControlIsDefault.TextValue;
        if(CheckBoxTextBoxControlOptionName.IsChecked)
            transaction.OptionName = CheckBoxTextBoxControlOptionName.TextValue;
        if(CheckBoxTextBoxControlOptionValue.IsChecked)
            transaction.OptionValue = CheckBoxTextBoxControlOptionValue.TextValue;
        if(CheckBoxTextBoxControlTitle.IsChecked)
            transaction.Title = CheckBoxTextBoxControlTitle.TextValue;
        if(CheckBoxTextBoxControlMaxLength.IsChecked)
            transaction.MaxLength = CheckBoxTextBoxControlMaxLength.TextValue;
        if(CheckBoxTextBoxControlOptions.IsChecked)
            transaction.Options = new List<string>(CheckBoxTextBoxControlOptions.TextValue.Split(','));
            
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
        CheckBoxTextBoxControlDate.IsChecked = transaction.Date != null; 
        if(transaction.Date != null)
            CheckBoxTextBoxControlDate.TextValue = transaction.Date;
        CheckBoxTextBoxControlMerchantId.IsChecked = transaction.MerchantId != null; 
        if(transaction.MerchantId != null)
            CheckBoxTextBoxControlMerchantId.TextValue = transaction.MerchantId;
        CheckBoxTextBoxControlPaymentType.IsChecked = transaction.PaymentType != null; 
        if(transaction.PaymentType != null)
            CheckBoxTextBoxControlPaymentType.TextValue = transaction.PaymentType;
        CheckBoxTextBoxControlPrompt.IsChecked = transaction.Prompt != null; 
        if(transaction.Prompt != null)
            CheckBoxTextBoxControlPrompt.TextValue = transaction.Prompt;
        CheckBoxTextBoxControlCountry.IsChecked = transaction.Country != null; 
        if(transaction.Country != null)
            CheckBoxTextBoxControlCountry.TextValue = transaction.Country;
        CheckBoxTextBoxControlBusinessName.IsChecked = transaction.BusinessName != null; 
        if(transaction.BusinessName != null)
            CheckBoxTextBoxControlBusinessName.TextValue = transaction.BusinessName;
        CheckBoxTextBoxControlContactName.IsChecked = transaction.ContactName != null; 
        if(transaction.ContactName != null)
            CheckBoxTextBoxControlContactName.TextValue = transaction.ContactName;
        CheckBoxTextBoxControlAddress.IsChecked = transaction.Address != null; 
        if(transaction.Address != null)
            CheckBoxTextBoxControlAddress.TextValue = transaction.Address;
        CheckBoxTextBoxControlCity.IsChecked = transaction.City != null; 
        if(transaction.City != null)
            CheckBoxTextBoxControlCity.TextValue = transaction.City;
        CheckBoxTextBoxControlState.IsChecked = transaction.State != null; 
        if(transaction.State != null)
            CheckBoxTextBoxControlState.TextValue = transaction.State;
        CheckBoxTextBoxControlZipCode.IsChecked = transaction.ZipCode != null; 
        if(transaction.ZipCode != null)
            CheckBoxTextBoxControlZipCode.TextValue = transaction.ZipCode;
        CheckBoxTextBoxControlEmail.IsChecked = transaction.Email != null; 
        if(transaction.Email != null)
            CheckBoxTextBoxControlEmail.TextValue = transaction.Email;
        CheckBoxTextBoxControlPhone.IsChecked = transaction.Phone != null; 
        if(transaction.Phone != null)
            CheckBoxTextBoxControlPhone.TextValue = transaction.Phone;
        CheckBoxTextBoxControlResellerName.IsChecked = transaction.ResellerName != null; 
        if(transaction.ResellerName != null)
            CheckBoxTextBoxControlResellerName.TextValue = transaction.ResellerName;
        CheckBoxTextBoxControlReferenceId.IsChecked = transaction.ReferenceId != null; 
        if(transaction.ReferenceId != null)
            CheckBoxTextBoxControlReferenceId.TextValue = transaction.ReferenceId;
        CheckBoxTextBoxControlTerminalSerialNumber.IsChecked = transaction.TerminalSerialNumber != null; 
        if(transaction.TerminalSerialNumber != null)
            CheckBoxTextBoxControlTerminalSerialNumber.TextValue = transaction.TerminalSerialNumber;
        CheckBoxTextBoxControlAmount.IsChecked = transaction.Amount != null; 
        if(transaction.Amount != null)
            CheckBoxTextBoxControlAmount.TextValue = transaction.Amount;
        CheckBoxTextBoxControlInvoiceNumber.IsChecked = transaction.InvoiceNumber != null; 
        if(transaction.InvoiceNumber != null)
            CheckBoxTextBoxControlInvoiceNumber.TextValue = transaction.InvoiceNumber;
        CheckBoxTextBoxControlToken.IsChecked = transaction.Token != null; 
        if(transaction.Token != null)
            CheckBoxTextBoxControlToken.TextValue = transaction.Token;
        CheckBoxTextBoxControlExpDate.IsChecked = transaction.ExpDate != null; 
        if(transaction.ExpDate != null)
            CheckBoxTextBoxControlExpDate.TextValue = transaction.ExpDate;
        CheckBoxTextBoxControlType.IsChecked = transaction.Type != null; 
        if(transaction.Type != null)
            CheckBoxTextBoxControlType.TextValue = transaction.Type;
        CheckBoxTextBoxControlData.IsChecked = transaction.Data != null; 
        if(transaction.Data != null)
            CheckBoxTextBoxControlData.TextValue = transaction.Data;
        CheckBoxTextBoxControlUrl.IsChecked = transaction.Url != null; 
        if(transaction.Url != null)
            CheckBoxTextBoxControlUrl.TextValue = transaction.Url;
        CheckBoxTextBoxControlIsDefault.IsChecked = transaction.IsDefault != null; 
        if(transaction.IsDefault != null)
            CheckBoxTextBoxControlIsDefault.TextValue = transaction.IsDefault;
        CheckBoxTextBoxControlOptionName.IsChecked = transaction.OptionName != null; 
        if(transaction.OptionName != null)
            CheckBoxTextBoxControlOptionName.TextValue = transaction.OptionName;
        CheckBoxTextBoxControlOptionValue.IsChecked = transaction.OptionValue != null; 
        if(transaction.OptionValue != null)
            CheckBoxTextBoxControlOptionValue.TextValue = transaction.OptionValue;
        CheckBoxTextBoxControlTitle.IsChecked = transaction.Title != null; 
        if(transaction.Title != null)
            CheckBoxTextBoxControlTitle.TextValue = transaction.Title;
        CheckBoxTextBoxControlMaxLength.IsChecked = transaction.MaxLength != null; 
        if(transaction.MaxLength != null)
            CheckBoxTextBoxControlMaxLength.TextValue = transaction.MaxLength;
        CheckBoxTextBoxControlOptions.IsChecked = transaction.Options != null; 
        if(transaction.Options != null)
            CheckBoxTextBoxControlOptions.TextValue = string.Join(",", transaction.Options);
    }
    private void ComboBoxEndpoints_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        SelectEndpointAvailableControls((Endpoint)comboBox.SelectedItem);
    }

    private void BtnGenerateRefId_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlRefId.TextValue = GenerateFieldValueHelper.GenerateRefId();
    }
    
    private void BtnChangeRefIdRefId_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlRefId.TextValue = GenerateFieldValueHelper.ChangeRefIdCase(CheckBoxTextBoxControlRefId.TextValue);
    }
    
    private void BtnGenerateDate_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlDate.TextValue = GenerateFieldValueHelper.GenerateDate();
    }
    
    private void BtnGenerateExpDate_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlExpDate.TextValue = GenerateFieldValueHelper.GenerateDate();
    }

    private void BtnSetIsDefaultTrue_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlIsDefault.TextValue = "True";
    }

    private void BtnSetIsDefaultFalse_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlIsDefault.TextValue = "False";
    }
    public string? GetUri()
    {
        var endpoint = (Endpoint)ComboBoxEndpoints.SelectedItem;
        return endpoint.Uri;
    }

    public void SetUri(Environment environment)
    {
        //there probably is a better way to do that, but in the meantime...
        foreach (Endpoint endpoint in ComboBoxEndpoints.Items)
        {
            if (!IsMatchingUri(environment, endpoint)) continue;
            ComboBoxEndpoints.SelectedItem = endpoint;
            break;
        }
    }

    private bool IsMatchingUri(Environment environment, Endpoint e)
    {
        return !(e.Uri != null && (
            e.Uri.StartsWith($"{environment.SoapUrl}/{GetUri()}?")
            || e.Uri.StartsWith($"{environment.RestUrl}/1.0/{GetUri()}")));
    }

    private void BtnGenerateAmount_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlAmount.TextValue = GenerateFieldValueHelper.GenerateAmount();
    }

    private void BtnSetTypeHtml_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlType.TextValue = "html";
    }

    private void BtnSetTypeJson_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlType.TextValue = "json";
    }
    private void BtnSetDataHtml_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlData.TextValue = "<!DOCTYPE html><html><body><h2>Menu</h2><dl><dt>Coffee</dt><dd>- black hot drink</dd><dt>Milk</dt><dd>- cold drink</dd></dl></body></html>";
    }

    private void BtnSetDataJson_Click(object sender, RoutedEventArgs e)
    {
        CheckBoxTextBoxControlData.TextValue = "{\"header\": {\"fgColor\": \"000000\",\"bgColor\": \"ffffff\",\"fontSz\": \"30\",\"rows\": [{\"text\": \"Joe's Pizza\",\"fontSz\":\"30\"},{\"text\": \"best pizza ever\",\"fontSz\":\"30\"}]},\"body\": {\"fgColor\": \"000000\",\"fontSz\": \"22\",\"rows\": [{\"text\": \"1- Pizza\",\"subText\": \"onions, peppers\",\"amt\":\"$9.00\",\"fgColor\":\"111111\",\"bgColor\":\"cccccc\",\"fontSz\":\"22\"},{\"text\": \"Breadsticks\",\"subText\": \"Marinara\",\"amt\":\"$1.00\",\"fgColor\":\"222222\",\"bgColor\":\"eeeeee\",\"fontSz\":\"22\"}]},\"footer\": {\"fgColor\": \"000000\",\"fontSz\": \"22\",\"rows\": [{\"text\": \"SubTotal\",\"amt\":\"$10.00\",\"fontSz\":\"22\"},{\"text\": \"Tax\",\"amt\":\"$1.00\",\"fontSz\":\"22\"},{\"text\": \"Total\",\"amt\":\"$12.00\",\"fontSz\":\"22\"}]}}";
    }
}
