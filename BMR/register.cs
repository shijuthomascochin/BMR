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
    public partial class register : Form
    {
        string eval = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        Match M1;
        mailreader mr = new mailreader();
        SpeechSynthesizer sp;
        int d = 0;
        public register()
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                errorProvider1.SetError(textBox2," Enter username");
                MessageBox.Show("enter username");
                return;
            }
            if (textBox3.Text == "")
            {
                errorProvider1.SetError(textBox3,"Enter password ");
                MessageBox.Show("enter password");
                return;
            }
            if (textBox4.Text == "")
            {
                errorProvider1.SetError(textBox4," enter age");
                MessageBox.Show("enter age");
                return;
            }
            if (textBox9.Text == "")
            {
                errorProvider1.SetError(textBox9,"enter gender" );
                MessageBox.Show("enter gender");
                return;
            }
            if (textBox5.Text == "")
            {
                errorProvider1.SetError(textBox5,"enter country");
                 MessageBox.Show("enter country");
                return;
            }
            if (textBox6.Text == "")
            {
                errorProvider1.SetError(textBox6,"enter gmailid");
                MessageBox.Show("enter gmailid");
                return;
            }
            if (textBox7.Text == "")
            {
                errorProvider1.SetError(textBox7,("enter password") );
                MessageBox.Show("enter password");
                return;
            }
            else
            {
                load();
            }
        }

        private void register_Load(object sender, EventArgs e)
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
        public void load()
        {
            mr.connect();
            String gid = textBox6.Text + "@gmail.com";
            int a = int.Parse(textBox4.Text);
            if (a > 18)
            {
               

                    string q = "insert into userdetails values('" + int.Parse(textBox1.Text) + "','" + textBox2.Text + "','" + textBox3.Text + "','" + int.Parse(textBox4.Text) + "','"+textBox9.Text+"','" + textBox5.Text + "','" +gid+ "','" + textBox7.Text + "')";

                    mr.idu(q);
                    MessageBox.Show("YOUR ACCOUNT HAS BEEN CREATED SUCCESSFULLY");

                
            }
            if (a < 18)
            {
                sp.Speak("SORRY AGE LIMIT ABOVE 18");
                MessageBox.Show("SORRY AGE LIMIT ABOVE 18");
            }

            loginpage f1 = new loginpage();
            f1.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            
            
            timer1.Enabled = false;

            mr.connect();
            string p = "select username from userdetails where username='" + textBox2.Text + "'";
            DataTable dt = mr.select(p);
            mr.idu(p);
            if (dt.Rows.Count == 1)
            {


                label10.Text = "USERNAME IN USE TRY ANOTHER USERNAME";
                textBox2.Text = "";
                textBox2.Focus();


            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
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


        

        private void textBox8_Leave(object sender, EventArgs e)
        {
            
            if (textBox8.Text == textBox3.Text)
            {
                sp.Speak("password match");
                MessageBox.Show("PASSWORD MATCH");
            }
            else if(textBox8.Text != textBox3.Text)
            {
                sp.Speak("password doesnot match");
                MessageBox.Show("PASSWORD DOESNOT MATCH");
                textBox8.Text = "";
                textBox8.Focus();

            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            //M1 = Regex.Match(textBox6.Text, eval);
            //if (M1.Success)
            //{

            //    textBox7.Focus();
            //}
            //else
            //{
            //    errorProvider1.SetError(textBox6, "enter the correct format of gmailid");
               
            //    MessageBox.Show("ENTER CORRECT FORMAT OF GMAILID");
                
            //    textBox6.Focus();
            //    textBox6.Text = "";
                
            //}

            
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            
            
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            
            
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
           
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            
            
        }

        
    }
}
