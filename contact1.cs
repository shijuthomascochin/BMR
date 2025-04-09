using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using blindmailreader;
using System.Speech.Synthesis;


namespace BMR
{
    public partial class contact1 : Form
    {
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        Match M1;
        SpeechSynthesizer sp;
        string username,user;
        int c = 0;
        mailreader mr = new mailreader();
        public contact1()
        {
            InitializeComponent();
        }
        public contact1(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;

        }
        public void uid()
        {
            mr.connect();
            string p = "select userid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                user = dr[0].ToString();
            }

        }

        private void contact1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("your contacts");

            uid();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            M1 = Regex.Match(textBox3.Text, eval);
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannot be blank");
                MessageBox.Show("required field cannot be blank");
                return;
            }
            else
            {
                if (M1.Success)
                {

                    timer1.Enabled = false;
                    mr.connect();
                    string q = "insert into contacts values('" + int.Parse(user) + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                    mr.idu(q);
                    sp.Speak("contact added");
                    MessageBox.Show("CONTACT ADDED");
                    textBox3.Text = "";
                    textBox2.Text = "";
                }

                else
                {
                    errorProvider1.SetError(textBox3, "enter the correct format of gmailid");
                    sp.Speak("enter the correct format of gmailid");
                    MessageBox.Show("ENTER CORRECT FORMAT OF GMAILID");

                    textBox3.Focus();
                    textBox3.Text = "";
                }
            
            }
                
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannot be blank");
                MessageBox.Show("required field cannot be blank");
                return;
            }
            else
            {
                timer1.Enabled = false;
                mr.connect();
                string q = "delete from contacts where contactname='" + textBox2.Text + "'";
                mr.idu(q);
                sp.Speak("contact deleted");
                MessageBox.Show("CONTACT DELETED");
                textBox3.Text = "";
                textBox2.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannot be blank");
                MessageBox.Show("required field cannot be blank");
                return;
            }
            else
            {
                timer1.Enabled = false;
                mr.connect();
                string q = "select gmailid from contacts where contactname='" + textBox2.Text + "'";
                DataTable dt = mr.select(q);
                foreach (DataRow dr in dt.Rows)
                {
                    textBox3.Text = dr[0].ToString();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            homepage HOME = new homepage(username);
            HOME.Show();
            this.Hide();
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

        private void textBox3_Leave(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            M1 = Regex.Match(textBox3.Text, eval);
            if (M1.Success)
            {

                timer1.Enabled = false;
                mr.connect();
                string q = "update contacts set gmailid='" + textBox3.Text + "' where contactname='" + textBox2.Text + "'";
                mr.idu(q);
                sp.Speak("contact edited");
                MessageBox.Show("CONTACT EDITED");
                textBox3.Text = "";
                textBox2.Text = "";
            }
            else
            {
                errorProvider1.SetError(textBox3, "enter the correct format of gmailid");
                sp.Speak("enter the correct format of gmailid");
                MessageBox.Show("ENTER CORRECT FORMAT OF GMAILID");

                textBox3.Focus();
                textBox3.Text = "";
            }

        }

       

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox2.Focus();
            }
        }
    }
}