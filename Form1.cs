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
using EASendMail;

namespace Kino___Cinema
{
    public partial class Form1 : Form
    {
        string[] labelTexts = new string[] { "TOP Cinema", "Kava", "Filmid" };
        int[] fontSizes = new int[] { 30, 15, 15 };
        public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\opilane\source\repos\Edgar Neverovski TARpv21\Kino\DB\KinoAB.mdf;Integrated Security = True");
        //public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\edgar\source\repos\Kino\DB\KinoAB.mdf;Integrated Security = True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataGridView dataGridView;
        public FlowLayoutPanel body;

        Label Kogus; 
        Button minus, plus;

        Saal saal = new Saal();
        Piletid pilet = new Piletid();

        Button maksa, valiKohad;

        TableLayoutPanel tableLayoutPanel;

        string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public Form1()
        {
            this.components = new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(500, 500);
            this.Text = "TOP Cinema - Home";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            FlowLayoutPanel head = new FlowLayoutPanel
            {
                Location = new Point(0, 0),
                Size = new Size(500, 50),
                BackColor = Color.Black,
                FlowDirection = FlowDirection.LeftToRight,
            };
            body = new FlowLayoutPanel
            {
                Location = new Point(0, 50),
                Size = new Size(500, 450),
                FlowDirection = FlowDirection.LeftToRight,
                
                AutoScroll = true,
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
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;

                head.Controls.Add(label);
            }     
            this.Controls.Add(head);
            this.Controls.Add(body);

            PictureBox picture = new PictureBox()
            {
                Size = new Size(500, 450),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };
            picture.Load("../../Images/Logo.png");
            body.BackgroundImage = picture.Image;
        }
        void Label_MouseLeave(object sender, EventArgs e)
        {
            Label pablo = (Label)sender;
            if (pablo.Text == labelTexts[0])
            {
                pablo.ForeColor = Color.Orange;
                pablo.BackColor = Color.Black;
            }
            else
            {
                pablo.ForeColor = Color.White;
                pablo.BackColor = Color.Black;
            }
        }
        void Label_MouseEnter(object sender, EventArgs e)
        {
            Label pablo = (Label)sender;
            if (pablo.Text == labelTexts[0])
            {
                pablo.ForeColor = Color.Black;
                pablo.BackColor = Color.Orange;
            }
            else
            {
                pablo.ForeColor = Color.Black;
                pablo.BackColor = Color.White;
            }
        }
        void Label_Click(object sender, EventArgs e)
        {
            Label pablo = (Label)sender;
            body.Controls.Clear();
            body.BackgroundImage = null;
            if (pablo.Text == labelTexts[0]) //Avaleht
            {
                PictureBox picture = new PictureBox()
                {
                    Size = new Size(500, 450),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                };
                picture.Load("../../Images/Logo.png");
                body.BackgroundImage = picture.Image;
            }
            else if (pablo.Text == labelTexts[1]) //Kava
            {
                cmd = new SqlCommand("SELECT Nimetus, Pilt, Kuupaev, Aeg FROM Tunniplaan " +
                    "INNER JOIN Filmid ON Tunniplaan.Filmid_ID = Filmid.ID", connect);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt_abi = new DataTable();
                adapter.Fill(dt_abi);

                List<string> pildid = new List<string>();
                foreach (DataRow nimetus in dt_abi.Rows)
                {
                    pildid.Add(nimetus["Pilt"].ToString());
                }

                for (int i = 0; i < dt_abi.Rows.Count; i++)
                {
                    cmd = new SqlCommand("SELECT Nimetus, Kuupaev, Aeg FROM Tunniplaan " +
                        "INNER JOIN Filmid ON Tunniplaan.Filmid_ID = Filmid.ID " +
                        "WHERE Tunniplaan.ID = " + (i + 4).ToString(), connect);
                    adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    PictureBox picture = new PictureBox()
                    {
                        Text = (i + 1).ToString(),
                        Size = new Size(200, 300),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                    };
                    picture.Load("../../Images/Filmid/" + pildid[i]);
                    picture.Click += Picture_Click;
                    body.Controls.Add(picture);

                    dataGridView = new DataGridView()
                    {
                        Size = new Size(300, 45),
                        RowHeadersVisible = false,
                        CellBorderStyle = DataGridViewCellBorderStyle.None,
                        Enabled = false,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.None,
                        DataSource = dt,
                    };

                    body.Controls.Add(dataGridView);
                }
            }
            else if (pablo.Text == labelTexts[2]) //Filmid
            {
                cmd = new SqlCommand("SELECT * FROM Filmid", connect);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt_abi = new DataTable();
                adapter.Fill(dt_abi);

                List<string> pildid = new List<string>();
                foreach (DataRow nimetus in dt_abi.Rows)
                {
                    pildid.Add(nimetus["Pilt"].ToString());
                }
                
                for (int i = 0; i < dt_abi.Rows.Count; i++)
                {                    
                    cmd = new SqlCommand("SELECT Nimetus, Zanr, Pikkus, Keel FROM Filmid WHERE ID = " + (i * 2 + 8).ToString(), connect);
                    adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    PictureBox picture = new PictureBox()
                    {
                        Size = new Size(200,300),
                        SizeMode = PictureBoxSizeMode.StretchImage,              
                    };
                    picture.Load("../../Images/Filmid/" + pildid[i]);
                    
                    body.Controls.Add(picture);
                    
                    dataGridView = new DataGridView()
                    {                        
                        Size = new Size(400, 45),
                        RowHeadersVisible = false,
                        CellBorderStyle = DataGridViewCellBorderStyle.None,
                        Enabled = false,
                        ReadOnly = true,
                        ScrollBars = ScrollBars.None,
                        DataSource = dt,
                    };

                    body.Controls.Add(dataGridView);
                }
            }
        }
        void Picture_Click(object sender, EventArgs e)
        {
            PictureBox pilt = (PictureBox)sender;
            string a = pilt.Text;
            pilet.Seanss = a;
            body.Controls.Clear();

            cmd = new SqlCommand("SELECT Rows, Kohad, VabuKohti FROM Seanss INNER JOIN Saalid ON Seanss.Saalid_ID = Saalid.ID WHERE Seanss.ID = " + a, connect);

            adapter = new SqlDataAdapter(cmd);
            DataTable dt_abi = new DataTable();
            adapter.Fill(dt_abi);

            foreach (DataRow nimetus in dt_abi.Rows)
            {
                saal.Read = nimetus["Rows"].ToString();
                saal.Kohad = nimetus["Kohad"].ToString();

                int b = int.Parse(saal.Read) * int.Parse(saal.Kohad);

                cmd = new SqlCommand("SELECT Koht FROM Piletid WHERE Seanss_ID = " + pilet.Seanss, connect);
                adapter = new SqlDataAdapter(cmd);
                DataTable dt3 = new DataTable();
                adapter.Fill(dt3);
               
                connect.Open();
                cmd = new SqlCommand("UPDATE Seanss SET VabuKohti=" + (b - dt3.Rows.Count).ToString() + " WHERE Seanss.ID = " + a, connect);
                cmd.ExecuteNonQuery();
                connect.Close();
            }

            cmd = new SqlCommand("SELECT Nimetus, Keel, Pikkus FROM Seanss INNER JOIN Filmid ON Seanss.Filmid_ID = Filmid.ID WHERE Seanss.ID = " + a, connect);

            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView = new DataGridView()
            {
                Size = new Size(300, 45),
                RowHeadersVisible = false,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                Enabled = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.None,
                DataSource = dt,
            };
            body.Controls.Add(dataGridView);

            cmd = new SqlCommand("SELECT Kuupaev, Aeg, Saal, VabuKohti FROM Seanss INNER JOIN Saalid ON Seanss.Saalid_ID = Saalid.ID INNER JOIN Tunniplaan ON Seanss.Tunniplaan_ID = Tunniplaan.ID WHERE Seanss.ID = " + a, connect);

            adapter = new SqlDataAdapter(cmd);
            DataTable dt2 = new DataTable();
            adapter.Fill(dt2);

            dataGridView = new DataGridView()
            {
                Size = new Size(400, 45),
                RowHeadersVisible = false,
                CellBorderStyle = DataGridViewCellBorderStyle.None,
                Enabled = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.None,
                DataSource = dt2,
            };
            body.Controls.Add(dataGridView);

            Label piletiNimi = new Label();
            piletiNimi.Text = "Tavapilet";
            piletiNimi.Size = new Size(110, 45);
            piletiNimi.Font = new Font("SimSun", 15, FontStyle.Bold);

            Label piletiHind = new Label();
            pilet.Hind = "10";
            piletiHind.Text = pilet.Hind + "€";
            piletiHind.Size = new Size(100, 45);
            piletiHind.Font = new Font("SimSun", 15, FontStyle.Bold);

            Kogus = new Label();
            Kogus.Font = new Font("SimSun", 15, FontStyle.Bold);
            Kogus.Text = "0";
            Kogus.Size = new Size(80,30);
            pilet.Kogus = Kogus.Text;            

            minus = new Button();
            minus.Text = "-";
            minus.Click += Button_Click;
            minus.Enabled = false;
            minus.Size = new Size(80,30);
            minus.Font = new Font("SimSun", 15, FontStyle.Bold);

            plus = new Button();
            plus.Text = "+";
            plus.Click += Button_Click;
            plus.Size = new Size(80,30);
            plus.Font = new Font("SimSun", 15, FontStyle.Bold);

            valiKohad = new Button();
            valiKohad.Text = "Vali kohad";
            valiKohad.Click += Button_Click;
            valiKohad.Size = new Size(80,30);

            if (pilet.Kogus == "0")
            {
                valiKohad.Enabled = false;
            }              

            //Плохое оформление + 2 бага с кнопками

            body.Controls.Add(piletiNimi);
            body.Controls.Add(piletiHind);
            body.Controls.Add(minus);
            body.Controls.Add(Kogus);
            body.Controls.Add(plus);
            body.Controls.Add(valiKohad);
        }
        void Button_Click(object sender, EventArgs e)
        {
            Button but = (Button)sender;
            if (Kogus.Text == "0")
            {
                minus.Enabled = false;
                valiKohad.Enabled = false;
            }
            else if (Kogus.Text == "5")
            {
                plus.Enabled = false;
            }
            else
            {
                minus.Enabled = true;
                plus.Enabled = true;
                valiKohad.Enabled = true;
            }
            if (but.Text == "+")
            {
                Kogus.Text = (int.Parse(Kogus.Text) + 1).ToString();
                pilet.Kogus = Kogus.Text;
                if (Kogus.Text == "5")
                {
                    plus.Enabled = false;
                }
            }
            else if (but.Text == "-")
            {
                Kogus.Text = (int.Parse(Kogus.Text) - 1).ToString();
                pilet.Kogus = Kogus.Text;
                if (Kogus.Text == "0")
                {
                    minus.Enabled = false;
                }
            }
            else if (but.Text == "Vali kohad")
            {
                body.Controls.Clear();
                tableLayoutPanel = new TableLayoutPanel()
                {
                    Size = new Size(480,400),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset,
                    AutoSize = false,
                };

                for (int i = 0; i < int.Parse(saal.Read); i++)
                {
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));

                    Label column = new Label()
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(25, 25),
                        Text = (i + 1).ToString(),
                        BackColor = Color.White,
                    };
                    Label read = new Label()
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Size = new Size(25, 25),
                        Text = (i + 1).ToString(),
                        BackColor = Color.White,
                    };
                    tableLayoutPanel.Controls.Add(column, i + 1, 0);
                    tableLayoutPanel.Controls.Add(read, 0, i + 1);

