using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            this.Icon = BitmapFrame.Create(new Uri("pack://application:,,,/gfx/icons/etelfutar.png"));
            tbx_cim.Text = MainWindow.client_address;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.client_address = tbx_cim.Text;
            MainWindow.sharedClient = new HttpClient()
            {
                BaseAddress = new Uri(MainWindow.client_address)
            };
        Close();
        }
    }
}
