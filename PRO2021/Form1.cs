using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRO2021
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            panel1.Visible = false;
        }
        List<string> fileNames = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer2.Interval = 1;

            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = true;
            file.ValidateNames = true;
            file.Filter = "JPEG|*.jpg";
            if (file.ShowDialog() == DialogResult.OK)
            {
              //  fileNames.Clear();
             //  listBox1.Items.Clear();
                foreach (string fil in file.FileNames)
                {
                    FileInfo fi = new FileInfo(fil);
                    if (fileNames.Contains(fi.FullName)) continue;
                    listBox1.Items.Add(fi.Name);
                    fileNames.Add(fi.FullName);
                }
            }
        }
        int ind = 0;
        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Please Choose At Least 1 Picture");
            }
            else
            {
                timer1.Stop();
                timer2.Stop();

                ind = 0;
                panel1.Visible = false;
                pictureBox1.Visible = true;
                panel1.Controls.Clear();
                PictureBox Images = new PictureBox();
                Images.Load(fileNames[listBox1.SelectedIndex]);
                panel1.Visible = false;
                this.pictureBox1.Image = Image.FromFile(fileNames[listBox1.SelectedIndex]);
                Images.SizeMode = PictureBoxSizeMode.Zoom;
                panel1.Controls.Add(Images);
                this.Text = listBox1.SelectedItem.ToString(); ;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ind == fileNames.Count)
            {
                ind = 0;
                pictureBox1.Visible = false;
                panel1.Visible = false;
                timer1.Stop();
                return ;
            }
            timer1.Interval = 1000;
            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Please Choose At Least 1 Picture");
            }
            else
            {
                this.pictureBox1.Image = Image.FromFile(fileNames[ind]);
                ind++;
            }
        }
        private void slideshowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileNames.Count < 1)
            {
                MessageBox.Show("Please upload At Least 1 Picture");
                return;

            }
            else
            {
                timer2.Stop();
                timer1.Start();
                Text = "SlideShow";
                timer1.Interval = 10;
                panel1.Visible = false;
                pictureBox1.Visible = true;
            }
        }
       
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        List<int> selected = new List<int>();
        int wid = 0;
        void resize()
        {
            panel1.Controls.Clear();
            pictureBox1.Visible = false;
            panel1.Visible = true;
            int x = 0, y = 0;
            foreach (var img in selected)
            {
                PictureBox imgp = new PictureBox();
                imgp.Image = Image.FromFile(fileNames[img]);
                imgp.Location = new Point(x, y);
                imgp.SizeMode = PictureBoxSizeMode.StretchImage;
                imgp.Size = new Size(200, 200);
                panel1.Controls.Add(imgp);
                x += 201;
                if (x > panel1.Width - 200)
                {
                    y += 201;
                    x = 0;
                }
            }
        }
        private void multiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileNames.Count < 1)
            {
                MessageBox.Show("Please choose At Least 1 Picture");
                return;

            }
            Text = "Multi";
            timer2.Start();
            timer1.Stop();
            List<int>  rese= new List<int>();
            selected.Clear();
            rese.Clear();
            while (listBox1.SelectedItems.Count > 0)
            {
                selected.Add(listBox1.SelectedIndex);
                rese.Add(listBox1.SelectedIndex);
                listBox1.SetSelected(listBox1.SelectedIndex, false);
            }
            foreach (var re in rese)
            {
                listBox1.SetSelected(re, true);
            }
            resize();
           
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel1.Width != wid && panel1.Visible==true)
            {
                wid = panel1.Width;
                resize();
            }
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileNames.Clear();
            listBox1.Items.Clear();
            pictureBox1.Visible = false;
            panel1.Visible = false;
            timer1.Stop();
            timer2.Stop();
            Text = "Gallery";
        }
    }
}