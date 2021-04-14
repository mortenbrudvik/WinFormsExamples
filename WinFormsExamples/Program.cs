using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsExamples.Properties;

namespace WinFormsExamples
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            static void CenterControl(Control parent, Control child) 
            {
                var x = parent.Left + parent.Width / 2 - child.Width / 2;
                var y = parent.Top + parent.Height / 2 - child.Height / 2;

                child.Location = new Point(x, y);
            }

            var transparentForm = new TransparentForm {Size = new Size(600, 600)};

            var buttonForm = new ButtonForm(Resources.logo);

            buttonForm.Show(transparentForm);
            CenterControl(transparentForm, buttonForm);
            
            transparentForm.Move += (sender, args) => CenterControl(sender as Form, buttonForm);

            Application.Run(transparentForm);
        }
    }
}
