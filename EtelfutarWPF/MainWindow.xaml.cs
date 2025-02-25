using EtelfutarWPF.DTOs;
using EtelfutarWPF.Models;
using EtelfutarWPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Security.Principal;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EtelfutarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string token = null;
        public static int jogosultsag = -1;
        public static string client_address = "http://localhost:5000";
        public static List<Felhasznalok> felhasznalok2 = new List<Felhasznalok>();
        public static List<Varosok> varosok2 = new List<Varosok>();
        public static List<Rendeles> rendeles2 = new List<Rendeles>();
        public static List<Learaza> learazas2 = new List<Learaza>();
        public static List<Ettermek> ettermek2 = new List<Ettermek>();
        public static List<Etelek> etelek2 = new List<Etelek>();
        public static List<Ertekelesek> ertekelesek2 = new List<Ertekelesek>();
        public static List<Chain> chain2 = new List<Chain>();

        public static HttpClient sharedClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        public static int SaltLength = 64;
        public MainWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            cbx_tablazatok.Items.Add("Felhasználók");
            cbx_tablazatok.Items.Add("Ételek");
            cbx_tablazatok.Items.Add("Rendelt Étel");
            cbx_tablazatok.Items.Add("Leárazás");
            cbx_tablazatok.Items.Add("Éttermek");
            cbx_tablazatok.Items.Add("Értékelések");
            cbx_tablazatok.Items.Add("Chain");
            cbx_tablazatok.Items.Add("Rendelés");
            cbx_tablazatok.Items.Add("Városok");
            cbx_tablazatok.SelectedIndex = -1;

        }

        private void Bejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.client = sharedClient;
            token = null;
            loginWindow.ShowDialog();
            if(token is not null)
            {
                menu_kijelentkezes.IsEnabled = true;
                menu_bejelentkezes.IsEnabled = false;
                if (jogosultsag > 0)
                {
                    cbx_tablazatok.IsEnabled = true;
                }
            }
            else
            {
                cbx_tablazatok.IsEnabled = false;
                menu_kijelentkezes.IsEnabled = false;
                btn_torles.IsEnabled = false;
                btn_modositas.IsEnabled = false;
                btn_uj.IsEnabled = false;
                dgr_adatok.ItemsSource = null;
                jogosultsag = -1;
            }
        }

        private void Regisztracio_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.client = sharedClient;
            registerWindow.ShowDialog();
        }

        private async void Kijelentkezes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = JsonSerializer.Serialize(token, JsonSerializerOptions.Default);
                MessageBox.Show(json);
                var body = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await sharedClient.PostAsync("api/Logout", body);
                MessageBox.Show("Sikeres kijelentkezés.");
                cbx_tablazatok.IsEnabled = false;
                menu_kijelentkezes.IsEnabled = false;
                dgr_adatok.ItemsSource = null;
                token = null;
                jogosultsag = -1;
                btn_torles.IsEnabled = false;
                btn_modositas.IsEnabled = false;
                btn_uj.IsEnabled = false;
                menu_bejelentkezes.IsEnabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void Beallitasok_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private async void Torles_Click(object sender, RoutedEventArgs e)
        {
            if (dgr_adatok.SelectedItem is not null)
            {
                switch (cbx_tablazatok.SelectedValue.ToString())
                {
                    case "Felhasználók":
                        felhasznalok2.Remove((Felhasznalok)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Felhasznalok/DeleteFelhasznaloAsync?id={((Felhasznalok)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Felhasznalok/DeleteFelhasznaloAsync?id={((Felhasznalok)dgr_adatok.SelectedItem).Id}");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //datagrid frissítése
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = felhasznalok2;
                        break;
                    case "Városok":
                        
                        break;
                    case "Rendelt Étel":
                        break;
                    case "Rendelés":
                        
                        break;
                    case "Leárazás":
                        break;
                    case "Éttermek":
                        
                        break;
                    case "Ételek":
                        break;
                    case "Értékelések":
                        break;
                    case "Chain":
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva elem!");
            }
        }
        private async void Modositas_Click(object sender, RoutedEventArgs e)
        {
            if (dgr_adatok.SelectedItem is not null)
            {
                switch (cbx_tablazatok.SelectedValue.ToString())
                {
                    case "Felhasználók":
                        EditUserWindow.kivalasztott_felhasznalo = (Felhasznalok)dgr_adatok.SelectedItem;
                        EditUserWindow editUserWindow = new EditUserWindow();
                        editUserWindow.ShowDialog();
                        List<Felhasznalok> felhasznalok = await sharedClient.GetFromJsonAsync<List<Felhasznalok>>("Felhasznalok/GetFelhasznalokAsync");
                        felhasznalok2 = felhasznalok;
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = felhasznalok2;
                        break;
                    case "Városok":
                        EditVarosokWindow.kivalasztott_varos = (Varosok)dgr_adatok.SelectedItem;
                        EditVarosokWindow editVarosokWindow = new EditVarosokWindow();
                        editVarosokWindow.ShowDialog();
                        List<Varosok> varosok = await sharedClient.GetFromJsonAsync<List<Varosok>>("Varosok/GetVarosokAsync");
                        varosok2 = varosok;
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = varosok2;
                        break;
                    case "Rendelt Étel":
                        break;
                    case "Rendelés":
                        EditRendelesWindow.kivalasztott_rendeles = (Rendeles)dgr_adatok.SelectedItem;
                        EditRendelesWindow editRendelesWindow = new EditRendelesWindow();
                        editRendelesWindow.ShowDialog();
                        List<Rendeles>? rendeles = await sharedClient.GetFromJsonAsync<List<Rendeles>>("Rendeles/GetRendelesAsync");
                        rendeles2 = rendeles;
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = rendeles2;
                        break;
                    case "Leárazás":
                        break;
                    case "Éttermek":
                        EditEttermekWindow.kivalasztott_etterem = (Ettermek)dgr_adatok.SelectedItem;
                        EditEttermekWindow editEttermekWindow = new EditEttermekWindow();
                        editEttermekWindow.ShowDialog();
                        List<Ettermek>? ettermek = await sharedClient.GetFromJsonAsync<List<Ettermek>>("Ettermek/GetEttermekAsync");
                        ettermek2 = ettermek;
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = ettermek2;
                        break;
                    case "Ételek":
                        EditEtelekWindow.kivalasztott_etel = (Etelek)dgr_adatok.SelectedItem;
                        EditEtelekWindow editEtelekWindow = new EditEtelekWindow();
                        editEtelekWindow.ShowDialog();
                        List<Etelek>? etelek = await sharedClient.GetFromJsonAsync<List<Etelek>>("Etelek/GetEtelekAsync");
                        etelek2 = etelek;
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = etelek2;
                        break;
                    case "Értékelések":
                        break;
                    case "Chain":
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva elem!");
            }
        }

        private async void Uj_Click(object sender, RoutedEventArgs e)
        {
            switch (cbx_tablazatok.SelectedValue.ToString())
            {
                case "Felhasználók":
                    NewUserWindow newUserWindow = new NewUserWindow();
                    newUserWindow.ShowDialog();
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = felhasznalok2;
                    break;
                case "Városok":
                   
                    break;
                case "Rendelt Étel":
                    break;
                case "Rendelés":
                    
                    break;
                case "Leárazás":
                    break;
                case "Éttermek":
                    
                    break;
                case "Ételek":
                    break;
                case "Értékelések":
                    break;
                case "Chain":
                    break;
                default:
                    break;
            }
        }

        private async void cbx_tablazatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbx_tablazatok.SelectedValue.ToString())
            {
                case "Felhasználók":
                    try
                    {
                        List<Felhasznalok>? felhasznalok = await sharedClient.GetFromJsonAsync<List<Felhasznalok>>("Felhasznalok/GetFelhasznalokAsync");
                        felhasznalok2 = felhasznalok;
                        dgr_adatok.ItemsSource = felhasznalok;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sikertelen betöltés!");
                    }
                    break;
                case "Városok":
                    try
                    {
                        List<Varosok>? varosok = await sharedClient.GetFromJsonAsync<List<Varosok>>("Varosok/GetVarosokAsync");
                        varosok2 = varosok;
                        dgr_adatok.ItemsSource = varosok;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sikertelen betöltés!");
                    }
                    break;
                case "Rendelt Étel":
                    break;
                case "Rendelés":
                    try
                    {
                        List<Rendeles>? rendeles = await sharedClient.GetFromJsonAsync<List<Rendeles>>("Rendeles/GetRendelesekAsync");
                        rendeles2 = rendeles;
                        dgr_adatok.ItemsSource = rendeles;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Leárazás":
                    break;
                case "Éttermek":
                    try
                    {
                        List<Ettermek>? ettermek = await sharedClient.GetFromJsonAsync<List<Ettermek>>("Ettermek/GetEttermekAsync");
                        ettermek2 = ettermek;
                        dgr_adatok.ItemsSource = ettermek;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sikertelen betöltés!");
                    }
                    break;
                case "Ételek":
                    try
                    {
                        List<Etelek>? etelek = await sharedClient.GetFromJsonAsync<List<Etelek>>("Etelek/GetEtelekAsync");
                        etelek2 = etelek;
                        dgr_adatok.ItemsSource = etelek;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Sikertelen betöltés!");
                    }
                    break;
                case "Értékelések":
                    break;
                case "Chain":
                    break;
                default:
                    break;
            }
        }

        public static string GenerateSalt()
        {
            Random random = new Random();
            string karakterek = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string salt = "";
            for (int i = 0; i < SaltLength; i++)
            {
                salt += karakterek[random.Next(karakterek.Length)];
            }
            return salt;
        }
        public static string CreateSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
