using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsum.observable;

namespace FrankThePOSsum.UserControls
{
    public partial class FullRequest: IGenerateRequests
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

        private NameValueCollection GenerateQueryString()
        {
            var queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            if(CheckBoxApiKey.IsChecked == true)
                queryString.Add("Key", App.Terminal.ApiKey);
            if(CheckBoxApiPassword.IsChecked == true)
                queryString.Add("Password", App.Terminal.ApiPassword);
            if(CheckBoxTerminalId.IsChecked == true)
                queryString.Add("TerminalId", App.Terminal.Id.ToString());
            if(CheckBoxCommand.IsChecked == true)
                queryString.Add("Command", (string)ComboBoxCommand.SelectedValue);
            if(CheckBoxRefId.IsChecked == true)
                queryString.Add("RefId", TextBoxRefId.Text);
            if(CheckBoxDate.IsChecked == true)
                queryString.Add("Date", TextBoxDate.Text);
            if(CheckBoxMerchantId.IsChecked == true)
                queryString.Add("MerchantId", TextBoxMerchantId.Text);
            if(CheckBoxPaymentType.IsChecked == true)
                queryString.Add("PaymentType", TextBoxPaymentType.Text);
            if(CheckBoxPrompt.IsChecked == true)
                queryString.Add("Prompt", TextBoxPrompt.Text);
            if(CheckBoxCountry.IsChecked == true)
                queryString.Add("Country", TextBoxCountry.Text);
            if(CheckBoxBusinessName.IsChecked == true)
                queryString.Add("BusinessName", TextBoxBusinessName.Text);
            if(CheckBoxContactName.IsChecked == true)
                queryString.Add("ContactName", TextBoxContactName.Text);
            if(CheckBoxAddress.IsChecked == true)
                queryString.Add("Address", TextBoxAddress.Text);
            if(CheckBoxCity.IsChecked == true)
                queryString.Add("City", TextBoxCity.Text);
            if(CheckBoxState.IsChecked == true)
                queryString.Add("State", TextBoxState.Text);
            if(CheckBoxZipCode.IsChecked == true)
                queryString.Add("ZipCode", TextBoxZipCode.Text);
            if(CheckBoxEmail.IsChecked == true)
                queryString.Add("Email", TextBoxEmail.Text);
            if(CheckBoxPhone.IsChecked == true)
                queryString.Add("Phone", TextBoxPhone.Text);
            if(CheckBoxResellerName.IsChecked == true)
                queryString.Add("ResellerName", TextBoxResellerName.Text);
            if(CheckBoxReferenceId.IsChecked == true)
                queryString.Add("ReferenceId", TextBoxReferenceId.Text);
            if(CheckBoxTerminalSerialNumber.IsChecked == true)
                queryString.Add("TerminalSerialNumber", TextBoxTerminalSerialNumber.Text);
            if(CheckBoxAmount.IsChecked == true)
                queryString.Add("Amount", TextBoxAmount.Text);
            if(CheckBoxInvoiceNumber.IsChecked == true)
                queryString.Add("InvoiceNumber", TextBoxInvoiceNumber.Text);
            if(CheckBoxToken.IsChecked == true)
                queryString.Add("Token", TextBoxToken.Text);
            if(CheckBoxExpDate.IsChecked == true)
                queryString.Add("ExpDate", TextBoxExpDate.Text);
            if(CheckBoxType.IsChecked == true)
                queryString.Add("Type", TextBoxType.Text);
            if(CheckBoxUrl.IsChecked == true)
                queryString.Add("Url", TextBoxUrl.Text);
            if(CheckBoxIsDefault.IsChecked == true)
                queryString.Add("IsDefault", TextBoxIsDefault.Text);
            if(CheckBoxOptionName.IsChecked == true)
                queryString.Add("OptionName", TextBoxOptionName.Text);
            if(CheckBoxOptionValue.IsChecked == true)
                queryString.Add("OptionValue", TextBoxOptionValue.Text);
            if(CheckBoxTitle.IsChecked == true)
                queryString.Add("Title", TextBoxTitle.Text);
            if(CheckBoxMaxLength.IsChecked == true)
                queryString.Add("MaxLength", TextBoxMaxLength.Text);
            if(CheckBoxOptions.IsChecked == true)
                queryString.Add("Options", TextBoxOptions.Text);
            return queryString;
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

        public string GenerateSoapRequest()
        {
            var endpoint = (Endpoint)ComboBoxEndpoints.SelectedItem;
            return $"{App.Environment.SoapUrl}/{endpoint.Uri}?{GenerateQueryString()}";
        }

        private JsonObject GenerateRestBody()
        {
            throw new NotImplementedException();
        }
        public StringContent GenerateRestRequest()
        {
            var restBody = GenerateRestBody();
            return new StringContent(restBody.ToString(), Encoding.UTF8, "application/json");
        }

        public Transaction GenerateRestRequestBody()
        {
            throw new NotImplementedException();
        }
    }
}

