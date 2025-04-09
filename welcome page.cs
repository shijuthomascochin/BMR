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
    public partial class Form1 : Form
    {
        mailreader mr = new mailreader();
        int c = 0;

        SpeechSynthesizer sp;

        public Form1()
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
        }







        private void Form1_Load_1(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("welcome to blind mail reader");
            sp.Speak("ENTER ONE FOR BLIND USER , TWO FOR NORMAL USER");
        }




       
        private void textBox1_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (textBox1.Text == "1")
            {
                Login log = new Login();
                log.Show();
               
                this.Hide();

            }
            else if (textBox1.Text == "2")
            {
                loginpage log1 = new loginpage();
                log1.Show();
                this.Hide();
            }
            else if(textBox1.Text=="")
            {
                errorProvider1.SetError(textBox1, "REQUIRED FIELD CANNOT BE BLANK");
                sp.Speak("REQUIRED FIELD CANNOT BE BLANK");
                textBox1.Focus();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

    
