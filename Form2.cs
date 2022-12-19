using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kino___Cinema
{
    public partial class Form2 : Form
    {
        Form1 form1;
        TextBox textBox;
        public Form2(Form1 owner)
        {
            form1 = owner;
            this.Text = "Pileti saatmine";
            this.components = new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Label l = new Label();
            l.Size = new Size(300, 50);
            l.Font = new Font("SimSun", 15, FontStyle.Bold);
            l.Text = "Kirjuta oma email pileti saamiseks:";
            this.Controls.Add(l);

            textBox = new TextBox();
            textBox.Size = new Size(200, 50);
            textBox.Location = new Point(20, 50);
            this.Controls.Add(textBox);

            Button button = new Button();
            button.Location = new Point(20, 100);
            button.Size = new Size(100, 20);
            button.Text = "Saada";
            button.Click += Button_Click;
            this.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (textBox.Text != string.Empty)
            {
                form1.Email = textBox.Text;
                form1.body.Controls.Clear();
                form1.body.BackgroundImage = Image.FromFile(@"../../Images/Logo.png");
                this.Close();
            }
        }
    }
}
