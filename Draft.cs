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
    public partial class draftform : Form
    {
        string b, id,cc, pswd, sendid, sub, body;
        SpeechSynthesizer sp;
        string username,user;
        int c = 0;
       
        mailreader mr = new mailreader();
        public draftform()
        {
            
            InitializeComponent();
        }
        public draftform(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
        }
        




        public void showgrid()
        {

            mr.connect();
            string q = "select * from inbox where sort='DRAFT' and userid='"+user+"'";
            DataTable dt = mr.select(q);
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();

            
        }

         private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void textBox2_Leave_1(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter valid input");
                textBox2.Focus();
            }
             else if (textBox2.Text == "1")
            {
                if (textBox1.Text == "")
                {
                    errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("enter mailnumber");
                    textBox1.Focus();
                }
                else
                {
                    sp.Speak("body of mail is");
                    sp.Speak(body);
                    mr.connect();
                    string q = "update inbox set sort='TRASH' where mailno='" + textBox1.Text + "'";
                    mr.idu(q);
                    showgrid();
                    sp.Speak("mail has been deleted");
                    textBox2.Text = "";
                    textBox2.Focus();
                   
                }

            }
            else if (textBox2.Text == "2")
            {
                if (textBox1.Text == "")
                {
                    errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("enter mailnumber");
                    textBox1.Focus();
                }
                else
                {
                    compose com = new compose(username, int.Parse(textBox2.Text));
                    com.Show();
                    this.Hide();


                }


            }
            else if (textBox2.Text == "3")
            {
                if (textBox1.Text == "")
                {
                    errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("enter mailnumber");
                    textBox1.Focus();
                }
                else
                {
                    sp.Speak("body of mail is");
                    sp.Speak(body);
                    SmtpClient mail = new SmtpClient("smtp.gmail.com", 587);
                    mail.EnableSsl = true;
                    NetworkCredential nw = new NetworkCredential(id, pswd);
                    mail.Credentials = nw;
                    mail.Send(id, sendid, sub, body);
                    mr.connect();
                    string q = "update inbox set sort='SENT' where mailno='" + textBox1.Text + "'";
                    mr.idu(q);
                    showgrid();
                    sp.Speak("mail has been sent");
                    //MessageBox.Show("MAIL SENT");
                    textBox2.Text = "";
                    textBox2.Focus();
                   
                }
            }
            else if (textBox2.Text == "4")
            {
                Form2 f2 = new Form2(username);
                f2.Show();
                loading();
                this.Hide();
            }

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            
        }

        private void draftform_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("your drafts");
            loading();
            showgrid();

            sp.Speak("mails in drafts are");
            mr.connect();
            string q = "select mailno from inbox where sort='DRAFT' and userid ='" + user + "'";
            DataTable dt = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt.Rows)
            {
                b = dr[0].ToString();

                sp.Speak(b);
            }
            sp.Speak("select a mail number ");
        }
           
            public void loading() 
            {
            mr.connect();
            string q = "select userid from userdetails where username='"+username+"' ";
            DataTable dt = mr.select(q);
            foreach (DataRow dr in dt.Rows)
            {
                user = dr[0].ToString();
            }
            }

            private void textBox1_Leave(object sender, EventArgs e)
            {
                
                    timer1.Enabled = false;
                    sp.Speak("press one for deleting the mail,two for recomposing the mail,three for senting the mail and 4 for navigating to homepage");
                    mr.connect();
                    string p = "select gmailid from userdetails where username='" + username + "' ";
                    DataTable dt = mr.select(p);
                    foreach (DataRow dr in dt.Rows)
                    {
                        id = dr[0].ToString();
                    }
                    mr.connect();
                    string q = "select toid from inbox  where mailno='" + textBox1.Text + "'";
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
                    string s = "select sub from inbox where mailno='" + textBox1.Text + "'";
                    DataTable dt3 = mr.select(s);
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        sub = dr3[0].ToString();
                    }
                    mr.connect();
                    string t = "select contents from inbox where mailno='" + textBox1.Text + "'";
                    DataTable dt4 = mr.select(t);
                    foreach (DataRow dr4 in dt4.Rows)
                    {
                        body = dr4[0].ToString();

                    }
                    mr.connect();
                    string u = "select Cc from inbox where mailno='" + textBox1.Text + "'";
                    DataTable dt5 = mr.select(u);
                    foreach (DataRow dr5 in dt5.Rows)
                    {
                        cc = dr5[0].ToString();
                    }
                
            }
            private void textBox3_TextChanged(object sender, EventArgs e)
            {

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

            private void textBox2_Enter(object sender, EventArgs e)
            {
                sp.Speak("press one for deleting the mail,two for recomposing the mail,three for senting the mail and 4 for navigating to homepage");
            }

           
            
        }

       
    }
