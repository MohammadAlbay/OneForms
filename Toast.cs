using System;
using System.Drawing;
using System.Windows.Forms;

namespace Modetor.Design.UI
{

    public partial class Toast : BaseForm
    {
        public bool AutoClose = true;
        private int mTimeout = 2000;
        public Toast(Form parent,string text, int Timeout = 2000) : base(0.20, 0.8)
        {
            InitializeComponent();
            mTimeout = Timeout;
            Location = new Point(((parent.Location.X)+(parent.Width/2 - Width/2)), (parent.Height + parent.Location.Y)-Height-20);
            label2.Text = text; //0xE8C9

            init();
        }

        public Toast(Rectangle parent, string text, int Timeout = 2000) : base(0.20, 0.8)
        {
            InitializeComponent();
            mTimeout = Timeout;
            Location = new Point(((parent.Location.X) + (parent.Width / 2 - Width / 2)), (parent.Height + parent.Location.Y) - Height - 20);
            label2.Text = text; //0xE8C9

            init();
        }

        private void init()
        {
            System.Threading.Tasks.Task.Delay(mTimeout).ContinueWith((r) => {
                try { Invoke(new Action(() => { try { Close(); } catch { } })); }
                catch { }
            }, System.Threading.Tasks.TaskContinuationOptions.RunContinuationsAsynchronously).ConfigureAwait(false);
        }



        public static void Create(Form parent, string text, int timeout = 2000)
        {
            new Toast(parent, text, timeout).Show();
        }
        public static void CreateToast(Form parent, string text, int timeout = 2000)
        {
            using (Toast d = new Toast(parent, text, timeout))
                d.ShowDialog();
        }

        public static void Create(Rectangle parent, string text, int timeout = 2000)
        {
            new Toast(parent, text, timeout).Show();
        }
        public static void CreateToast(Rectangle parent, string text, int timeout = 2000)
        {
            using (Toast d = new Toast(parent, text, timeout))
                d.ShowDialog();
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
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Open Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(435, 63);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alert text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Toast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(459, 81);
            this.Controls.Add(this.label2);
            this.Name = "Toast";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AlertDialog";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.AlertDialog_Activated);
            this.Deactivate += new System.EventHandler(this.AlertDialog_Deactivate);
            this.ResumeLayout(false);

        }

        #endregion
        private Label label2;

        private void AlertDialog_Activated(object sender, EventArgs e)
        {
            Console.WriteLine("Activaed");
        }

        private void AlertDialog_Deactivate(object sender, EventArgs e)
        {
            if (AutoClose)
                Close();

        }
    }
}
