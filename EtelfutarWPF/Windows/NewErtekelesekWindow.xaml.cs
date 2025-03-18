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
    /// Interaction logic for NewErtekelesekWindow.xaml
    /// </summary>
    public partial class NewErtekelesekWindow : Window
    {
        public NewErtekelesekWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_felhasznalo_id.Text != "" && int.TryParse(tbx_felhasznalo_id.Text, out int tbx_felhasznalo_id_int))
            {
                if (tbx_etterem_id.Text != "" && int.TryParse(tbx_etterem_id.Text, out int tbx_etterem_id_int))
                {
                    if (tbx_szoveg.Text != "")
                    {
                        if (tbx_ertekeles.Text != "" && int.TryParse(tbx_ertekeles.Text, out int tbx_ertekeles_int))
                        {
                            //Ha minden adatot megadtunk
                            Ertekelesek uj_ertekeles = new Ertekelesek
                            {
                                Id = 0,
                                FelhasznaloId = int.Parse(tbx_felhasznalo_id.Text),
                                EtteremId = int.Parse(tbx_etterem_id.Text),
                                Szoveg = tbx_szoveg.Text,
                                Ertekeles = int.Parse(tbx_ertekeles.Text)
                            };
                            try
                            {
                                string json = JsonSerializer.Serialize(uj_ertekeles, JsonSerializerOptions.Default);
                                MessageBox.Show(json);
                                var body = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await MainWindow.sharedClient.PostAsync("Ertekeles/POST/Értékelés", body);
                                result.Content.ReadAsStringAsync().Wait();
                                if (result.IsSuccessStatusCode)
                                {
                                    MessageBox.Show("Sikeres mentés.");
                                }
                                else
                                {
                                    MessageBox.Show($"Sikertelen mentés!\n{result.StatusCode}");
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
                            MessageBox.Show("Nincs megadva Értékelés!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nincs megadva Szöveg!");
                    }
                }
                else
                {
                    MessageBox.Show("Nincs megadva Étterem Id!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva Felhasználó Id!");
            }
        }
    }
}
