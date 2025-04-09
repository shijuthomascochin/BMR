using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using blindmailreader;
using System.Speech.Synthesis;


namespace BMR
{
    public partial class loginpage : Form
    {
        mailreader mr = new mailreader();
        int c = 0;

        SpeechSynthesizer sp;
        public loginpage()
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            register REG = new register();
            REG.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox1.Focus();
            }
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox2.Focus();
            }
            else
            {
                mr.connect();
                string q = "select * from userdetails where username='" + textBox1.Text + "'and password='" + textBox2.Text + "'";
                DataTable dt = mr.select(q);
                mr.idu(q);

                if (dt.Rows.Count == 1)
                {
                    sp.Speak("hello");
                    sp.Speak(textBox1.Text);
                    homepage home = new homepage(textBox1.Text);
                    home.Show();
                    this.Hide();
                }

                else
                {
                    sp.Speak("invalid user name or password");
                    MessageBox.Show("INVALID USER NAME OR PASSWORD");
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }
        private void loginpage_Load(object sender, EventArgs e)
        {
            sp.Speak("login page");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            if (c == 100)
            {
                sp.Speak("enter something otherwise the application will exit");
            }
            if (c == 300)
            {
                sp.Speak("you have been idle ,try another time");
                Application.Exit();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            register REG = new register();
            REG.Show();
            this.Hide();
        }
    }
}
