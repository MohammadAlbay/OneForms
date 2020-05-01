using System;
using System.Drawing;
using System.Windows.Forms;
namespace Modetor.Design.UI
{
    public partial class BaseForm : Form
    {
        public int DefaultHeightPercent { get; protected set; } = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.90);
        public int DefaultWidthPercent { get; protected set; } = (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.60);

        public bool CaptureSystemEventForUserPreferenceChanging { get; protected set; } = true;
        public Color SystemColor { get; private set; }
        public BaseForm() => InitForm();

        public BaseForm(double wPrecent, double hPercent)
        {
            
            DefaultHeightPercent = (int)(Screen.PrimaryScreen.WorkingArea.Height * hPercent);
            DefaultWidthPercent = (int)(Screen.PrimaryScreen.WorkingArea.Width * wPrecent);

            InitForm();
        }

        private void InitForm()
        {
            InitializeComponent();
            Size = new Size(DefaultWidthPercent, DefaultHeightPercent);
            SystemColor = ThemeInfo.ThemeColor();
            m_aeroEnabled = false;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);


            Microsoft.Win32.SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            windowPOSX = Left;
            windowPOSY = Top;
        }

        public void ResetDefaultSize() => Size = new Size(DefaultWidthPercent, DefaultHeightPercent);
        public virtual void SystemColorChanged(Color c) { }
        private void SystemEvents_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {

            if (CaptureSystemEventForUserPreferenceChanging)
            {
                SystemColor = ThemeInfo.ThemeColor();
                if (e.Category == Microsoft.Win32.UserPreferenceCategory.General)
                {
                    //TitlebarPanel.BackColor = SystemColor;
                    //CloseICON.BackColor = MaxICON.BackColor = MinICON.BackColor = MenuICON.BackColor = SystemColor;
                    SystemColorChanged(SystemColor);
                }
            }
        }

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private int windowPOSX = 0, windowPOSY = 0;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        private static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; _ = DwmIsCompositionEnabled(ref enabled);
                return enabled == 1;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        _ = DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; _ = DwmExtendFrameIntoClientArea(Handle, ref margins);
                    }
                    break;

                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }

        protected virtual void OnResized(object sender, EventArgs e) { }

        protected void AdaptXY()
        {
            windowPOSY = Top;
            windowPOSX = Left;
        }






























        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(862, 621);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StyledForm";
            this.SizeChanged += new System.EventHandler(this.OnResized);
            this.ResumeLayout(false);

        }

        #endregion
    }
}














/*///
            /// set icon content
            /// 
            CloseICON.Text = ((char)0xE711).ToString();
            MaxICON.Text = ((char)0xE740).ToString();  //0xE73F
            MinICON.Text = ((char)0xE949).ToString();
            MenuICON.Text = ((char)0xE712).ToString();*/
