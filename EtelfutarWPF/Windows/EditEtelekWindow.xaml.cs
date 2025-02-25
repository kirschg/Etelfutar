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
    /// Interaction logic for EditEtelekWindow.xaml
    /// </summary>
    public partial class EditEtelekWindow : Window
    {
        public static Etelek kivalasztott_etel = null;
        public EditEtelekWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_nev.Text = kivalasztott_etel.Nev;
            tbx_kaloria.Text = kivalasztott_etel.Kaloria.ToString();
            tbx_ar.Text = kivalasztott_etel.Ar.ToString();
            tbx_chain_id.Text = kivalasztott_etel.ChainId.ToString();
            tbx_index_kep.Text = kivalasztott_etel.Indexkep;
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
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
                                kivalasztott_etel.Nev = tbx_nev.Text;
                                kivalasztott_etel.Kaloria = int.Parse(tbx_kaloria.Text);
                                kivalasztott_etel.Ar = int.Parse(tbx_ar.Text);
                                kivalasztott_etel.ChainId = int.Parse(tbx_chain_id.Text);
                                kivalasztott_etel.Indexkep = tbx_index_kep.Text;
                                try
                                {
                                    string json = JsonSerializer.Serialize(kivalasztott_etel, JsonSerializerOptions.Default);
                                    MessageBox.Show(json);
                                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                                    var result = await MainWindow.sharedClient.PutAsync("Etelek/PutEtelAsync", body);
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
                    MessageBox.Show("Nincs megadva Kalória!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva Cím!");
            }
        }
    }
}
