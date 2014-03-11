namespace Reminder19.src
{
    partial class AlertDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.disableButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.snoozeField = new System.Windows.Forms.TextBox();
            this.snoozeComboBox = new System.Windows.Forms.ComboBox();
            this.snoozeButton = new System.Windows.Forms.Button();
            this.acknowledgeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fiveMinSnooze = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.oneHourSnooze = new System.Windows.Forms.Button();
            this.tenMinSnooze = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.oneDaySnooze = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.alertLabel = new System.Windows.Forms.Label();
            this.messageBox = new RichTextBoxExtended.RichTextBoxExtended();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Message:";
            // 
            // disableButton
            // 
            this.disableButton.Location = new System.Drawing.Point(158, 103);
            this.disableButton.Name = "disableButton";
            this.disableButton.Size = new System.Drawing.Size(127, 23);
            this.disableButton.TabIndex = 2;
            this.disableButton.Text = "Disable Reminder";
            this.disableButton.UseVisualStyleBackColor = true;
            this.disableButton.Click += new System.EventHandler(this.disableButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Custom Snooze:";
            // 
            // snoozeField
            // 
            this.snoozeField.Location = new System.Drawing.Point(94, 57);
            this.snoozeField.Name = "snoozeField";
            this.snoozeField.Size = new System.Drawing.Size(77, 20);
            this.snoozeField.TabIndex = 9;
            this.snoozeField.TabStop = false;
            // 
            // snoozeComboBox
            // 
            this.snoozeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.snoozeComboBox.FormattingEnabled = true;
            this.snoozeComboBox.Items.AddRange(new object[] {
            "Minute(s)",
            "Hour(s)",
            "Day(s)",
            "Week(s)"});
            this.snoozeComboBox.Location = new System.Drawing.Point(177, 57);
            this.snoozeComboBox.Name = "snoozeComboBox";
            this.snoozeComboBox.Size = new System.Drawing.Size(130, 21);
            this.snoozeComboBox.TabIndex = 10;
            this.snoozeComboBox.TabStop = false;
            // 
            // snoozeButton
            // 
            this.snoozeButton.Location = new System.Drawing.Point(313, 57);
            this.snoozeButton.Name = "snoozeButton";
            this.snoozeButton.Size = new System.Drawing.Size(93, 23);
            this.snoozeButton.TabIndex = 11;
            this.snoozeButton.TabStop = false;
            this.snoozeButton.Text = "Snooze";
            this.snoozeButton.UseVisualStyleBackColor = true;
            this.snoozeButton.Click += new System.EventHandler(this.snoozeButton_Click);
            // 
            // acknowledgeButton
            // 
            this.acknowledgeButton.Location = new System.Drawing.Point(13, 103);
            this.acknowledgeButton.Name = "acknowledgeButton";
            this.acknowledgeButton.Size = new System.Drawing.Size(139, 23);
            this.acknowledgeButton.TabIndex = 1;
            this.acknowledgeButton.Text = "Acknowledge Reminder";
            this.acknowledgeButton.UseVisualStyleBackColor = true;
            this.acknowledgeButton.Click += new System.EventHandler(this.acknowledgeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(394, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "(Snooze if you want to ignore the reminder for now and be reminded at a later tim" +
                "e)";
            // 
            // fiveMinSnooze
            // 
            this.fiveMinSnooze.Location = new System.Drawing.Point(86, 28);
            this.fiveMinSnooze.Name = "fiveMinSnooze";
            this.fiveMinSnooze.Size = new System.Drawing.Size(77, 23);
            this.fiveMinSnooze.TabIndex = 5;
            this.fiveMinSnooze.Text = "5 min";
            this.fiveMinSnooze.UseVisualStyleBackColor = true;
            this.fiveMinSnooze.Click += new System.EventHandler(this.fiveMinSnooze_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Snooze for:";
            // 
            // oneHourSnooze
            // 
            this.oneHourSnooze.Location = new System.Drawing.Point(250, 28);
            this.oneHourSnooze.Name = "oneHourSnooze";
            this.oneHourSnooze.Size = new System.Drawing.Size(75, 23);
            this.oneHourSnooze.TabIndex = 7;
            this.oneHourSnooze.Text = "1 Hour";
            this.oneHourSnooze.UseVisualStyleBackColor = true;
            this.oneHourSnooze.Click += new System.EventHandler(this.oneHourSnooze_Click);
            // 
            // tenMinSnooze
            // 
            this.tenMinSnooze.Location = new System.Drawing.Point(169, 28);
            this.tenMinSnooze.Name = "tenMinSnooze";
            this.tenMinSnooze.Size = new System.Drawing.Size(75, 23);
            this.tenMinSnooze.TabIndex = 6;
            this.tenMinSnooze.Text = "10 min";
            this.tenMinSnooze.UseVisualStyleBackColor = true;
            this.tenMinSnooze.Click += new System.EventHandler(this.tenMinSnooze_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(291, 103);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(119, 23);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove Reminder";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // oneDaySnooze
            // 
            this.oneDaySnooze.Location = new System.Drawing.Point(331, 28);
            this.oneDaySnooze.Name = "oneDaySnooze";
            this.oneDaySnooze.Size = new System.Drawing.Size(75, 23);
            this.oneDaySnooze.TabIndex = 8;
            this.oneDaySnooze.Text = "1 Day";
            this.oneDaySnooze.UseVisualStyleBackColor = true;
            this.oneDaySnooze.Click += new System.EventHandler(this.oneDaySnooze_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.oneDaySnooze);
            this.panel1.Controls.Add(this.disableButton);
            this.panel1.Controls.Add(this.removeButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tenMinSnooze);
            this.panel1.Controls.Add(this.snoozeField);
            this.panel1.Controls.Add(this.oneHourSnooze);
            this.panel1.Controls.Add(this.snoozeComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.snoozeButton);
            this.panel1.Controls.Add(this.fiveMinSnooze);
            this.panel1.Controls.Add(this.acknowledgeButton);
            this.panel1.Location = new System.Drawing.Point(7, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(416, 136);
            this.panel1.TabIndex = 16;
            // 
            // alertLabel
            // 
            this.alertLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alertLabel.AutoSize = true;
            this.alertLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.alertLabel.Location = new System.Drawing.Point(297, 9);
            this.alertLabel.Name = "alertLabel";
            this.alertLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.alertLabel.Size = new System.Drawing.Size(114, 13);
            this.alertLabel.TabIndex = 13;
            this.alertLabel.Text = "10/22/2008 10:51 PM";
            this.alertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.alertLabel.Click += new System.EventHandler(this.alertLabel_Click);
            // 
            // messageBox
            // 
            this.messageBox.AcceptsTab = false;
            this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.messageBox.AutoWordSelection = true;
            this.messageBox.BackColor = System.Drawing.SystemColors.Control;
            this.messageBox.DetectURLs = true;
            this.messageBox.Location = new System.Drawing.Point(12, 32);
            this.messageBox.MinimumSize = new System.Drawing.Size(406, 158);
            this.messageBox.Name = "messageBox";
            this.messageBox.ReadOnly = false;
            // 
            // 
            // 
            this.messageBox.RichTextBox.AutoWordSelection = true;
            this.messageBox.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBox.RichTextBox.Location = new System.Drawing.Point(0, 5);
            this.messageBox.RichTextBox.Name = "rtb1";
            this.messageBox.RichTextBox.Size = new System.Drawing.Size(411, 159);
            this.messageBox.RichTextBox.TabIndex = 1;
            this.messageBox.ShowBold = false;
            this.messageBox.ShowCenterJustify = false;
            this.messageBox.ShowColors = false;
            this.messageBox.ShowCopy = false;
            this.messageBox.ShowCut = false;
            this.messageBox.ShowFont = false;
            this.messageBox.ShowFontSize = false;
            this.messageBox.ShowItalic = false;
            this.messageBox.ShowLeftJustify = false;
            this.messageBox.ShowOpen = false;
            this.messageBox.ShowPaste = false;
            this.messageBox.ShowRedo = false;
            this.messageBox.ShowRightJustify = false;
            this.messageBox.ShowSave = false;
            this.messageBox.ShowStamp = false;
            this.messageBox.ShowStrikeout = false;
            this.messageBox.ShowToolBarText = false;
            this.messageBox.ShowUnderline = false;
            this.messageBox.ShowUndo = false;
            this.messageBox.Size = new System.Drawing.Size(411, 164);
            this.messageBox.StampAction = RichTextBoxExtended.StampActions.EditedBy;
            this.messageBox.StampColor = System.Drawing.Color.Blue;
            this.messageBox.TabIndex = 0;
            // 
            // 
            // 
            this.messageBox.Toolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.messageBox.Toolbar.ButtonSize = new System.Drawing.Size(16, 16);
            this.messageBox.Toolbar.Divider = false;
            this.messageBox.Toolbar.DropDownArrows = true;
            this.messageBox.Toolbar.Location = new System.Drawing.Point(0, 0);
            this.messageBox.Toolbar.Name = "tb1";
            this.messageBox.Toolbar.ShowToolTips = true;
            this.messageBox.Toolbar.Size = new System.Drawing.Size(411, 5);
            this.messageBox.Toolbar.TabIndex = 0;
            // 
            // AlertDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 345);
            this.Controls.Add(this.alertLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(448, 379);
            this.Name = "AlertDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Alert!!!";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.AlertDialog_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertDialog_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBoxExtended.RichTextBoxExtended messageBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button disableButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox snoozeField;
        private System.Windows.Forms.ComboBox snoozeComboBox;
        private System.Windows.Forms.Button snoozeButton;
        private System.Windows.Forms.Button acknowledgeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button fiveMinSnooze;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button oneHourSnooze;
        private System.Windows.Forms.Button tenMinSnooze;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button oneDaySnooze;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label alertLabel;
    }
}

