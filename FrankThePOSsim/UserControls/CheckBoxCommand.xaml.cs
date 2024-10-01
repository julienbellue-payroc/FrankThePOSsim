using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FrankThePOSsim.observable;

namespace FrankThePOSsim.UserControls;

public partial class CheckBoxCommand
{
    public event SelectionChangedEventHandler? SelectionChanged;
    public CheckBoxCommand()
    {
        InitializeComponent();
        ItemsSource = new CommandObservable(Commands.GetCommands);
        if (ItemsSource is IList<string> { Count: > 0 } items)
        {
            SelectedValue = items[0]; // Set the first item as the selected value
        }
    }
    
    // Properties to bind the checkbox states
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
    public static readonly DependencyProperty IsCheckedProperty =
        DependencyProperty.Register(nameof(IsChecked), typeof(bool), typeof(CheckBoxCommand), new PropertyMetadata(true));
    
    // Dependency Property for SelectedValue
    public string SelectedValue
    {
        get => (string)GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }

    public static readonly DependencyProperty SelectedValueProperty =
        DependencyProperty.Register(nameof(SelectedValue), typeof(string), typeof(CheckBoxCommand), new PropertyMetadata(string.Empty));
    
    // Dependency Property for ItemsSource
    public object ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        init => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(CheckBoxCommand), new PropertyMetadata(null));

    // Dependency Property for SelectedIndex
    public int SelectedIndex
    {
        get => (int)GetValue(SelectedIndexProperty);
        set => SetValue(SelectedIndexProperty, value);
    }

    public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(nameof(SelectedIndex), typeof(int), typeof(CheckBoxCommand), new PropertyMetadata(-1));

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }
}