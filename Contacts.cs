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
using System.Text.RegularExpressions;


namespace BMR
{
    public partial class contacts : Form
    {
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        Match M1;
        SpeechSynthesizer sp;
        string a, username,userid;
        int c = 0;
        mailreader mr = new mailreader();
        public contacts()
        {
            
            InitializeComponent();
        }
        public contacts(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
            
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



        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;

            if (textBox3.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox3.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox3.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox3.Text = "";
                        sp.Speak(a);
                    }
                }
            }
        }
        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
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
        private void textBox4_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            textBox4.Text = a;
            a = "";
            if (textBox4.Text == "")
            {
                errorProvider1.SetError(textBox4, "enter the correct format of gmailid");
                sp.Speak("enter the correct format of gmailid");
                MessageBox.Show("ENTER CORRECT FORMAT OF GMAILID");

                textBox4.Focus();
                //textBox4.Text = "";
            }
            else
            {
                //M1 = Regex.Match(textBox4.Text, eval);
                //if (M1.Success)
                //{

                   // textBox4.Text = a;
                    timer1.Enabled = false;

                    sp.Speak("Gmailid");
                    sp.Speak(textBox4.Text+"@gmail.com");
                    //sp.Speak("enter gmail id");
                //}
                
           }
            }

            
            
                    

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.Text = a;
            a = "";
            if (textBox3.Text == "")
            {
                errorProvider1.SetError(textBox3, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter the contact name");
                textBox3.Focus();

            }
            else
            {
                timer1.Enabled = false;
                
                sp.Speak("contact name");
                sp.Speak(textBox3.Text);
                sp.Speak("enter gmail id");
            }
        }

        private void contacts_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("your contacts");
            
            
            uid();
            
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (textBox5.Text == "")
            {
                errorProvider1.SetError(textBox5, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter valid input");
                textBox5.Focus();
            }
            else if (textBox5.Text == "1")
            {
               // M1 = Regex.Match(textBox4.Text, eval);
                //if (M1.Success)
                //{

                    //textBox4.Text = a;
                String gid = textBox4.Text + "@gmail.com";
                    mr.connect();
                    string q = "insert into contacts values('" + int.Parse(userid) + "','" + textBox3.Text + "','" +gid+ "')";
                    mr.idu(q);
                    sp.Speak("contact added");
                   // MessageBox.Show("CONTACT ADDED");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    //textBox4.Focus();
                    textBox5.Focus();
               // }
                //else
                //{
                //    errorProvider1.SetError(textBox4, "enter the correct format of gmailid");
                //    sp.Speak("enter the correct format of gmailid");
                //    MessageBox.Show("ENTER CORRECT FORMAT OF GMAILID");

                //    textBox4.Focus();
                //    textBox4.Text = "";
                //}
               
            }

            else if (textBox5.Text == "2")
            {
                if (textBox3.Text == "")
                {
                    errorProvider1.SetError(textBox3, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("enter contact name");
                }
                else
                {
                    mr.connect();
                    string q = "delete from contacts where contactname='" + textBox3.Text + "'";
                    mr.idu(q);
                    sp.Speak("contact deleted");
                    MessageBox.Show("CONTACT DELETED");
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox4.Focus();
                }
            }
            else if (textBox5.Text == "3")
            {
                if (textBox3.Text == "")
                {
                    errorProvider1.SetError(textBox3, "REQUIRED FIELD CANNOT BE BLANK");
                    sp.Speak("enter contact name");
                }
                else
                {
                    mr.connect();
                    string q = "select gmailid from contacts where contactname='" + textBox3.Text + "'";
                    DataTable dt = mr.select(q);
                    foreach (DataRow dr in dt.Rows)
                    {
                        textBox4.Text = dr[0].ToString();
                        sp.Speak(textBox4.Text);

                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";

                    }
                }
            }
            else if (textBox5.Text == "4")
            {
               // M1 = Regex.Match(textBox4.Text, eval);
                //if (M1.Success)
                //{

                    textBox4.Text = a;
                //}
                //else
                //{
                   // errorProvider1.SetError(textBox4, "enter the correct format of gmailid");
                    sp.Speak("enter the correct format of gmailid");
                    MessageBox.Show("ENTER CORRECT FORMAT OF GMAILID");

                    textBox4.Focus();
                    textBox4.Text = "";
               // }
                mr.connect();
                string p = "update contacts set gmailid='" + textBox4.Text + "' where contactname='" + textBox3.Text + "'";
                mr.idu(p);
            }

            else if(textBox5.Text=="5")
            {
                Form2 f2 = new Form2(username);
                f2.Show();
                this.Hide();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            sp.Speak("enter contact name");
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter one for adding contact,2 for deleting contact,3 for searching contact and 4 for editing contacts and 5 for navigating to home page");
        }
    }
}
