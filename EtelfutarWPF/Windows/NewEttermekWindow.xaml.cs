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
    /// Interaction logic for NewEttermekWindow.xaml
    /// </summary>
    public partial class NewEttermekWindow : Window
    {
        public NewEttermekWindow()
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
            if (tbx_cim.Text != "")
            {
                if (tbx_chain_id.Text != "" && int.TryParse(tbx_chain_id.Text, out int tbx_chain_id_int))
                {
                    if (tbx_varos_id.Text != "" && int.TryParse(tbx_varos_id.Text, out int tbx_varos_id_int))
                    {
                        if (tbx_index_kep.Text != "")
                        {
                            //Ha minden adatot megadtunk
                            Ettermek uj_etterem = new Ettermek
                            {
                                Cim = tbx_cim.Text,
                                ChainId = int.Parse(tbx_chain_id.Text),
                                VarosId = int.Parse(tbx_varos_id.Text),
                                Indexkep = tbx_index_kep.Text
                            };
                            try
                            {
                                string json = JsonSerializer.Serialize(uj_etterem, JsonSerializerOptions.Default);
                                MessageBox.Show(json);
                                var body = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await MainWindow.sharedClient.PostAsync("Ettermek/PostEtteremAsync", body);
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
                        MessageBox.Show("Nincs megadva Város Id!");
                    }
                }
                else
                {
                    MessageBox.Show("Nincs megadva Chain Id!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva Cím!");
            }
        }
    }
}
