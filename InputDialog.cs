using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modetor.Design.UI
{

    public partial class InputDialog : BaseForm
    {
        public enum InputDataType { Int, Float, Double, String }
        public bool AutoClose = false;
        private System.Windows.Forms.Panel panel1;
        private string val = string.Empty;
        private InputDataType Dtype;
        public string Hint { get; set; } = string.Empty;
        const string hint = "Enter a value";
        public InputDialog(string text, string hint = hint, InputDataType type = InputDataType.String) : base(0.30, 0.30)
        {
            Hint = hint;
            Dtype = type;
            init();
            label2.Text = text; //0xE8C9
        }

        private void init()
        {
            InitializeComponent();
            ResetDefaultSize();

            Label l = new Label() { AutoSize = false, TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                                    Text = Hint, ForeColor = System.Drawing.SystemColors.ControlDark, Size = textBox1.Size, Location = textBox1.Location,
                                    Font = new System.Drawing.Font("Open sans", 11.3f), Cursor = Cursors.IBeam
            };
            l.Click += (s, e) =>
            {
                Controls.Remove(l);
                textBox1.Focus();
            };
            Controls.Add(l);
            l.BringToFront();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            val = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(val)) { panel1.BackColor = System.Drawing.Color.DarkRed; return; }

            switch (Dtype)
            {
                case InputDataType.Int:
                    {
                        if (!int.TryParse(val, out _))
                        {
                            ToolTip t = new ToolTip();
                            t.Active = true;
                            t.IsBalloon = true;
                            t.AutoPopDelay = 1500;
                            t.SetToolTip(textBox1, "Invalid value for integer");
                            return;
                        }
                        break;
                    }
                case InputDataType.Float:
                    {
                        if (!float.TryParse(val, out _))
                        {
                            ToolTip t = new ToolTip();
                            t.Active = true;
                            t.AutoPopDelay = 1500;
                            t.SetToolTip(textBox1, "Invalid value for float");
                            return;
                        }
                        break;
                    }
                case InputDataType.Double:
                    {
                        if (!double.TryParse(val, out _))
                        {
                            ToolTip t = new ToolTip();
                            t.Active = true;
                            t.AutoPopDelay = 1500;
                            t.SetToolTip(textBox1, "Invalid value for double");
                            return;
                        }
                        break;
                    }
                default:
                    break;
            }
            Close();
        }



        public static string InputString(string text, string hint = hint)
        {
            using (InputDialog d = new InputDialog(text))
            {
                d.ShowDialog();
                return d.val;
            }
        }
        public static int InputInt(string text, string hint = hint)
        {
            using (InputDialog d = new InputDialog(text, hint, InputDataType.Int))
            {
                d.ShowDialog();
                return int.Parse(d.val);
            }
        }
        public static float InputFloat(string text, string hint = hint)
        {
            using (InputDialog d = new InputDialog(text, hint, InputDataType.Float))
            {
                d.ShowDialog();
                return float.Parse(d.val);
            }
        }
        public static double InputDouble(string text, string hint = hint)
        {
            using (InputDialog d = new InputDialog(text, hint, InputDataType.Double))
            {
                d.ShowDialog();
                return double.Parse(d.val);
            }
        }

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;



























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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Font = new System.Drawing.Font("SF Pro Rounded", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.Location = new System.Drawing.Point(277, 248);
            this.button1.MinimumSize = new System.Drawing.Size(113, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Open Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(534, 140);
            this.label2.TabIndex = 2;
            this.label2.Text = "Alert text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(37, 169);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(483, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("SF Pro Rounded", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.Location = new System.Drawing.Point(158, 248);
            this.button2.MinimumSize = new System.Drawing.Size(113, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Location = new System.Drawing.Point(37, 193);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 2);
            this.panel1.TabIndex = 5;
            // 
            // InputDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(555, 300);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Name = "InputDialog";
            this.ShowInTaskbar = false;
            this.Text = "AlertDialog";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.AlertDialog_Activated);
            this.Deactivate += new System.EventHandler(this.AlertDialog_Deactivate);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
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

        private void textBox1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(textBox1.Text.Length == 0) panel1.BackColor = System.Drawing.Color.DarkRed; 
            else panel1.BackColor = System.Drawing.Color.DarkGray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            val = "-1";
            Close();
        }
    }
}