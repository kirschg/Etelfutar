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
    /// Interaction logic for EditEttermekWindow.xaml
    /// </summary>
    public partial class EditEttermekWindow : Window
    {
        public static Ettermek kivalasztott_etterem = null;
        public EditEttermekWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_cim.Text = kivalasztott_etterem.Cim;
            tbx_chain_id.Text = kivalasztott_etterem.ChainId.ToString();
            tbx_varos_id.Text = kivalasztott_etterem.VarosId.ToString();
            tbx_index_kep.Text = kivalasztott_etterem.Indexkep;
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
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
                            kivalasztott_etterem.Cim = tbx_cim.Text;
                            kivalasztott_etterem.ChainId = int.Parse(tbx_chain_id.Text);
                            kivalasztott_etterem.VarosId = int.Parse(tbx_varos_id.Text);
                            kivalasztott_etterem.Indexkep = tbx_index_kep.Text;
                            try
                            {
                                string json = JsonSerializer.Serialize(kivalasztott_etterem, JsonSerializerOptions.Default);
                                MessageBox.Show(json);
                                var body = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await MainWindow.sharedClient.PutAsync("Ettermek/PutEtteremAsync", body);
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
                MessageBox.Show("Nincs felhasználó cím!");
            }
        }
    }
}
