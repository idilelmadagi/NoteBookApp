namespace NoteBook
{
    partial class Alarm
    {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Alarm));
            this.dakika_txt = new System.Windows.Forms.NumericUpDown();
            this.saat_txt = new System.Windows.Forms.NumericUpDown();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.alar_btn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dakika_txt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saat_txt)).BeginInit();
            this.SuspendLayout();
            // 
            // dakika_txt
            // 
            this.dakika_txt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dakika_txt.Location = new System.Drawing.Point(71, 79);
            this.dakika_txt.Name = "dakika_txt";
            this.dakika_txt.Size = new System.Drawing.Size(54, 20);
            this.dakika_txt.TabIndex = 8;
            this.dakika_txt.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // saat_txt
            // 
            this.saat_txt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saat_txt.Location = new System.Drawing.Point(71, 46);
            this.saat_txt.Name = "saat_txt";
            this.saat_txt.Size = new System.Drawing.Size(54, 20);
            this.saat_txt.TabIndex = 7;
            this.saat_txt.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(71, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hour : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Minute : ";
            // 
            // alar_btn
            // 
            this.alar_btn.BackColor = System.Drawing.Color.MistyRose;
            this.alar_btn.Location = new System.Drawing.Point(158, 46);
            this.alar_btn.Name = "alar_btn";
            this.alar_btn.Size = new System.Drawing.Size(113, 53);
            this.alar_btn.TabIndex = 11;
            this.alar_btn.Text = "Set Alarm";
            this.alar_btn.UseVisualStyleBackColor = false;
            this.alar_btn.Click += new System.EventHandler(this.Alar_btn_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Date : ";
            // 
            // Alarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RosyBrown;
            this.ClientSize = new System.Drawing.Size(294, 126);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.alar_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dakika_txt);
            this.Controls.Add(this.saat_txt);
            this.Controls.Add(this.dateTimePicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Alarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alarm";
            ((System.ComponentModel.ISupportInitialize)(this.dakika_txt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saat_txt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown dakika_txt;
        private System.Windows.Forms.NumericUpDown saat_txt;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button alar_btn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label3;
    }
}