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
            lblCustomer = new Label();
            lblReg = new Label();
            lblStation = new Label();
            lblDate = new Label();
            lblTime = new Label();
            txtCustomerCode = new TextBox();
            txtReg = new TextBox();
            txtStation = new TextBox();
            txtDate = new TextBox();
            btnCalendar = new Button();
            txtTime = new TextBox();
            btnGenerate = new Button();
            lblUuid = new Label();
            txtUuid = new TextBox();
            lblIsoPreview = new Label();
            txtIsoPreview = new TextBox();
            lblAirportInfo = new Label();
            lblAirportSource = new Label();
            lblOperatorInfo = new Label();
            monthCalendar = new MonthCalendar();
            infoIcon = new Label();
            themeIcon = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblCustomer
            // 
            lblCustomer.Location = new Point(10, 15);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(88, 23);
            lblCustomer.TabIndex = 0;
            lblCustomer.Text = "CUSTOMER CODE:";
            lblCustomer.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblReg
            // 
            lblReg.Location = new Point(10, 47);
            lblReg.Name = "lblReg";
            lblReg.Size = new Size(88, 23);
            lblReg.TabIndex = 1;
            lblReg.Text = "AIRCRAFT REG:";
            lblReg.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblStation
            // 
            lblStation.Location = new Point(10, 79);
            lblStation.Name = "lblStation";
            lblStation.Size = new Size(88, 23);
            lblStation.TabIndex = 2;
            lblStation.Text = "STATION:";
            lblStation.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDate
            // 
            lblDate.Location = new Point(10, 111);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(88, 23);
            lblDate.TabIndex = 3;
            lblDate.Text = "SCHEDULED DATE:";
            lblDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTime
            // 
            lblTime.Location = new Point(10, 143);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(88, 23);
            lblTime.TabIndex = 4;
            lblTime.Text = "SCHEDULED TIME:";
            lblTime.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtCustomerCode
            // 
            txtCustomerCode.Location = new Point(104, 11);
            txtCustomerCode.Name = "txtCustomerCode";
            txtCustomerCode.Size = new Size(55, 23);
            txtCustomerCode.TabIndex = 5;
            // 
            // txtReg
            // 
            txtReg.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtReg.Location = new Point(104, 43);
            txtReg.Name = "txtReg";
            txtReg.Size = new Size(20, 23);
            txtReg.TabIndex = 6;
            // 
            // txtStation
            // 
            txtStation.Location = new Point(104, 75);
            txtStation.Name = "txtStation";
            txtStation.Size = new Size(55, 23);
            txtStation.TabIndex = 7;
            // 
            // txtDate
            // 
            txtDate.Location = new Point(104, 107);
            txtDate.Name = "txtDate";
            txtDate.PlaceholderText = "MM/DD/YYYY";
            txtDate.Size = new Size(120, 23);
            txtDate.TabIndex = 8;
            // 
            // btnCalendar
            // 
            btnCalendar.Location = new Point(230, 111);
            btnCalendar.Name = "btnCalendar";
            btnCalendar.Size = new Size(28, 23);
            btnCalendar.TabIndex = 9;
            btnCalendar.Text = "📅";
            btnCalendar.UseVisualStyleBackColor = true;
            btnCalendar.Click += btnCalendar_Click;
            // 
            // txtTime
            // 
            txtTime.Location = new Point(104, 139);
            txtTime.Name = "txtTime";
            txtTime.PlaceholderText = "HH:MM";
            txtTime.Size = new Size(80, 23);
            txtTime.TabIndex = 10;
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGenerate.Location = new Point(297, 139);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(175, 27);
            btnGenerate.TabIndex = 11;
            btnGenerate.Text = "GENERATE UUID";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // lblUuid
            // 
            lblUuid.Location = new Point(10, 211);
            lblUuid.Name = "lblUuid";
            lblUuid.Size = new Size(88, 23);
            lblUuid.TabIndex = 12;
            lblUuid.Text = "UUID:";
            lblUuid.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtUuid
            // 
            txtUuid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUuid.Location = new Point(104, 207);
            txtUuid.Name = "txtUuid";
            txtUuid.ReadOnly = true;
            txtUuid.Size = new Size(368, 23);
            txtUuid.TabIndex = 13;
            // 
            // lblIsoPreview
            // 
            lblIsoPreview.Location = new Point(10, 179);
            lblIsoPreview.Name = "lblIsoPreview";
            lblIsoPreview.Size = new Size(88, 23);
            lblIsoPreview.TabIndex = 14;
            lblIsoPreview.Text = "UUID SEED STRING:";
            lblIsoPreview.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtIsoPreview
            // 
            txtIsoPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtIsoPreview.Location = new Point(104, 175);
            txtIsoPreview.Name = "txtIsoPreview";
            txtIsoPreview.ReadOnly = true;
            txtIsoPreview.Size = new Size(368, 23);
            txtIsoPreview.TabIndex = 15;
            // 
            // lblAirportInfo
            // 
            lblAirportInfo.AutoSize = true;
            lblAirportInfo.ForeColor = SystemColors.GrayText;
            lblAirportInfo.Location = new Point(226, 83);
            lblAirportInfo.Name = "lblAirportInfo";
            lblAirportInfo.Size = new Size(0, 15);
            lblAirportInfo.TabIndex = 16;
            // 
            // lblAirportSource
            // 
            lblAirportSource.AutoSize = true;
            lblAirportSource.ForeColor = SystemColors.GrayText;
            lblAirportSource.Location = new Point(226, 100);
            lblAirportSource.Name = "lblAirportSource";
            lblAirportSource.Size = new Size(0, 15);
            lblAirportSource.TabIndex = 17;
            // 
            // lblOperatorInfo
            // 
            lblOperatorInfo.AutoSize = true;
            lblOperatorInfo.ForeColor = SystemColors.GrayText;
            lblOperatorInfo.Location = new Point(226, 19);
            lblOperatorInfo.Name = "lblOperatorInfo";
            lblOperatorInfo.Size = new Size(0, 15);
            lblOperatorInfo.TabIndex = 17;
            // 
            // monthCalendar
            // 
            monthCalendar.Location = new Point(186, 43);
            monthCalendar.MaxSelectionCount = 1;
            monthCalendar.Name = "monthCalendar";
            monthCalendar.TabIndex = 18;
            monthCalendar.DateSelected += monthCalendar_DateSelected;
            // 
            // infoIcon
            // 
            infoIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            infoIcon.AutoSize = true;
            infoIcon.ForeColor = Color.Silver;
            infoIcon.Location = new Point(457, 9);
            infoIcon.Name = "infoIcon";
            infoIcon.Size = new Size(17, 15);
            infoIcon.TabIndex = 19;
            infoIcon.Text = "🛈";
            // 
            // themeIcon
            // 
            themeIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            themeIcon.AutoSize = true;
            themeIcon.ForeColor = Color.Silver;
            themeIcon.Location = new Point(432, 9);
            themeIcon.Name = "themeIcon";
            themeIcon.Size = new Size(17, 15);
            themeIcon.TabIndex = 20;
            themeIcon.Text = "◐";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(453, 210);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 21;
            label1.Text = "⧉";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += btnCopyUuid_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 256);
            Controls.Add(label1);
            Controls.Add(infoIcon);
            Controls.Add(themeIcon);
            Controls.Add(monthCalendar);
            Controls.Add(lblOperatorInfo);
            Controls.Add(lblAirportSource);
            Controls.Add(lblAirportInfo);
            Controls.Add(txtIsoPreview);
            Controls.Add(lblIsoPreview);
            Controls.Add(txtUuid);
            Controls.Add(lblUuid);
            Controls.Add(btnGenerate);
            Controls.Add(txtTime);
            Controls.Add(btnCalendar);
            Controls.Add(txtDate);
            Controls.Add(txtStation);
            Controls.Add(txtReg);
            Controls.Add(txtCustomerCode);
            Controls.Add(lblTime);
            Controls.Add(lblDate);
            Controls.Add(lblStation);
            Controls.Add(lblReg);
            Controls.Add(lblCustomer);
            MinimumSize = new Size(500, 295);
            Name = "MainForm";
            Text = "Ground Time UUID Generator";
            ResumeLayout(false);
            PerformLayout();
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
        private Label label1;
    }
}
