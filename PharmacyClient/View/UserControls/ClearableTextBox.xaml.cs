using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace PharmacyClient.View.UserControls;

public partial class ClearableTextBox : UserControl, INotifyPropertyChanged
{
    public ClearableTextBox()
    {
        DataContext = this;
        InitializeComponent();

    }

    private string placeholder;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Placeholder
    {
        get { return placeholder; }
        set
        {
            placeholder = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName]string? propertyName=null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        txtInput.Clear();
        txtInput.Focus();
    }
    private void txtInput_TextChanged(object sender, RoutedEventArgs e)
    {
        tbPlaceholder.Visibility = string.IsNullOrEmpty(txtInput.Text) ? Visibility.Visible : Visibility.Hidden;
    }
}