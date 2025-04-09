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

namespace BMR
{
    public partial class replypage : Form
    {
        SpeechSynthesizer sp;
        string a, username, p, pwd, mailno;
        int c = 0, mailnum;
        mailreader mr = new mailreader();
        public replypage()
        {
            InitializeComponent();
        }
        public replypage(string s, int g)
        {

            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
            mailnum = g;
        }


        private void replypage_Load(object sender, EventArgs e)
        {
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
            string k = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt9 = mr.select(k);
            foreach (DataRow dr in dt9.Rows)
            {
                from.Text = dr[0].ToString();
            }
            
            mr.connect();
            string q = "select fromid from inbox where mailno='" + mailnum + "'";
            DataTable dt2 = mr.select(q);
            foreach (DataRow dr in dt2.Rows)
            {
                to.Text = dr[0].ToString();
            }
            
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
                pwd = dr[0].ToString();

            }
        }
        public void mail()
        {
            mr.connect();

            string q = "select isnull(max(mailno)+1,1)from inbox";
            DataTable dt = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt.Rows)
            {
                mailno = dr[0].ToString();
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
            if (SUB.Text == "")
            {
                errorProvider1.SetError(SUB, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                SUB.Focus();
            }
            if (body.Text == "")
            {
                errorProvider1.SetError(body, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                body.Focus();
            }
            else
            {
                SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                mail.EnableSsl = true;
                string id = from.Text;
                string sendid = to.Text;
                string pswd = pwd;
                NetworkCredential nw = new NetworkCredential(id, pswd);
                mail.Credentials = nw;
                mail.Send(id, sendid, SUB.Text, body.Text);
                mr.connect();
                string q = "insert into inbox values('" + int.Parse(p) + "','" + int.Parse(mailno) +"','" + from.Text + "','" + to.Text + "','nil','" + SUB.Text + "','" + body.Text + "','" + textBox2.Text + "','SENT')";
                mr.idu(q);
                sp.Speak("mail has been sent");
                MessageBox.Show("MAIL HAS BEEN SENT");
                inbox1 inb = new inbox1(username);
                inb.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mr.connect();
            string q = "insert into inbox values('" + int.Parse(p) + "','" + mailnum + "','" + from.Text + "','" + to.Text + "','nil','" + SUB.Text + "','" + body.Text + "','" + textBox2.Text + "','DRAFT')";
            mr.idu(q);
            sp.Speak("mail saved to drafts");
            MessageBox.Show("MAIL SAVED TO DRAFTS");
            inbox1 inb = new inbox1(username);
            this.Hide();
            inb.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inbox1 inb = new inbox1(username);
            this.Hide();
            inb.Show();
            
        }

        private void to_Leave(object sender, EventArgs e)
        {

        }

        private void SUB_Leave(object sender, EventArgs e)
        {
            
        }

        private void body_Leave(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
