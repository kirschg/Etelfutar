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
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
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
            if (tbx_felhasznalo_nev.Text != "")
            {
                if (tbx_email_cim.Text != "")
                {
                    if (tbx_varos_id.Text != "" && int.TryParse(tbx_varos_id.Text, out int tbx_varos_id_int))
                    {
                        if (tbx_lakcim.Text != "")
                        {
                            if (pbx_jelszo.Password != "" && pbx_jelszo_ujra.Password != "")
                            {
                                if (pbx_jelszo.Password == pbx_jelszo_ujra.Password)
                                {
                                    if(tbx_jogosultsag.Text != "" && int.TryParse(tbx_jogosultsag.Text,out int tbx_jogosultag_int))
                                    {
                                        //Ha minden adatot megadtunk
                                        string salt = MainWindow.GenerateSalt();
                                        string hashedPassword = MainWindow.CreateSHA256(pbx_jelszo.Password + salt);
                                        string doubleHashedPassword = MainWindow.CreateSHA256(hashedPassword);
                                        Felhasznalok uj_felhasznalo = new Felhasznalok
                                        {
                                            FelhasznaloNev = tbx_felhasznalo_nev.Text,
                                            TeljesNev = tbx_teljes_nev.Text,
                                            Email = tbx_email_cim.Text,
                                            VarosId = int.Parse(tbx_varos_id.Text),
                                            Lakcim = tbx_lakcim.Text,
                                            Hash = doubleHashedPassword,
                                            Salt = salt,
                                            Jogosultsag = int.Parse(tbx_jogosultsag.Text)
                                        };
                                        try
                                        {
                                            string json = JsonSerializer.Serialize(uj_felhasznalo, JsonSerializerOptions.Default);
                                            MessageBox.Show(json);
                                            var body = new StringContent(json, Encoding.UTF8, "application/json");
                                            var result = await MainWindow.sharedClient.PostAsync("Felhasznalok/PostFelhasznaloAsync", body);
                                            result.Content.ReadAsStringAsync().Wait();
                                            if (result.IsSuccessStatusCode)
                                            {
                                                MessageBox.Show("Sikeres mentés.");
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
                                        MessageBox.Show("Nincs megadva jogosultság!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("A jelszavak nem egyeznek meg!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Nincs megadva jelszó!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nincs megadva lakcím!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nincs megadva városId!");
                    }
                }
                else
                {
                    MessageBox.Show("Nincs megadva email cím!");
                }
            }
            else
            {
                MessageBox.Show("Nincs megadva felhasználónév!");
            }
        }
    }
}
