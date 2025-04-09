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
    public partial class REPLY : Form
    {
        SpeechSynthesizer sp;
        string a, username, p, pwd, mailid;
        int c = 0, mailnum;
        mailreader mr = new mailreader();
        public REPLY()
        {
            InitializeComponent();
        }
        public REPLY(string s, int g)
        {

            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
            mailnum = g;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void REPLY_Load(object sender, EventArgs e)
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
            sp.Speak("date");
            sp.Speak(textBox2.Text);
            mr.connect();
            string k = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt9 = mr.select(k);
            foreach (DataRow dr in dt9.Rows)
            {
                from.Text = dr[0].ToString();
            }
            sp.Speak("from id ");
            sp.Speak(from.Text);
            mr.connect();
            string q = "select fromid from inbox where mailno='" + mailnum + "'";
            DataTable dt2 = mr.select(q);
            foreach (DataRow dr in dt2.Rows)
            {
                to.Text = dr[0].ToString();
            }
            sp.Speak("to id");
            sp.Speak(to.Text);
            uid();
            password();
            mail();

        }
        public void mail()
        {
            string q = "select isnull(max(mailno)+1,1)from inbox";
            DataTable dt6 = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt6.Rows)
            {
                mailid = dr[0].ToString();
            }
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

        private void SUB_KeyUp(object sender, KeyEventArgs e)
        {
            if (SUB.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (SUB.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + SUB.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();

                        SUB.Text = "";
                    }
                    sp.Speak(a);
                }

            }
        }

        private void body_KeyUp(object sender, KeyEventArgs e)
        {
            if (body.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (body.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + body.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();

                        body.Text = "";
                    }
                    sp.Speak(a);
                }

            }
        }

        private void body_Leave(object sender, EventArgs e)
        {
            body.Text = a;
            a = "";
            if (body.Text == "")
            {
                errorProvider1.SetError(body, "required field cannot be blank");
                sp.Speak("enter body of mail");
                body.Focus();
            }
            else
            {

                sp.Speak(body.Text);
                sp.Speak("press one to sent mail,two to save to draft,three for clearing inputs and 4 to navigate to inbox page");
            }
        }

        private void SUB_Leave(object sender, EventArgs e)
        {
            SUB.Text = a;
            a = "";
            if (SUB.Text == "")
            {
                errorProvider1.SetError(SUB, "required field cannot be blank");
                sp.Speak("enter subject of mail");
                SUB.Focus();

            }
            else
            {
                sp.Speak(SUB.Text);
            }
            

        }

        private void option_Leave(object sender, EventArgs e)
        {

            if (option.Text == "1")
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
                string q = "insert into inbox values('" + int.Parse(p) + "','" + int.Parse(mailid) + "','" + from.Text + "','" + to.Text + "','nil','" + SUB.Text + "','" + body.Text + "','" + textBox2.Text + "','SENT')";
                mr.idu(q);
                sp.Speak("mail has been sent");
               // MessageBox.Show("MAIL HAS BEEN SENT");
                inboxform inb = new inboxform(username);
                inb.Show();
                this.Hide();
            }
            if (option.Text == "2")
            {
                mr.connect();
                string q = "insert into inbox values('" + int.Parse(p) + "','" + mailnum + "','" + from.Text + "','" + to.Text + "','nil','" + SUB.Text + "','" + body.Text + "','" + textBox2.Text + "','DRAFT')";
                mr.idu(q);
                sp.Speak("mail saved to drafts");
                MessageBox.Show("MAIL SAVED TO DRAFTS");
                inboxform f2 = new inboxform(username);
                f2.Show();
                this.Hide();
            }
            if (option.Text == "3")
            {
                SUB.Clear();
                body.Clear();
            }
            if (option.Text == "4")
            {
                inboxform inb = new inboxform(username);
                this.Hide();
                inb.Show();
            }
            if (option.Text == "")
            {
                errorProvider1.SetError(option, "required field cannot be blank");
                sp.Speak("ENTER VALID OPTION");
                option.Focus();
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

        private void option_Enter(object sender, EventArgs e)
        {

        }
    }
}

