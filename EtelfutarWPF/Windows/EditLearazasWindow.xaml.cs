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
    /// Interaction logic for EditLearazasWindow.xaml
    /// </summary>
    public partial class EditLearazasWindow : Window
    {
        public static Learaza kivalasztott_learazas = null;
        public EditLearazasWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_etterem_id.Text = kivalasztott_learazas.EtteremId.ToString();
            tbx_etel_id.Text = kivalasztott_learazas.EtelId.ToString();
            tbx_learazas.Text = kivalasztott_learazas.Learazas.ToString();
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_etterem_id.Text != "" && int.TryParse(tbx_etterem_id.Text, out int tbx_etterem_id_int))
            {
                if (tbx_etel_id.Text != "" && int.TryParse(tbx_etel_id.Text, out int tbx_etel_id_int))
                {
                    if (tbx_learazas.Text != "" && int.TryParse(tbx_learazas.Text, out int tbx_learazas_int))
                    {
                        //Ha minden adatot megadtunk
                        kivalasztott_learazas.EtteremId = int.Parse(tbx_etterem_id.Text);
                        kivalasztott_learazas.EtelId = int.Parse(tbx_etel_id.Text);
                        kivalasztott_learazas.Learazas = int.Parse(tbx_learazas.Text);
                        try
                        {
                            string json = JsonSerializer.Serialize(kivalasztott_learazas, JsonSerializerOptions.Default);
                            MessageBox.Show(json);
                            var body = new StringContent(json, Encoding.UTF8, "application/json");
                            var result = await MainWindow.sharedClient.PutAsync("Learazas/PutLearazasAsync", body);
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
                        MessageBox.Show("Nincs megadva Leárazás!");
                    }
                }
                else
                {
                    MessageBox.Show("Nincs megadva Étel Id!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva Étterem Id!");
            }
        }
    }
}
