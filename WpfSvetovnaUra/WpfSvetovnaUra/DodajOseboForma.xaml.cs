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

namespace WpfSvetovnaUra
{

    public enum UsedConstructor { Default, kraj, oseba }
    public partial class DodajOseboForma : Window
    {
        public Oseba iskana = new Oseba();
        UsedConstructor UsedConstructor { get; }
        public Oseba oseba1 = new Oseba();
        public Kraj tempKraj = new Kraj();
        public TimeSpan zdaj = DateTime.Now.TimeOfDay;
        public Action<Oseba> osebaPoslji;
        public XmlSerializer serializer = new XmlSerializer(typeof(List<Kraj>));
        public DodajOseboForma()
        {
            UsedConstructor = UsedConstructor.Default;
            InitializeComponent();
            navoljotb1.Text = "00:00:00";
            navoljotb2.Text = "00:00:00";
        }
        public DodajOseboForma(Kraj kraj)
        {
            UsedConstructor = UsedConstructor.kraj;
            InitializeComponent();
            tempKraj = kraj;
            navoljotb1.Text = "00:00:00";
            navoljotb2.Text = "00:00:00";

        }
        public DodajOseboForma(Oseba oseba)
        {
            UsedConstructor = UsedConstructor.oseba;
            InitializeComponent();
            oseba1 = oseba;
            Imetb.Text = oseba.ime;
            priimektb.Text = oseba.priimek;
            telefonskatb.Text = oseba.telefon;
            opombetb.Text = oseba.opombe;
            navoljotb1.Text = oseba.navoljo.Ura_od1.ToString();
            navoljotb2.Text = oseba.navoljo.Ura_do1.ToString();
            iskana = new Oseba { ime = Imetb.Text, priimek = priimektb.Text, opombe = opombetb.Text, telefon = telefonskatb.Text, navoljo = new Ura { Ura_od1 = navoljotb1.Text, Ura_do1 = navoljotb2.Text } };

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)//Gumb dodaj/uredi
        {
            List<Kraj> krajs = new List<Kraj>();
            Kraj kraj = new Kraj();
            using (TextReader beri = new StreamReader("PriljubljeniKraji.xml")) krajs = (List<Kraj>)serializer.Deserialize(beri);
            if (UsedConstructor == UsedConstructor.kraj)//dodajanje
            {
                Oseba oseba1 = new Oseba { ime = Imetb.Text, priimek = priimektb.Text, opombe = opombetb.Text, telefon = telefonskatb.Text, navoljo = new Ura { Ura_od1 = navoljotb1.Text, Ura_do1 = navoljotb2.Text } };
                foreach (var item in krajs)
                {
                    if (item.kraj == tempKraj.kraj)
                    {
                        item.OsebeVKraju.Add(oseba1);
                        kraj = item;
                        if (oseba1.ime != null)
                        {
                            osebaPoslji(oseba1);//HEREEEE DODJANJA NA EVENT
                        }
                    }
                }
                if (oseba1.navoljo.Ura_od < zdaj && zdaj < oseba1.navoljo.Ura_do)
                {
                    oseba1.slika = @"C:\Users\Tomi\source\repos\WpfSvetovnaUra\WpfSvetovnaUra\slike\zelena_pika.png";
                }
                else
                {
                    oseba1.slika = @"C:\Users\Tomi\source\repos\WpfSvetovnaUra\WpfSvetovnaUra\slike\prazna.png";
                }
                using (TextWriter pisi = new StreamWriter("PriljubljeniKraji.xml"))
                {
                    serializer.Serialize(pisi, krajs);
                }
                Close();
            }

            if (UsedConstructor == UsedConstructor.oseba)
            {
                foreach (var item in krajs)
                {
                    foreach (var item2 in item.OsebeVKraju)
                    {
                        if (item2.telefon == iskana.telefon)
                        {
                            tempKraj = item;
                        }
                    }
                }

                foreach (var item in krajs)
                {
                    if (item.kraj == tempKraj.kraj)
                    {
                        iskana = item.OsebeVKraju.Find(x => x.ime == Imetb.Text && x.priimek == priimektb.Text && x.telefon == telefonskatb.Text && x.opombe == opombetb.Text);
                    }
                }

                foreach (var item in krajs)
                {
                    if (item.kraj == tempKraj.kraj)
                    {
                        foreach (var item2 in item.OsebeVKraju)
                        {
                            if (item2 == iskana)
                            {
                                item2.ime = Imetb.Text;
                                item2.priimek = priimektb.Text;
                                item2.telefon = telefonskatb.Text;
                                item2.navoljo.Ura_od1 = navoljotb1.Text;
                                item2.navoljo.Ura_do1 = navoljotb2.Text;
                                if (item2.navoljo.Ura_od < zdaj && zdaj < item2.navoljo.Ura_do)
                                {
                                    item2.slika = @"C:\Users\Tomi\source\repos\WpfSvetovnaUra\WpfSvetovnaUra\slike\zelena_pika.png";
                                }
                                else
                                {
                                    item2.slika = @"C:\Users\Tomi\source\repos\WpfSvetovnaUra\WpfSvetovnaUra\slike\prazna.png";
                                }
                            }
                        }
                    }
                }
                using (TextWriter pisi = new StreamWriter("PriljubljeniKraji.xml"))serializer.Serialize(pisi, krajs);
                
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) => Close();
    }
}
