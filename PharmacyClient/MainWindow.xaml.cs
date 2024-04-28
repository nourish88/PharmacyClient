using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Http;
using Newtonsoft.Json;

namespace PharmacyClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var converter = new BrushConverter();
        ObservableCollection<MedicineViewModel> medicines = new ObservableCollection<MedicineViewModel>();
   

        var client = new HttpClient();
      
        string url = "http://localhost:5421/medicines";
        HttpRequestMessage m = new HttpRequestMessage(HttpMethod.Get, url);
        HttpResponseMessage resp = client.Send(m);


        


   

        string json = resp.Content.ReadAsStringAsync().Result;

        var results = JsonConvert.DeserializeObject<GetAllMedicinesResponse>(json);

        foreach (var result in results.Medicines)
        {
            var med = new MedicineViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price
            };
            medicines.Add(med);
        }
        
      



        membersDataGrid.ItemsSource = medicines;
    }

    private bool IsMaximize = false;
    private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            if (IsMaximize)
            {
                this.WindowState = WindowState.Normal;
                this.Width = 1080;
                this.Height = 720;

                IsMaximize = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;

                IsMaximize = true;
            }
        }
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            this.DragMove();
        }
    }
    private void OpenModal_Click(object sender, RoutedEventArgs e)
    {
        // Assuming 'MyModalDialog' is a Window that represents your modal dialog
        var modalDialog = new ConsumerInsertForm
        {
            Owner = this // Set the owner window
        };
        modalDialog.ShowDialog(); // This will show your modal dialog
    }
    public class GetAllMedicinesResponse
    {
        [JsonProperty("medicines")]
        public List<MedicineViewModel> Medicines { get; set; }
    }
    public sealed record Member(int Id, string? Name, string? SurName, string? PhoneNumber, string? Email, string? Address);
    //public sealed record MedicineViewModel(int Id, string? Name, decimal? Price);

    public class MedicineViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
}