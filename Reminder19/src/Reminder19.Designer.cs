namespace Reminder19.src
{
    partial class Reminder19
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reminder19));
            this.createNewAlertBut = new System.Windows.Forms.Button();
            this.editAlertBut = new System.Windows.Forms.Button();
            this.removeAlertBut = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.alertsListView = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.notifyIconRestoreItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconCloseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableAllAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAllAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllDisabledAlertsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bySearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byNextAletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerReminder19ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconContextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // createNewAlertBut
            // 
            this.createNewAlertBut.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.createNewAlertBut.Location = new System.Drawing.Point(12, 270);
            this.createNewAlertBut.Name = "createNewAlertBut";
            this.createNewAlertBut.Size = new System.Drawing.Size(134, 23);
            this.createNewAlertBut.TabIndex = 2;
            this.createNewAlertBut.Text = "Create New Alert";
            this.createNewAlertBut.UseVisualStyleBackColor = true;
            this.createNewAlertBut.Click += new System.EventHandler(this.createNewAlertBut_Click);
            // 
            // editAlertBut
            // 
            this.editAlertBut.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.editAlertBut.Location = new System.Drawing.Point(152, 270);
            this.editAlertBut.Name = "editAlertBut";
            this.editAlertBut.Size = new System.Drawing.Size(135, 23);
            this.editAlertBut.TabIndex = 3;
            this.editAlertBut.Text = "Edit Selected Alert";
            this.editAlertBut.UseVisualStyleBackColor = true;
            this.editAlertBut.Click += new System.EventHandler(this.editAlertBut_Click);
            // 
            // removeAlertBut
            // 
            this.removeAlertBut.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.removeAlertBut.Location = new System.Drawing.Point(293, 270);
            this.removeAlertBut.Name = "removeAlertBut";
            this.removeAlertBut.Size = new System.Drawing.Size(136, 23);
            this.removeAlertBut.TabIndex = 4;
            this.removeAlertBut.Text = "Remove Selected Alert";
            this.removeAlertBut.UseVisualStyleBackColor = true;
            this.removeAlertBut.Click += new System.EventHandler(this.removeAlertBut_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Alerts:";
            // 
            // alertsListView
            // 
            this.alertsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.alertsListView.CheckBoxes = true;
            this.alertsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.alertsListView.FullRowSelect = true;
            this.alertsListView.GridLines = true;
            this.alertsListView.Location = new System.Drawing.Point(12, 45);
            this.alertsListView.Name = "alertsListView";
            this.alertsListView.Size = new System.Drawing.Size(417, 219);
            this.alertsListView.TabIndex = 6;
            this.alertsListView.UseCompatibleStateImageBehavior = false;
            this.alertsListView.View = System.Windows.Forms.View.Details;
            this.alertsListView.DoubleClick += new System.EventHandler(this.alertsListView_DoubleClick);
            this.alertsListView.Resize += new System.EventHandler(this.alertsListView_Resize);
            this.alertsListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.alertsListView_ColumnClick);
            this.alertsListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.alertsListView_ColumnWidthChanging);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Enabled";
            this.columnHeader4.Width = 55;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Title";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Next Alert";
            this.columnHeader3.Width = 136;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Reminder19";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.notifyIconRestoreItem,
            this.notifyIconCloseItem});
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(167, 48);
            // 
            // notifyIconRestoreItem
            // 
            this.notifyIconRestoreItem.Name = "notifyIconRestoreItem";
            this.notifyIconRestoreItem.Size = new System.Drawing.Size(166, 22);
            this.notifyIconRestoreItem.Text = "Restore";
            this.notifyIconRestoreItem.Click += new System.EventHandler(this.notifyIconRestoreItem_Click);
            // 
            // notifyIconCloseItem
            // 
            this.notifyIconCloseItem.Name = "notifyIconCloseItem";
            this.notifyIconCloseItem.Size = new System.Drawing.Size(166, 22);
            this.notifyIconCloseItem.Text = "Close Application";
            this.notifyIconCloseItem.Click += new System.EventHandler(this.notifyIconCloseItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(441, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableAllAlertsToolStripMenuItem,
            this.disableAllAlertsToolStripMenuItem,
            this.removeAllDisabledAlertsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // enableAllAlertsToolStripMenuItem
            // 
            this.enableAllAlertsToolStripMenuItem.Name = "enableAllAlertsToolStripMenuItem";
            this.enableAllAlertsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.enableAllAlertsToolStripMenuItem.Text = "Enable All Alerts";
            this.enableAllAlertsToolStripMenuItem.Click += new System.EventHandler(this.enableAllAlertsToolStripMenuItem_Click);
            // 
            // disableAllAlertsToolStripMenuItem
            // 
            this.disableAllAlertsToolStripMenuItem.Name = "disableAllAlertsToolStripMenuItem";
            this.disableAllAlertsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.disableAllAlertsToolStripMenuItem.Text = "Disable All Alerts";
            this.disableAllAlertsToolStripMenuItem.Click += new System.EventHandler(this.disableAllAlertsToolStripMenuItem_Click);
            // 
            // removeAllDisabledAlertsToolStripMenuItem
            // 
            this.removeAllDisabledAlertsToolStripMenuItem.Name = "removeAllDisabledAlertsToolStripMenuItem";
            this.removeAllDisabledAlertsToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.removeAllDisabledAlertsToolStripMenuItem.Text = "Remove All Disabled Alerts";
            this.removeAllDisabledAlertsToolStripMenuItem.Click += new System.EventHandler(this.removeAllDisabledAlertsToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bySearchToolStripMenuItem,
            this.byToolStripMenuItem,
            this.byStatusToolStripMenuItem,
            this.byTitleToolStripMenuItem,
            this.byNextAletToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.sortToolStripMenuItem.Text = "Sort";
            // 
            // bySearchToolStripMenuItem
            // 
            this.bySearchToolStripMenuItem.Name = "bySearchToolStripMenuItem";
            this.bySearchToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.bySearchToolStripMenuItem.Text = "By Search";
            this.bySearchToolStripMenuItem.Click += new System.EventHandler(this.bySearchToolStripMenuItem_Click);
            // 
            // byToolStripMenuItem
            // 
            this.byToolStripMenuItem.Name = "byToolStripMenuItem";
            this.byToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.byToolStripMenuItem.Text = "By Creation Order";
            this.byToolStripMenuItem.Click += new System.EventHandler(this.byToolStripMenuItem_Click);
            // 
            // byStatusToolStripMenuItem
            // 
            this.byStatusToolStripMenuItem.Name = "byStatusToolStripMenuItem";
            this.byStatusToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.byStatusToolStripMenuItem.Text = "By Status";
            this.byStatusToolStripMenuItem.Click += new System.EventHandler(this.byStatusToolStripMenuItem_Click);
            // 
            // byTitleToolStripMenuItem
            // 
            this.byTitleToolStripMenuItem.Name = "byTitleToolStripMenuItem";
            this.byTitleToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.byTitleToolStripMenuItem.Text = "By Title";
            this.byTitleToolStripMenuItem.Click += new System.EventHandler(this.byTitleToolStripMenuItem_Click);
            // 
            // byNextAletToolStripMenuItem
            // 
            this.byNextAletToolStripMenuItem.Name = "byNextAletToolStripMenuItem";
            this.byNextAletToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.byNextAletToolStripMenuItem.Text = "By Next Alert";
            this.byNextAletToolStripMenuItem.Click += new System.EventHandler(this.byNextAletToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerReminder19ToolStripMenuItem,
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // registerReminder19ToolStripMenuItem
            // 
            this.registerReminder19ToolStripMenuItem.Name = "registerReminder19ToolStripMenuItem";
            this.registerReminder19ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.registerReminder19ToolStripMenuItem.Text = "Register Reminder 19";
            this.registerReminder19ToolStripMenuItem.Click += new System.EventHandler(this.registerReminder19ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Reminder19
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 304);
            this.Controls.Add(this.alertsListView);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.removeAlertBut);
            this.Controls.Add(this.editAlertBut);
            this.Controls.Add(this.createNewAlertBut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(449, 338);
            this.Name = "Reminder19";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Alert Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Reminder19_FormClosed);
            this.Resize += new System.EventHandler(this.Reminder19_Resize);
            this.Shown += new System.EventHandler(this.Reminder19_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Reminder19_FormClosing);
            this.notifyIconContextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createNewAlertBut;
        private System.Windows.Forms.Button editAlertBut;
        private System.Windows.Forms.Button removeAlertBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView alertsListView;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip notifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem notifyIconRestoreItem;
        private System.Windows.Forms.ToolStripMenuItem notifyIconCloseItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableAllAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableAllAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllDisabledAlertsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bySearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byNextAletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerReminder19ToolStripMenuItem;
    }
}

