namespace PilotClient
{
    partial class Form1
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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.txtLAT = new System.Windows.Forms.TextBox();
            this.txtLon = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHDG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtALT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPitch = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(253, 248);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // txtLAT
            // 
            this.txtLAT.Location = new System.Drawing.Point(12, 390);
            this.txtLAT.Name = "txtLAT";
            this.txtLAT.Size = new System.Drawing.Size(100, 20);
            this.txtLAT.TabIndex = 1;
            // 
            // txtLon
            // 
            this.txtLon.Location = new System.Drawing.Point(165, 390);
            this.txtLon.Name = "txtLon";
            this.txtLon.Size = new System.Drawing.Size(100, 20);
            this.txtLon.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 374);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "LAT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "LON";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "HDG";
            // 
            // txtHDG
            // 
            this.txtHDG.Location = new System.Drawing.Point(12, 338);
            this.txtHDG.Name = "txtHDG";
            this.txtHDG.Size = new System.Drawing.Size(100, 20);
            this.txtHDG.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "ALT";
            // 
            // txtALT
            // 
            this.txtALT.Location = new System.Drawing.Point(165, 338);
            this.txtALT.Name = "txtALT";
            this.txtALT.Size = new System.Drawing.Size(100, 20);
            this.txtALT.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "PITCH";
            // 
            // txtPitch
            // 
            this.txtPitch.Location = new System.Drawing.Point(165, 289);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.Size = new System.Drawing.Size(100, 20);
            this.txtPitch.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 413);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPitch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtALT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtHDG);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLon);
            this.Controls.Add(this.txtLAT);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.OnSimConnectEvent += new SimLib.SimConnectForm.SimConnectEvent(this.Form1_OnSimConnectEvent);
            this.SimConnectHeadingChanged += new System.EventHandler(this.Form1_SimConnectHeadingChanged);
            this.SimConnectLatitudeChanged += new System.EventHandler(this.Form1_SimConnectLatitudeChanged);
            this.SimConnectLongitudeChanged += new System.EventHandler(this.Form1_SimConnectLongitudeChanged);
            this.SimConnectAltitudeChanged += new System.EventHandler(this.Form1_SimConnectAltitudeChanged);
            this.SimConnectPitchChanged += new System.EventHandler(this.Form1_SimConnectPitchChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.TextBox txtLAT;
        private System.Windows.Forms.TextBox txtLon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHDG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtALT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPitch;
    }
}

