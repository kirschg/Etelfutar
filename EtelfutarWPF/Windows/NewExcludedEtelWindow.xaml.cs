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
    /// Interaction logic for NewExcludedEtelWindow.xaml
    /// </summary>
    public partial class NewExcludedEtelWindow : Window
    {
        public HttpClient? client;
        public NewExcludedEtelWindow()
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
            if (tbx_etel_id.Text != "" && int.TryParse(tbx_etel_id.Text, out int tbx_etel_id_int))
            {
                if (tbx_etterem_id.Text != "" && int.TryParse(tbx_etterem_id.Text, out int tbx_etterem_id_int))
                {
                    //Ha minden adatot megadtunk
                    try
                    {
                        string json = JsonSerializer.Serialize("", JsonSerializerOptions.Default);
                        var body = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = await client.PostAsync($"{client.BaseAddress}Excludedetel/PostExcludedetelAsync?etelId={tbx_etel_id.Text}&etteremId={tbx_etterem_id.Text}", body);
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sikeres mentés.");
                        }
                        else
                        {
                            MessageBox.Show($"Error: {result}\n" + $"{client.BaseAddress}Excludedetel/PostExcludedetelAsync?id={tbx_etel_id}&etteremId={tbx_etterem_id.Text}");
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
