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

namespace BMR
{
    public partial class trash1 : Form
    {
        mailreader mr = new mailreader();
        string  username,userid;
        int c = 0;
        SpeechSynthesizer sp;
        public trash1()
        {
            InitializeComponent();
        }
         public trash1(string s)
        {
            sp = new SpeechSynthesizer();
            InitializeComponent();
            username = s;
        }
         public void loading()
         {
             mr.connect();
             string q = "select userid from userdetails where username='" + username + "' ";
             DataTable dt = mr.select(q);
             foreach (DataRow dr in dt.Rows)
             {
                 userid = dr[0].ToString();
             }
         }

         public void showgrid()
         {

             mr.connect();
            // String j="select                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
             string p = "select * from inbox where sort='TRASH' and userid='" + userid + "'";
             DataTable dt1 = mr.select(p);
             dataGridView1.DataSource = dt1;
             dataGridView1.Refresh();


         }

        private void trash1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
            sp.Speak("trash");
            loading();
            showgrid();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mr.connect();
            string q = "delete from inbox  where mailno='" + textBox2.Text + "'";
            mr.idu(q);
            showgrid();
            sp.Speak("mail has been deleted");
            MessageBox.Show("MAIL DELETED");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mr.connect();
            string q = "update inbox set sort='DRAFT' where mailno='" + textBox2.Text + "'";
            mr.idu(q);
            showgrid();
            sp.Speak("mail restored to drafts");
            MessageBox.Show("MAIL RESTORED TO DRAFT");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            homepage HOME = new homepage(username);
            HOME.Show();
            this.Hide();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            if (c == 1000)
            {
                sp.Speak("enter something otherwise the application will exit");
            }
            if (c == 3000)
            {
                sp.Speak("you have been idle ,try another time");
                Application.Exit();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
