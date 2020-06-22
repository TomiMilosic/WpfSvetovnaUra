using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace WpfSvetovnaUra
{
    public class Ura
    {
        [XmlIgnore]
        public TimeSpan Ura_od;
        [XmlIgnore]
        public TimeSpan Ura_do;


        public string Ura_do1
        {
            get { return Ura_do.ToString(); }
            set { Ura_do = TimeSpan.Parse(value); }
        }

        public string Ura_od1
        {
            get { return Ura_od.ToString(); }
            set { Ura_od = TimeSpan.Parse(value); }
        }
        public Ura() { }
        public Ura(TimeSpan ura_od, TimeSpan ura_do)
        {
            Ura_od = ura_od;
            Ura_do = ura_do;
        }
    }
}