                    for (int j = 0; j < int.Parse(saal.Kohad); j++)
                    {
                        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

                        PictureBox pictureBox = new PictureBox()
                        {
                            Name = (i + 1).ToString() + "-" + (j + 1).ToString(),
                            Size = new Size(25, 25),
                            Image = Image.FromFile(@"../../Images/istukoht.png"),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Dock = DockStyle.Fill,
                            BackColor = Color.Green,
                        };
                        pictureBox.Click += PictureBox_Click;

                        tableLayoutPanel.Controls.Add(pictureBox, i + 1, j + 1);
                    }
                }
                body.Controls.Add(tableLayoutPanel);

                cmd = new SqlCommand("SELECT Koht FROM Piletid WHERE Seanss_ID = " + pilet.Seanss, connect);

                adapter = new SqlDataAdapter(cmd);
                DataTable dt_abi = new DataTable();
                adapter.Fill(dt_abi);

                foreach (DataRow nimetus in dt_abi.Rows)
                {
                    List<string> words = nimetus["Koht"].ToString().Split('-').ToList();
                    Control red = tableLayoutPanel.GetControlFromPosition(int.Parse(words[0]), int.Parse(words[1]));
                    red.BackColor = Color.Red;
                    red.Enabled = false;
                    words.Clear();
                }
                

                maksa = new Button();
                maksa.Text = "Maksa";
                maksa.Click += Maksa_Click;
                body.Controls.Add(maksa);
            }
        }
        void Maksa_Click(object sender, EventArgs e) //Добавить >>> если места не выбраны, то ошибка
        {
            foreach (Control kohad in tableLayoutPanel.Controls)
            {  
                if (kohad.BackColor == Color.Yellow)
                    pilet.Koht = kohad.Name;
                
            }
            foreach (string kohad in pilet.Kohad)
            {
                connect.Open();
                cmd = new SqlCommand("INSERT INTO Piletid(Seanss_ID, Hind, Koht) VALUES(@seanss, @hind, @koht)", connect);
                cmd.Parameters.AddWithValue("@seanss", pilet.Seanss);
                cmd.Parameters.AddWithValue("@hind", pilet.Hind);
                cmd.Parameters.AddWithValue("@koht", kohad);
                cmd.ExecuteNonQuery();
                connect.Close();
            }                        
            new Form2(this).Show();           
        }
        void PictureBox_Click(object sender, EventArgs e) //Добавить лимит выбора мест
        {
            PictureBox pilt = (PictureBox)sender;

            if (pilt.BackColor == Color.Green)
            {
                pilt.BackColor = Color.Yellow;
            }
            else if (pilt.BackColor == Color.Yellow)
            {
                pilt.BackColor = Color.Green;
            }
        }
        public void emailSend()
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = "edgar.neverovski@hotmail.com";
                oMail.To = email;
                oMail.Subject = "Apollo kino - pilet";

                cmd = new SqlCommand("SELECT Nimetus, Kuupaev, Aeg, Saal FROM Seanss " +
                "INNER JOIN Filmid ON Seanss.Filmid_ID = Filmid.ID " +
                "INNER JOIN Saalid ON Seanss.Saalid_ID = Saalid.ID " +
                "INNER JOIN Tunniplaan ON Seanss.Tunniplaan_ID = Tunniplaan.ID " +
                "WHERE Seanss.ID = " + pilet.Seanss, connect);

                adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow nimetus in dt.Rows)
                {
                    string a = "";
                    foreach (string kohad in pilet.Kohad)
                    {
                        a += kohad;
                    }
                    oMail.TextBody = "Film: " + nimetus["Nimetus"].ToString() 
                        + "\nKuupäev: " + nimetus["Kuupaev"].ToString() 
                        + "\nAeg: " + nimetus["Aeg"].ToString() 
                        + "\nSaal: " + nimetus["Saal"].ToString() 
                        + "\nRida - Koht: " + a 
                        + "\nHind: " + pilet.Hind + "€";
                }
                pilet.Kohad.Clear();

                SmtpServer oServer = new SmtpServer("smtp.office365.com");
                oServer.User = "edgar.neverovski@hotmail.com";
                oServer.Password = "qawsedrf1";
                oServer.Port = 587;
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                MessageBox.Show("start to send email over TLS...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                MessageBox.Show("email was sent successfully!");
            }
            catch (Exception ep)
            {
                MessageBox.Show("failed to send email with the following error:");
                MessageBox.Show(ep.Message);
            }

        }
    }
}