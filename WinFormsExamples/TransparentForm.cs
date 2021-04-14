using System.Drawing;
using System.Windows.Forms;

namespace WinFormsExamples
{
    public class TransparentForm : Form
    {
        public TransparentForm()
        {
            BackColor = Color.LimeGreen;
            TransparencyKey = Color.LimeGreen;
        }
    }
}