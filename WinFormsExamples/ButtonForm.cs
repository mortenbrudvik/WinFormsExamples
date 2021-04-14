using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsExamples
{
    public class ButtonForm : TransparentForm
    {
        private readonly ImageButton _imageButton;

        public ButtonForm(Image buttonImage)
        {
            _imageButton = new ImageButton(buttonImage)
            {
                Dock = DockStyle.Fill,
                Location = Point.Empty,
                TabIndex = 0,
                TabStop = false
            };
            _imageButton.MouseDown += ImageButtonOnMouseDown;

            ClientSize = buttonImage.Size;
            ControlBox = false;
            Controls.Add(_imageButton);
            FormBorderStyle = FormBorderStyle.None;

            Name = "ButtomForm";
            ShowInTaskbar = false;
        }

        public override bool AutoSize => false;

        private static void ImageButtonOnMouseDown(object sender, MouseEventArgs e)
        {
            Console.Out.WriteLine("Mouse down");
        }
    }
}