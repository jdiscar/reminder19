namespace Reminder19.src.messageboxes
{
    partial class MaxAlertPopUp
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
            this.richTextBoxExtended1 = new RichTextBoxExtended.RichTextBoxExtended();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxExtended1
            // 
            this.richTextBoxExtended1.AcceptsTab = false;
            this.richTextBoxExtended1.AutoWordSelection = true;
            this.richTextBoxExtended1.DetectURLs = true;
            this.richTextBoxExtended1.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxExtended1.Name = "richTextBoxExtended1";
            this.richTextBoxExtended1.ReadOnly = true;
            // 
            // 
            // 
            this.richTextBoxExtended1.RichTextBox.AutoWordSelection = true;
            this.richTextBoxExtended1.RichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxExtended1.RichTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxExtended1.RichTextBox.Name = "rtb1";
            this.richTextBoxExtended1.RichTextBox.ReadOnly = true;
            this.richTextBoxExtended1.RichTextBox.Size = new System.Drawing.Size(494, 75);
            this.richTextBoxExtended1.RichTextBox.TabIndex = 1;
            this.richTextBoxExtended1.ShowBold = false;
            this.richTextBoxExtended1.ShowCenterJustify = false;
            this.richTextBoxExtended1.ShowColors = false;
            this.richTextBoxExtended1.ShowCopy = false;
            this.richTextBoxExtended1.ShowCut = false;
            this.richTextBoxExtended1.ShowFont = false;
            this.richTextBoxExtended1.ShowFontSize = false;
            this.richTextBoxExtended1.ShowItalic = false;
            this.richTextBoxExtended1.ShowLeftJustify = false;
            this.richTextBoxExtended1.ShowOpen = false;
            this.richTextBoxExtended1.ShowPaste = false;
            this.richTextBoxExtended1.ShowRedo = false;
            this.richTextBoxExtended1.ShowRightJustify = false;
            this.richTextBoxExtended1.ShowSave = false;
            this.richTextBoxExtended1.ShowStamp = false;
            this.richTextBoxExtended1.ShowStrikeout = false;
            this.richTextBoxExtended1.ShowToolBarText = false;
            this.richTextBoxExtended1.ShowUnderline = false;
            this.richTextBoxExtended1.ShowUndo = false;
            this.richTextBoxExtended1.Size = new System.Drawing.Size(494, 75);
            this.richTextBoxExtended1.StampAction = RichTextBoxExtended.StampActions.EditedBy;
            this.richTextBoxExtended1.StampColor = System.Drawing.Color.Blue;
            this.richTextBoxExtended1.TabIndex = 0;
            // 
            // 
            // 
            this.richTextBoxExtended1.Toolbar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.richTextBoxExtended1.Toolbar.ButtonSize = new System.Drawing.Size(16, 16);
            this.richTextBoxExtended1.Toolbar.Divider = false;
            this.richTextBoxExtended1.Toolbar.DropDownArrows = true;
            this.richTextBoxExtended1.Toolbar.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxExtended1.Toolbar.Name = "tb1";
            this.richTextBoxExtended1.Toolbar.ShowToolTips = true;
            this.richTextBoxExtended1.Toolbar.Size = new System.Drawing.Size(369, 20);
            this.richTextBoxExtended1.Toolbar.TabIndex = 0;
            this.richTextBoxExtended1.Toolbar.Visible = false;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(219, 93);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // MaxAlertPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 123);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.richTextBoxExtended1);
            this.MaximumSize = new System.Drawing.Size(534, 159);
            this.MinimumSize = new System.Drawing.Size(534, 159);
            this.Name = "MaxAlertPopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Please Register Reminder 19";
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBoxExtended.RichTextBoxExtended richTextBoxExtended1;
        private System.Windows.Forms.Button okButton;
    }
}