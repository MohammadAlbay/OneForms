using System;
using System.Drawing;
using System.Windows.Forms;
using Modetor.Design.UI;
namespace Modetor.Design.UI
{
    public partial class StyledForm : Form
    {
        public int DefaultHeightPercent = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.90);
        public int DefaultWidthPercent = (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.60);

        public new Size DefaultSize;
        protected bool CaptureSystemEventForUserPreferenceChanging = true;
        public Color SystemColor;
        public StyledForm()
        {
            DefaultSize = new Size(DefaultWidthPercent, DefaultHeightPercent);
            InitializeComponent();
            
            SystemColor = ThemeInfo.ThemeColor();
            m_aeroEnabled = false;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
            

            Microsoft.Win32.SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            windowPOSX = Left;
            windowPOSY = Top;
        }
        public void ResetDefaultSize() => DefaultSize = new Size(DefaultWidthPercent, DefaultHeightPercent);
        public virtual void SystemColorChanged(Color c) { }
        private void SystemEvents_UserPreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            
            if (CaptureSystemEventForUserPreferenceChanging)
            {
                SystemColor = ThemeInfo.ThemeColor();
                if (e.Category == Microsoft.Win32.UserPreferenceCategory.General)
                {
                    TitlebarPanel.BackColor = SystemColor;
                    CloseICON.BackColor = MaxICON.BackColor = MinICON.BackColor = MenuICON.BackColor = SystemColor;
                    SystemColorChanged(SystemColor);
                }
            }
            else
                CloseICON.BackColor = MaxICON.BackColor = MinICON.BackColor = MenuICON.BackColor = TitlebarPanel.BackColor;

        }

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled,
                     MaxState = false;

        private int windowPOSX = 0, windowPOSY = 0;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
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
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
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
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Clicks == 2)
            {
                if(!MaxState)
                {
                    Top = 0; Left = 0;
                    Size = Screen.PrimaryScreen.WorkingArea.Size;
                    MaxState = true;
                    MaxICON.Text = ((char)0xE73F).ToString();
                }
                else
                {
                    Left = windowPOSX;
                    Top = windowPOSY;
                    ClientSize = DefaultSize;
                    MaxState = false;
                    MaxICON.Text = ((char)0xE740).ToString();
                }
            }
            else
            {
                Drag = true;
                MouseX = Cursor.Position.X - this.Left;
                MouseY = Cursor.Position.Y - this.Top;
            }
            
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                Top = Cursor.Position.Y - MouseY;
                Left = Cursor.Position.X - MouseX;

                AdaptXY();
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }

        private void OnResized(object sender, EventArgs e)
        {
            MainViewPanel.Width = Width;
            MainViewPanel.Height = Height - TitlebarPanel.Height;
            TitlebarPanel.Width = Width;
            AppName.Location = new Point((Width / 2) - AppName.Width/2, AppName.Location.Y);
        }

        protected void AdaptXY()
        {
            windowPOSY = Top;
            windowPOSX = Left;
        }
        private void TitlebarICONMouseLeave__Event(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = SystemColor;
        }

        private void MaxICON_Click(object sender, EventArgs e)
        {
            if(MaxState)
            {
                Left = windowPOSX;
                Top = windowPOSY;
                ClientSize = DefaultSize;
                MaxICON.Text = ((char)0xE740).ToString();  //0xE73F
            }
            else
            {
                Top = 0; Left = 0;
                Size = Screen.PrimaryScreen.WorkingArea.Size;
                MaxICON.Text = ((char)0xE73F).ToString();
            }
            MaxState = !MaxState;
        }

        private void MinICON_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void TitlebarICONMouseEnter__Event(object sender, EventArgs e)
        {
            Label l = sender as Label;
            if (l.Equals(CloseICON))
            {
                l.BackColor = Color.FromArgb(219, 68, 55);// red
            }
            else
                l.BackColor = Color.FromArgb(SystemColor.R > 10 ? SystemColor.R - 10 : SystemColor.R,
                                             SystemColor.G > 10 ? SystemColor.G - 10 : SystemColor.G,
                                             SystemColor.B > 10 ? SystemColor.B - 10 : SystemColor.B);
        }

        protected void ClearPredefinedCloseEvent() => CloseICON.Click -= CloseICON_Click;
        private void CloseICON_Click(object sender, EventArgs e)
        {
            try {
                for (int i = 0; i < 3; i++)
                {
                    Opacity -= 0.25;
                    System.Threading.Thread.Sleep(40);
                }
            }
            catch { }
            Close();
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
            this.TitlebarPanel = new System.Windows.Forms.Panel();
            this.MenuICON = new System.Windows.Forms.Label();
            this.MinICON = new System.Windows.Forms.Label();
            this.MaxICON = new System.Windows.Forms.Label();
            this.CloseICON = new System.Windows.Forms.Label();
            this.AppName = new System.Windows.Forms.Label();
            this.MainViewPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TitlebarPanel.SuspendLayout();
            this.MainViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitlebarPanel
            // 
            this.TitlebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(224)))));
            this.TitlebarPanel.Controls.Add(this.MenuICON);
            this.TitlebarPanel.Controls.Add(this.MinICON);
            this.TitlebarPanel.Controls.Add(this.MaxICON);
            this.TitlebarPanel.Controls.Add(this.CloseICON);
            this.TitlebarPanel.Controls.Add(this.AppName);
            this.TitlebarPanel.Location = new System.Drawing.Point(0, 0);
            this.TitlebarPanel.Name = "TitlebarPanel";
            this.TitlebarPanel.Size = new System.Drawing.Size(858, 41);
            this.TitlebarPanel.TabIndex = 0;
            this.TitlebarPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseDown);
            this.TitlebarPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseMove);
            this.TitlebarPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelMove_MouseUp);
            // 
            // MenuICON
            // 
            this.MenuICON.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuICON.Font = new System.Drawing.Font("Segoe MDL2 Assets", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuICON.ForeColor = System.Drawing.Color.White;
            this.MenuICON.Location = new System.Drawing.Point(0, 0);
            this.MenuICON.Name = "MenuICON";
            this.MenuICON.Size = new System.Drawing.Size(78, 41);
            this.MenuICON.TabIndex = 2;
            this.MenuICON.Text = "";
            this.MenuICON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MenuICON.MouseEnter += new System.EventHandler(this.TitlebarICONMouseEnter__Event);
            this.MenuICON.MouseLeave += new System.EventHandler(this.TitlebarICONMouseLeave__Event);
            // 
            // MinICON
            // 
            this.MinICON.Dock = System.Windows.Forms.DockStyle.Right;
            this.MinICON.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinICON.ForeColor = System.Drawing.Color.White;
            this.MinICON.Location = new System.Drawing.Point(678, 0);
            this.MinICON.Name = "MinICON";
            this.MinICON.Size = new System.Drawing.Size(60, 41);
            this.MinICON.TabIndex = 4;
            this.MinICON.Text = "";
            this.MinICON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MinICON.Click += new System.EventHandler(this.MinICON_Click);
            this.MinICON.MouseEnter += new System.EventHandler(this.TitlebarICONMouseEnter__Event);
            this.MinICON.MouseLeave += new System.EventHandler(this.TitlebarICONMouseLeave__Event);
            // 
            // MaxICON
            // 
            this.MaxICON.Dock = System.Windows.Forms.DockStyle.Right;
            this.MaxICON.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaxICON.ForeColor = System.Drawing.Color.White;
            this.MaxICON.Location = new System.Drawing.Point(738, 0);
            this.MaxICON.Name = "MaxICON";
            this.MaxICON.Size = new System.Drawing.Size(60, 41);
            this.MaxICON.TabIndex = 3;
            this.MaxICON.Text = "";
            this.MaxICON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.MaxICON.Click += new System.EventHandler(this.MaxICON_Click);
            this.MaxICON.MouseEnter += new System.EventHandler(this.TitlebarICONMouseEnter__Event);
            this.MaxICON.MouseLeave += new System.EventHandler(this.TitlebarICONMouseLeave__Event);
            // 
            // CloseICON
            // 
            this.CloseICON.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseICON.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseICON.ForeColor = System.Drawing.Color.White;
            this.CloseICON.Location = new System.Drawing.Point(798, 0);
            this.CloseICON.Name = "CloseICON";
            this.CloseICON.Size = new System.Drawing.Size(60, 41);
            this.CloseICON.TabIndex = 2;
            this.CloseICON.Text = "";
            this.CloseICON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CloseICON.Click += new System.EventHandler(this.CloseICON_Click);
            this.CloseICON.MouseEnter += new System.EventHandler(this.TitlebarICONMouseEnter__Event);
            this.CloseICON.MouseLeave += new System.EventHandler(this.TitlebarICONMouseLeave__Event);
            // 
            // AppName
            // 
            this.AppName.AutoSize = true;
            this.AppName.Font = new System.Drawing.Font("Ara Aqeeq Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.AppName.ForeColor = System.Drawing.Color.Snow;
            this.AppName.Location = new System.Drawing.Point(342, 8);
            this.AppName.Name = "AppName";
            this.AppName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.AppName.Size = new System.Drawing.Size(86, 25);
            this.AppName.TabIndex = 0;
            this.AppName.Text = "Company";
            this.AppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainViewPanel
            // 
            this.MainViewPanel.BackColor = System.Drawing.Color.White;
            this.MainViewPanel.Controls.Add(this.label1);
            this.MainViewPanel.Location = new System.Drawing.Point(0, 40);
            this.MainViewPanel.Margin = new System.Windows.Forms.Padding(0);
            this.MainViewPanel.Name = "MainViewPanel";
            this.MainViewPanel.Size = new System.Drawing.Size(862, 579);
            this.MainViewPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 557);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(862, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "Company - 2020";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // StyledForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(862, 621);
            this.Controls.Add(this.MainViewPanel);
            this.Controls.Add(this.TitlebarPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StyledForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StyledForm";
            this.SizeChanged += new System.EventHandler(this.OnResized);
            this.TitlebarPanel.ResumeLayout(false);
            this.TitlebarPanel.PerformLayout();
            this.MainViewPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel TitlebarPanel;
        protected System.Windows.Forms.Label AppName;
        protected System.Windows.Forms.Panel MainViewPanel;
        protected System.Windows.Forms.Label CloseICON;
        protected System.Windows.Forms.Label MaxICON;
        protected System.Windows.Forms.Label MinICON;
        protected System.Windows.Forms.Label MenuICON;
        protected System.Windows.Forms.Label label1;
    }
}














/*///
            /// set icon content
            /// 
            CloseICON.Text = ((char)0xE711).ToString();
            MaxICON.Text = ((char)0xE740).ToString();  //0xE73F
            MinICON.Text = ((char)0xE949).ToString();
            MenuICON.Text = ((char)0xE712).ToString();*/
