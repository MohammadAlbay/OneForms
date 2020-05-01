using System;
using System.Windows.Forms;

namespace Modetor.Design.UI
{

    public partial class LoaderDialog : BaseForm
    {
        public bool AutoClose = false;
        public LoaderDialog(string text) : base(0.30, 0.25)
        {
            init();
            label2.Text = text; //0xE8C9
        }

        public LoaderDialog(string text, IconSet icon) : base(0.30, 0.30)
        {
            init();
            label2.Text = text; //0xE8C9
        }


        private void init()
        {
            InitializeComponent();
            ResetDefaultSize();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }



        public static void Create(string text, bool allow_abort)
        {
            new LoaderDialog(text) { AutoClose = allow_abort }.Show();
        }

        public static void Create(string text)
        {
            new LoaderDialog(text).Show();
        }
        ///
        /// Creates the dialog with ShaowDialog method
        ///
        public static void CreateDialog(string text)
        {
            using (LoaderDialog d = new LoaderDialog(text))
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Open Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(176, 9);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(367, 174);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alert text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::Modetor.Design.UI.Resources.loader_black;
            this.pictureBox1.Location = new System.Drawing.Point(49, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(99, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // LoaderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(555, 192);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Name = "LoaderDialog";
            this.ShowInTaskbar = false;
            this.Text = "AlertDialog";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.AlertDialog_Activated);
            this.Deactivate += new System.EventHandler(this.AlertDialog_Deactivate);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Label label2;
        private PictureBox pictureBox1;

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
