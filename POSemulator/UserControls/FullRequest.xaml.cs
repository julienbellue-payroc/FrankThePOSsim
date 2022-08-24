using System;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsum.observable;

namespace FrankThePOSsum.UserControls
{
    public partial class FullRequest: IGenerateTransaction
    {
        public FullRequest()
        {
            InitializeComponent();

            ComboBoxEndpoints.DisplayMemberPath = "Uri";
            ComboBoxEndpoints.ItemsSource = new EndPointObservable(new Endpoints());
            ComboBoxEndpoints.SelectedIndex = 0;

            ComboBoxCommand.ItemsSource = new CommandObservable(Commands.GetCommands);
            ComboBoxCommand.SelectedIndex = 0;
        }

        private void SwitchAllCheckboxesOff()
        {
            CheckBoxApiPassword.IsChecked = false;
            CheckBoxApiKey.IsChecked = false;
            CheckBoxTerminalId.IsChecked = false;
            CheckBoxCommand.IsChecked = false;
            CheckBoxRefId.IsChecked = false;
            CheckBoxDate.IsChecked = false;
            CheckBoxMerchantId.IsChecked = false;
            CheckBoxPaymentType.IsChecked = false;
            CheckBoxPrompt.IsChecked = false;
            CheckBoxCountry.IsChecked = false;
            CheckBoxBusinessName.IsChecked = false;
            CheckBoxContactName.IsChecked = false;
            CheckBoxAddress.IsChecked = false;
            CheckBoxCity.IsChecked = false;
            CheckBoxState.IsChecked = false;
            CheckBoxZipCode.IsChecked = false;
            CheckBoxEmail.IsChecked = false;
            CheckBoxPhone.IsChecked = false;
            CheckBoxResellerName.IsChecked = false;
            CheckBoxReferenceId.IsChecked = false;
            CheckBoxTerminalSerialNumber.IsChecked = false;
            CheckBoxAmount.IsChecked = false;
            CheckBoxInvoiceNumber.IsChecked = false;
            CheckBoxToken.IsChecked = false;
            CheckBoxExpDate.IsChecked = false;
            CheckBoxType.IsChecked = false;
            CheckBoxUrl.IsChecked = false;
            CheckBoxIsDefault.IsChecked = false;
            CheckBoxOptionName.IsChecked = false;
            CheckBoxOptionValue.IsChecked = false;
            CheckBoxTitle.IsChecked = false;
            CheckBoxMaxLength.IsChecked = false;
            CheckBoxOptions.IsChecked = false;
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
                        CheckBoxApiKey.IsChecked = true;
                        break;
                    case RequestFields.Password:
                        CheckBoxApiPassword.IsChecked = true;
                        break;
                    case RequestFields.TerminalId:
                        CheckBoxTerminalId.IsChecked = true;
                        break;
                    case RequestFields.Command:
                        CheckBoxCommand.IsChecked = true;
                        break;
                    case RequestFields.RefId:
                        CheckBoxRefId.IsChecked = true;
                        break;
                    case RequestFields.Date:
                        CheckBoxDate.IsChecked = true;
                        break;
                    case RequestFields.MerchantId:
                        CheckBoxMerchantId.IsChecked = true;
                        break;
                    case RequestFields.PaymentType:
                        CheckBoxPaymentType.IsChecked = true;
                        break;
                    case RequestFields.Prompt:
                        CheckBoxPrompt.IsChecked = true;
                        break;
                    case RequestFields.Country:
                        CheckBoxCountry.IsChecked = true;
                        break;
                    case RequestFields.BusinessName:
                        CheckBoxBusinessName.IsChecked = true;
                        break;
                    case RequestFields.ContactName:
                        CheckBoxContactName.IsChecked = true;
                        break;
                    case RequestFields.Address:
                        CheckBoxAddress.IsChecked = true;
                        break;
                    case RequestFields.City:
                        CheckBoxCity.IsChecked = true;
                        break;
                    case RequestFields.State:
                        CheckBoxState.IsChecked = true;
                        break;
                    case RequestFields.ZipCode:
                        CheckBoxZipCode.IsChecked = true;
                        break;
                    case RequestFields.Email:
                        CheckBoxEmail.IsChecked = true;
                        break;
                    case RequestFields.Phone:
                        CheckBoxPhone.IsChecked = true;
                        break;
                    case RequestFields.ResellerName:
                        CheckBoxResellerName.IsChecked = true;
                        break;
                    case RequestFields.ReferenceId:
                        CheckBoxReferenceId.IsChecked = true;
                        break;
                    case RequestFields.TerminalSerialNumber:
                        CheckBoxTerminalSerialNumber.IsChecked = true;
                        break;
                    case RequestFields.Amount:
                        CheckBoxAmount.IsChecked = true;
                        break;
                    case RequestFields.InvoiceNumber:
                        CheckBoxInvoiceNumber.IsChecked = true;
                        break;
                    case RequestFields.Token:
                        CheckBoxToken.IsChecked = true;
                        break;
                    case RequestFields.ExpDate:
                        CheckBoxExpDate.IsChecked = true;
                        break;
                    case RequestFields.Type:
                        CheckBoxType.IsChecked = true;
                        break;
                    case RequestFields.Url:
                        CheckBoxUrl.IsChecked = true;
                        break;
                    case RequestFields.IsDefault:
                        CheckBoxIsDefault.IsChecked = true;
                        break;
                    case RequestFields.OptionName:
                        CheckBoxOptionName.IsChecked = true;
                        break;
                    case RequestFields.OptionValue:
                        CheckBoxOptionValue.IsChecked = true;
                        break;
                    case RequestFields.Title:
                        CheckBoxTitle.IsChecked = true;
                        break;
                    case RequestFields.MaxLength:
                        CheckBoxMaxLength.IsChecked = true;
                        break;
                    case RequestFields.Options:
                        CheckBoxOptions.IsChecked = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Transaction GenerateTransaction()
        {
            Transaction transaction = new();

            if(CheckBoxApiKey.IsChecked == true && App.Terminal.ApiKey != null)
                transaction.Key = App.Terminal.ApiKey;
            if(CheckBoxApiPassword.IsChecked == true && App.Terminal.ApiPassword != null)
                transaction.Password = App.Terminal.ApiPassword;
            if(CheckBoxTerminalId.IsChecked == true)
                transaction.TerminalId = App.Terminal.Id.ToString();
            if(CheckBoxCommand.IsChecked == true)
                transaction.Command = (string)ComboBoxCommand.SelectedValue;
            if(CheckBoxRefId.IsChecked == true)
                transaction.RefId = TextBoxRefId.Text;
            if(CheckBoxDate.IsChecked == true)
                transaction.Date = TextBoxDate.Text;
            if(CheckBoxMerchantId.IsChecked == true)
                transaction.MerchantId = TextBoxMerchantId.Text;
            if(CheckBoxPaymentType.IsChecked == true)
                transaction.PaymentType = TextBoxPaymentType.Text;
            if(CheckBoxPrompt.IsChecked == true)
                transaction.Prompt = TextBoxPrompt.Text;
            if(CheckBoxCountry.IsChecked == true)
                transaction.Country = TextBoxCountry.Text;
            if(CheckBoxBusinessName.IsChecked == true)
                transaction.BusinessName = TextBoxBusinessName.Text;
            if(CheckBoxContactName.IsChecked == true)
                transaction.ContactName = TextBoxContactName.Text;
            if(CheckBoxAddress.IsChecked == true)
                transaction.Address = TextBoxAddress.Text;
            if(CheckBoxCity.IsChecked == true)
                transaction.City = TextBoxCity.Text;
            if(CheckBoxState.IsChecked == true)
                transaction.State = TextBoxState.Text;
            if(CheckBoxZipCode.IsChecked == true)
                transaction.ZipCode = TextBoxZipCode.Text;
            if(CheckBoxEmail.IsChecked == true)
                transaction.Email = TextBoxEmail.Text;
            if(CheckBoxPhone.IsChecked == true)
                transaction.Phone = TextBoxPhone.Text;
            if(CheckBoxResellerName.IsChecked == true)
                transaction.ResellerName = TextBoxResellerName.Text;
            if(CheckBoxReferenceId.IsChecked == true)
                transaction.ReferenceId = TextBoxReferenceId.Text;
            if(CheckBoxTerminalSerialNumber.IsChecked == true)
                transaction.TerminalSerialNumber = TextBoxTerminalSerialNumber.Text;
            if(CheckBoxAmount.IsChecked == true)
                transaction.Amount = TextBoxAmount.Text;
            if(CheckBoxInvoiceNumber.IsChecked == true)
                transaction.InvoiceNumber = TextBoxInvoiceNumber.Text;
            if(CheckBoxToken.IsChecked == true)
                transaction.Token = TextBoxToken.Text;
            if(CheckBoxExpDate.IsChecked == true)
                transaction.ExpDate = TextBoxExpDate.Text;
            if(CheckBoxType.IsChecked == true)
                transaction.Type = TextBoxType.Text;
            if(CheckBoxUrl.IsChecked == true)
                transaction.Url = TextBoxUrl.Text;
            if(CheckBoxIsDefault.IsChecked == true)
                transaction.IsDefault = TextBoxIsDefault.Text;
            if(CheckBoxOptionName.IsChecked == true)
                transaction.OptionName = TextBoxOptionName.Text;
            if(CheckBoxOptionValue.IsChecked == true)
                transaction.OptionValue = TextBoxOptionValue.Text;
            if(CheckBoxTitle.IsChecked == true)
                transaction.Title = TextBoxTitle.Text;
            if(CheckBoxMaxLength.IsChecked == true)
                transaction.MaxLength = TextBoxMaxLength.Text;
            if(CheckBoxOptions.IsChecked == true)
                transaction.Options = TextBoxOptions.Text;
            
            return transaction;
        }

        private void ComboBoxEndpoints_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            SelectEndpointAvailableControls((Endpoint)comboBox.SelectedItem);
        }

        private void BtnGenerateRefId_Click(object sender, RoutedEventArgs e)
        {
            TextBoxRefId.Text = Guid.NewGuid().ToString();
        }
        private void BtnGenerateDate_Click(object sender, RoutedEventArgs e)
        {
            TextBoxDate.Text = DateTime.Now.ToString("MMddyyyy");
        }

        private void BtnSetIsDefaultTrue_Click(object sender, RoutedEventArgs e)
        {
            TextBoxIsDefault.Text = "True";
        }

        private void BtnSetIsDefaultFalse_Click(object sender, RoutedEventArgs e)
        {
            TextBoxIsDefault.Text = "False";
        }
        public string? GetUri()
        {
            var endpoint = (Endpoint)ComboBoxEndpoints.SelectedItem;
            return endpoint.Uri;
        }

        private void BtnGenerateAmount_Click(object sender, RoutedEventArgs e)
        {
            TextBoxAmount.Text = "1";
        }
    }
}

