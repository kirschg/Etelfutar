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

namespace EtelfutarWPF
{
    /// <summary>
    /// Interaction logic for EditRendelesWindow.xaml
    /// </summary>
    public partial class EditRendelesWindow : Window
    {
        public static Rendeles kivalasztott_rendeles = null;
        public EditRendelesWindow()
        {
            InitializeComponent();
            
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_felhasznalo_id.Text != "" && int.TryParse(tbx_felhasznalo_id.Text,out int tbx_felhasznalo_id_int))
            {
                if (tbx_ossz_ar.Text != "" && int.TryParse(tbx_ossz_ar.Text,out int tbx_ossz_ar_int))
                {
                    //Ha minden adatot megadtunk
                    kivalasztott_rendeles.FelhasznaloId = int.Parse(tbx_felhasznalo_id.Text);
                    kivalasztott_rendeles.OsszAr = int.Parse(tbx_ossz_ar.Text);
                    try
                    {
                        string json = JsonSerializer.Serialize(kivalasztott_rendeles, JsonSerializerOptions.Default);
                        MessageBox.Show(json);
                        var body = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = await MainWindow.sharedClient.PutAsync("Felhasznalok/PutFelhasznaloAsync", body);
                        result.Content.ReadAsStringAsync().Wait();
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sikeres módosítás.");
                        }
                        else
                        {
                            MessageBox.Show(result.RequestMessage.ToString());
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
                    MessageBox.Show("Nincs megadva össz ár!");
                }
            }
            else
            {
                MessageBox.Show("Nincs felhasználó id!");
            }
        }
    }
}
