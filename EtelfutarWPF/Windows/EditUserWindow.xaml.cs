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

namespace EtelfutarWPF
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public static Felhasznalok kivalasztott_felhasznalo = null;
        public EditUserWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_felhasznalo_nev.Text = kivalasztott_felhasznalo.FelhasznaloNev;
            tbx_teljes_nev.Text = kivalasztott_felhasznalo.TeljesNev;
            tbx_email_cim.Text = kivalasztott_felhasznalo.Email;
            tbx_varos_id.Text = kivalasztott_felhasznalo.VarosId.ToString();
            tbx_lakcim.Text = kivalasztott_felhasznalo.Lakcim;
            pbx_jelszo.Password = "";
            pbx_jelszo_ujra.Password = "";
            tbx_jogosultsag.Text = kivalasztott_felhasznalo.Jogosultsag.ToString();
            tbx_aktiv.Text = kivalasztott_felhasznalo.Aktiv.ToString();
        }
        private void Megse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
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
                                    if (tbx_jogosultsag.Text != "" && int.TryParse(tbx_jogosultsag.Text, out int tbx_jogosultag_int))
                                    {
                                        if (tbx_aktiv.Text != "" && int.TryParse(tbx_aktiv.Text, out int tbx_aktiv_int))
                                        {
                                            //Ha minden adatot megadtunk
                                            string salt = MainWindow.GenerateSalt();
                                            string hashedPassword = MainWindow.CreateSHA256(pbx_jelszo.Password + salt);
                                            string doubleHashedPassword = MainWindow.CreateSHA256(hashedPassword);
                                            kivalasztott_felhasznalo.FelhasznaloNev = tbx_felhasznalo_nev.Text;
                                            kivalasztott_felhasznalo.Email = tbx_email_cim.Text;
                                            kivalasztott_felhasznalo.Salt = salt;
                                            kivalasztott_felhasznalo.TeljesNev = tbx_teljes_nev.Text;
                                            kivalasztott_felhasznalo.Lakcim = tbx_lakcim.Text;
                                            kivalasztott_felhasznalo.Hash = doubleHashedPassword;
                                            kivalasztott_felhasznalo.VarosId = int.Parse(tbx_varos_id.Text);
                                            kivalasztott_felhasznalo.Jogosultsag = int.Parse(tbx_jogosultsag.Text);
                                            kivalasztott_felhasznalo.Aktiv = int.Parse(tbx_aktiv.Text);
                                            try
                                            {
                                                string json = JsonSerializer.Serialize(kivalasztott_felhasznalo, JsonSerializerOptions.Default);
                                                MessageBox.Show(json);
                                                var body = new StringContent(json, Encoding.UTF8, "application/json");
                                                var result = await MainWindow.sharedClient.PutAsync("Felhasznalok/PutFelhasznaloAsync", body);
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
                                            MessageBox.Show("Nincs megadva aktív-e!");
                                        }
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
