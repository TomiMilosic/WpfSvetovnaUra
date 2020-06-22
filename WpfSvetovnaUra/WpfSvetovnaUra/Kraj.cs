using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace WpfSvetovnaUra
{
    public class Kraj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [XmlIgnore]
        public DateTime Ura { get; set; }

        [XmlElement("Kraj")]
        public string kraj { get; set; }

        [XmlElement("OsebeVkraju", Type = typeof(List<Oseba>))]
        public List<Oseba> OsebeVKraju { get; set; }

        [XmlElement("Razlika")]
        public int razlika { get; set; }
        
        public enum ClanicaEU { da, ne };

        [XmlElement("ClanicaEU")]
        public ClanicaEU clanicaEU {get;set;}


        public string Komentar { get; set; }


        public string komentar
        {
            get { return Komentar; }
            set { Komentar = value; OnPropertyChanged("Komentar"); }
        }


        public void OnPropertyChanged(string  e) 
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(e));
            }
           
        }
        

        public void DodajOsebo(Oseba oseba)
        {
            OsebeVKraju.Add(oseba);
        }
        public Kraj()
        {
            OsebeVKraju = new List<Oseba>();
        }

        public Kraj(DateTime ura, string kraj)
        {
           
            Ura = ura;
            this.kraj = kraj;   
        }

        public Kraj(DateTime ura, string kraj, List<Oseba> osebeVKraju, int razlika) : this(ura, kraj)
        {
            OsebeVKraju = osebeVKraju;
            this.razlika = razlika;
            OsebeVKraju = new List<Oseba>();
        }
    }
}
