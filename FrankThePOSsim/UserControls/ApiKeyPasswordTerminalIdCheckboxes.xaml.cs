using System.Windows;
using System.Windows.Controls;

namespace FrankThePOSsim.UserControls;

public partial class ApiKeyPasswordTerminalIdCheckboxes : UserControl
{
    public ApiKeyPasswordTerminalIdCheckboxes()
    {
        InitializeComponent();
        DataContext = this;
    }
    // Properties to bind the checkbox states
    public bool IsApiKeyChecked
    {
        get { return (bool)GetValue(IsApiKeyCheckedProperty); }
        set { SetValue(IsApiKeyCheckedProperty, value); }
    }

    public static readonly DependencyProperty IsApiKeyCheckedProperty =
        DependencyProperty.Register("IsApiKeyChecked", typeof(bool), typeof(ApiKeyPasswordTerminalIdCheckboxes), new PropertyMetadata(true));

    public bool IsApiPasswordChecked
    {
        get { return (bool)GetValue(IsApiPasswordCheckedProperty); }
        set { SetValue(IsApiPasswordCheckedProperty, value); }
    }

    public static readonly DependencyProperty IsApiPasswordCheckedProperty =
        DependencyProperty.Register("IsApiPasswordChecked", typeof(bool), typeof(ApiKeyPasswordTerminalIdCheckboxes), new PropertyMetadata(true));

    public bool IsTerminalIdChecked
    {
        get { return (bool)GetValue(IsTerminalIdCheckedProperty); }
        set { SetValue(IsTerminalIdCheckedProperty, value); }
    }

    public static readonly DependencyProperty IsTerminalIdCheckedProperty =
        DependencyProperty.Register("IsTerminalIdChecked", typeof(bool), typeof(ApiKeyPasswordTerminalIdCheckboxes), new PropertyMetadata(true));
    private void CheckBox_Change(object sender, RoutedEventArgs e)
    {
        if (sender is not CheckBox checkBox) return;
        switch (checkBox.Name)
        {
            case "CheckBoxApiKey":
                IsApiKeyChecked = checkBox.IsChecked == true;
                break;
            case "CheckBoxApiPassword":
                IsApiPasswordChecked = checkBox.IsChecked == true;
                break;
            case "CheckBoxTerminalId":
                IsTerminalIdChecked = checkBox.IsChecked == true;
                break;
        }
    }
}