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
using System.Net;
using System.Net.Mail;


namespace BMR
{
    public partial class outboxform : Form
    {
        string id, cc, pswd, sendid, sub, body;
        string b;
        SpeechSynthesizer sp;
        int c = 0;
        string username,userid;
        mailreader mr = new mailreader();
        public outboxform()
        {
            
            InitializeComponent();
        }
        public outboxform(string s)
     
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
        }
        private void outboxform_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            
            sp.Speak("outbox");
            loading();

            showgrid();
            sp.Speak("mails in outbox are");
            mr.connect();
            string q = "select mailno from inbox where sort='SENT' and userid ='" + userid + "'";
            DataTable dt2 = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt2.Rows)
            {
                b = dr[0].ToString();
                sp.Speak(b);
            }
            sp.Speak("select a mailnumber");
            
        }
        public void showgrid()
        {

            mr.connect();
            string p = "select * from inbox where sort='SENT' and userid='" + userid + "'";
            DataTable dt = mr.select(p);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

        }
        public void loading()
        {
            mr.connect();
            string q= "select userid from userdetails where username='" + username + "' ";
            DataTable dt1 = mr.select(q);
            foreach (DataRow dr1 in dt1.Rows)
            {
                userid = dr1[0].ToString();
            }
        }

        

        

       

        private void textBox3_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            textBox2.Text = "";
            if (textBox3.Text == "1")
            {
                mr.connect();
                string q="update inbox set sort='TRASH' where mailno='"+textBox2.Text+"'";
                mr.idu(q);
                showgrid();
                sp.Speak("mail deleted");
                MessageBox.Show("MAIL DELETED");
                textBox3.Text = "";
            }
            else if (textBox3.Text == "2")
            {
                sp.Speak("body of mail is");
                sp.Speak(body);
                SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                mail.EnableSsl = true;
                NetworkCredential nw = new NetworkCredential(id, pswd);
                mail.Credentials = nw;
                mail.Send(id, sendid, sub, body);
                mr.connect();
                string q = "update inbox set sort='SENT' where mailno='" + textBox2.Text + "'";
                mr.idu(q);
                showgrid();
                sp.Speak("mail has been sent");
                MessageBox.Show("MAIL SENT");
            }
            else
            {
                Form2 f2 = new Form2(username);
                f2.Show();
                this.Hide();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;          
            mr.connect();
            string p = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                id = dr[0].ToString();
            }
            mr.connect();
            string q = "select toid from inbox  where mailno='" + textBox2.Text + "'";
            DataTable dt1 = mr.select(q);
            foreach (DataRow dr1 in dt1.Rows)
            {
                sendid = dr1[0].ToString();
            }
            mr.connect();
            string r = "select gmailpassword from userdetails where username='" + username + "' ";
            DataTable dt2 = mr.select(r);
            foreach (DataRow dr2 in dt2.Rows)
            {
                pswd = dr2[0].ToString();
            }
            mr.connect();
            string s = "select sub from inbox where mailno='" + textBox2.Text + "'";
            DataTable dt3 = mr.select(s);
            foreach (DataRow dr3 in dt3.Rows)
            {
                sub = dr3[0].ToString();
            }
            mr.connect();
            string t = "select contents from inbox where mailno='" + textBox2.Text + "'";
            DataTable dt4 = mr.select(t);
            foreach (DataRow dr4 in dt4.Rows)
            {
                body = dr4[0].ToString();

            }
            mr.connect();
            string u = "select Cc from inbox where mailno='" + textBox2.Text + "'";
            DataTable dt5 = mr.select(u);
            foreach (DataRow dr5 in dt5.Rows)
            {
                cc = dr5[0].ToString();
            }
        
            
          
            
            sp.Speak("press one for deleting mail from outbox ,two for resending the mail and three for navigating to home page");
            
    
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
    }
}
