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
        string[] labelTexts = new string[] { "TOP Cinema", "Kava" };
        int[] fontSizes = new int[] { 30, 15 };
        public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\opilane\source\repos\Edgar Neverovski TARpv21\Kino\DB\KinoAB.mdf;Integrated Security = True");
        public Form1()
        {
            this.components = new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 500);
            this.Text = "TOP Cinema - Home";

            FlowLayoutPanel menu = new FlowLayoutPanel
            {
                Location = new Point(0, 0),
                Size = new Size(500, 100),
                BackColor = Color.Black,
                FlowDirection = FlowDirection.LeftToRight,
            };



            
            for (int i = 0; i < labelTexts.Length; i++)
            {
                Label label = new Label
                {
                    Font = new Font("SimSun", fontSizes[i], FontStyle.Bold),
                    ForeColor = Color.Orange,
                    Text = labelTexts[i],
                    AutoSize = true,
                };
                if (i > 0)
                {
                    label.ForeColor = Color.White;
                }
                label.Click += Label_Click;

                menu.Controls.Add(label);
            }



            
            this.Controls.Add(menu);
        }

        void Label_Click(object sender, EventArgs e)
        {
            Label pablo = (Label)sender;
            if (pablo.Text == labelTexts[1])
            {
                int x;
                for (int i = 0; i < 5; i++)
                {
                    PictureBox film = new PictureBox
                    {
                        Location = new Point(0, 100),
                        Size = new Size(200, 300),
                        BackColor = Color.Orange,
                        SizeMode = PictureBoxSizeMode.StretchImage,

                    };
                    film.Load(@"../../Images/Menu.jpg");
                    this.Controls.Add(film);
                }

            }
        }
    }
}
