using System;
using System.Drawing;

namespace Modetor.Design.UI
{
    
    public partial class AlertDialog : BaseForm
    {
        public bool AutoClose = false;
        public AlertDialog(string text) : base(0.30, 0.30)
        {
            init();
            IconLabel.Text = ((char)IconSet.Important) +"";
            label2.Text = text; //0xE8C9
        }

        public AlertDialog(string text, IconSet icon) : base(0.30, 0.30)
        {
            init();
            IconLabel.Text = ((char)icon) + "";
            label2.Text = text; //0xE8C9
        }
        public AlertDialog(string text, IconSet icon, Color c) : base(0.30, 0.30)
        {
            init();
            IconLabel.ForeColor = c;
            IconLabel.Text = ((char)icon) + "";
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



        public static void Create(string text)
        {
            using (AlertDialog d = new AlertDialog(text))
                d.ShowDialog();

        }
        public static void Create(string text, bool closeOnLoseFocus)
        {
            new AlertDialog(text) { AutoClose = closeOnLoseFocus }.Show();
        }
        public static void Create(string text, IconSet icon)
        {
            using (AlertDialog d = new AlertDialog(text, icon))
                d.ShowDialog();
        }
        public static void Create(string text, IconSet icon, bool closeOnLoseFocus)
        {
            new AlertDialog(text, icon) { AutoClose = closeOnLoseFocus }.Show();
        }
        public static void Create(string text, IconSet icon, Color c)
        {
            using (AlertDialog d = new AlertDialog(text, icon, c))
                d.ShowDialog();
        }
        public static void Create(string text, IconSet icon, Color c, bool closeOnLoseFocus)
        {
            new AlertDialog(text, icon, c) { AutoClose = closeOnLoseFocus }.Show();
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
            this.button1 = new System.Windows.Forms.Button();
            this.IconLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Font = new System.Drawing.Font("SF Pro Rounded", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.Location = new System.Drawing.Point(309, 248);
            this.button1.MinimumSize = new System.Drawing.Size(113, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // IconLabel
            // 
            this.IconLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.IconLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.IconLabel.Font = new System.Drawing.Font("Segoe MDL2 Assets", 61.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IconLabel.Location = new System.Drawing.Point(0, 0);
            this.IconLabel.Name = "IconLabel";
            this.IconLabel.Size = new System.Drawing.Size(173, 300);
            this.IconLabel.TabIndex = 1;
            this.IconLabel.Text = "!";
            this.IconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Open Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(182, 9);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(364, 230);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alert text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlertDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(555, 300);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IconLabel);
            this.Controls.Add(this.button1);
            this.Name = "AlertDialog";
            this.Text = "AlertDialog";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.AlertDialog_Activated);
            this.Deactivate += new System.EventHandler(this.AlertDialog_Deactivate);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Label IconLabel;
        private System.Windows.Forms.Label label2;

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
