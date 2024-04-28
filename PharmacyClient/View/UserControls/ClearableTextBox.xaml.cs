using System.Windows;
using System.Windows.Controls;

namespace PharmacyClient.View.UserControls;

public partial class ClearableTextBox : UserControl
{
    public ClearableTextBox()
    {
        InitializeComponent();
    }

    private string placeholder;

    public string Placeholder
    {
        get { return placeholder; }
        set { placeholder = value; }
    }
    private void btnClear_Click(object sender,RoutedEventArgs e)
    {
        txtInput.Clear();
        txtInput.Focus();
    }
    private void txtInput_TextChanged(object sender, RoutedEventArgs e)
    {
        tbPlaceholder.Visibility = string.IsNullOrEmpty(txtInput.Text) ? Visibility.Visible : Visibility.Hidden;
    }
}