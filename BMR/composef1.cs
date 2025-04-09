using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using blindmailreader;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;


namespace BMR
{
    public partial class composef1 : Form
    {
        string id, pswd, sendid, sub, body,body1;

        SpeechSynthesizer sp;
        string username, userid, mailid;
        int c = 0,mailnum;
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        Match M1;
        mailreader mr = new mailreader();
        public composef1()
        {
            InitializeComponent();
        }
         public composef1(string s,int g)
        {
            sp = new SpeechSynthesizer();  
            InitializeComponent();
            username = s;
            mailnum = g;
       }

        private void composef1_Load(object sender, EventArgs e)
        {
            
        
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("recompose your mail");
            timer1.Enabled = false;
            string p = "select getdate()";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                textBox2.Text = dr[0].ToString();

            }
            sp.Speak("date");
            sp.Speak(textBox2.Text);
            mr.connect();
                uid1();

                mail();
             
        
            mr.connect();
            string p1 = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt4 = mr.select(p1);
            foreach (DataRow dr in dt4.Rows)
            {
                id = dr[0].ToString();
                textBox4.Text = id;
            }
           
            mr.connect();
            string r = "select gmailpassword from userdetails where username='" + username + "' ";
            DataTable dt2 = mr.select(r);
            foreach (DataRow dr2 in dt2.Rows)
            {
                pswd = dr2[0].ToString();
            }
            mr.connect();
            string q = "select toid from inbox  where mailno='" + mailnum + "'";
            DataTable dt1 = mr.select(q);
            foreach (DataRow dr1 in dt1.Rows)
            {
                sendid = dr1[0].ToString();
                textBox6.Text = sendid;
            }
            mr.connect();
            string s = "select sub from inbox where mailno='" + mailnum + "'";
            DataTable dt3 = mr.select(s);
            foreach (DataRow dr3 in dt3.Rows)
            {
                sub = dr3[0].ToString();
                textBox8.Text = sub;

            }
            mr.connect();
            string t = "select contents from inbox where mailno='" + mailnum + "'";
            DataTable dt9 = mr.select(t);
            foreach (DataRow dr4 in dt9.Rows)
            {
                body = dr4[0].ToString();

            }

            
           
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
        public void uid1()
        {
            mr.connect();
            string p = "select userid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
               userid= dr[0].ToString();
            }
        }
        

        private void textBox1_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            
        }

        

        private void textBox9_Leave(object sender, EventArgs e)
        {
             body1 = body + textBox9.Text;
             textBox9.Text = body1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sp.Speak("body of mail is");
            sp.Speak(body1);
            mr.connect();
            id = textBox4.Text;
           
            sendid = textBox6.Text;
            string q = "insert into inbox values('" + int.Parse(userid)+ "','" + int.Parse(mailid) + "','" + textBox4.Text + "','" + textBox6.Text + "','nil','" + textBox8.Text + "','" + textBox9.Text+  "','" + textBox2.Text + "','SENT')";
            mr.idu(q);
            SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
            mail.EnableSsl = true;
            NetworkCredential nw = new NetworkCredential(id, pswd);
            mail.Credentials = nw;
            mail.Send(id, sendid, textBox8.Text, textBox9.Text);
            mr.connect();
            string p = "update inbox set sort='SENT' where mailno='" + mailnum+ "'";
            mr.idu(p);

            sp.Speak("mail has been sent");
            MessageBox.Show("MAIL SENT");
            draft1 dft = new draft1(username);
            dft.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            draft1 DFT = new draft1(username);
            DFT.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            homepage home = new homepage(username);
            home.Show();
            this.Hide();
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

        private void button4_Click(object sender, EventArgs e)
        {
            mr.connect();
            string q = "insert into inbox values('" + int.Parse(userid) + "','" + mailnum + "','" + textBox4.Text + "','" + textBox6.Text + "','nil','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox2.Text + "','DRAFT')";
            mr.idu(q);
            sp.Speak("mail saved to drafts");
            MessageBox.Show("MAIL SAVED TO DRAFTS");
            homepage f2 = new homepage(username);
            f2.Show();
            this.Hide();
        }

               

        private void textBox8_Leave(object sender, EventArgs e)
        {
            
        }

        }
    }

