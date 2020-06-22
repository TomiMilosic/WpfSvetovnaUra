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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Timers;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using System.Security.Permissions;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace WpfSvetovnaUra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlSerializer serializer = new XmlSerializer(typeof(List<Kraj>));
        public List<Kraj> kraji = new List<Kraj>();

        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            if (kraji.Count == 0)
            {
                using (TextReader beri = new StreamReader("VsiKraji.xml"))kraji = (List<Kraj>)serializer.Deserialize(beri);
                //KrajiSeznam.ItemsSource = kraji;
                foreach (var item in kraji)
                {
                    item.Ura = DateTime.Now.AddHours(item.razlika);
                    KrajiSeznam.Items.Add(item);
                }
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {
            UraTick.Header = DateTime.Now.ToLongTimeString();
            var number = 0;
            foreach (var item in kraji)
            {
                var a = KrajiSeznam.Columns[1].GetCellContent(KrajiSeznam.Items[number]) as TextBlock;
                a.Text = DateTime.Now.AddHours(item.razlika).ToLongTimeString();
                number++;
            }

            Kraj kraj = (Kraj)KrajiSeznam.SelectedItem;
            if (kraj != null)
            {
                uraLabel.Content = DateTime.Now.AddHours(kraj.razlika).ToLongTimeString();
            }
        }


        private void KrajiSeznam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kraj kraj = (Kraj)KrajiSeznam.SelectedItem;
            if (kraj !=null)
            {
                KrajLabel.Content = kraj.kraj;
                uraLabel.Content = kraj.Ura.ToString("HH\\:mm\\:ss");
            }
            else
            {
                KrajLabel.Content = "Kraj";
                uraLabel.Content = "Ura";
            }   
        }
        private void DodajNaSeznam(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Dodaj na seznam priljubljenih?", "Dodaj?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                bool flag = false;
                List<Kraj> priljubljeni = new List<Kraj>();
                //Preverjaj st priljubljenih krajev, če je manj ko 1- root element is missing

                using (TextReader beri = new StreamReader("PriljubljeniKraji.xml"))priljubljeni = (List<Kraj>)serializer.Deserialize(beri);
                var kraj = (Kraj)KrajiSeznam.SelectedItem;
                flag = priljubljeni.All(y => y.kraj == kraj.kraj);
                if (!flag)
                {
                    priljubljeni.Add(kraj);
                    using (TextWriter pisi = new StreamWriter("PriljubljeniKraji.xml")) serializer.Serialize(pisi, priljubljeni);
                }
                SeznamPriljubljenih nekaj = new SeznamPriljubljenih(this);
                nekaj.ShowDialog();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DodajKraj dodajKraj = new DodajKraj();
            dodajKraj.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SeznamPriljubljenih seznam = new SeznamPriljubljenih(this);
            seznam.ShowDialog();
        }

        private void KrajiSeznam_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Ali želite izbrisati kraj?", "Izbriši", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Kraj neke = KrajiSeznam.SelectedItem as Kraj;
                KrajiSeznam.Items.Remove(neke);
                foreach (var item in kraji)
                {
                    if (item.kraj==neke.kraj)
                    {
                        kraji.Remove(item);
                        break;
                    }
                }
                using (TextWriter writer = new StreamWriter("VsiKraji.xml"))serializer.Serialize(writer, kraji);
            }
        }

        private void KrajLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Storyboard story = new Storyboard();
            StringAnimationUsingKeyFrames frames = new StringAnimationUsingKeyFrames();
            frames.Duration = new Duration(new TimeSpan(0, 0, 5));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame(KrajLabel.Content.ToString()[0].ToString(), TimeSpan.FromSeconds(1)));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame(KrajLabel.Content.ToString()[1].ToString(), TimeSpan.FromSeconds(2)));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame(KrajLabel.Content.ToString()[2].ToString(), TimeSpan.FromSeconds(3)));
            frames.KeyFrames.Add(new DiscreteStringKeyFrame(KrajLabel.Content.ToString()[3].ToString(), TimeSpan.FromSeconds(4)));
            Storyboard.SetTargetName(frames, KrajLabel.Name);
            Storyboard.SetTargetProperty(frames, new PropertyPath(ContentProperty));
            story.Children.Add(frames);
            story.Begin(this);
        }

        
    }
}
