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
    /// Interaction logic for NewVarosokWindow.xaml
    /// </summary>
    public partial class NewVarosokWindow : Window
    {
        public NewVarosokWindow()
        {
            InitializeComponent();
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Mentes_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_nev.Text != "")
            {
                if (tbx_index_kep.Text != "")
                {
                    //Ha minden adatot megadtunk
                    Varosok uj_varos = new Varosok
                    {
                        Nev = tbx_nev.Text,
                        IndexKep = tbx_index_kep.Text
                    };
                    try
                    {
                        string json = JsonSerializer.Serialize(uj_varos, JsonSerializerOptions.Default);
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
