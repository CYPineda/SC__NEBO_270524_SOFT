using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SC__NEBO.Clases
{
    public  class Color_Formulario
    {
        private const int WM_NCPAINT = 0x85;
        private const int WM_ACTIVATEAPP = 0x1C;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        public static void ChangeFormBorderStyle(Form form, Color borderColor)
        {
            form.FormBorderStyle = FormBorderStyle.None;

            // Change the border color by handling the non-client area paint event.
            form.Paint += (sender, e) =>
            {
                IntPtr hWnd = (sender as Form).Handle;
                IntPtr hDC = GetDC(hWnd);
                using (Graphics g = Graphics.FromHdc(hDC))
                {
                    Pen pen = new Pen(borderColor, 2); // You can adjust the border thickness here.
                    g.DrawRectangle(pen, 0, 0, form.Width - 1, form.Height - 1);
                    pen.Dispose();
                }
                ReleaseDC(hWnd, hDC);
            };

            // This part makes sure the border color change is visible during activation.
            form.Activated += (sender, e) =>
            {
                IntPtr hWnd = (sender as Form).Handle;
                int style = GetWindowLong(hWnd, -16); // -16 (GWL_STYLE) represents the window style.
                style |= 0x10000; // 0x10000 (WS_BORDER) adds a standard border to the window style.
                SetWindowLong(hWnd, -16, style);
                SetWindowLong(hWnd, -20, 0x00080000); // 0x00080000 (WS_EX_LAYERED) makes the window layered.
                form.Invalidate();
            };
        }
    }
    
}
