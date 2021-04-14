using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PInvoke;

namespace WinFormsExamples
{
    /// <summary>
    /// Displays a button image and draw a border when hovering over.
    /// </summary>
    public class ImageButton : Button
    {
        private readonly Image _originalImage;
        private readonly double _oldScaleFactor;
        private bool ShowBorder { get; set; }
        public override bool AutoSize => true;

        public ImageButton(Image image)
        {
            _originalImage = image;
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            SetStyle(ControlStyles.Selectable, false);
            FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255); 
            FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255); 
            FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            Image = ScaleImage(_originalImage);
            _oldScaleFactor = ScaleFactor;
        }

        private double ScaleFactor => GetDisplayScaleFactor(Handle);
        
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            ShowBorder = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            ShowBorder = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!_oldScaleFactor.AlmostEqualTo(ScaleFactor))
            {
                Image = ScaleImage(_originalImage);
            }
            
            if (!DesignMode && !ShowBorder) return;
            
            var pen = new Pen(Color.FromArgb(45, 68, 108), 1);
            var rectangle = new Rectangle(0, 0, Size.Width - 1, Size.Height - 1);
            e.Graphics.DrawRectangle(pen, rectangle);
        }

        private Image ScaleImage(Image image)
        {
            Size = image.Size;

            var newWidth = (int)Math.Round(image.Width * ScaleFactor);
            var newHeight = (int)Math.Round(image.Height * ScaleFactor);
            var newImage = new Bitmap(newWidth, newHeight);
            
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            
            return newImage;
        }

        private static float GetDisplayScaleFactor(IntPtr windowHandle)
        {
            try
            {
                return User32.GetDpiForWindow(windowHandle) / 96f;
            }
            catch
            {
                return 1; // Or fallback to GDI solutions above
            }
        }

        [Browsable(false)]
        public override string Text { get => ""; set => base.Text = ""; }

        [Browsable(false)]
        public override ContentAlignment TextAlign { get => base.TextAlign; set => base.TextAlign = value; }
    }

    internal static class DoubleExtension 
    {
        public static bool AlmostEqualTo(this double value1, double value2) => 
            Math.Abs(value1 - value2) < 0.01; 
    }
}