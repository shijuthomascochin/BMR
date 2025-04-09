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
using System.Net;
using System.Net.Mail;
using System.IO;
using Net.Mail;
using Net.Mime;
using System.Security.Cryptography;
using System.Collections;
using System.Text.RegularExpressions;


namespace BMR
{
    public partial class inbox1 : Form
    {
        private const string PopServer = "pop.gmail.com";
        private const int PopPort = 995;
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        Match M1;
        SpeechSynthesizer sp;
        string FromName, Title, Content, body, pswd, sendid, id, sub,a;
        int c = 0;
        string username, gmail1, mailno, userid, b, content;
        mailreader mr = new mailreader();
        string at;
        string bt, folder = Environment.CurrentDirectory + "\\inbox\\";
        string folder1 = "D:\bmr";
        private String last_accessed_DateTime = "31-Jan-2011 00:00:00";
        public inbox1()
        {
            
            InitializeComponent();
        }
        public void mailfn()
        {
            mr.connect();

            string q = "select isnull(max(mailno)+1,1)from inbox";
            DataTable dt = mr.select(q);
            // mr.idu(q);
            foreach (DataRow dr in dt.Rows)
            {
                mailno = dr[0].ToString();
            }

        }
         public inbox1(string s)
     
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
            class1.ReadOptions();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            if (c == 1000)
            {
                sp.Speak("enter something otherwise the application will exit");
            }
            if (c == 3000)
            {
                sp.Speak("you have been idle ,try another time");
                Application.Exit();
            }
        }
        public void pop()
        {
            Pop3Client client = new Pop3Client(PopServer, PopPort, true, at, bt);
            {
                client.Authenticate();
                client.Stat();
                foreach (Pop3ListItem item in client.List())
                {
                    MailMessageEx message = client.RetrMailMessageEx(item);
                    id = message.From.ToString();
                    sub = message.Subject;
                    body = HtmlRemoval.StripTagsRegex(message.Body);
                    mr.connect();
                    mailfn();
                    string t = "insert into inbox values('" + int.Parse(userid) + "',' " + int.Parse(mailno) + "','" + id.ToString() + "','" + gmail1.ToString() + "','nil','" + sub.ToString() + "','" + body.ToString() + "','" + textBox1.Text + "','INBOX')";
                    mr.idu(t);

                }
                client.Noop();
                client.Rset();
                client.Quit();
            }

        }

        private void inbox1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            textBox3.Visible = false;
            label5.Visible = false;
            sp.Speak("you are in inbox page");
            uid();
            gmail();
            string p = "select getdate()";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {

                textBox1.Text = dr[0].ToString();

            }

            mr.connect();
            string q = "select gmailid from userdetails where username='" + username + "'";
            DataTable dt5 = mr.select(q);
            foreach (DataRow dr in dt5.Rows)
            {
                at = dr[0].ToString();
            }
            mr.connect();
            string g = "select gmailpassword from userdetails where username='" + username + "'";
            DataTable dt1 = mr.select(g);
            foreach (DataRow dr1 in dt1.Rows)
            {
                bt = dr1[0].ToString();
            }
            
            showgrid();
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
            sp.Speak("mails in inbox are");
            mr.connect();
            string k = "select mailno from inbox where sort='inbox' and userid ='" + userid + "'";
            DataTable dt2 = mr.select(k);
            mr.idu(k);
            foreach (DataRow dr1 in dt2.Rows)
            {
                b = dr1[0].ToString();

                sp.Speak(b);
            }
            sp.Speak("select a mail number ");
        }
        public void uid()
        {
            mr.connect();
            string p = "select userid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                userid = dr[0].ToString(); ;
            }
        }

        
        public void gmail()
        {
            mr.connect();
            string p = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                gmail1 = dr[0].ToString();
            }
        }
        public void showgrid()
        {

            mr.connect();
           // string q = "select mailno,fromid,toid,sub,contents from inbox where sort='inbox' and userid='" + userid + "'";
            string q = "select mailno,fromid,toid,sub,contents from inbox where sort='inbox' and toid='" + gmail1 + "'";
            DataTable dt = mr.select(q);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();


        }


        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannnot be blank");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox2.Focus();


            }
            else
            {
                mr.connect();
                string q = "select contents from inbox where mailno='" + textBox2.Text + "'";
                DataTable dt = mr.select(q);
                foreach (DataRow dr in dt.Rows)
                {
                    content = dr[0].ToString();
                    sp.Speak(content);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannnot be blank");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox2.Focus();


            }
            else
            {
                textBox3.Visible = true;
                label5.Visible = true;
                mr.connect();
                string p = "select gmailid from userdetails where username='" + username + "' ";
                DataTable dt = mr.select(p);
                foreach (DataRow dr in dt.Rows)
                {
                    id = dr[0].ToString();

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

            }

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            M1 = Regex.Match(textBox3.Text, eval);
            if (M1.Success)
            {

                sendid = textBox3.Text;
                
                
                SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                mail.EnableSsl = true;
                NetworkCredential nw = new NetworkCredential(id, pswd);
                mail.Credentials = nw;
                mail.Send(id, sendid, sub, body);
                mr.connect();

                mailfn();
        
                String n = mailno;
                string f = "insert into inbox values('" + int.Parse(userid) + "','" +mailno + "','" + id.ToString() + "','" + gmail1.ToString() + "','nil','" + sub.ToString() + "','" + body.ToString() + "','" + textBox1.Text + "','SENT')";
                mr.idu(f);
                MessageBox.Show("mail has been sent");
            }
            else
            {
                errorProvider1.SetError(textBox3, "enter the correct format of gmailid");
                MessageBox.Show("enter the correct format of gmailid");

                textBox3.Text = "";
                textBox3.Focus();
                
            }         
            

            
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            sp.Speak("press tab after entering the mail id");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannnot be blank");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox2.Focus();


            }
            else
            {
                mr.connect();
                string q = "update inbox set sort='TRASH' where mailno='" + textBox2.Text + "'";
                mr.idu(q);
                showgrid();
                sp.Speak("mail has been deleted");

                textBox1.Text = "";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            homepage hme = new homepage(username);
            hme.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "required field cannnot be blank");
                MessageBox.Show("REQUIRED FIELD CANNOT BE BLANK");
                textBox2.Focus();


            }
            else
            {
                replypage REP = new replypage(username, int.Parse(textBox2.Text));
                REP.Show();
                this.Hide();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pop();
            showgrid();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            homepage h1 = new homepage(username);
            h1.Show();
            this.Hide();
        }
    }
}
