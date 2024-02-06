using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint_3._0
{
    public partial class Form1 : Form
    {
        bool drawing;
        GraphicsPath currentPath; 
        Point oldLocation; 
        Pen currentPen;
        public Form1()
        {
            InitializeComponent();
            drawing = false;
            currentPen = new Pen(Color.Black);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picDrawingSurface_Click(object sender, EventArgs e)
        {
            
            

        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            { MessageBox.Show("Сначала создайте новый файл!");
                return; }
          
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picDrawingSurface.Image != null)
            {
                var result = MessageBox.Show("Сохранить текущее изображение перед созданием нового рисунка?", "Предупреждение", MessageBoxButtons.YesNoCancel);
                switch (result) 
                { case DialogResult.No: 
                        break; 
                    case DialogResult.Yes: saveToolStripMenuItem_Click(sender, e); 
                        break; 
                    case DialogResult.Cancel:
                        return; }
            }
            Bitmap pic = new Bitmap(750, 500);
            picDrawingSurface.Image = pic;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picDrawingSurface.Image != null)
            {
                SaveFileDialog SaveDlg = new SaveFileDialog();

                SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
                SaveDlg.Title = "Save an Image File";
                SaveDlg.FilterIndex = 4;
                //По умолчанию будет выбрано последнее расширение *.png
                SaveDlg.ShowDialog();
                if (SaveDlg.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)SaveDlg.OpenFile();
                    switch (this.SaveDlg.FilterIndex)
                    {
                        case 1:
                            this.picDrawingSurface.Image.Save(fs, ImageFormat.Jpeg);
                            break;
                        case 2:
                            this.picDrawingSurface.Image.Save(fs, ImageFormat.Bmp);
                            break;
                        case 3:
                            this.picDrawingSurface.Image.Save(fs, ImageFormat.Gif);
                            break;
                        case 4:
                            this.picDrawingSurface.Image.Save(fs, ImageFormat.Png);
                            break;
                    }
                    fs.Close();
                }
            }
            else
            {
                MessageBox.Show("нечего сохранять!");
            }
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File"; 
            OP.FilterIndex = 1; 

          if (OP.ShowDialog() != DialogResult.Cancel) picDrawingSurface.Load(OP.FileName); 
            picDrawingSurface.AutoSize = true;
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Прикл прог уыыыааы");
        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            try { currentPath.Dispose(); }
            catch { };
        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing) 
            { 
                Graphics g = Graphics.FromImage(picDrawingSurface.Image);
                currentPath.AddLine(oldLocation, e.Location); g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location; g.Dispose(); picDrawingSurface.Invalidate();
            }
        }
    }
}
