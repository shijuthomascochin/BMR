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
    public partial class compose : Form
    {
        string   id, pswd, sendid, sub, body;
       
        SpeechSynthesizer sp;
        string a, username, userid, mailid;
        int c = 0,mailnum;
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        Match M1;
        mailreader mr = new mailreader();
        public compose()
        {
            InitializeComponent();
        }
         public compose(string s,int g)
        {
            sp = new SpeechSynthesizer();  
            InitializeComponent();
            username = s;
            mailnum = g;
             
       }
       


        private void compose_Load(object sender, EventArgs e)
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
            DataTable dt4 = mr.select(t);
            foreach (DataRow dr4 in dt4.Rows)
            {
                body = dr4[0].ToString();

            }

            
                uid();
                mail();
                
         
        
            mr.connect();
            string o = "select gmailid from userdetails where username='" + username + "' ";
            DataTable dt7 = mr.select(o);
            foreach (DataRow dr in dt7.Rows)
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
           
        }
        public void uid()
        {
            mr.connect();
            string p = "select userid from userdetails where username='" + username + "' ";
            DataTable dt = mr.select(p);
            foreach (DataRow dr in dt.Rows)
            {
                userid = dr[0].ToString();
            }
        }
       
        

        private void textBox1_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
                       
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
            
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            sp.Speak("from id");
            sp.Speak(textBox4.Text);


        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            sp.Speak("to id");
            sp.Speak(textBox6.Text+"@gmail.com+");
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            sp.Speak("subject");
            sp.Speak(textBox8.Text);
           
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

        private void textBox9_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            sp.Speak("body of mail");
            textBox9.Text = body + a;
            sp.Speak("press one to sent the mail , two for saving to draft , three for forwarding ,4 for clearing input and 5 for returning to draft ");
        }

        private void textBox9_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (textBox9.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox9.Text.Length == 3)
                {

                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox9.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox9.Text = "";
                        sp.Speak(a);

                    }

                }
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            if (textBox10.Text == "")
            {
                errorProvider1.SetError(textBox10, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter valid input");
                textBox10.Focus();
            }
            else if (textBox10.Text == "1")
            {
                sp.Speak("body of mail is");
                sp.Speak(body);
                mr.connect();
                id = textBox4.Text;
                
                sendid = textBox6.Text;
                string q = "insert into inbox values('" +userid  + "','" +int.Parse(mailid)+ "','" + textBox4.Text + "','" + textBox6.Text + "','nil','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox2.Text + "','SENT')";
                mr.idu(q);
                SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                mail.EnableSsl = true;
                NetworkCredential nw = new NetworkCredential(id, pswd);
                mail.Credentials = nw;
                mail.Send(id, sendid, textBox8.Text, textBox9.Text);
                mr.connect();
                string p = "update inbox set sort='SENT' where mailno='" + mailnum + "'";
                mr.idu(p);
                
                sp.Speak("mail has been sent");
                MessageBox.Show("MAIL SENT");
                draftform dft = new draftform(username);
                dft.Show();
                this.Hide();
                
            }
            else if (textBox10.Text == "2")
            {
                mr.connect();
                string q = "insert into inbox values('" + int.Parse(userid) + "','" + int.Parse(mailid) + "','" + textBox4.Text + "','" + textBox6.Text + "','nil','" + textBox8.Text + "','" + textBox9.Text + "','" + textBox2.Text + "','DRAFT')";
                mr.idu(q);
                sp.Speak("mail saved to drafts");
                MessageBox.Show("MAIL SAVED TO DRAFTS");
                homepage f2 = new homepage(username);
                f2.Show();
                this.Hide();
            }
            else if (textBox10.Text == "3")
            {
                textBox6.Clear();
            }
            else if (textBox10.Text == "4")
            
            {
                draftform dft = new draftform(username);
                dft.Show();
                this.Hide();
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
                sp.Speak("you have been idle ,application is going to exit");
                Application.Exit();
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
            
        }

       
    }



