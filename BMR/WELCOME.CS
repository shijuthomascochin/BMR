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
    public partial class WELCOME : Form
    {
        mailreader mr = new mailreader();
        int c = 0;
        
        SpeechSynthesizer sp;

        public WELCOME()
        {
           
            sp = new SpeechSynthesizer();
            InitializeComponent();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "1")
            {
                Login lg = new Login();
                lg.Show();
                this.Hide();
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sp.Speak("ENTER ONE FOR BLIND USER AND TWO FOR NORMAL USER"); 
        }

        
    }
}
