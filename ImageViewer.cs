using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ChatApp
{
    public partial class ImageViewer : Form
    {
        public ImageViewer(System.Drawing.Image image) // Ensure correct namespace is used
        {
            InitializeComponent();

            int newWidth = image.Width/2;
            int newHeight = image.Height/2;

            // Set the PictureBox image and size
            pictureBox1.Image = image;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Maintain aspect ratio
            pictureBox1.Dock = DockStyle.Fill;

            // Set the form size based on the PictureBox size
            this.ClientSize = new Size(newWidth, newHeight);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

