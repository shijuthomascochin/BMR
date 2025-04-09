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
    public partial class Form2 : Form
    {
        string uname;
        SpeechSynthesizer sp;
        int c = 0;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            uname = s;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (textBox1.Text == "1")
            {
                inboxform inb = new inboxform(uname);
                inb.Show();
                this.Hide();
            }
            else if (textBox1.Text == "2")
            {
                OUTBOX ot = new OUTBOX(uname);
                ot.Show();
                this.Hide();
            }
            else if (textBox1.Text == "3")
            {
                trashform trash = new trashform(uname);
                trash.Show();
                this.Hide();

            }
            else if (textBox1.Text == "4")
            {
                draftform draft = new draftform(uname);
                draft.Show();
                this.Hide();
            }
            else if (textBox1.Text == "5")
            {
                Form1 f2 = new Form1();
                f2.Show();
                this.Hide();
            }
            else if (textBox1.Text == "6")
            {
                composeform cmp = new composeform(uname);
                cmp.Show();
                this.Hide();
            }
            else if (textBox1.Text == "7")
            {
                
                contacts cnt = new contacts(uname);
                cnt.Show();
                this.Hide();

            }
            else if (textBox1.Text == "8")
            {
                password pwd = new password(uname);
                pwd.Show();
                this.Hide();
            }

            else if(textBox1.Text=="")
            {
                errorProvider1.SetError(textBox1, "required field cannot be balnk");
                sp.Speak("enter valid input");
                textBox1.Focus();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            inboxform inb = new inboxform();
            inb.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OUTBOX outb = new OUTBOX();
            outb.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            trashform trash = new trashform();
            trash.Show();
            this.Hide();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            draftform draft = new draftform(uname);
            draft.Show();
            this.Hide();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("home  page");
            sp.Speak("enter   one for inbox, two for outbox ,three for trash ,four for draft ,five for logout ,six for composing mail, seven for contact form and eight for changing password");
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

       

        

        
        
        

       
     

        }



        
          
       

        




    }

