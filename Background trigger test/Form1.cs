using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Background_trigger_test
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);


        public const int MOD_ALT = 0x1;
        public const int MOD_CONTROL = 0x2;
        public const int MOD_SHIFT = 0x4;
        public const int MOD_WIN = 0x8;
        public const int WM_HOTKEY = 0x312;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                textBox1.Text += DateTime.Now.ToString() + ". Hotkey pressed, ID = 0x" + m.WParam.ToString("X");
                textBox1.Text += Environment.NewLine;
                m.Result = (IntPtr)0;
                return;
            }

            base.WndProc(ref m);
        }

        public Form1()
        {
            InitializeComponent();

            bool res = RegisterHotKey(this.Handle, 1, 0, (uint)Keys.F2);//регистрируем горячую клавишу
            res = RegisterHotKey(this.Handle, 1, MOD_SHIFT, (uint)Keys.F1);
            if (res == false) MessageBox.Show("RegisterHotKey failed");
        }
    }
}
