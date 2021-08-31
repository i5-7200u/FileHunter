using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace filehunter
{
    public partial class fh : Form
    {
        long filelenghtcontrol;
        int randomchararraylenght = 3072;
        int shreddedcontrol = 0;
        public fh()
        {
            InitializeComponent();
        }
    //downstairs code invalid
        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) > 50)
            {
                MessageBox.Show("Maksimum 50 MB Ayarlayabilirsiniz uzunluğu!", "FH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (int.Parse(textBox1.Text) < 7)
            {
                MessageBox.Show("Minimum 7 MB Ayarlayabilirsiniz uzunluğu!", "FH", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                randomchararraylenght = int.Parse(textBox1.Text) * 1024;
                MessageBox.Show("Ayarlandi!", "FH", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MessageBox.Show(Convert.ToString(randomchararraylenght));
        }
        //downstairs code valid
        public string randomcode()
        {
            Random randomcreate = new Random();
            int randominteger;
            string randomchararray = "";
            for (int whilestopper = 1; whilestopper > 0;)
            {
                randominteger = randomcreate.Next(50, 100);
                randomchararray += Convert.ToChar(randominteger);
                if (randomchararray.Length == randomchararraylenght)
                    {
                    whilestopper = 0;
                }
            }
            return randomchararray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                backgroundWorker1.RunWorkerAsync();
                timer1.Start();
                }
        }

        public void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.IO.FileInfo fileinformation = new System.IO.FileInfo(openFileDialog1.FileName);
            long filelenght = fileinformation.Length;
            System.IO.File.WriteAllText(openFileDialog1.FileName, "");
            for (int whilestopper = 1; whilestopper > 0;)
            {
                if (filelenghtcontrol > filelenght)
                {
                    shreddedcontrol++;
                    System.IO.File.WriteAllText(openFileDialog1.FileName, "");
                }
                if (shreddedcontrol == 60)
                {
                    whilestopper = 0;
                    shreddedcontrol = 0;
                    timer1.Stop();
                    MessageBox.Show("Tamamlandi!", "FH", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    backgroundWorker1.CancelAsync();
                }
                System.IO.FileInfo fileinfor = new System.IO.FileInfo(openFileDialog1.FileName);
                filelenghtcontrol = fileinfor.Length;
                          System.IO.File.AppendAllText(openFileDialog1.FileName, randomcode());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.IO.FileInfo fileinformation = new System.IO.FileInfo(openFileDialog1.FileName);
            label1.Text = Convert.ToString(fileinformation.Length / 1024 / 1024) + " MB";
            label2.Text = "Shredd Integer = " + Convert.ToString(shreddedcontrol);
        }
    }
}
