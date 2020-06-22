using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace WpfSvetovnaUra
{
    public class Oseba
    {
        public string ime { get; set; }
        public string  priimek { get; set; }
        public string opombe { get; set; }
        public Ura navoljo { get; set; }
        [XmlIgnore]
        public string slika { get; set; }
        public string telefon { get; set; }

        public Oseba() { }
        public Oseba(string ime, string priimek)
        {
            this.ime = ime;
            this.priimek = priimek;
          
        }

        public Oseba(string ime, string priimek, string opombe, Ura navoljo) : this(ime, priimek)
        {

            this.opombe = opombe;
            this.navoljo = navoljo;
      
        }
    }
}
