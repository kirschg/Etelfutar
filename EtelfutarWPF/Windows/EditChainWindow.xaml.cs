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
    /// Interaction logic for EditChainWindow.xaml
    /// </summary>
    public partial class EditChainWindow : Window
    {
        public static Chain kivalasztott_chain = null;
        public EditChainWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_nev.Text = kivalasztott_chain.Nev;
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_nev.Text != "")
            {
                //Ha minden adatot megadtunk
                kivalasztott_chain.Nev = tbx_nev.Text;
                try
                {
                    string json = JsonSerializer.Serialize(kivalasztott_chain, JsonSerializerOptions.Default);
                    MessageBox.Show(json);
                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                    var result = await MainWindow.sharedClient.PutAsync("Chain/PutChainAsync", body);
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
                MessageBox.Show("Nincs megadva Név!");
            }
        }
    }
}
