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
            this.btnCreateAI = new System.Windows.Forms.Button();
            this.btnGetPosition = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(253, 350);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // btnCreateAI
            // 
            this.btnCreateAI.Location = new System.Drawing.Point(12, 378);
            this.btnCreateAI.Name = "btnCreateAI";
            this.btnCreateAI.Size = new System.Drawing.Size(105, 23);
            this.btnCreateAI.TabIndex = 1;
            this.btnCreateAI.Text = "Create AI LPPT 03";
            this.btnCreateAI.UseVisualStyleBackColor = true;
            this.btnCreateAI.Click += new System.EventHandler(this.btnCreateAI_Click);
            // 
            // btnGetPosition
            // 
            this.btnGetPosition.Location = new System.Drawing.Point(123, 378);
            this.btnGetPosition.Name = "btnGetPosition";
            this.btnGetPosition.Size = new System.Drawing.Size(75, 23);
            this.btnGetPosition.TabIndex = 2;
            this.btnGetPosition.Text = "GetPosition";
            this.btnGetPosition.UseVisualStyleBackColor = true;
            this.btnGetPosition.Click += new System.EventHandler(this.btnGetPosition_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 413);
            this.Controls.Add(this.btnGetPosition);
            this.Controls.Add(this.btnCreateAI);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.OnSimConnectEvent += new SimLib.SimConnectForm.SimConnectEvent(this.Form1_OnSimConnectEvent);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnCreateAI;
        private System.Windows.Forms.Button btnGetPosition;
    }
}

