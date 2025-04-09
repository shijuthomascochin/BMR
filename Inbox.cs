using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Net;
using System.Net.Mail;
using System.IO;
using blindmailreader;
using Net.Mail;
using Net.Mime;


using System.Security.Cryptography;
using System.Collections;
using System.Text.RegularExpressions;


namespace BMR
{
    public partial class inboxform : Form
    {
        private const string PopServer = "pop.gmail.com";
        private const int PopPort = 995;
        private string at;
        private string bt;
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        Match M1;
        SpeechSynthesizer sp;
        string FromName, Title, Content, body, pswd, sendid, id, sub, date, a;
        int c = 0;
        string username, gmail1, mailno, userid, b, content;
        mailreader mr = new mailreader();
        string folder = Environment.CurrentDirectory + "\\inbox\\";
        string folder1 = "E:\bmr";
        private String last_accessed_DateTime = "31-Jan-2011 00:00:00";
        public inboxform()
        {


            InitializeComponent();
        }
        public void mailfn()
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
        public inboxform(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
            class1.ReadOptions();
        }

        private void inboxform_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            textBox4.Visible = false;
            label5.Visible = false;
            sp.Speak("you are in inbox page");
            uid();
            gmail();

            string p = "select getdate()";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {

                date = dr[0].ToString();
                textBox3.Text = date;
            }


            mr.connect();
            string q = "select gmailid from userdetails where username='" + username + "'";
            DataTable dt5 = mr.select(q);
            foreach (DataRow dr in dt5.Rows)
            {
                at = dr[0].ToString();
            }
            mr.connect();
            string m = "select gmailpassword from userdetails where username='" + username + "'";
            DataTable dt1 = mr.select(m);
            foreach (DataRow dr1 in dt1.Rows)
            {
                bt = dr1[0].ToString();
            }

            showgrid();

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

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                mr.connect();
                string q = "select contents from inbox where mailno='" + textBox2.Text + "'";
                DataTable dt = mr.select(q);
                foreach (DataRow dr in dt.Rows)
                {
                    content = dr[0].ToString();
                    sp.Speak(content);
                }
                textBox1.Text = "";
                textBox1.Focus();
                sp.Speak("enter 1 for reading the selected mail,2 for forwarding the mail ,3 for deleting the mail, 4 for replying and 5 for navigating to home page");

            }
            else if (textBox1.Text == "2")
            {
                textBox4.Visible = true;
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


                //textBox1.Text = "";
                textBox4.Focus();

            }
            else if (textBox1.Text == "3")
            {
                mr.connect();
                string q = "update inbox set sort='TRASH' where mailno='" + textBox2.Text + "'";
                mr.idu(q);
                showgrid();
                sp.Speak("mail has been deleted");

                textBox1.Text = "";
                textBox1.Focus();
                sp.Speak("enter 1 for reading the selected mail,2 for forwarding the mail ,3 for deleting the mail, 4 for replying and 5 for navigating to home page");
                //textBox2.Focus();
                //sp.Speak("mails in inbox are");
                //mr.connect();
                //string k = "select mailno from inbox where sort='inbox' and userid ='" + userid + "'";
                //DataTable dt2 = mr.select(k);
                //mr.idu(k);
                //foreach (DataRow dr1 in dt2.Rows)
                //{
                //    b = dr1[0].ToString();

                //    sp.Speak(b);
                //}
                //sp.Speak("select a mail number ");
            }
            else if (textBox1.Text == "4")
            {
                REPLY reply = new REPLY(username, int.Parse(textBox2.Text));
                reply.Show();
                this.Hide();
            }
            else if(textBox1.Text=="5")
            {
                Form2 f2 = new Form2(username);
                f2.Show();
                this.Hide();

            }
            else if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("REQUIRED FIELD CANNOT BE BLANK");
                textBox1.Focus();
            }
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
            string q = "select * from inbox where sort='inbox' and userid='" + userid + "'";
            DataTable dt = mr.select(q);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
            //textBox2.Focus();

        }
        //public void showgrid1()
        //{

        //    mr.connect();
        //    string q1 = "select * from inbox where sort='inbox' and userid='" + userid + "'";
        //    DataTable dt1 = mr.select(q1);
        //    dataGridView1.DataSource = dt;
        //    dataGridView1.Refresh();
        //    //textBox2.Focus();

        //}



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            sp.Speak("enter 1 for reading the selected mail,2 for forwarding the mail ,3 for deleting the mail, 4 for replying and 5 for navigating to home page");

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            sp.Speak("press 1 for downloading new mails if you are connected to internet else press 2");

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            textBox4.Text = a;
            a = "";
            M1 = Regex.Match(textBox4.Text, eval);
            if (M1.Success)
            {
                sendid = textBox4.Text;
                SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                mail.EnableSsl = true;
                NetworkCredential nw = new NetworkCredential(id, pswd);
                mail.Credentials = nw;
                mail.Send(id, sendid, sub, body);
                mr.connect();
               
                mailfn();
                String sid = sendid;
                String ma = mailno;
                string q = "insert into inbox values('" + int.Parse(userid) + "','" +mailno+ "','" + id.ToString() + "','" + textBox4.Text + "','nil','" + sub.ToString() + "','" + body.ToString() + "','" + textBox3.Text + "','SENT')";
                mr.idu(q);
               
                string f = "update inbox set sort='SENT' where mailno='" + textBox2.Text + "'";
                mr.idu(f);

                sp.Speak("mail has been sent");
                textBox1.Text = "";
                textBox1.Focus();
                sp.Speak("enter 1 for reading the selected mail,2 for forwarding the mail ,3 for deleting the mail, 4 for replying and 5 for navigating to home page");


            }
            //else
            //{
            //    errorProvider1.SetError(textBox4, "enter the correct format of gmailid");
            //    sp.Speak("enter the correct format of gmailid");
            //    textBox4.Focus();
            //    textBox4.Text = "";
            //    a = "";
            //}
            
            
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
                    string t = "insert into inbox values('" + int.Parse(userid) + "',' " + int.Parse(mailno) + "','" + id.ToString() + "','" + gmail1.ToString() + "','nil','" + sub.ToString() + "','" + body.ToString() + "','" + textBox3.Text + "','inbox')";
                    mr.idu(t);

                }
                client.Noop();
                client.Rset();
                client.Quit();
            }

        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox4.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox4.Text.Length == 3)
                {

                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox4.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox4.Text = "";
                        sp.Speak(a);

                    }

                }
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "1")
            {
                pop();
                showgrid();
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
                textBox2.Focus();
            }

            else
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

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

