using System.Windows;

namespace FrankThePOSsim.UserControls;

public partial class CheckBoxTextBoxControl
{
    
    // Event handlers for button clicks
    public event RoutedEventHandler? Button1Click;
    public event RoutedEventHandler? Button2Click;
    public CheckBoxTextBoxControl()
    {
        InitializeComponent();
        DataContext = this;
    }

    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(false));

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    public static readonly DependencyProperty TextValueProperty =
        DependencyProperty.Register(nameof(TextValue), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(string.Empty));

    public string TextValue
    {
        get => (string)GetValue(TextValueProperty);
        set => SetValue(TextValueProperty, value);
    }

    public static readonly DependencyProperty Button1TypeProperty =
        DependencyProperty.Register(nameof(Button1Type), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata("Image"));

    public string Button1Type
    {
        get => (string)GetValue(Button1TypeProperty);
        set
        {
            SetValue(Button1TypeProperty, value); 
            UpdateButtonVisibility();
        }
    }
    public static readonly DependencyProperty Button1TextProperty =
        DependencyProperty.Register(nameof(Button1Text), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(string.Empty, OnButton1ContentChanged));

    public string Button1Text
    {
        get => (string)GetValue(Button1TextProperty);
        set => SetValue(Button1TextProperty, value);
    }
    public static readonly DependencyProperty Button1ImageProperty =
        DependencyProperty.Register(nameof(Button1Image), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(string.Empty, OnButton1ContentChanged));

    public string Button1Image
    {
        get => (string)GetValue(Button1ImageProperty);
        set => SetValue(Button1ImageProperty, value);
    }

    private static void OnButton1ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CheckBoxTextBoxControl)d;
        var newValue = e.NewValue as string;

        // Set Button1Visibility based on whether Button1Image is set
        control.Button1Visibility = string.IsNullOrEmpty(newValue) ? Visibility.Collapsed : Visibility.Visible;
    }
    
    public static readonly DependencyProperty Button1VisibilityProperty =
        DependencyProperty.Register(nameof(Button1Visibility), typeof(Visibility), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(Visibility.Collapsed));

    public Visibility Button1Visibility
    {
        get => (Visibility)GetValue(Button1VisibilityProperty);
        set => SetValue(Button1VisibilityProperty, value);
    }
    
    public static readonly DependencyProperty Button2TypeProperty =
        DependencyProperty.Register(nameof(Button2Type), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata("Image")); 
    public string Button2Type
    {
        get => (string)GetValue(Button2TypeProperty);
        set
        {
            SetValue(Button2TypeProperty, value); 
            UpdateButtonVisibility();
        }
    }
    public static readonly DependencyProperty Button2ImageProperty =
        DependencyProperty.Register(nameof(Button2Image), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(string.Empty, OnButton2ContentChanged));

    public string Button2Image
    {
        get => (string)GetValue(Button2ImageProperty);
        set => SetValue(Button2ImageProperty, value);
    }
    public static readonly DependencyProperty Button2TextProperty =
        DependencyProperty.Register(nameof(Button2Text), typeof(string), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(string.Empty, OnButton2ContentChanged));

    public string Button2Text
    {
        get => (string)GetValue(Button2TextProperty);
        set => SetValue(Button2TextProperty, value);
    }
    private static void OnButton2ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (CheckBoxTextBoxControl)d;
        var newValue = e.NewValue as string;

        // Set Button2Visibility based on whether Button2Image is set
        control.Button2Visibility = string.IsNullOrEmpty(newValue) ? Visibility.Collapsed : Visibility.Visible;
    }

    public static readonly DependencyProperty Button2VisibilityProperty =
        DependencyProperty.Register(nameof(Button2Visibility), typeof(Visibility), typeof(CheckBoxTextBoxControl),
            new PropertyMetadata(Visibility.Collapsed));

    public Visibility Button2Visibility
    {
        get => (Visibility)GetValue(Button2VisibilityProperty);
        set => SetValue(Button2VisibilityProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(string), typeof(CheckBoxTextBoxControl), new PropertyMetadata(string.Empty));

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        Button1Click?.Invoke(this, e);
    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
        Button2Click?.Invoke(this, e);
    }
    
    private void UpdateButtonVisibility()
    {
        // Button 1
        if (Button1Type == "Text")
        {
            Button1TextContent.Visibility = Visibility.Visible;
            Button1ImageContent.Visibility = Visibility.Collapsed;
        }
        else if (Button1Type == "Image")
        {
            Button1TextContent.Visibility = Visibility.Collapsed;
            Button1ImageContent.Visibility = Visibility.Visible;
        }

        // Button 2
        if (Button2Type == "Text")
        {
            Button1TextContent.Visibility = Visibility.Visible;
            Button1ImageContent.Visibility = Visibility.Collapsed;
        }
        else if (Button2Type == "Image")
        {
            Button1TextContent.Visibility = Visibility.Collapsed;
            Button1ImageContent.Visibility = Visibility.Visible;
        }
    }
}
