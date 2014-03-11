namespace Reminder19.src
{
    partial class AlertSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertSearch));
            this.queryField = new System.Windows.Forms.TextBox();
            this.titlesCheckbox = new System.Windows.Forms.CheckBox();
            this.messagesCheckbox = new System.Windows.Forms.CheckBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // queryField
            // 
            this.queryField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.queryField.Location = new System.Drawing.Point(12, 41);
            this.queryField.Name = "queryField";
            this.queryField.Size = new System.Drawing.Size(527, 20);
            this.queryField.TabIndex = 0;
            this.queryField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.queryField_KeyDown);
            // 
            // titlesCheckbox
            // 
            this.titlesCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.titlesCheckbox.AutoSize = true;
            this.titlesCheckbox.Checked = true;
            this.titlesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.titlesCheckbox.Location = new System.Drawing.Point(3, 3);
            this.titlesCheckbox.Name = "titlesCheckbox";
            this.titlesCheckbox.Size = new System.Drawing.Size(88, 17);
            this.titlesCheckbox.TabIndex = 1;
            this.titlesCheckbox.Text = "Search Titles";
            this.titlesCheckbox.UseVisualStyleBackColor = true;
            // 
            // messagesCheckbox
            // 
            this.messagesCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.messagesCheckbox.AutoSize = true;
            this.messagesCheckbox.Checked = true;
            this.messagesCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.messagesCheckbox.Location = new System.Drawing.Point(169, 3);
            this.messagesCheckbox.Name = "messagesCheckbox";
            this.messagesCheckbox.Size = new System.Drawing.Size(111, 17);
            this.messagesCheckbox.TabIndex = 2;
            this.messagesCheckbox.Text = "Search Messages";
            this.messagesCheckbox.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.searchButton.Location = new System.Drawing.Point(114, 3);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(88, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Sort By Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelButton.Location = new System.Drawing.Point(327, 3);
            this.cancelButton.MaximumSize = new System.Drawing.Size(0, 23);
            this.cancelButton.MinimumSize = new System.Drawing.Size(75, 23);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.AutoSize = true;
            this.helpButton.Location = new System.Drawing.Point(495, 4);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(29, 13);
            this.helpButton.TabIndex = 6;
            this.helpButton.TabStop = true;
            this.helpButton.Text = "Help";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.titlesCheckbox);
            this.panel1.Controls.Add(this.messagesCheckbox);
            this.panel1.Controls.Add(this.helpButton);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 22);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.searchButton);
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Location = new System.Drawing.Point(12, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(527, 30);
            this.panel2.TabIndex = 8;
            // 
            // AlertSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 116);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.queryField);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(5000, 150);
            this.MinimumSize = new System.Drawing.Size(567, 150);
            this.Name = "AlertSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AlertSearch";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox queryField;
        private System.Windows.Forms.CheckBox titlesCheckbox;
        private System.Windows.Forms.CheckBox messagesCheckbox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.LinkLabel helpButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}