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
using System.Collections.ObjectModel;


namespace WpfSvetovnaUra
{
    /// <summary>
    /// Interaction logic for OsebeVmestu.xaml
    /// </summary>
    public partial class OsebeVmestu : Window
    {
        public XmlSerializer serializer = new XmlSerializer(typeof(List<Kraj>));
        public DodajOseboForma izbran;
        public List<Kraj> kraji = new List<Kraj>();
        public ObservableCollection<Oseba> osebas = new ObservableCollection<Oseba>();
        public Kraj kraj2 = new Kraj();
        public Oseba NovaOseba = new Oseba();
        public Kraj izbranKraj = new Kraj();
        public Action<Kraj> SendKraj;
        public TimeSpan zdaj = DateTime.Now.TimeOfDay;

        
        public OsebeVmestu(Kraj kraj)
        {
            InitializeComponent();
            kraj2 = kraj;
            foreach (var item in kraj.OsebeVKraju)osebas.Add(item);
            krajlabel.Content = kraj.kraj;
            using (TextReader reader = new StreamReader("PriljubljeniKraji.xml")) kraji = (List<Kraj>)serializer.Deserialize(reader);
            foreach (var item in osebas)
            {
                OsebeSeznam.Items.Add(item);
                if (item.navoljo.Ura_od < zdaj && zdaj < item.navoljo.Ura_do)
                {
                    item.slika = @"C:\Users\Tomi\source\repos\WpfSvetovnaUra\WpfSvetovnaUra\slike\zelena_pika.png";
                }
                else
                {
                    item.slika = @"C:\Users\Tomi\source\repos\WpfSvetovnaUra\WpfSvetovnaUra\slike\prazna.png";
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//urejanje oseb na kraj
        {
            Oseba oseba = (Oseba)OsebeSeznam.SelectedItem;
            DodajOseboForma dodajOseboForma = new DodajOseboForma(oseba);
            izbran = dodajOseboForma;
            dodajOseboForma.ShowDialog();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//dodajanje oseb na en kraj
        {
            using (TextReader reader = new StreamReader("PriljubljeniKraji.xml")) kraji = (List<Kraj>)serializer.Deserialize(reader);
            foreach (var item in kraji) if (item.kraj == krajlabel.Content.ToString()) izbranKraj = item;
            DodajOseboForma dodajOseboForma = new DodajOseboForma(izbranKraj);//najdi uzbran kraj pa mu daj cloecka not pol pa ta kraj poslnji okno vijse
            dodajOseboForma.osebaPoslji += value => NovaOseba = value;//POslusanje hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh

            int stevilo = izbranKraj.OsebeVKraju.Count();   
            if (!(bool)dodajOseboForma.ShowDialog() && NovaOseba.ime!=null && NovaOseba.priimek!=null)
            {
                OsebeSeznam.Items.Add(NovaOseba);
                izbranKraj.OsebeVKraju.Add(NovaOseba);

                if (izbranKraj.OsebeVKraju.Count()>=stevilo)
                {
                    SendKraj(izbranKraj);
                }

                if (izbranKraj.OsebeVKraju.Count()<stevilo)
                {
                    SendKraj(new Kraj { kraj = "Uspelo", razlika = 2 });
                }

            }
            
        }

        private void OsebeSeznam_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Oseba oseba = OsebeSeznam.SelectedItem as Oseba;
            if (MessageBox.Show("Ali želite odstraniti osebo iz kraja?", "Odstrani", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in kraji)
                {
                    foreach (var item2 in item.OsebeVKraju)
                    {
                        if (item2.telefon == oseba.telefon)
                        {
                            item.OsebeVKraju.Remove(item2);
                            OsebeSeznam.Items.Remove(oseba);
                            break;
                        }
                    }
                }
            }
            using (TextWriter writer = new StreamWriter("PriljubljeniKraji.xml")) serializer.Serialize(writer, kraji);

        }

        private void Iskanje_KeyUp(object sender, KeyEventArgs e)
        {
          
           List<Oseba> matchingvalues = osebas.Where(stringToCheck => stringToCheck.ime.Contains(Iskanje.Text)).ToList<Oseba>();
            OsebeSeznam.ItemsSource = null;
            OsebeSeznam.Items.Clear();
            OsebeSeznam.ItemsSource = matchingvalues;
        }

        private void Iskanje_GotFocus(object sender, RoutedEventArgs e)
        {
            Iskanje.Text = string.Empty;
        }
    }
}
