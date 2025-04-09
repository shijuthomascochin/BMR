using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;


namespace BMR
{
    public partial class homepage : Form
    {
        string uname;
        SpeechSynthesizer sp;
        int c = 0;
        public homepage()
        {
            InitializeComponent();
        }
        public homepage(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            uname = s;
        }

        private void homepage_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("home page, click on the link to where you need to navigate");
     
        }
        

       
       

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            inbox1 inb = new inbox1(uname);
            inb.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            otbox ot = new otbox(uname);
            ot.Show();
            this.Hide();

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            trash1 trash = new trash1(uname);
            trash.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            draft1 draft = new draft1(uname);
            draft.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            Form1 f2 = new Form1();
            f2.Show();
            this.Hide();
            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            compose1 comp = new compose1(uname);
            comp.Show();
            this.Hide();
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            contact1 con = new contact1(uname);
            con.Show();
            this.Hide();
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

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            password1 pwd = new password1(uname);
            pwd.Show();
            this.Hide();
        }
    }
}
