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



namespace BMR
{
    public partial class Login : Form
    {
        string a;
        mailreader mr = new mailreader();
        int c = 0;

        SpeechSynthesizer sp;
        public Login()
        {
            sp = new SpeechSynthesizer();
            a = "";
            InitializeComponent();
           
        }

       private void loginform_Load(object sender, EventArgs e)
        {
            
            
            sp.Speak("enter one for registration ");
            sp.Speak("press tab for entering username and password ");
            timer1.Start();
            timer1.Enabled = true;
        }
           
        private void Login_MouseClick(object sender, MouseEventArgs e)
        {
            
            timer1.Enabled = false;
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
       

                 
        
    

  
            
       private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            if (textBox1.TextLength == 0 && e.KeyCode.ToString().Equals("back"))
            {
                if (a != "")
                {
                    a = a.Substring(0, a.Length - 1);
                }
            }
            if(e.KeyValue>47 && e.KeyValue<58)
            {
                insert();
            }

        }
        public void insert()
        {
           
            if (textBox1.Text.Length == 3)
            {
                mr.connect();
                string q = "select alphabets from keyconversion where keycode='" + textBox1.Text + "'";

                DataTable dt = mr.select(q);

                if (dt.Rows.Count == 1)
                {
                    a = a + dt.Rows[0][0].ToString();
                    textBox1.Text = "";
                    sp.Speak(a);
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Text = a;
            a = "";
            
                timer1.Enabled = false;

                
                sp.Speak("username");
                sp.Speak(textBox1.Text);

            
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
        public void load()
        {
            mr.connect();
            string q = "select * from userdetails where username='" + textBox1.Text + "'and password='" + textBox2.Text + "'";
            DataTable dt = mr.select(q);
            mr.idu(q);

            if (dt.Rows.Count == 1)
            {
                sp.Speak("hello");
                sp.Speak(textBox1.Text);
                Form2 f2 = new Form2(textBox1.Text);
                f2.Show();
                this.Hide();
            }

            else
            {
                sp.Speak("invalid user name or password");
                MessageBox.Show("INVALID USER NAME OR PASSWORD");
                textBox1.Clear();
                textBox2.Clear();
            }


        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            textBox2.Text = a;
            a = "";
            if (textBox1.Text == "")
            {
                errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("enter user name");
                textBox1.Focus();
            }
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2, "REQUIRED FIELD CANNOT BE BLANK");
                MessageBox.Show("ENTER PASSWORD");
                textBox2.Focus();
            }
            else
            {


                sp.Speak("password");
                sp.Speak(textBox2.Text);
                load();
            }
        }
        
        private void textBox3_Leave(object sender, EventArgs e)
        {
            timer1.Stop();

            timer1.Enabled = false;
            
            

                if (textBox3.Text == "1")
                {
                    registrationform reg = new registrationform();
                    reg.Show();
                    this.Hide();
                }
                else if (textBox3.Text == "2")
                {
                    Application.Exit();

                }
                
            
        }

 private void textBox2_Enter(object sender, EventArgs e)
 {
     sp.Speak("enter password");
 }

 private void textBox1_Enter(object sender, EventArgs e)
 {
     sp.Speak("enter username");
 }

 private void label4_Click(object sender, EventArgs e)
 {

 }
}
       


}


        
       

       

        
    
