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
    /// Interaction logic for NewEtelekWindow.xaml
    /// </summary>
    public partial class NewEtelekWindow : Window
    {
        public NewEtelekWindow()
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
                if (tbx_kaloria.Text != "" && int.TryParse(tbx_kaloria.Text, out int tbx_kaloria_int))
                {
                    if (tbx_ar.Text != "" && int.TryParse(tbx_ar.Text, out int tbx_ar_int))
                    {
                        if (tbx_chain_id.Text != "" && int.TryParse(tbx_chain_id.Text, out int tbx_chain_id_int))
                        {
                            if (tbx_index_kep.Text != "")
                            {
                                //Ha minden adatot megadtunk
                                Etelek uj_etel = new Etelek
                                {
                                    Nev = tbx_nev.Text,
                                    Kaloria = int.Parse(tbx_kaloria.Text),
                                    Ar = int.Parse(tbx_ar.Text),
                                    ChainId = int.Parse(tbx_chain_id.Text),
                                    Indexkep = tbx_index_kep.Text
                                };
                                try
                                {
                                    string json = JsonSerializer.Serialize(uj_etel, JsonSerializerOptions.Default);
                                    MessageBox.Show(json);
                                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                                    var result = await MainWindow.sharedClient.PostAsync("Etelek/PostEtelAsync", body);
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
                            MessageBox.Show("Nincs megadva Chain Id!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nincs megadva Ár!");
                    }
                }
                else
                {
                    MessageBox.Show("Nincs megadva Kalória!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva Név!");
            }
        }
    }
}
