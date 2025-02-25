using EtelfutarWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EtelfutarWPF.Windows
{
    /// <summary>
    /// Interaction logic for NewRendelesWindow.xaml
    /// </summary>
    public partial class NewRendelesWindow : Window
    {
        public NewRendelesWindow()
        {
            InitializeComponent();
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_felhasznalo_id.Text != "" && int.TryParse(tbx_felhasznalo_id.Text, out int tbx_felhasznalo_id_int))
            {
                if (tbx_ossz_ar.Text != "" && int.TryParse(tbx_ossz_ar.Text,out int tbx_ossz_ar_int))
                {
                    //Ha minden adatot megadtunk
                    Rendeles uj_rendeles = new Rendeles
                    {
                        FelhasznaloId = int.Parse(tbx_felhasznalo_id.Text),
                        OsszAr = int.Parse(tbx_ossz_ar.Text)
                    };
                    try
                    {
                        string json = JsonSerializer.Serialize(uj_rendeles, JsonSerializerOptions.Default);
                        MessageBox.Show(json);
                        var body = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = await MainWindow.sharedClient.PostAsync("Varosok/PostVarosAsync", body);
                        result.Content.ReadAsStringAsync().Wait();
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sikeres mentés.");
                        }
                        else
                        {
                            MessageBox.Show("Sikertelen mentés!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show("Nincs megadva Index Kép!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva Név!");
            }
        }
    }
}
