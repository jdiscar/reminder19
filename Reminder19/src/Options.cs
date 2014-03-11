using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Reminder19.src.cntrmsgbox.Dialog;

namespace Reminder19.src
{
    public partial class Options : Form
    {
        private Controller controller;

        public Options(Controller controller)
        {
            InitializeComponent();

            this.controller = controller;

            string positionType = controller.getSetting("WindowsPositioning");
            if (positionType.Equals("default"))
            {
                defaultRadio.Checked = true;
            }
            else if (positionType.Equals("center"))
            {
                centerRadio.Checked = true;
            }
            else if (positionType.Equals("custom"))
            {
                customRadio.Checked = true;
            }

            if (controller.getSetting("WarnBeforeClose").Equals("true"))
            {
                closeWarningCheckbox.Checked = true;
            }

            alertFontSizeField.Text = "" + controller.getSetting("AlertFont");

            colorBox.Text = controller.getSetting("AlertBackground");
            if (!colorBox.Text.Equals("Window"))
            {
                colorBox.BackColor = Color.FromArgb(System.Convert.ToInt32(colorBox.Text));
            }

            soundBox.Text = controller.getSetting("AlertSound");           
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = Color.FromName(colorBox.Text);
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    colorBox.BackColor = colorDialog.Color;
                    colorBox.Text = "" + colorDialog.Color.ToArgb();
                }
                catch
                {
                    MsgBox.Show("Invalid Color Selection");
                }
            }
        }

        private void colorBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorBox.BackColor = Color.FromName("Window");
        }

        private void soundButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Alarm Sound";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Wav (*.wav)|*.wav|Mp3 (*.mp3)|*.mp3";
            openFileDialog.CheckFileExists = true; 
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                soundBox.Text = openFileDialog.FileName;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (defaultRadio.Checked)
            {
                controller.updateSettings("WindowsPositioning", "default");
            }
            else if (centerRadio.Checked)
            {
                controller.updateSettings("WindowsPositioning", "center");
            }
            else if (customRadio.Checked)
            {
                controller.updateSettings("WindowsPositioning", "custom");
            }

            if (closeWarningCheckbox.Checked)
            {
                controller.updateSettings("WarnBeforeClose", "true");
            }
            else
            {
                controller.updateSettings("WarnBeforeClose", "false");
            }

            try
            {
                int alertFont = 10;
                alertFont = System.Convert.ToInt32( alertFontSizeField.Text );
                if (alertFont < 8 || alertFont > 72)
                {
                    MsgBox.Show("Alert Font must be between 8 and 72.");
                    return;
                }
                controller.updateSettings("AlertFont", ""+alertFont);
            }
            catch
            {
                MsgBox.Show("The alert font must be a number.");
                return;
            }

            try
            {
                if (!colorBox.Text.Equals("Window"))
                {
                    colorBox.BackColor = Color.FromArgb(System.Convert.ToInt32(colorBox.Text));
                }
                controller.updateSettings("AlertBackground", ""+colorBox.BackColor.ToArgb());
            }
            catch
            {
                MsgBox.Show("Invalid Color Selection");
                return;
            }

            if (soundBox.Text.ToUpper().Equals("NONE"))
                soundBox.Text = "";

            if(!soundBox.Text.Equals(""))
            {
                if (!File.Exists(soundBox.Text))
                {
                    MsgBox.Show("The specified sound file does not exist.");
                    return;
                }
                else if (!soundBox.Text.ToUpper().EndsWith(".WAV"))
                {
                    if (!Registration.isValid())
                    {
                        MsgBox.Show("Only a registered version of Reminder 19 can play non-wave files.");
                        return;
                    }
                    else
                    {
                        MsgBox.Show("Warning: Wave Sound Format is preferred.");
                    }
                }                
            }
            controller.updateSettings("AlertSound", soundBox.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}