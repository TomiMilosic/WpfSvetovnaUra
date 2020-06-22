using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using static System.Text.RegularExpressions.Regex;
using System.Security.Permissions;
using System.Collections.ObjectModel;


namespace WpfSvetovnaUra
{
    /// <summary>
    /// Interaction logic for DodajKraj.xaml
    /// </summary>
    public partial class DodajKraj : Window
    {
        public DodajKraj()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Match matches = Match(seznamUr.Text, @"[+-]?\d+(\.\d+)?");
            if (matches != null && ImeKraja != null)
            {
                Kraj kraj;
                List<Kraj> vsikraji = new List<Kraj>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<Kraj>));
                int nekaj = int.Parse(matches.Value);
                using (TextReader beri = new StreamReader("VsiKraji.xml"))vsikraji = (List<Kraj>)serializer.Deserialize(beri);
                
                if (ClanicaEUDA.IsChecked==true)
                {
                    kraj = new Kraj { kraj = ImeKraja.Text, razlika = nekaj, clanicaEU=Kraj.ClanicaEU.da, Komentar=KomentarKraja.Text };
                }
                else
                {
                    kraj = new Kraj { kraj = ImeKraja.Text, razlika = nekaj, clanicaEU= Kraj.ClanicaEU.ne, Komentar=KomentarKraja.Text};
                }

                using (TextWriter writer = new StreamWriter("VsiKraji.xml"))
                {
                    vsikraji.Add(kraj);
                    serializer.Serialize(writer, vsikraji);
                    writer.Close();
                }
                ((MainWindow)Application.Current.MainWindow).KrajiSeznam.Items.Add(kraj);//!!!!!!!!!!!!!!!!!!!!
                //((MainWindow)Application.Current.MainWindow).kraji.Add(kraj);//!!!!!!!!!!!!!!!!!!!!
                ((MainWindow)Application.Current.MainWindow).kraji.Add(kraj);
                Close();
            }
            else
            {
                MessageBox.Show("Napaka!");
            }


        }
    }
}
