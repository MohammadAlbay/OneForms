using System;
using System.Drawing;
using System.Windows.Forms;

namespace Modetor.Design.UI
{

    public partial class ConfirmDialog : BaseForm
    {
        const string OKButtonText  = "OK";
        const string CancelButtonText = "Cancel";


        public bool AutoClose = false;
        public ConfirmDialog(string text, string okBtn = OKButtonText, string cancelBtn = CancelButtonText) : base(0.30, 0.30)
        {
            init(okBtn, cancelBtn);
            IconLabel.Text = ((char)IconSet.Important) + "";
            label2.Text = text; //0xE8C9
        }

        public ConfirmDialog(string text, IconSet icon, string okBtn = OKButtonText, string cancelBtn = CancelButtonText) : base(0.30, 0.30)
        {
            init(okBtn, cancelBtn);
            IconLabel.Text = ((char)icon) + "";
            label2.Text = text; //0xE8C9
        }
        public ConfirmDialog(string text, IconSet icon, Color c, string okBtn = OKButtonText, string cancelBtn = CancelButtonText) : base(0.30, 0.30)
        {
            init(okBtn, cancelBtn);
            IconLabel.ForeColor = c;
            IconLabel.Text = ((char)icon) + "";
            label2.Text = text; //0xE8C9
        }

        private void init(string b1, string b2)
        {
            InitializeComponent();
            ResetDefaultSize();

            button1.Text = b1; button2.Text = b2;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }



        public static bool Confirm(string text, string OkBtn = "OK", string CancelBtn = "Cancel")
        {
            using (ConfirmDialog d = new ConfirmDialog(text, OkBtn, CancelBtn))
                return d.ShowDialog() == DialogResult.OK;

        }

        private Button button2;

        
        public static bool Confirm(string text, IconSet icon, string OkBtn = "OK", string CancelBtn = "Cancel")
        {
            using (ConfirmDialog d = new ConfirmDialog(text, icon, OkBtn, CancelBtn))
                return d.ShowDialog() == DialogResult.OK;
        }
        public static bool Confirm(string text, IconSet icon, Color c, string OkBtn = "OK", string CancelBtn = "Cancel")
        {
            using (ConfirmDialog d = new ConfirmDialog(text, icon, c, OkBtn, CancelBtn))
            {
                return d.ShowDialog() == DialogResult.OK;
            }
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
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Font = new System.Drawing.Font("SF Pro Rounded", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.Location = new System.Drawing.Point(371, 248);
            this.button1.MinimumSize = new System.Drawing.Size(113, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
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
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("SF Pro Rounded", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.Location = new System.Drawing.Point(252, 248);
            this.button2.MinimumSize = new System.Drawing.Size(113, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 40);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ConfirmDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(555, 300);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IconLabel);
            this.Controls.Add(this.button1);
            this.Name = "ConfirmDialog";
            this.ShowInTaskbar = false;
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

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
