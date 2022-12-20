using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kino___Cinema
{
    public class Saal
    {        
        string read, kohad;
        public string Read
        {
            get { return read; }
            set { read = value; }
        }
        public string Kohad
        {
            get { return kohad; }
            set { kohad = value; }
        }
    }
}
