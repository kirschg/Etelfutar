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
    /// Interaction logic for EditErtekelesekWindow.xaml
    /// </summary>
    public partial class EditErtekelesekWindow : Window
    {
        public static Ertekelesek kivalasztott_ertekeles = null;
        public EditErtekelesekWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_felhasznalo_id.Text = kivalasztott_ertekeles.FelhasznaloId.ToString();
            tbx_etterem_id.Text = kivalasztott_ertekeles.EtteremId.ToString();
            tbx_szoveg.Text = kivalasztott_ertekeles.Szoveg.ToString();
            tbx_ertekeles.Text = kivalasztott_ertekeles.Ertekeles.ToString();
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_felhasznalo_id.Text != "" && int.TryParse(tbx_felhasznalo_id.Text,out int tbx_felhasznalo_id_int))
            {
                if (tbx_etterem_id.Text != "" && int.TryParse(tbx_etterem_id.Text, out int tbx_etterem_id_int))
                {
                    if (tbx_szoveg.Text != "")
                    {
                        if (tbx_ertekeles.Text != "" && int.TryParse(tbx_ertekeles.Text, out int tbx_ertekeles_int))
                        {
                            //Ha minden adatot megadtunk
                            kivalasztott_ertekeles.FelhasznaloId = int.Parse(tbx_felhasznalo_id.Text);
                            kivalasztott_ertekeles.EtteremId = int.Parse(tbx_etterem_id.Text);
                            kivalasztott_ertekeles.Szoveg = tbx_szoveg.Text;
                            kivalasztott_ertekeles.Ertekeles = int.Parse(tbx_ertekeles.Text);
                            try
                            {
                                string json = JsonSerializer.Serialize(kivalasztott_ertekeles, JsonSerializerOptions.Default);
                                MessageBox.Show(json);
                                var body = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await MainWindow.sharedClient.PutAsync("Ertekeles/PUT/Értékelés", body);
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
                            MessageBox.Show("Nem megadva Értékelés!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nem megadva Szöveg!");
                    }
                }
                else
                {
                    MessageBox.Show("Nem megadva Étterem Id!");
                }
            }
            else
            {
                MessageBox.Show("Nem megadva Felhasználó Id!");
            }
        }
    }
}
