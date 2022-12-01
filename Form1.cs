using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kino___Cinema
{
    public partial class Form1 : Form
    {
        public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\opilane\source\repos\Edgar Neverovski TARpv21\Kino\DB\KinoAB.mdf;Integrated Security = True");
        public Form1()
        {
            InitializeComponent();            
        }
    }
}
