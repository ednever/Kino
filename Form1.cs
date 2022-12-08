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
        string[] labelTexts = new string[] { "TOP Cinema", "Kava" , "Filmid"};
        int[] fontSizes = new int[] { 30, 15 , 15};
        public SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\opilane\source\repos\Edgar Neverovski TARpv21\Kino\DB\KinoAB.mdf;Integrated Security = True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        DataGridView dataGridView;
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
                label.MouseEnter += Label_MouseEnter;
                label.MouseLeave += Label_MouseLeave;

                menu.Controls.Add(label);
            }



            
            this.Controls.Add(menu);
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
            if (pablo.Text == labelTexts[0]) //Avaleht
            {

            }
            else if (pablo.Text == labelTexts[1]) //Kava
            {


                PictureBox film = new PictureBox
                {
                    Location = new Point(0, 100),
                    Size = new Size(200, 300),
                    BackColor = Color.Orange,
                    SizeMode = PictureBoxSizeMode.StretchImage,

                };
                film.Load(@"../../Images/Filmid/Menu.jpg");

                this.Controls.Add(film);
              
            }
            else if (pablo.Text == labelTexts[2]) //Filmid
            {
                cmd = new SqlCommand("SELECT * FROM Filmid", connect);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                dataGridView = new DataGridView()
                {
                    Location = new Point(0, 100),
                    Size = new Size(500, 45),
                    RowHeadersVisible = false,
                    CellBorderStyle = DataGridViewCellBorderStyle.None,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.None,

                    DataSource = dt,

                };
                this.Controls.Add(dataGridView);

            }
        }
        void emailSend()
        { 
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Your email address
                oMail.From = "edgar.neverovski@hotmail.com";

                // Set recipient email address
                oMail.To = "edgarneverovskij@gmail.com";

                // Set email subject
                oMail.Subject = "test email from hotmail, outlook, office 365 account";

                // Set email body
                oMail.TextBody = "this is a test email sent from c# project using hotmail.";

                // Hotmail/Outlook SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // If your account is office 365, please change to Office 365 SMTP server
                // SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // User authentication should use your
                // email address as the user name.
                oServer.User = "edgar.neverovski@hotmail.com";

                // If you got authentication error, try to create an app password instead of your user password.
                https://support.microsoft.com/en-us/account-billing/using-app-passwords-with-apps-that-don-t-support-two-step-verification-5896ed9b-4263-e681-128a-a6f2979a7944 
                oServer.Password = "qawsedrf1";

                // use 587 TLS port
                oServer.Port = 587;

                // detect SSL/TLS connection automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                MessageBox.Show("start to send email over TLS...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                MessageBox.Show("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
            /**
             * edgar.neverovski@hotmail.com
             * qawsedrf1
             */
        }
    }
}