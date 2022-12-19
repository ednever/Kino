using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino___Cinema
{
    public class Piletid
    {
        string koht, seanss, hind;
        List<string> kohad = new List<string>();

        public string Koht
        {
            get { return koht; }
            set { koht = value; kohad.Add(koht); }
        }

        public List<string> Kohad
        {
            get { return kohad; }
            set { kohad = value; }
        }

        public string Seanss
        {
            get { return seanss; }
            set { seanss = value; }
        }

        public string Hind
        {
            get { return hind; }
            set { hind = value; }
        }
    }
}
