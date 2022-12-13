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
//using EASendMail;

namespace Kino___Cinema
{
    public partial class Form1 : Form
    {
        string[] labelTexts = new string[] { "TOP Cinema", "Kava", "Filmid" };
        int[] fontSizes = new int[] { 30, 15, 15 };
        //public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\opilane\source\repos\Edgar Neverovski TARpv21\Kino\DB\KinoAB.mdf;Integrated Security = True");
        public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\edgar\source\repos\Kino\DB\KinoAB.mdf;Integrated Security = True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataGridView dataGridView;
        FlowLayoutPanel body;
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
                        //SelectionMode = DataGridViewSelectionMode.FullRowSelect,
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
                        //SelectionMode = DataGridViewSelectionMode.FullRowSelect,
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
            MessageBox.Show("UUUU");
        }
        //void emailSend()
        //{ 
        //    try
        //    {
        //        SmtpMail oMail = new SmtpMail("TryIt");

        //        // Your email address
        //        oMail.From = "edgar.neverovski@hotmail.com";

        //        // Set recipient email address
        //        oMail.To = "edgarneverovskij@gmail.com";

        //        // Set email subject
        //        oMail.Subject = "test email from hotmail, outlook, office 365 account";

        //        // Set email body
        //        oMail.TextBody = "this is a test email sent from c# project using hotmail.";

        //        // Hotmail/Outlook SMTP server address
        //        SmtpServer oServer = new SmtpServer("smtp.office365.com");

        //        // If your account is office 365, please change to Office 365 SMTP server
        //        // SmtpServer oServer = new SmtpServer("smtp.office365.com");

        //        // User authentication should use your
        //        // email address as the user name.
        //        oServer.User = "edgar.neverovski@hotmail.com";

        //        oServer.Password = "qawsedrf1";

        //        // use 587 TLS port
        //        oServer.Port = 587;

        //        // detect SSL/TLS connection automatically
        //        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

        //        MessageBox.Show("start to send email over TLS...");

        //        SmtpClient oSmtp = new SmtpClient();
        //        oSmtp.SendMail(oServer, oMail);

        //        MessageBox.Show("email was sent successfully!");
        //    }
        //    catch (Exception ep)
        //    {
        //        MessageBox.Show("failed to send email with the following error:");
        //        MessageBox.Show(ep.Message);
        //    }
        //    /**
        //     * edgar.neverovski@hotmail.com
        //     * qawsedrf1
        //     */
        //}
    }
}