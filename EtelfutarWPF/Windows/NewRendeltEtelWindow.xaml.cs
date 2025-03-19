using EtelfutarWPF.DTOs;
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
    /// Interaction logic for NewRendeltEtelWindow.xaml
    /// </summary>
    public partial class NewRendeltEtelWindow : Window
    {
        public HttpClient? client;
        public NewRendeltEtelWindow()
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
                if (tbx_rendeles_id.Text != "" && int.TryParse(tbx_rendeles_id.Text, out int tbx_rendeles_id_int))
                {
                    //Ha minden adatot megadtunk
                    try
                    {
                        string json = JsonSerializer.Serialize("", JsonSerializerOptions.Default);
                        var body = new StringContent(json, Encoding.UTF8, "application/json");
                        var result = await client.PostAsync($"{client.BaseAddress}Rendeltetel/PostRendeltetelAsync?etelId={tbx_etel_id.Text}&rendelesId={tbx_rendeles_id.Text}",body);
                        if (result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sikeres mentés.");
                        }
                        else
                        {
                            MessageBox.Show($"Error: {result}\n" + $"{client.BaseAddress}Rendeltetel/PostRendeltetelAsync?id={tbx_etel_id}&rendelesId={tbx_rendeles_id.Text}");
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
