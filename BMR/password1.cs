using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using blindmailreader;

namespace BMR
{
    public partial class password1 : Form
    {
        string uname;
        SpeechSynthesizer sp;
        int c = 0;
        mailreader mr = new mailreader();
        public password1()
        {
            InitializeComponent();
        }
        public password1(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            uname = s;
        }

        private void password1_Load(object sender, EventArgs e)
        {
            textBox2.Text = uname;
            sp.Speak("change your password");

        }


        private void timer1_Tick_1(object sender, EventArgs e)
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


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                errorProvider1.SetError(textBox3, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("ENTER OLD PASSWORD");
                textBox3.Focus();
            }
            if (textBox4.Text == "")
            {
                errorProvider1.SetError(textBox4, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("ENTER NEW PASSWORD");
                textBox4.Focus();
            }
            if (textBox5.Text == "")
            {
                errorProvider1.SetError(textBox5, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("ENTER CONFIRM PASSWORD");
                textBox5.Focus();
            }
            if (comboBox1.SelectedText == "gmail password")
            {
                if (textBox5.Text == textBox4.Text)
                {
                    mr.connect();
                    string q = "update userdetails set gmailpassword='" + textBox4.Text + "' where username='" + textBox2.Text + "' and gmailpassword='"+textBox3.Text+"'";
                    mr.idu(q);
                    MessageBox.Show("GMAIL PASSWORD CHANGED");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                }
                else
                {
                    MessageBox.Show("PASSWORD ENTERED IS INCORRECT CHECK YOUR PASSWORD");
                }
            }
            else 
            {
                if(textBox5.Text==textBox4.Text)
                {
                mr.connect();
                string q = "update userdetails set password='" + textBox4.Text + "' where username='" + textBox2.Text + "' and password='"+textBox3.Text+"'";
                mr.idu(q);
                MessageBox.Show("BMR PASSWORD CHANGED");
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                }
                else
                {
                    MessageBox.Show("PASSWORD ENTERED IS INCORRECT CHECK YOUR PASSWORD");
                }
            }
            
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
               
                homepage hm = new homepage(uname);
                hm.Show();
                this.Hide();
                
            }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            
        }

      

       

        
       
        
    }
}