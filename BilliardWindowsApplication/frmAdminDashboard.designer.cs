namespace BilliardWindowsApplication
{
    partial class frmAdminDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdminDashboard));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewEmployeeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewReceptionistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDepartmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVisitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewVisitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todaysVisitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vBlockedUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blacklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blacklistedVisitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToBlacklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMoonlitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToUseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgWaiting = new System.Windows.Forms.DataGridView();
            this.ColNameWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMobileWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColEmailWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPurposeWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTypeWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCheckinWaiting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaitingDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColCancelWaiting = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colViewVisitorWaiting = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgSucessfull = new System.Windows.Forms.DataGridView();
            this.colNameSucessfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMobileSucessfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmailSucessfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPurposeSucessfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeSucessfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckinTimeSucessfull = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCancelSucessfull = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colViewVisitorSucessfull = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWaiting)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSucessfull)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.blacklistToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1247, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.homeToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewEmployeeToolStripMenuItem,
            this.viewReceptionistToolStripMenuItem,
            this.viewDepartmentToolStripMenuItem,
            this.viewVisitorToolStripMenuItem,
            this.viewVisitToolStripMenuItem,
            this.todaysVisitorToolStripMenuItem,
            this.vBlockedUsersToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.reportToolStripMenuItem.Text = "Report";
            // 
            // viewEmployeeToolStripMenuItem
            // 
            this.viewEmployeeToolStripMenuItem.Name = "viewEmployeeToolStripMenuItem";
            this.viewEmployeeToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.viewEmployeeToolStripMenuItem.Text = "View Employee";
            // 
            // viewReceptionistToolStripMenuItem
            // 
            this.viewReceptionistToolStripMenuItem.Name = "viewReceptionistToolStripMenuItem";
            this.viewReceptionistToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.viewReceptionistToolStripMenuItem.Text = "View Receptionist";
            // 
            // viewDepartmentToolStripMenuItem
            // 
            this.viewDepartmentToolStripMenuItem.Name = "viewDepartmentToolStripMenuItem";
            this.viewDepartmentToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.viewDepartmentToolStripMenuItem.Text = "View Department";
            // 
            // viewVisitorToolStripMenuItem
            // 
            this.viewVisitorToolStripMenuItem.Name = "viewVisitorToolStripMenuItem";
            this.viewVisitorToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.viewVisitorToolStripMenuItem.Text = "View Visitor";
            // 
            // viewVisitToolStripMenuItem
            // 
            this.viewVisitToolStripMenuItem.Name = "viewVisitToolStripMenuItem";
            this.viewVisitToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.viewVisitToolStripMenuItem.Text = "View Visit";
            // 
            // todaysVisitorToolStripMenuItem
            // 
            this.todaysVisitorToolStripMenuItem.Name = "todaysVisitorToolStripMenuItem";
            this.todaysVisitorToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.todaysVisitorToolStripMenuItem.Text = "Todays Visitor";
            // 
            // vBlockedUsersToolStripMenuItem
            // 
            this.vBlockedUsersToolStripMenuItem.Name = "vBlockedUsersToolStripMenuItem";
            this.vBlockedUsersToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.vBlockedUsersToolStripMenuItem.Text = "View Blocked Users";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshFrequencyToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // refreshFrequencyToolStripMenuItem
            // 
            this.refreshFrequencyToolStripMenuItem.Name = "refreshFrequencyToolStripMenuItem";
            this.refreshFrequencyToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.refreshFrequencyToolStripMenuItem.Text = "Refresh Frequency";
            // 
            // blacklistToolStripMenuItem
            // 
            this.blacklistToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blacklistedVisitorToolStripMenuItem,
            this.addToBlacklistToolStripMenuItem});
            this.blacklistToolStripMenuItem.Name = "blacklistToolStripMenuItem";
            this.blacklistToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.blacklistToolStripMenuItem.Text = "Blacklist";
            // 
            // blacklistedVisitorToolStripMenuItem
            // 
            this.blacklistedVisitorToolStripMenuItem.Name = "blacklistedVisitorToolStripMenuItem";
            this.blacklistedVisitorToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.blacklistedVisitorToolStripMenuItem.Text = "Blacklisted Visitor";
            // 
            // addToBlacklistToolStripMenuItem
            // 
            this.addToBlacklistToolStripMenuItem.Name = "addToBlacklistToolStripMenuItem";
            this.addToBlacklistToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.addToBlacklistToolStripMenuItem.Text = "Add To Blacklist";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSoftwareToolStripMenuItem,
            this.aboutMoonlitToolStripMenuItem,
            this.howToUseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutSoftwareToolStripMenuItem
            // 
            this.aboutSoftwareToolStripMenuItem.Name = "aboutSoftwareToolStripMenuItem";
            this.aboutSoftwareToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.aboutSoftwareToolStripMenuItem.Text = "About Software";
            // 
            // aboutMoonlitToolStripMenuItem
            // 
            this.aboutMoonlitToolStripMenuItem.Name = "aboutMoonlitToolStripMenuItem";
            this.aboutMoonlitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.aboutMoonlitToolStripMenuItem.Text = "About Moonlit";
            // 
            // howToUseToolStripMenuItem
            // 
            this.howToUseToolStripMenuItem.Name = "howToUseToolStripMenuItem";
            this.howToUseToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.howToUseToolStripMenuItem.Text = "How To Use";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 106);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1223, 545);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.dgWaiting);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1215, 516);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "     CLUB DETAILS     ";
            // 
            // dgWaiting
            // 
            this.dgWaiting.AllowUserToAddRows = false;
            this.dgWaiting.AllowUserToDeleteRows = false;
            this.dgWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgWaiting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgWaiting.BackgroundColor = System.Drawing.Color.White;
            this.dgWaiting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWaiting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNameWaiting,
            this.ColMobileWaiting,
            this.ColEmailWaiting,
            this.ColPurposeWaiting,
            this.ColTypeWaiting,
            this.ColCheckinWaiting,
            this.colWaitingDuration,
            this.ColCancelWaiting,
            this.colViewVisitorWaiting});
            this.dgWaiting.Location = new System.Drawing.Point(10, 6);
            this.dgWaiting.Name = "dgWaiting";
            this.dgWaiting.ReadOnly = true;
            this.dgWaiting.Size = new System.Drawing.Size(1198, 504);
            this.dgWaiting.TabIndex = 23;
            // 
            // ColNameWaiting
            // 
            this.ColNameWaiting.HeaderText = "Club Name";
            this.ColNameWaiting.Name = "ColNameWaiting";
            this.ColNameWaiting.ReadOnly = true;
            // 
            // ColMobileWaiting
            // 
            this.ColMobileWaiting.HeaderText = "Contact Person";
            this.ColMobileWaiting.Name = "ColMobileWaiting";
            this.ColMobileWaiting.ReadOnly = true;
            // 
            // ColEmailWaiting
            // 
            this.ColEmailWaiting.HeaderText = "Company Name ";
            this.ColEmailWaiting.Name = "ColEmailWaiting";
            this.ColEmailWaiting.ReadOnly = true;
            // 
            // ColPurposeWaiting
            // 
            this.ColPurposeWaiting.HeaderText = "Email";
            this.ColPurposeWaiting.Name = "ColPurposeWaiting";
            this.ColPurposeWaiting.ReadOnly = true;
            // 
            // ColTypeWaiting
            // 
            this.ColTypeWaiting.HeaderText = "Country";
            this.ColTypeWaiting.Name = "ColTypeWaiting";
            this.ColTypeWaiting.ReadOnly = true;
            // 
            // ColCheckinWaiting
            // 
            this.ColCheckinWaiting.HeaderText = "Cell";
            this.ColCheckinWaiting.Name = "ColCheckinWaiting";
            this.ColCheckinWaiting.ReadOnly = true;
            // 
            // colWaitingDuration
            // 
            this.colWaitingDuration.HeaderText = "Registered On";
            this.colWaitingDuration.Name = "colWaitingDuration";
            this.colWaitingDuration.ReadOnly = true;
            // 
            // ColCancelWaiting
            // 
            this.ColCancelWaiting.HeaderText = "Status";
            this.ColCancelWaiting.Name = "ColCancelWaiting";
            this.ColCancelWaiting.ReadOnly = true;
            this.ColCancelWaiting.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColCancelWaiting.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colViewVisitorWaiting
            // 
            this.colViewVisitorWaiting.HeaderText = "View";
            this.colViewVisitorWaiting.Name = "colViewVisitorWaiting";
            this.colViewVisitorWaiting.ReadOnly = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.dgSucessfull);
            this.tabPage2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1215, 516);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "     PLAYER DETAILS     ";
            // 
            // dgSucessfull
            // 
            this.dgSucessfull.AllowUserToAddRows = false;
            this.dgSucessfull.AllowUserToDeleteRows = false;
            this.dgSucessfull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSucessfull.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSucessfull.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgSucessfull.BackgroundColor = System.Drawing.Color.White;
            this.dgSucessfull.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSucessfull.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNameSucessfull,
            this.colMobileSucessfull,
            this.colEmailSucessfull,
            this.colPurposeSucessfull,
            this.colTypeSucessfull,
            this.colCheckinTimeSucessfull,
            this.colCancelSucessfull,
            this.colViewVisitorSucessfull});
            this.dgSucessfull.Location = new System.Drawing.Point(10, 6);
            this.dgSucessfull.Name = "dgSucessfull";
            this.dgSucessfull.ReadOnly = true;
            this.dgSucessfull.Size = new System.Drawing.Size(1198, 504);
            this.dgSucessfull.TabIndex = 24;
            // 
            // colNameSucessfull
            // 
            this.colNameSucessfull.HeaderText = "Name";
            this.colNameSucessfull.Name = "colNameSucessfull";
            this.colNameSucessfull.ReadOnly = true;
            // 
            // colMobileSucessfull
            // 
            this.colMobileSucessfull.HeaderText = "Gender";
            this.colMobileSucessfull.Name = "colMobileSucessfull";
            this.colMobileSucessfull.ReadOnly = true;
            // 
            // colEmailSucessfull
            // 
            this.colEmailSucessfull.HeaderText = "Email";
            this.colEmailSucessfull.Name = "colEmailSucessfull";
            this.colEmailSucessfull.ReadOnly = true;
            // 
            // colPurposeSucessfull
            // 
            this.colPurposeSucessfull.HeaderText = "Country";
            this.colPurposeSucessfull.Name = "colPurposeSucessfull";
            this.colPurposeSucessfull.ReadOnly = true;
            // 
            // colTypeSucessfull
            // 
            this.colTypeSucessfull.HeaderText = "Cell";
            this.colTypeSucessfull.Name = "colTypeSucessfull";
            this.colTypeSucessfull.ReadOnly = true;
            // 
            // colCheckinTimeSucessfull
            // 
            this.colCheckinTimeSucessfull.HeaderText = "Registered On";
            this.colCheckinTimeSucessfull.Name = "colCheckinTimeSucessfull";
            this.colCheckinTimeSucessfull.ReadOnly = true;
            // 
            // colCancelSucessfull
            // 
            this.colCancelSucessfull.HeaderText = "Status";
            this.colCancelSucessfull.Name = "colCancelSucessfull";
            this.colCancelSucessfull.ReadOnly = true;
            this.colCancelSucessfull.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCancelSucessfull.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCancelSucessfull.Visible = false;
            // 
            // colViewVisitorSucessfull
            // 
            this.colViewVisitorSucessfull.HeaderText = "View";
            this.colViewVisitorSucessfull.Name = "colViewVisitorSucessfull";
            this.colViewVisitorSucessfull.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(24, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1200, 26);
            this.label1.TabIndex = 25;
            this.label1.Text = "Club and Players user details";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.LimeGreen;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(36, 670);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1176, 51);
            this.label2.TabIndex = 26;
            this.label2.Text = "www.biliardoprofessionale.it";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1247, 730);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAdminDashboard";
            this.ShowInTaskbar = false;
            this.Text = "Admin Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAdminDashboard_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgWaiting)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSucessfull)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewEmployeeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewReceptionistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDepartmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVisitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewVisitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todaysVisitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vBlockedUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshFrequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blacklistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blacklistedVisitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToBlacklistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSoftwareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMoonlitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToUseToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.DataGridView dgWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNameWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMobileWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColEmailWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPurposeWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTypeWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCheckinWaiting;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWaitingDuration;
        private System.Windows.Forms.DataGridViewButtonColumn ColCancelWaiting;
        private System.Windows.Forms.DataGridViewButtonColumn colViewVisitorWaiting;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgSucessfull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNameSucessfull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMobileSucessfull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmailSucessfull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPurposeSucessfull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeSucessfull;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCheckinTimeSucessfull;
        private System.Windows.Forms.DataGridViewButtonColumn colCancelSucessfull;
        private System.Windows.Forms.DataGridViewButtonColumn colViewVisitorSucessfull;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bgWorker;
    }
}