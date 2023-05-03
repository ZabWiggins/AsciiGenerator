using System.Media;
using System.Windows.Forms;


namespace AsciiGenerator_2_20_23
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            k_height.Value = 1; 
            k_width.Value = 1;
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calls open file
            openFile();
        
        }
        public void openFile()
        {
            openFileDialog1.Filter = "Picture Files | *.JPEG; *.JPG; *.PNG"; //limits to picture files

            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                {
                    pictureBox1.Image = new Bitmap (openFileDialog1.FileName); //sets picturebox to show user image
                }
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //checks to make sure height is not 0 
            if (k_height.Value == 0)
            {
                string message = "Kernel Height cannot be 0.";
                string caption = "Invalid Kernel Size";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                k_height.Value = 1;
            }
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //checks to make sure width is not 0
            if (k_width.Value == 0)
            {
                string message = "Kernel Width cannot be 0.";
                string caption = "Invalid Kernel Size";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                k_width.Value = 1;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resets form and calls open file
            pictureBox1.Image = null;
            richTextBox2.Text = null;
            openFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ensures an image is chosen
            if (pictureBox1.Image == null)
            {
                string message = "A picture file (PNG .JPG .JPEG) must be chosen to proceed.";
                string caption = "No Picture Selected";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                openFile();
            }
            else
            {
                //creates new bitmap from user selected image
                Bitmap userImage = (Bitmap)pictureBox1.Image;

                //creates new bitmapascii from userImage bitmap
                BitmapAscii asciify = new BitmapAscii(userImage);

                //sets user entered values as height and width for the kernel
                asciify.kernelHeight = (int)k_height.Value;
                asciify.kernelWidth = (int)k_width.Value;

                //displays output
                richTextBox2.Text = asciify.Asciitize(userImage);
            }
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Use the 'Open' link under the File tab and upload a picture image. Then set the size of the Kernel you would like. " +
                "This will smooth the image depending on how large you set it. Then click 'Convert'. Your image will now be a greyscale image made of Ascii characters!";
            MessageBox.Show(message);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void saveFile()
        {
            saveFileDialog1.Filter = "Text File | *.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox2.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
            }
        }
    }
}