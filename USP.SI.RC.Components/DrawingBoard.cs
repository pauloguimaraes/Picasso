using System;
using System.Drawing;
using System.Windows.Forms;

namespace USP.SI.RC.Components
{
    public partial class DrawingBoard : UserControl
    {
        private Bitmap buffer;

        private bool mouseDown = false;
        private Color color = Color.Blue;
        private Bitmap bit = new Bitmap(600, 400);
        private Graphics graphics;
        private Point current = new Point();
        private Point old = new Point();
        private Pen pen = new Pen(Color.Black, 3);

        public bool EnableEdit { get; set; }

        public DrawingBoard()
        {
            InitializeComponent();
            graphics = panel1.CreateGraphics();
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            panel1_Resize(this, null);

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            // Resize the buffer, if it is growing
            if (buffer == null ||
                buffer.Width < panel1.Width ||
                buffer.Height < panel1.Height)
            {
                Bitmap newBuffer = new Bitmap(panel1.Width, panel1.Height);
                if (buffer != null)
                    using (Graphics bufferGrph = Graphics.FromImage(newBuffer))
                        bufferGrph.DrawImageUnscaled(buffer, Point.Empty);
                buffer = newBuffer;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Draw the buffer into the panel
            e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            old = e.Location;
        }

        public Action BroadCastMethod { get; set; }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            BroadCastMethod?.Invoke();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && EnableEdit)
            {
                current = e.Location;

                using (Graphics bufferGrph = Graphics.FromImage(buffer))
                {
                    bufferGrph.DrawLine(pen, old, current);
                }
                panel1.Invalidate();
                
                old = current;
            }
        }

        public delegate Bitmap SetTextCallback();

        public Bitmap GetBitmap()
        {
            Bitmap result = new Bitmap(600, 400);

            if (this.panel1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(GetBitmap);
                return (Bitmap) this.Invoke(d, new object[] { });
            }
            else
            {
                panel1.DrawToBitmap(result, new Rectangle(0, 0, 600, 400));
                return result;
            }
        }

        public void SetBitmap(Bitmap bitmap)
        {
            if (!EnableEdit)
                this.panel1.BackgroundImage = bitmap;
        }

        public void SetBitmap(String file)
        {
            if (!EnableEdit)
            {
                using (var bmpTemp = new Bitmap(file))
                {
                    buffer = new Bitmap(bmpTemp);
                }
                panel1.Invalidate();
            }
        }

        public void Clean()
        {
            this.panel1.BackgroundImage = new Bitmap(600, 400);
        }

        public void SetBackground(byte[] buffer)
        {
            panel1.BackgroundImage = Image.FromFile("C:\\users\\redhe\\desepero2.jpg");
        }
    }
}
