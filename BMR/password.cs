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

namespace BMR
{
    public partial class password : Form
    {
        string uname, a;
        SpeechSynthesizer sp;
        int c = 0;
        mailreader mr = new mailreader();
        public password()
        {
            InitializeComponent();
        }
        public password(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            uname = s;
        }

        private void password_Load(object sender, EventArgs e)
        {
            textBox2.Text = uname;
            sp.Speak("change your password");
            sp.Speak("select 1 for changing bmr password , 2 for changing gmail password ");
        }


        private void timer1_Tick_1(object sender, EventArgs e)
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



        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                errorProvider1.SetError(textBox5, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("ENTER CONFIRM PASSWORD");
                return;
            }
            if (textBox3.Text == "")
            {
                errorProvider1.SetError(textBox3, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("ENTER  old PASSWORD ");
                return;
            }

            if (textBox4.Text == "")
            {
                errorProvider1.SetError(textBox4, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("ENTER NEW PASSWORD");
                return;
            }
            
            
            if (textBox6.Text == "")
            {
                errorProvider1.SetError(textBox6, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("ENTER VALID OPTION");
                textBox6.Focus();
            }
            
            else
            {
                if (textBox6.Text == "1")
                {
                    if (textBox1.Text == "1")
                    {
                        if (textBox5.Text == textBox4.Text)
                        {
                            mr.connect();
                            string q = "update userdetails set gmailpassword='" + textBox4.Text + "' where username='" + textBox2.Text + "' and gmailpassword='" + textBox3.Text + "'";
                            mr.idu(q);
                            sp.Speak("PASSWORD CHANGED");
                            //textBox5.Text = "";
                            //textBox5.Focus();
                            textBox6.Text = "";
                            //sp.Speak("enter two for navigating to home page");
                            textBox6.Focus();
                        }
                        else
                        {
                            sp.Speak("password not correct");
                            textBox5.Focus();
                        }
                    }


                    
                    else if (textBox1.Text == "2")
                    {
                        if (textBox5.Text == textBox4.Text)
                        {
                            mr.connect();
                            string q = "update userdetails set password='" + textBox4.Text + "' where username='" + textBox2.Text + "' and password='" + textBox3.Text + "'";
                            mr.idu(q);
                            sp.Speak("PASSWORD CHANGED");
                        }
                        else
                        {
                            sp.Speak("password not correct");
                            textBox5.Focus();

                        }
                    }
                }
                else if(textBox6.Text=="2")
                {
                    Form2 home = new Form2(uname);
                    home.Show();
                    this.Hide();

                }
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            
            
                textBox3.Text = a;
                a = "";

            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {

            
                textBox4.Text = a;
                a = "";
            
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            
            
                textBox5.Text = a;
                a = "";
            
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
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

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox5.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox5.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox5.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox5.Text = "";
                        sp.Speak(a);
                    }
                }
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter old password");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter new password");
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            sp.Speak("confirm password");
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter one for saving password and two for navigating to home page");
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox3.Focus();
        }
    }
}