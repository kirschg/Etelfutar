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
using System.IO;

namespace EtelfutarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string username = null;
        public static string password = null;
        public static string token = null;
        public static int jogosultsag = -1;
        public static string client_address = "http://localhost:5000";
        public static List<Felhasznalok> felhasznalok2 = new List<Felhasznalok>();
        public static List<Varosok> varosok2 = new List<Varosok>();
        public static List<Rendeles> rendeles2 = new List<Rendeles>();
        public static List<Learaza> learazas2 = new List<Learaza>();
        public static List<Learaza> rendelt_etel2 = new List<Learaza>();
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
            try
            {
                string[] client_sorok = File.ReadAllLines("client.txt");
                client_address = client_sorok[0];
                MainWindow.sharedClient = new HttpClient()
                {
                    BaseAddress = new Uri(MainWindow.client_address)
                };
                string[] login_sorok = File.ReadAllLines("login.txt");
                username = login_sorok[0];
                password = login_sorok[1];
            }
            catch(Exception ex)
            {

            }
            AutoLogin();



        }

        public async void AutoLogin()
        {
            if(username is not null && password is not null)
            {
                try
                {
                    var response = await sharedClient.PostAsync($"api/Login/GetSalt/{username}", new StringContent(password, Encoding.UTF8, "text/plain"));
                    string salt = "";
                    if (response.IsSuccessStatusCode)
                    {
                        salt = await response.Content.ReadAsStringAsync();
                        //innen pwd+salt hash és mehet a login
                        try
                        {
                            string tmpHash = MainWindow.CreateSHA256(password + salt);
                            LoginDTO loginDTO = new LoginDTO()
                            {
                                LoginName = username,
                                TmpHash = tmpHash
                            };

                            string json = JsonSerializer.Serialize(loginDTO, JsonSerializerOptions.Default);
                            var body = new StringContent(json, Encoding.UTF8, "application/json");
                            var result = await sharedClient.PostAsync("api/Login", body);
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
                                MainWindow.token = loggedUser.Token;
                                MainWindow.jogosultsag = loggedUser.Jogosultsag;
                                BejelentkezesEllenorzese();
                            }
                            else
                            {
                                string valasz = await result.Content.ReadAsStringAsync();
                                MessageBox.Show($"Sikertelen Bejelentkezés!\n{valasz}");
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
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void Bejelentkezes_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.client = sharedClient;
            token = null;
            loginWindow.ShowDialog();
            BejelentkezesEllenorzese();
        }

        public void BejelentkezesEllenorzese()
        {
            if (token is not null)
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
                File.WriteAllText("login.txt", "");
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
            if (dgr_adatok.SelectedItem is not null && dgr_adatok.Items.Count > 0)
            {
                switch (cbx_tablazatok.SelectedValue.ToString())
                {
                    case "Felhasználók":
                        felhasznalok2.Remove((Felhasznalok)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Felhasznalok/DeleteFelhasznaloAsync?id={((FelhasznalokDTO)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Felhasznalok/DeleteFelhasznaloAsync?id={((FelhasznalokDTO)dgr_adatok.SelectedItem).Id}");
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
                        varosok2.Remove((Varosok)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Varosok/DeleteVarosAsync?id={((Varosok)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Varosok/DeleteVarosAsync?id={((Varosok)dgr_adatok.SelectedItem).Id}");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //datagrid frissítése
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = varosok2;
                        break;
                    case "Rendelt Étel":
                        
                        break;
                    case "Rendelés":
                        rendeles2.Remove((Rendeles)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Rendeles/DeleteRendelesAsync?id={((Rendeles)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Rendeles/DeleteRendelesAsync?id={((Rendeles)dgr_adatok.SelectedItem).Id}");
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
                    case "Leárazás":
                        break;
                    case "Éttermek":
                        ettermek2.Remove((Ettermek)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Ettermek/DeleteEtteremAsync?id={((Ettermek)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Ettermek/DeleteEtteremAsync?id={((Ettermek)dgr_adatok.SelectedItem).Id}");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //datagrid frissítése
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = ettermek2;
                        break;
                    case "Ételek":
                        etelek2.Remove((Etelek)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Etelek/DeleteEtelAsync?id={((Etelek)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Etelek/DeleteEtelAsync?id={((Etelek)dgr_adatok.SelectedItem).Id}");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //datagrid frissítése
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = etelek2;
                        break;
                    case "Értékelések":
                        break;
                    case "Chain":
                        chain2.Remove((Chain)dgr_adatok.SelectedItem);
                        //felhasználó törlése az adatbázisból
                        try
                        {
                            var result = await sharedClient.DeleteAsync($"{sharedClient.BaseAddress}Chain/DeleteChainAsync?id={((Chain)dgr_adatok.SelectedItem).Id}");
                            if (result.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Sikeres törlés.");
                            }
                            else
                            {
                                MessageBox.Show($"Error: {result}\n" + $"{sharedClient.BaseAddress}Chain/DeleteChainAsync?id={((Chain)dgr_adatok.SelectedItem).Id}");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        //datagrid frissítése
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = chain2;
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
            if (dgr_adatok.SelectedItem is not null && dgr_adatok.Items.Count > 0)
            {
                switch (cbx_tablazatok.SelectedValue.ToString())
                {
                    case "Felhasználók":
                        EditUserWindow.kivalasztott_felhasznalo = (Felhasznalok)dgr_adatok.SelectedItem;
                        EditUserWindow editUserWindow = new EditUserWindow();
                        editUserWindow.ShowDialog();
                        List<Felhasznalok> felhasznalok = await sharedClient.GetFromJsonAsync<List<Felhasznalok>>("Felhasznalok/GetFelhasznalokAsync");
                        felhasznalok2.Clear();
                        foreach(var felhasznalo in felhasznalok)
                        {
                            felhasznalok2.Add(felhasznalo);
                        }
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = felhasznalok2;
                        break;
                    case "Városok":
                        EditVarosokWindow.kivalasztott_varos = (Varosok)dgr_adatok.SelectedItem;
                        EditVarosokWindow editVarosokWindow = new EditVarosokWindow();
                        editVarosokWindow.ShowDialog();
                        List<Varosok> varosok = await sharedClient.GetFromJsonAsync<List<Varosok>>("Varosok/GetVarosokAsync");
                        varosok2.Clear();
                        foreach(var varos in varosok)
                        {
                            varosok2.Add(varos);
                        }
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = varosok2;
                        break;
                    case "Rendelt Étel":
                        break;
                    case "Rendelés":
                        
                        EditRendelesWindow.kivalasztott_rendeles = (Rendeles)dgr_adatok.SelectedItem;
                        EditRendelesWindow editRendelesWindow = new EditRendelesWindow();
                        editRendelesWindow.ShowDialog();
                        List<Rendeles>? rendeles = await sharedClient.GetFromJsonAsync<List<Rendeles>>("Rendeles/GetRendelesekAsync");
                        rendeles2.Clear();
                        foreach(var rendele in rendeles)
                        {
                            rendeles2.Add(rendele);
                        }
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
                        ettermek2.Clear();
                        foreach (var etterem in ettermek)
                        {
                            ettermek2.Add(etterem);
                        }
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = ettermek2;
                        break;
                    case "Ételek":
                        EditEtelekWindow.kivalasztott_etel = (Etelek)dgr_adatok.SelectedItem;
                        EditEtelekWindow editEtelekWindow = new EditEtelekWindow();
                        editEtelekWindow.ShowDialog();
                        List<EtelekDTO>? etelek = await sharedClient.GetFromJsonAsync<List<EtelekDTO>>("Etelek/GetEtelekAsync");
                        etelek2.Clear();
                        foreach (var etel in etelek)
                        {
                            etelek2.Add(new Etelek(etel));
                        }
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = etelek2;
                        break;
                    case "Értékelések":
                        break;
                    case "Chain":
                        EditChainWindow.kivalasztott_chain = (Chain)dgr_adatok.SelectedItem;
                        EditChainWindow editChainWindow = new EditChainWindow();
                        editChainWindow.ShowDialog();
                        List<Chain>? chain = await sharedClient.GetFromJsonAsync<List<Chain>>("Chain/GetChainAsync");
                        chain2.Clear();
                        foreach(var chai in chain)
                        {
                            chain2.Add(chai);
                        }
                        dgr_adatok.ItemsSource = null;
                        dgr_adatok.ItemsSource = chain2;
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
                    List<Felhasznalok>? felhasznalok = await sharedClient.GetFromJsonAsync<List<Felhasznalok>>("Felhasznalok/GetFelhasznalokAsync");
                    felhasznalok2.Clear();
                    foreach (var felhasznalo in felhasznalok)
                    {
                        felhasznalok2.Add(felhasznalo);
                    }
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = felhasznalok2;
                    break;
                case "Városok":
                    NewVarosokWindow newVarosokWindow = new NewVarosokWindow();
                    newVarosokWindow.ShowDialog();
                    List<Varosok>? varosok = await sharedClient.GetFromJsonAsync<List<Varosok>>("Varosok/GetVarosokAsync");
                    varosok2.Clear();
                    foreach(var varos in varosok)
                    {
                        varosok2.Add(varos);
                    }
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = varosok2;
                    break;
                case "Rendelt Étel":
                    break;
                case "Rendelés":
                    NewRendelesWindow newRendelesWindow = new NewRendelesWindow();
                    newRendelesWindow.ShowDialog();
                    List<Rendeles>? rendeles = await sharedClient.GetFromJsonAsync<List<Rendeles>>("Rendeles/GetRendelesekAsync");
                    rendeles2.Clear();
                    foreach(var rendele in rendeles)
                    {
                        rendeles2.Add(rendele);
                    }
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = rendeles2;
                    break;
                case "Leárazás":

                    break;
                case "Éttermek":
                    NewEttermekWindow newEttermekWindow = new NewEttermekWindow();
                    newEttermekWindow.ShowDialog();
                    List<Ettermek>? ettermek = await sharedClient.GetFromJsonAsync<List<Ettermek>>("Ettermek/GetEttermekAsync");
                    ettermek2.Clear();
                    foreach (var etterem in ettermek)
                    {
                        ettermek2.Add(etterem);
                    }
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = ettermek2;
                    break;
                case "Ételek":
                    NewEtelekWindow newEtelekWindow = new NewEtelekWindow();
                    newEtelekWindow.ShowDialog();
                    List<Etelek>? etelek = await sharedClient.GetFromJsonAsync<List<Etelek>>("Etelek/GetEtelekAsync");
                    etelek2.Clear();
                    foreach (var etel in etelek)
                    {
                        etelek2.Add(etel);
                    }
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = etelek2;
                    break;
                case "Értékelések":
                    break;
                case "Chain":
                    NewChainWindow newChainWindow = new NewChainWindow();
                    newChainWindow.ShowDialog();
                    List<Chain>? chain = await sharedClient.GetFromJsonAsync<List<Chain>>("Chain/GetChainAsync");
                    chain2.Clear();
                    foreach(var chai in chain)
                    {
                        chain2.Add(chai);
                    }
                    dgr_adatok.ItemsSource = null;
                    dgr_adatok.ItemsSource = chain2;
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
                        felhasznalok2.Clear();
                        foreach (var felhasznalo in felhasznalok)
                        {
                            felhasznalo.VarosId = felhasznalo.Varos.Id;
                            felhasznalok2.Add(felhasznalo);
                        }
                        dgr_adatok.ItemsSource = felhasznalok2;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Városok":
                    try
                    {
                        List<Varosok>? varosok = await sharedClient.GetFromJsonAsync<List<Varosok>>("Varosok/GetVarosokAsync");
                        varosok2.Clear();
                        foreach(var varos in varosok)
                        {
                            varosok2.Add(varos);
                        }
                        dgr_adatok.ItemsSource = varosok2;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Rendelt Étel":
                    break;
                case "Rendelés":
                    try
                    {
                        List<Rendeles>? rendeles = await sharedClient.GetFromJsonAsync<List<Rendeles>>("Rendeles/GetRendelesekAsync");
                        rendeles2.Clear();
                        foreach(var rendele in rendeles)
                        {
                            rendeles2.Add(rendele);
                        }
                        dgr_adatok.ItemsSource = rendeles2;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Leárazás":
                    try
                    {
                        List<Learaza>? learazas = await sharedClient.GetFromJsonAsync<List<Learaza>>("Learazas/GetLearazasokAsync");
                        learazas2 = learazas;
                        dgr_adatok.ItemsSource = learazas;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Éttermek":
                    try
                    {
                        List<Ettermek>? ettermek = await sharedClient.GetFromJsonAsync<List<Ettermek>>("Ettermek/GetEttermekAsync");
                        ettermek2.Clear();
                        foreach (var etterem in ettermek)
                        {
                            ettermek2.Add(etterem);
                        }
                        dgr_adatok.ItemsSource = ettermek2;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Ételek":
                    try
                    {
                        List<Etelek>? etelek = await sharedClient.GetFromJsonAsync<List<Etelek>>("Etelek/GetEtelekAsync");
                        etelek2.Clear();
                        foreach(var etel in etelek)
                        {
                            etelek2.Add(etel);
                        }
                        dgr_adatok.ItemsSource = etelek2;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Értékelések":
                    try
                    {
                        List<Ertekelesek>? ertekelesek = await sharedClient.GetFromJsonAsync<List<Ertekelesek>>("Ertekelesek/GetErtekelesAsync");
                        ertekelesek2 = ertekelesek;
                        dgr_adatok.ItemsSource = ertekelesek;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
                    break;
                case "Chain":
                    try
                    {
                        List<Chain>? chain = await sharedClient.GetFromJsonAsync<List<Chain>>("Chain/GetChainAsync");
                        chain2 = chain;
                        dgr_adatok.ItemsSource = chain;
                        if (jogosultsag > 1)
                        {
                            btn_torles.IsEnabled = true;
                            btn_modositas.IsEnabled = true;
                            btn_uj.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Sikertelen betöltés!\n{ex.Message}");
                    }
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
