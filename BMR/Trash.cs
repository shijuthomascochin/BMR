using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using blindmailreader;
using System.Speech.Synthesis;

namespace BMR
{
    public partial class trashform : Form
    {
        mailreader mr = new mailreader();
        string b, username,userid;
        int c = 0;
        SpeechSynthesizer sp;
        public trashform()
        {
           
            InitializeComponent();
        }
        public trashform(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
        }

        private void trashform_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("trash");
            loading();
            showgrid();
            
            sp.Speak("mails in trash are");
            mr.connect();
            string q = "select mailno from inbox where sort='TRASH' and userid ='" + userid + "'";
            DataTable dt = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt.Rows)
            {
                b = dr[0].ToString();
                if (b == null)
                {
                    sp.Speak("there is no mails in trash");
                }
                else
                {
                    sp.Speak(b);
                }
            }
            sp.Speak("select a mail number ");
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
            
            
        }
        public void loading()
        {
            mr.connect();
            string q = "select userid from userdetails where username='" + username + "' ";
            DataTable dt1 = mr.select(q);
            foreach (DataRow dr1 in dt1.Rows)
            {
                userid = dr1[0].ToString();
            }
        }
        public void showgrid()
        {

            mr.connect();
            string p = "select * from inbox where sort='TRASH' and userid='" + userid + "'";
            DataTable dt2 = mr.select(p);
            dataGridView1.DataSource = dt2;
            dataGridView1.Refresh();


        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (textBox3.Text == "1")
            {
                mr.connect();
                string q = "delete from inbox  where mailno='" + textBox2.Text + "'";
                mr.idu(q);
                showgrid();
                sp.Speak("mail has been deleted");
                
                //textBox2.Text = "";
                textBox3.Text = "";
                textBox3.Focus();
                sp.Speak("press one for deleting the mail,two for restoring  the mail to drafts and 3 for navigating to homepage");


            }
            else if (textBox3.Text == "2")
            {
                mr.connect();
                string q = "update inbox set sort='DRAFT' where mailno='" + textBox2.Text + "'";
                mr.idu(q);
                showgrid();
                sp.Speak("mail restored to drafts");
                //MessageBox.Show("MAIL RESTORED TO DRAFT");
                textBox3.Text = "";
                textBox3.Focus();
                sp.Speak("press one for deleting the mail,two for restoring  the mail to drafts and 3 for navigating to homepage");

            }
            else if(textBox3.Text=="3")
            {
                Form2 f2 = new Form2(username);
                f2.Show();
                loading();
                this.Hide();
            }
            else if (textBox3.Text == "")
            {
                errorProvider1.SetError(textBox3, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter valid input");
                textBox3.Focus();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            sp.Speak("press one for deleting the mail,two for restoring  the mail to drafts and 3 for navigating to homepage");
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

        private void trashform_Enter(object sender, EventArgs e)
        {
            
        }

    }
}
