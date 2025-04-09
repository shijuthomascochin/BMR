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
    public partial class composeform : Form
    {
        SpeechSynthesizer sp;
        string a,username,p,pwd,mailid;
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        Match M1;
        int c = 0;
        mailreader mr = new mailreader();
        public composeform()
        {
            InitializeComponent();
        }
        public composeform(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
        }





        private void to_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (to.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {

                if (to.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + to.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        to.Text = "";
                        sp.Speak(a);
                    }
                }
            }
        }

        

        
        

        private void to_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            to.Text = a;
            a = "";
           // M1 = Regex.Match(to.Text, eval);
            //if (M1.Success)
            //{

                sp.Speak("to");
                sp.Speak(to.Text+"@gmail.com");
            //}
            //else
            //{
            //    errorProvider1.SetError(to, "enter the correct format of gmailid");
            //    sp.Speak("enter the correct format of gmailid");
            //    to.Focus();
            //    to.Text = "";
            //    a = "";
            //}
            
            
            
            sp.Speak("enter subject of mail");
        }

       


        private void option_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (option.Text == "")
            {
                errorProvider1.SetError(option, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter valid input");
                option.Focus();
            }
            else if (option.Text == "1")
            {
                if (SUB.Text == "")
                {
                    errorProvider1.SetError(SUB, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("ENTER THE SUBJECT OF MAIL");
                    SUB.Focus();
                }
                if (body.Text == "")
                {
                    errorProvider1.SetError(body, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("ENTER THE body OF MAIL");
                    body.Focus();
                }
                else
                {
                    String gid = to.Text + "@gmail.com";
                    SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                    mail.EnableSsl = true;
                    string id = from.Text;
                    string sendid = to.Text+"@gmail.com";
                    string pswd = pwd;
                    NetworkCredential nw = new NetworkCredential(id, pswd);
                    mail.Credentials = nw;
                    mail.Send(id, sendid, SUB.Text, body.Text);
                    mr.connect();
                    string q = "insert into inbox values('" + int.Parse(p) + "','" + int.Parse(mailid) + "','" + from.Text + "','" +gid + "','nil','" + SUB.Text + "','" + body.Text + "','" + textBox2.Text + "','SENT')";
                    mr.idu(q);
                    sp.Speak("mail has been sent");
                   // MessageBox.Show("MAIL HAS BEEN SENT");
                    Form2 f2 = new Form2(username);
                    f2.Show();
                    this.Hide();
                }
            }
            else if (option.Text == "2")
            {
                String gid1 = to.Text + "@gmail.com";
                mr.connect();
                string q = "insert into inbox values('" + int.Parse(p) + "','" + int.Parse(mailid) + "','" + from.Text + "','" + gid1 + "','nil','" + SUB.Text + "','" + body.Text + "','" + textBox2.Text + "','DRAFT')";
                mr.idu(q);
                sp.Speak("mail saved to drafts");
                MessageBox.Show("MAIL SAVED TO DRAFTS");
                Form2 f2 = new Form2(username);
                f2.Show();
                this.Hide();

            }
            else if (option.Text == "3")
            {
                to.Clear();
                SUB.Clear();
                body.Clear();

            }
            else if (option.Text == "4")
            {
                Form2 f2 = new Form2(username);
                f2.Show();
                this.Hide();

            }
        }

        private void SUB_KeyUp_1(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
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
                        sp.Speak(a);
                    }
                }
            }
        }

        private void SUB_Leave_1(object sender, EventArgs e)
        {
            SUB.Text = a;
            a = "";           


                timer1.Enabled = true;
                
                sp.Speak("subject");
                sp.Speak(SUB.Text);
                sp.Speak("type the body of mail");
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
            

                sp.Speak(body.Text);
                sp.Speak("press one to sent mail,two to save to draft,three for clearing inputs and 4 to navigate to home page");
            
        }

        private void composeform_Load(object sender, EventArgs e)
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
            sp.Speak("enter to id");
            mr.connect();
            
                string q = "select isnull(max(mailno)+1,1)from inbox";
                DataTable dt6 = mr.select(q);
                mr.idu(q);
                foreach (DataRow dr in dt6.Rows)
                {
                   mailid = dr[0].ToString();
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
                p= dr[0].ToString();
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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
                sp.Speak("you have been idle ,the appilcation is going to exit");
                Application.Exit();
            }
            
        }

       
       
    }

}