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
using System.Text.RegularExpressions;

namespace BMR
{
    public partial class registrationform : Form
    {
        string a;
       // string eval="\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
               @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
               @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
       
        //Match M1;
        mailreader mr = new mailreader();
        SpeechSynthesizer sp;
        int d = 0;
        public registrationform()
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            load();  
        }




        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;  
            sp.Speak("registration form");
            mr.connect();
            string q = "select isnull(max(userid)+1,1)from userdetails";
            DataTable dt = mr.select(q);
            mr.idu(q);
            foreach (DataRow dr in dt.Rows)
            {
                textBox1.Text = dr[0].ToString();
            }
        }






        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            
            if (textBox2.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox2.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox2.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox2.Text = "";
                        sp.Speak(a);
                    }
                }



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


        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {

            timer1.Enabled = true;
            if (textBox5.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                insert();
            }
        }
        public void insert()
        {

            if (textBox5.Text.Length == 3)
            {
                mr.connect();
                string q = "select alphabets from keyconversion where keycode='" + textBox5.Text + "'";

                DataTable dt = mr.select(q);

                if (dt.Rows.Count == 1)
                {
                    a = a + dt.Rows[0][0].ToString();
                    textBox5.Text = "";
                    sp.Speak(a);
                }
            }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            
            if (textBox6.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox6.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox6.Text + "'";
                    DataTable dt = mr.select(q);
                   // mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox6.Text = "";
                        sp.Speak(a);
                    }
                }
            }
        }


        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (textBox7.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox7.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox7.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox7.Text = "";
                        sp.Speak(a);
                    }

                }
            }
        }
        private void textBox7_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
           
                textBox7.Text = a;
                a = "";
                sp.Speak("gmail password");
                sp.Speak(textBox7.Text);
                
            }
        
        private void textBox6_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            textBox6.Text = a;
            a = "";
           // System.Text.RegularExpressions.Match match =
               // Regex.Match(textBox6.Text.Trim(), pattern, RegexOptions.IgnoreCase);

            //if (match.Success)
            //{
                
                sp.Speak("gmail id");
                sp.Speak(textBox6.Text+"@gmail.com");
            //}
            //else
            //{
            //    errorProvider1.SetError(textBox6, "enter the correct format of gmailid");
            //    sp.Speak("enter the correct format of gmailid");
            //    textBox6.Focus();
            //    textBox6.Text = "";
            //    a = "";
            //}

            
            
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            textBox5.Text = a;
            a = "";

            timer1.Enabled = false;


            sp.Speak("username");
            sp.Speak(textBox5.Text);
            
        }

        

        private void textBox3_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;  
            textBox3.Text = a;
            a = "";
            sp.Speak("password");
            sp.Speak(textBox3.Text);
            
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            
            mr.connect();
            string p = "select username from userdetails where username='"+a+"'";
            DataTable dt = mr.select(p);
            mr.idu(p);
            if(dt.Rows.Count==1)
            {
               
                    sp.Speak("username is in use, try another username");
                   
                    a = "";
                    textBox2.Clear();
                    textBox2.Focus();
                    
                }

                else
                {
                    textBox2.Text = a;
                    a = "";
                    sp.Speak("username");
                    sp.Speak(textBox2.Text);
                    
                }
            }
        
            

                   
        public void load()
        {
            mr.connect();
            int a = int.Parse(textBox4.Text);
            String gid = textBox6.Text + "@gmail.com";
            if (a > 18)
            {
                if (textBox9.Text == "1")
                {
                    
                    string q = "insert into userdetails values('" + int.Parse(textBox1.Text) + "','" + textBox2.Text + "','" + textBox3.Text + "','" + int.Parse(textBox4.Text) + "','MALE','" + textBox5.Text + "','" +gid+ "','" + textBox7.Text + "')";
                    mr.idu(q);
                    

                }
                else
                {

                    string q = "insert into userdetails values('" + int.Parse(textBox1.Text) + "','" + textBox2.Text + "','" + textBox3.Text + "','" + int.Parse(textBox4.Text) + "','FEMALE','" + textBox5.Text + "','" +gid+ "','" + textBox7.Text + "')";

                    mr.idu(q);
                    

                }
            }
            if (a < 18)
            {
                sp.Speak("SORRY AGE LIMIT ABOVE 18");
                
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
            }
            
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            
            if (textBox8.Text == "1")
            {
                if (textBox2.Text == "")
                {
                    errorProvider1.SetError(textBox2, "required field cannot be blank");
                    sp.Speak("enter user name");
                    return;
                }
                if (textBox3.Text == "")
                {
                    errorProvider1.SetError(textBox3, "required field cannot be blank");
                    sp.Speak("enter password");
                    return;
                }
                if (textBox10.Text == "")
                {
                    errorProvider1.SetError(textBox10, "required field cannot be blank");
                    sp.Speak("enter password again");
                    return;
                }
                if (textBox4.Text == "")
                {
                    errorProvider1.SetError(textBox4, "required field cannot be blank");
                    sp.Speak("enter age");
                    return;
                }
                if (textBox9.Text == "")
                {
                    errorProvider1.SetError(textBox9, "required field cannot be blank");
                    sp.Speak("enter gender");
                    return;
                }
                if (textBox5.Text == "")
                {
                    errorProvider1.SetError(textBox5, "required field cannot be blank");
                    sp.Speak("enter country");
                    return;
                }
                if (textBox7.Text == "")
                {
                    errorProvider1.SetError(textBox7, "required field cannot be blank");
                    sp.Speak("enter gmail password");
                    return;
                }
                if (textBox8.Text == "")
                {
                    errorProvider1.SetError(textBox8, "required field cannot be blank");
                    sp.Speak("enter valid option");
                    return;
                }
                else
                {

                    load();
                    sp.Speak("your account has been created successfully");
                    Login l = new Login();
                    l.Show();
                }

            }

            else if (textBox8.Text == "2")
            {
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                textBox7.Clear();
            }
            else if(textBox8.Text=="3")
            {
                Login lg = new Login();
                lg.Show();
                this.Hide();
            }
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;  
            sp.Speak("age");
            sp.Speak(textBox4.Text);
            
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;  
            if (textBox9.Text == "1")
            {
                sp.Speak("gender");
                sp.Speak("male");
            }
            else
            {
                sp.Speak("gender");
                sp.Speak("female");
            }
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;  
            
            
        }

       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            d++;
            if (d == 100)
            {
                sp.Speak("enter something otherwise the application will exit");
            }
            if (d == 400)
            {
                sp.Speak("you have been idle ,try another time");
                Application.Exit();
            }
            

        }

        private void textBox10_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (textBox10.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if (e.KeyValue > 47 && e.KeyValue < 58)
            {
                if (textBox10.Text.Length == 3)
                {
                    mr.connect();
                    string q = "select alphabets from keyconversion where keycode='" + textBox10.Text + "'";
                    DataTable dt = mr.select(q);
                    mr.idu(q);

                    if (dt.Rows.Count == 1)
                    {
                        a = a + dt.Rows[0][0].ToString();
                        textBox10.Text = "";
                        sp.Speak(a);
                    }
                }
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
           
            timer1.Enabled = false;


           // sp.Speak("username");
            //sp.Speak(textBox5.Text);
            if (a == textBox3.Text)
            {
                textBox10.Text = a;
                a = "";
                sp.Speak("password correct ");
            }
            else
            {
                textBox10.Text = "";
                sp.Speak("password incorrect");
                textBox10.Text = "";
                textBox10.Focus();
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter username");
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter gender,press one for male and two for female");
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter one for creating account,  two for clearing text boxes , three for navigating to login page ");
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter password");
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter your password once again");
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter age");
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter country");
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter gmail id");
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            sp.Speak("enter gamil password");
        }

       
       

        
       
    }
}
