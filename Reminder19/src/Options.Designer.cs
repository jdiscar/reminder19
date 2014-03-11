namespace Reminder19.src
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customRadio = new System.Windows.Forms.RadioButton();
            this.centerRadio = new System.Windows.Forms.RadioButton();
            this.defaultRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.soundBox = new System.Windows.Forms.TextBox();
            this.soundButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.colorBox = new System.Windows.Forms.ComboBox();
            this.closeWarningCheckbox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.alertFontSizeField = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Window Positioning:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customRadio);
            this.panel1.Controls.Add(this.centerRadio);
            this.panel1.Controls.Add(this.defaultRadio);
            this.panel1.Location = new System.Drawing.Point(15, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 88);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // customRadio
            // 
            this.customRadio.AutoSize = true;
            this.customRadio.Location = new System.Drawing.Point(15, 58);
            this.customRadio.Name = "customRadio";
            this.customRadio.Size = new System.Drawing.Size(142, 17);
            this.customRadio.TabIndex = 2;
            this.customRadio.Text = "Use last known positions";
            this.customRadio.UseVisualStyleBackColor = true;
            // 
            // centerRadio
            // 
            this.centerRadio.AutoSize = true;
            this.centerRadio.Location = new System.Drawing.Point(15, 35);
            this.centerRadio.Name = "centerRadio";
            this.centerRadio.Size = new System.Drawing.Size(139, 17);
            this.centerRadio.TabIndex = 0;
            this.centerRadio.TabStop = true;
            this.centerRadio.Text = "Always Center Windows";
            this.centerRadio.UseVisualStyleBackColor = true;
            // 
            // defaultRadio
            // 
            this.defaultRadio.AutoSize = true;
            this.defaultRadio.Location = new System.Drawing.Point(15, 12);
            this.defaultRadio.Name = "defaultRadio";
            this.defaultRadio.Size = new System.Drawing.Size(136, 17);
            this.defaultRadio.TabIndex = 0;
            this.defaultRadio.Text = "Use the System Default";
            this.defaultRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Default Alert Background Color: ";
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(317, 195);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(80, 23);
            this.colorButton.TabIndex = 3;
            this.colorButton.Text = "Change Color";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(313, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Default Alert Sound (Wave Preferred, Leave blank for no sound):";
            // 
            // soundBox
            // 
            this.soundBox.Location = new System.Drawing.Point(30, 248);
            this.soundBox.Name = "soundBox";
            this.soundBox.Size = new System.Drawing.Size(285, 20);
            this.soundBox.TabIndex = 4;
            // 
            // soundButton
            // 
            this.soundButton.Location = new System.Drawing.Point(322, 246);
            this.soundButton.Name = "soundButton";
            this.soundButton.Size = new System.Drawing.Size(75, 23);
            this.soundButton.TabIndex = 5;
            this.soundButton.Text = "Browse";
            this.soundButton.UseVisualStyleBackColor = true;
            this.soundButton.Click += new System.EventHandler(this.soundButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(94, 285);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(90, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save Changes";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(235, 285);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // colorBox
            // 
            this.colorBox.FormattingEnabled = true;
            this.colorBox.Items.AddRange(new object[] {
            "Window"});
            this.colorBox.Location = new System.Drawing.Point(30, 195);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(285, 21);
            this.colorBox.TabIndex = 2;
            this.colorBox.SelectedIndexChanged += new System.EventHandler(this.colorBox_SelectedIndexChanged);
            // 
            // closeWarningCheckbox
            // 
            this.closeWarningCheckbox.AutoSize = true;
            this.closeWarningCheckbox.Location = new System.Drawing.Point(15, 119);
            this.closeWarningCheckbox.Name = "closeWarningCheckbox";
            this.closeWarningCheckbox.Size = new System.Drawing.Size(165, 17);
            this.closeWarningCheckbox.TabIndex = 11;
            this.closeWarningCheckbox.TabStop = false;
            this.closeWarningCheckbox.Text = "Warn Before Closing Program";
            this.closeWarningCheckbox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Default Alert Font Size:";
            // 
            // alertFontSizeField
            // 
            this.alertFontSizeField.Location = new System.Drawing.Point(133, 147);
            this.alertFontSizeField.Name = "alertFontSizeField";
            this.alertFontSizeField.Size = new System.Drawing.Size(33, 20);
            this.alertFontSizeField.TabIndex = 1;
            this.alertFontSizeField.Text = "10";
            this.alertFontSizeField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 316);
            this.Controls.Add(this.alertFontSizeField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.closeWarningCheckbox);
            this.Controls.Add(this.colorBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.soundButton);
            this.Controls.Add(this.soundBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(429, 350);
            this.MinimumSize = new System.Drawing.Size(429, 350);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton customRadio;
        private System.Windows.Forms.RadioButton centerRadio;
        private System.Windows.Forms.RadioButton defaultRadio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox soundBox;
        private System.Windows.Forms.Button soundButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox colorBox;
        private System.Windows.Forms.CheckBox closeWarningCheckbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox alertFontSizeField;
    }
}