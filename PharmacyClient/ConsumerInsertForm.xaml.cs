using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PharmacyClient;

/// <summary>
/// Interaction logic for ConsumerInsertForm.xaml
/// </summary>
public partial class ConsumerInsertForm : Window
{
     
    public ConsumerInsertForm()
    {
        InitializeComponent();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close(); // Close the modal dialog
    }
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        // Implement the save logic here
    }

    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    private void TextBox_TextChanged_1(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }
}