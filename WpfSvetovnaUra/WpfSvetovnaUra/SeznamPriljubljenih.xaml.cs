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
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Threading;

namespace WpfSvetovnaUra
{
    /// <summary>
    /// Interaction logic for SeznamPriljubljenih.xaml
    /// </summary>
    public partial class SeznamPriljubljenih : Window
    {
        public List<Kraj> PriljubljeniKraji = new List<Kraj>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<Kraj>));
        public Kraj kraj2;

        public SeznamPriljubljenih()
        {
            InitializeComponent();
            using (TextReader reader = new StreamReader("PriljubljeniKraji.xml")) PriljubljeniKraji = (List<Kraj>)serializer.Deserialize(reader);
            foreach (var item in PriljubljeniKraji)
            {
                item.Ura = DateTime.Now.AddHours(item.razlika);
                SeznamPriljubljenihDataGrid.Items.Add(item);
            }
        }

        public SeznamPriljubljenih(MainWindow mainWindow)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            InitializeComponent();
            using (TextReader reader = new StreamReader("PriljubljeniKraji.xml")) PriljubljeniKraji = (List<Kraj>)serializer.Deserialize(reader);
            foreach (var item in PriljubljeniKraji)
            {
                item.Ura = DateTime.Now.AddHours(item.razlika);
                SeznamPriljubljenihDataGrid.Items.Add(item);
            }
          
        }
        void timer_Tick(object sender, EventArgs e)
        {
            UraTick.Header = DateTime.Now.ToLongTimeString();
            var number = 0;
            foreach (var item in PriljubljeniKraji)
            {
                var a = SeznamPriljubljenihDataGrid.Columns[1].GetCellContent(SeznamPriljubljenihDataGrid.Items[number]) as TextBlock;
                a.Text = DateTime.Now.AddHours(item.razlika).ToLongTimeString();
                number++;
            }
        }
        private void OdstraniIzPriljubljenih(object sender, RoutedEventArgs e)
        {
            Kraj kraj = (Kraj)SeznamPriljubljenihDataGrid.SelectedItem;
            kraj = PriljubljeniKraji.Where(x => x.kraj == kraj.kraj) as Kraj;
            PriljubljeniKraji.Remove(kraj);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Kraj kraj = (Kraj)SeznamPriljubljenihDataGrid.SelectedItem;
            OsebeVmestu vmestu = new OsebeVmestu(kraj);
            vmestu.SendKraj += value => kraj2 = value;
            if (!(bool)vmestu.ShowDialog() && kraj2!=null)
            {
                SeznamPriljubljenihDataGrid.Items.Remove(kraj);
                if (kraj2.OsebeVKraju.Count() >= kraj.OsebeVKraju.Count())
                {
                    SeznamPriljubljenihDataGrid.Items.Add(kraj2);
                }
                 
            }

            if (kraj2==null)
            {
                SeznamPriljubljenihDataGrid.Items.Clear();
                using (TextReader reader = new StreamReader("PriljubljeniKraji.xml")) PriljubljeniKraji = (List<Kraj>)serializer.Deserialize(reader);
                foreach (var item in PriljubljeniKraji)
                {
                    item.Ura = DateTime.Now.AddHours(item.razlika);
                    SeznamPriljubljenihDataGrid.Items.Add(item);
                };
            }
        }

        private void SeznamPriljubljenihDataGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Ali želite odstraniti kraj iz priljubljenih?", "Odstrani", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Kraj neke = SeznamPriljubljenihDataGrid.SelectedItem as Kraj;

                SeznamPriljubljenihDataGrid.Items.Remove(neke);

                foreach (var item in PriljubljeniKraji)
                {
                    if (item.kraj == neke.kraj)
                    {
                        PriljubljeniKraji.Remove(item);
                        break;
                    }
                }
                using (TextWriter writer = new StreamWriter("PriljubljeniKraji.xml"))serializer.Serialize(writer, PriljubljeniKraji);
            }
        }
    }
}
