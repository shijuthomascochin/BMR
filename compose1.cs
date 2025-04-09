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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace BMR
{
    public partial class compose1 : Form
    {
        
        SpeechSynthesizer sp;
        
        string username,mailid,pass,p;
        int c = 0;
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        Match M1;
        mailreader mr = new mailreader();
        public compose1()
        {
            InitializeComponent();
        }
        public compose1(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
        }
        public void from()
        {
            mr.connect();
            string p = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                textBox4.Text = dr[0].ToString();
            }
        }


        private void compose1_Load(object sender, EventArgs e)
        {
            listBox1.Visible =false;
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("compose your mail");
            string p = "select getdate()";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr[0].ToString();

            }
            mr.connect();

            string q = "select isnull(max(mailno)+1,1)from inbox";
            DataTable dt5 = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt5.Rows)
            {
            mailid = dr[0].ToString();
    
            }

            from();
            uid();
            password();
        }
        public void uid()
        {
            mr.connect();
            string q = "select userid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(q);
            foreach (DataRow dr in dt.Rows)
            {
                p = dr[0].ToString();
            }
        }
        public void password()
        {
            mr.connect();
            string p = "select gmailpassword from userdetails where username='" + username + "'";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                pass = dr[0].ToString();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            if (c == 300)
            {
                sp.Speak("enter something otherwise the application will exit");
            }
            if (c == 1000)
            {
                sp.Speak("you have been idle ,try another time");
                Application.Exit();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                errorProvider1.SetError(textBox8, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("ENTER SUBJECT OF MAIL");
                textBox8.Focus();
            }
            if (textBox9.Text == "")
            {
                errorProvider1.SetError(textBox9, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("ENTER BODY OF MAIL");
                textBox9.Focus();
            }
            else
            {
               
                SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                mail.EnableSsl = true;
                string id = textBox4.Text;
                string sendid = textBox6.Text;
                string pwd = pass;
                NetworkCredential nw = new NetworkCredential(id, pwd);
                mail.Credentials = nw;
                mail.Send(id, sendid, textBox8.Text, textBox9.Text);
                mr.connect();
                string q = "insert into inbox values('" + int.Parse(p) + "','" + int.Parse(mailid) + "','" + textBox4.Text + "','" + textBox6.Text + "','nil','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox2.Text + "','inbox','SENT')";
                mr.idu(q);
                sp.Speak("mail has been sent");
                MessageBox.Show("MAIL HAS BEEN SENT");
                homepage f2 = new homepage(username);
                f2.Show();
                this.Hide();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            mr.connect();
            string q = "insert into inbox values('" + int.Parse(p) + "','" + int.Parse(mailid) + "','" + textBox4.Text + "','" + textBox6.Text + "','nil','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox2.Text + "','DRAFT')";
            mr.idu(q);
            sp.Speak("mail saved to drafts");
            MessageBox.Show("MAIL SAVED TO DRAFTS");
            homepage f2 = new homepage(username);
            f2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            homepage home = new homepage(username);
            home.Show();
            this.Hide();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
            
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox6_Enter(object sender, EventArgs e)
        {

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            
            //listBox1.Visible = true;
            //mr.connect();
            //string q = "select gmailid from contacts where gmailid like '" + textBox6.Text + "%' and userid='"+p+"'";
            //DataTable dt = mr.select(q);
            //foreach (DataRow dr in dt.Rows)
            //{
                
            //        listBox1.Items.Add( dr[0].ToString());

            //}
            
            
            
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox6.Text = listBox1.SelectedItem.ToString();
            listBox1.Visible = false;

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_Enter_1(object sender, EventArgs e)
        {
            
        }

    }
}

