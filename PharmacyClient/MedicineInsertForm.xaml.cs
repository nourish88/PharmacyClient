using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace PharmacyClient;

/// <summary>
/// Interaction logic for ConsumerInsertForm.xaml
/// </summary>
public partial class ConsumerInsertForm : Window
{
    private MainWindow _mainWindow;
    public ConsumerInsertForm(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        InitializeComponent();
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close(); // Close the modal dialog
    }
    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {

        string url = "https://localhost:7220";
        var client2 = new RestClient(url);
        var request = new RestRequest("medicines");
        request.AddBody(new
        {
            Name = txtMedicineName.txtInput.Text,
            Price = Convert.ToDecimal(txtMedicinePrice.txtInput.Text)
        });
        var response = client2.ExecutePost(request);
        if(response.StatusDescription=="Bad Request")
        {
            var data = JsonSerializer.Deserialize<ValidationProblemDetails>(response.Content!)!;

            string validationErrors = string.Join(".\n", data.Errors.Select(x => x.Errors).FirstOrDefault());

            ShowMessageBox("Error", $"Failed to save data. Status code: {response.StatusDescription}.\n  {validationErrors}", MessageBoxButton.OK, MessageBoxImage.Error);
        }
       else if (response.StatusDescription == "Ok")
        {
            ShowMessageBox("Error", $"Failed to save data. Status code: {response.StatusDescription}", MessageBoxButton.OK, MessageBoxImage.Error);
        }
       




        _mainWindow.GetMedicines();
        this.Close();




    }
    public class ValidationExceptionModel
    {
        public string? Property { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
    internal class ValidationProblemDetails : ProblemDetails
    {
        public IEnumerable<ValidationExceptionModel> Errors { get; init; }

        public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
        {
            Title = "Validation error(s)";
            Detail = "One or more validation errors occurred.";
            Errors = errors;
            Status = StatusCodes.Status400BadRequest;
            Type = "https://example.com/probs/validation";
        }
    }

    private void ShowMessageBox(string header, string message, MessageBoxButton button, MessageBoxImage icon)
    {
        MessageBoxResult result = MessageBox.Show(message, header, button, icon);
    }
    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }
    private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }

    //private void SaveButton_Click(object sender, RoutedEventArgs e)
    //{

    //}

    //private void SaveButton_Click(object sender, RoutedEventArgs e)
    //{

    //}
}

