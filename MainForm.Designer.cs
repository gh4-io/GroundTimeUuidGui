namespace GroundTimeUuidGui
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblReg = new System.Windows.Forms.Label();
            this.lblStation = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.txtReg = new System.Windows.Forms.TextBox();
            this.txtStation = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblUuid = new System.Windows.Forms.Label();
            this.txtUuid = new System.Windows.Forms.TextBox();
            this.lblIsoPreview = new System.Windows.Forms.Label();
            this.txtIsoPreview = new System.Windows.Forms.TextBox();
            this.lblAirportInfo = new System.Windows.Forms.Label();
            this.lblAirportSource = new System.Windows.Forms.Label();
            this.lblOperatorInfo = new System.Windows.Forms.Label();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.infoIcon = new System.Windows.Forms.Label();
            this.themeIcon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(10, 15);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(150, 23);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "CUSTOMER CODE:";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblReg
            // 
            this.lblReg.Location = new System.Drawing.Point(10, 47);
            this.lblReg.Name = "lblReg";
            this.lblReg.Size = new System.Drawing.Size(150, 23);
            this.lblReg.TabIndex = 1;
            this.lblReg.Text = "AIRCRAFT REG:";
            this.lblReg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStation
            // 
            this.lblStation.Location = new System.Drawing.Point(10, 79);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(150, 23);
            this.lblStation.TabIndex = 2;
            this.lblStation.Text = "STATION:";
            this.lblStation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            this.lblDate.Location = new System.Drawing.Point(10, 111);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(150, 23);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "SCHEDULED DATE:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(10, 143);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(150, 23);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "SCHEDULED TIME:";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(170, 15);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(55, 23);
            this.txtCustomerCode.TabIndex = 5;
            // 
            // txtReg
            // 
            this.txtReg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReg.Location = new System.Drawing.Point(170, 47);
            this.txtReg.Name = "txtReg";
            this.txtReg.Size = new System.Drawing.Size(80, 23);
            this.txtReg.TabIndex = 6;
            // 
            // txtStation
            // 
            this.txtStation.Location = new System.Drawing.Point(170, 79);
            this.txtStation.Name = "txtStation";
            this.txtStation.Size = new System.Drawing.Size(55, 23);
            this.txtStation.TabIndex = 7;
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(170, 111);
            this.txtDate.Name = "txtDate";
            this.txtDate.PlaceholderText = "MM/DD/YYYY";
            this.txtDate.Size = new System.Drawing.Size(120, 23);
            this.txtDate.TabIndex = 8;
            // 
            // btnCalendar
            // 
            this.btnCalendar.Location = new System.Drawing.Point(296, 111);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(28, 23);
            this.btnCalendar.TabIndex = 9;
            this.btnCalendar.Text = "📅";
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(170, 143);
            this.txtTime.Name = "txtTime";
            this.txtTime.PlaceholderText = "HH:MM";
            this.txtTime.Size = new System.Drawing.Size(80, 23);
            this.txtTime.TabIndex = 10;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(355, 143);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(175, 27);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "GENERATE UUID";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblUuid
            // 
            this.lblUuid.Location = new System.Drawing.Point(10, 211);
            this.lblUuid.Name = "lblUuid";
            this.lblUuid.Size = new System.Drawing.Size(150, 23);
            this.lblUuid.TabIndex = 12;
            this.lblUuid.Text = "UUID:";
            this.lblUuid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUuid
            // 
            this.txtUuid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUuid.Location = new System.Drawing.Point(170, 211);
            this.txtUuid.Name = "txtUuid";
            this.txtUuid.ReadOnly = true;
            this.txtUuid.Size = new System.Drawing.Size(360, 23);
            this.txtUuid.TabIndex = 13;
            // 
            // lblIsoPreview
            // 
            this.lblIsoPreview.Location = new System.Drawing.Point(10, 179);
            this.lblIsoPreview.Name = "lblIsoPreview";
            this.lblIsoPreview.Size = new System.Drawing.Size(150, 23);
            this.lblIsoPreview.TabIndex = 14;
            this.lblIsoPreview.Text = "UUID SEED STRING:";
            this.lblIsoPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIsoPreview
            // 
            this.txtIsoPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIsoPreview.Location = new System.Drawing.Point(170, 179);
            this.txtIsoPreview.Name = "txtIsoPreview";
            this.txtIsoPreview.ReadOnly = true;
            this.txtIsoPreview.Size = new System.Drawing.Size(360, 23);
            this.txtIsoPreview.TabIndex = 15;
            // 
            // lblAirportInfo
            // 
            this.lblAirportInfo.AutoSize = true;
            this.lblAirportInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAirportInfo.Location = new System.Drawing.Point(226, 83);
            this.lblAirportInfo.Name = "lblAirportInfo";
            this.lblAirportInfo.Size = new System.Drawing.Size(0, 15);
            this.lblAirportInfo.TabIndex = 16;
            // 
            // lblAirportSource
            // 
            this.lblAirportSource.AutoSize = true;
            this.lblAirportSource.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblAirportSource.Location = new System.Drawing.Point(226, 100);
            this.lblAirportSource.Name = "lblAirportSource";
            this.lblAirportSource.Size = new System.Drawing.Size(0, 12);
            this.lblAirportSource.TabIndex = 17;
            // 
            // lblOperatorInfo
            // 
            this.lblOperatorInfo.AutoSize = true;
            this.lblOperatorInfo.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblOperatorInfo.Location = new System.Drawing.Point(226, 19);
            this.lblOperatorInfo.Name = "lblOperatorInfo";
            this.lblOperatorInfo.Size = new System.Drawing.Size(0, 15);
            this.lblOperatorInfo.TabIndex = 17;
            // 
            // monthCalendar
            // 
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 18;
            this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateSelected);
            // 
            // infoIcon
            // 
            this.infoIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.infoIcon.AutoSize = true;
            this.infoIcon.ForeColor = System.Drawing.Color.Silver;
            this.infoIcon.Location = new System.Drawing.Point(515, 9);
            this.infoIcon.Name = "infoIcon";
            this.infoIcon.Size = new System.Drawing.Size(22, 15);
            this.infoIcon.TabIndex = 19;
            this.infoIcon.Text = "🛈";
            // 
            
            // themeIcon
            // 
            this.themeIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.themeIcon.AutoSize = true;
            this.themeIcon.ForeColor = System.Drawing.Color.Silver;
            this.themeIcon.Location = new System.Drawing.Point(490, 9);
            this.themeIcon.Name = "themeIcon";
            this.themeIcon.Size = new System.Drawing.Size(22, 15);
            this.themeIcon.TabIndex = 20;
            this.themeIcon.Text = "◐";
// MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 256);
                        this.Controls.Add(this.infoIcon);
            this.Controls.Add(this.themeIcon);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.lblOperatorInfo);
            this.Controls.Add(this.lblAirportSource);
            this.Controls.Add(this.lblAirportInfo);
            this.Controls.Add(this.txtIsoPreview);
            this.Controls.Add(this.lblIsoPreview);
            this.Controls.Add(this.txtUuid);
            this.Controls.Add(this.lblUuid);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.btnCalendar);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.txtStation);
            this.Controls.Add(this.txtReg);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblStation);
            this.Controls.Add(this.lblReg);
            this.Controls.Add(this.lblCustomer);
            this.MinimumSize = new System.Drawing.Size(560, 295);
            this.Name = "MainForm";
            this.Text = "Ground Time UUID Generator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblReg;
        private System.Windows.Forms.Label lblStation;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtCustomerCode;
        private System.Windows.Forms.TextBox txtReg;
        private System.Windows.Forms.TextBox txtStation;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Button btnCalendar;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblUuid;
        private System.Windows.Forms.TextBox txtUuid;
        private System.Windows.Forms.Label lblIsoPreview;
        private System.Windows.Forms.TextBox txtIsoPreview;
        private System.Windows.Forms.Label lblAirportInfo;
        private System.Windows.Forms.Label lblAirportSource;
        private System.Windows.Forms.Label lblOperatorInfo;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Label infoIcon;
        private System.Windows.Forms.Label themeIcon;
    }
}
