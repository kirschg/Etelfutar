using EtelfutarWPF.DTOs;
using EtelfutarWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EtelfutarWPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public HttpClient? client;
        public RegisterWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
        }

        
        private async void Regisztracio_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_felhasznalo_nev.Text != "")
            {
                if (tbx_email_cim.Text != "")
                {
                    if (tbx_varos_id.Text != "" && int.TryParse(tbx_varos_id.Text,out int tbx_varos_id_int))
                    {
                        if (tbx_lakcim.Text != "")
                        {
                            if(pbx_jelszo.Password != "" && pbx_jelszo_ujra.Password != "")
                            {
                                if (pbx_jelszo.Password == pbx_jelszo_ujra.Password)
                                {
                                    //Ha minden adatot megadtunk
                                    string salt = MainWindow.GenerateSalt();
                                    string hashedPassword = MainWindow.CreateSHA256(pbx_jelszo.Password + salt);
                                    RegistryFelhasznalokDTO ujFelhasznalo = new RegistryFelhasznalokDTO()
                                    {
                                        Email = tbx_email_cim.Text,
                                        FelhasznaloNev = tbx_felhasznalo_nev.Text,
                                        TeljesNev = tbx_teljes_nev.Text,
                                        Hash = hashedPassword,
                                        VarosId = int.Parse(tbx_varos_id.Text),
                                        LakCim = tbx_lakcim.Text,
                                        Salt = salt
                                    };
                                    try
                                    {
                                        string json = JsonSerializer.Serialize(ujFelhasznalo, JsonSerializerOptions.Default);
                                        MessageBox.Show(json);
                                        var body = new StringContent(json, Encoding.UTF8, "application/json");
                                        var result = await client.PostAsync("api/Registry", body);
                                        MessageBox.Show("Sikeres regisztráció. Ellenőrizze az emailjeit és erősítse meg az email címét!");
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
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

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
