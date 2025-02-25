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
    /// Interaction logic for EditVarosokWindow.xaml
    /// </summary>
    public partial class EditVarosokWindow : Window
    {
        public static Varosok kivalasztott_varos = null;
        public EditVarosokWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_nev.Text = kivalasztott_varos.Nev;
            tbx_index_kep.Text = kivalasztott_varos.IndexKep;
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_nev.Text != "")
            {
                if (tbx_index_kep.Text != "")
                {
                    //Ha minden adatot megadtunk
                    kivalasztott_varos.Nev = tbx_nev.Text;
                    kivalasztott_varos.IndexKep = tbx_index_kep.Text;
                    try
                    {
                        string json = JsonSerializer.Serialize(kivalasztott_varos, JsonSerializerOptions.Default);
                        MessageBox.Show(json);
                        var body = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = await MainWindow.sharedClient.PutAsync("Varosok/PutVarosAsync", body);
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
                    MessageBox.Show("Nincs megadva index kép!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva név!");
            }
        }
    }
}
