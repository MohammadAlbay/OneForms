using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modetor.Design.UI
{
    public partial class NotificationCard : Form
    {
        public static int ActiveCards { get; private set; } = 0;
        public SideColor CardSideColor
        {
            set
            {
                Color c = Color.FromArgb(33, 33, 33);
                if (value == SideColor.Green) c = Color.FromArgb(46, 125, 50);
                else if (value == SideColor.Blue) c = Color.FromArgb(21, 101, 192);
                else if (value == SideColor.Red) c = Color.FromArgb(198, 40, 40);
                else if (value == SideColor.Yellow) c = Color.FromArgb(255, 160, 0);
                SidePanel.BackColor = c;
            }
        }
        public int DelayToDisapear { set; get; } = 3000;
        public bool AllowDisapear = true;
        public NotificationCard(string text, string title)
        {
            ActiveCards++;

            InitializeComponent();
            label1.Text = title;
            label2.Text = text;
            m_aeroEnabled = false;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);

            Top = Screen.PrimaryScreen.WorkingArea.Height - (Height*ActiveCards) - 20;
            Left = Screen.PrimaryScreen.WorkingArea.Width- Width - 20;
        }
        public virtual void SystemColorChanged(Color c) { }



        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled = false;


        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;

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


        public enum SideColor
        {
            Blue, Green, Red, Black, Yellow
        }

        private void ShownEvent(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                System.Threading.Thread.Sleep(DelayToDisapear);
                Invoke(new Action(() => {
                    for (int i = 1; i < 8; i++)
                    {
                        Opacity -= 0.09;
                        System.Threading.Thread.Sleep(40);
                    }
                    Close();
                }));
            });

            t.Start();
        }



        public static void Create(string text) => Create(SideColor.Black, 2500, text, "Modetor");
        public static void Create(string text, string title) => Create(SideColor.Black, 2500, text, title);
        public static void Create(int delay, string text) => Create(SideColor.Black, delay, text, "Modetor");
        public static void Create(SideColor sideColor, string text) => Create(sideColor, 2500, text, "Modetor");
        public static void Create(SideColor sideColor, int delay, string text) => Create(sideColor, delay, text, "Modetor");
        public static void Create(SideColor sideColor, int delay, string text, string title, bool allowToDisapear = true)
        {
            new System.Threading.Thread(() =>
            {
                using (NotificationCard card = new NotificationCard(text, title) { DelayToDisapear = delay, AllowDisapear = allowToDisapear, CardSideColor = sideColor })
                {
                    card.ShowDialog();
                }
            }).Start();
        }

        private void ClosedEvent(object sender, FormClosedEventArgs e) => ActiveCards--;
















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
            this.SidePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.SidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SidePanel.Location = new System.Drawing.Point(0, 0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(7, 125);
            this.SidePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modetor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Roboto Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 36);
            this.label2.MaximumSize = new System.Drawing.Size(373, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(373, 80);
            this.label2.TabIndex = 1;
            this.label2.Text = "Successfuly activated.\r\nYou can find your activation details in File > Details\r\na" +
    "nd look down to the last item withen the list";
            // 
            // NotificationCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(400, 125);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SidePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationCard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NotificationCard";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClosedEvent);
            this.Shown += new System.EventHandler(this.ShownEvent);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
