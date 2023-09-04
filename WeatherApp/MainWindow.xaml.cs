using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

namespace WeatherApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string baseApi = "http://api.weatherapi.com/v1/current.json?key=ab6a6db4fda04775877113817230409%20&q=";
        public string city = "Lille";
        public string JsonString;


        // Partie fonction main
        public MainWindow()
        {
            InitializeComponent();
            SetHeaderImg();
            SetDay();
            GetApiResponse();



        }

        // Partie fonctions

        // Fonction API/JSON
        public void GetApiResponse()
        {
            using(WebClient wc  = new WebClient())
            {
                // Télecharger du JSON depuis l'API
                JsonString = wc.DownloadString(baseApi + city);
                // Encodage UTF8 de la rép
                byte[] bytes = Encoding.UTF8.GetBytes(JsonString);
                JsonString = Encoding.UTF8.GetString(bytes);
                SetUiInfos();
            }
        }

        public void SetUiInfos()
        {
            JObject o = JObject.Parse(JsonString);
            // MessageBox.Show(JsonString);
            weatherDesc.Content = o["condition:text"];
        }

            // Fonction de base
        // Gérer la date 
        public void SetDay()
        {
            // Premiere lettre en MAJUSCULE
            dateTxt.Content = CapitalizeStr(DateTime.Now.ToString("dddd dd MMMM yyyy"));

            forecast1.Content = CapitalizeStr(DateTime.Now.AddDays(1).ToString("ddd"));
            forecast2.Content = CapitalizeStr(DateTime.Now.AddDays(2).ToString("ddd"));
            forecast3.Content = CapitalizeStr(DateTime.Now.AddDays(3).ToString("ddd"));
        }

        public string CapitalizeStr(string str)
        {
            string strFormat = str;
            strFormat = char.ToUpper(str[0]) + str.Substring(1);
            return strFormat;
        }

        public void SetHeaderImg()
        {
            string meteo = "pluie";
            if (meteo == "pluie")
            {
                // Modifier l'image du header
                headerImg.Source = new BitmapImage(new Uri(@"/sunset.jpg", UriKind.Relative));
            }
            else
            {
                // On ne modifie pas l'image 
            }
        }

        // Partie gestion des evenements

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            city = cityTxt.Text;
            cityTitle.Content = city;
            GetApiResponse();
        }

        private void btnInfos_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Développé par Vincent CARDON", "Crédits", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
