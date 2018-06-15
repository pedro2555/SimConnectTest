namespace PilotClient
{
    partial class connectedExampleFrm
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
            this.btnGetPositionAsync = new System.Windows.Forms.Button();
            this.btnGetXpndrAsync = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnGetSimModels = new System.Windows.Forms.Button();
            this.btnGetServerModels = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 12);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(494, 510);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // btnGetPositionAsync
            // 
            this.btnGetPositionAsync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetPositionAsync.Location = new System.Drawing.Point(294, 528);
            this.btnGetPositionAsync.Name = "btnGetPositionAsync";
            this.btnGetPositionAsync.Size = new System.Drawing.Size(98, 23);
            this.btnGetPositionAsync.TabIndex = 1;
            this.btnGetPositionAsync.Text = "GetPositionAsnc";
            this.btnGetPositionAsync.UseVisualStyleBackColor = true;
            this.btnGetPositionAsync.Click += new System.EventHandler(this.btnGetPositionAsync_Click);
            // 
            // btnGetXpndrAsync
            // 
            this.btnGetXpndrAsync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetXpndrAsync.Location = new System.Drawing.Point(398, 528);
            this.btnGetXpndrAsync.Name = "btnGetXpndrAsync";
            this.btnGetXpndrAsync.Size = new System.Drawing.Size(98, 23);
            this.btnGetXpndrAsync.TabIndex = 1;
            this.btnGetXpndrAsync.Text = "GetXpndrAsnc";
            this.btnGetXpndrAsync.UseVisualStyleBackColor = true;
            this.btnGetXpndrAsync.Click += new System.EventHandler(this.btnGeXpndrAsync_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(12, 528);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnGetSimModels
            // 
            this.btnGetSimModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetSimModels.Location = new System.Drawing.Point(198, 528);
            this.btnGetSimModels.Name = "btnGetSimModels";
            this.btnGetSimModels.Size = new System.Drawing.Size(90, 23);
            this.btnGetSimModels.TabIndex = 3;
            this.btnGetSimModels.Text = "GetSim Models";
            this.btnGetSimModels.UseVisualStyleBackColor = true;
            this.btnGetSimModels.Click += new System.EventHandler(this.GetMyModels_Click);
            // 
            // btnGetServerModels
            // 
            this.btnGetServerModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetServerModels.Location = new System.Drawing.Point(93, 528);
            this.btnGetServerModels.Name = "btnGetServerModels";
            this.btnGetServerModels.Size = new System.Drawing.Size(99, 23);
            this.btnGetServerModels.TabIndex = 4;
            this.btnGetServerModels.Text = "Server Models";
            this.btnGetServerModels.UseVisualStyleBackColor = true;
            this.btnGetServerModels.Click += new System.EventHandler(this.btnGetServerModels_Click);
            // 
            // connectedExampleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 563);
            this.Controls.Add(this.btnGetServerModels);
            this.Controls.Add(this.btnGetSimModels);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnGetXpndrAsync);
            this.Controls.Add(this.btnGetPositionAsync);
            this.Controls.Add(this.txtLog);
            this.Name = "connectedExampleFrm";
            this.Text = "Form1";
            this.SimConnectClosed += new System.EventHandler(this.connectedExampleFrm_SimConnectClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnGetPositionAsync;
        private System.Windows.Forms.Button btnGetXpndrAsync;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnGetSimModels;
        private System.Windows.Forms.Button btnGetServerModels;
    }
}

