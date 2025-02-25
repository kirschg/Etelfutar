using EtelfutarWPF.DTOs;
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

namespace EtelfutarWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public HttpClient? client;

        public LoginWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
        }

        private async void Bejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_felhasznalo_nev.Text != "")
            {
                if(pbx_jelszo.Password != "")
                {
                    //Ha minden adatot megadtunk
                    try
                    {
                        var response = await client.PostAsync($"api/Login/GetSalt/{tbx_felhasznalo_nev.Text}", new StringContent(pbx_jelszo.Password, Encoding.UTF8, "text/plain"));
                        string salt = "";
                        if (response.IsSuccessStatusCode)
                        {
                            salt = await response.Content.ReadAsStringAsync();
                            MessageBox.Show(salt);
                            //innen pwd+salt hash és mehet a login
                            try
                            {
                                string tmpHash = MainWindow.CreateSHA256(pbx_jelszo.Password + salt);
                                LoginDTO loginDTO = new LoginDTO()
                                {
                                    LoginName = tbx_felhasznalo_nev.Text,
                                    TmpHash = tmpHash
                                };

                                string json = JsonSerializer.Serialize(loginDTO, JsonSerializerOptions.Default);
                                MessageBox.Show(json);
                                var body = new StringContent(json, Encoding.UTF8, "application/json");
                                var result = await client.PostAsync("api/Login", body);
                                if (result.IsSuccessStatusCode)
                                {
                                    var options = new JsonSerializerOptions
                                    {
                                        WriteIndented = true,
                                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                                        PropertyNameCaseInsensitive = true
                                    };
                                    string valaszJson = await result.Content.ReadAsStringAsync();
                                    LoggedUser loggedUser = JsonSerializer.Deserialize<LoggedUser>(valaszJson, options);
                                    MessageBox.Show(loggedUser.Token);
                                    MainWindow.token = loggedUser.Token;
                                    MainWindow.jogosultsag = loggedUser.Jogosultsag;
                                }
                                else
                                {
                                    string valasz = await result.Content.ReadAsStringAsync();
                                    MessageBox.Show($"Valami nem ok!\n{valasz}");
                                }
                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Sikertelen bejelentkezés.");
                        }
                        Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Hiányzó jelszó!");
                }
            }
            else
            {
                MessageBox.Show("Hiányzó felhasználó név!");
            }
        }

        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
        }

    }
}
