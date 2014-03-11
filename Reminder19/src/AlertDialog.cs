//This class does not actually make changes to the database.  The
//alert variable stored by this class will be saved by someone else.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Reminder19.src.cntrmsgbox.Dialog;

namespace Reminder19.src
{
    public partial class AlertDialog : Form
    {
        private Alert alert;
        private string soundFile;       

        public AlertDialog(Alert alert, String color, String soundFile)
        {
            InitializeComponent();
            this.Text = "Alert!! - " + alert.getTitle();
            this.alert = alert;
            messageBox.RichTextBox.Rtf = alert.getMessage();

            try
            {
                messageBox.RichTextBox.BackColor = Color.FromArgb(System.Convert.ToInt32(color));
            }
            catch
            {
                messageBox.RichTextBox.BackColor = Color.FromName("Window");
            }

            alertLabel.Text = ""+alert.getWakeUpTime();

            this.soundFile = soundFile;
        }

        private void AlertDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save Message Box Changes
            try
            {
                if (Reminder19.threadModalDialogs > -1 && messageBox.RichTextBox.Rtf != alert.getMessage() && !alert.getRemoved())
                {
                    Reminder19.threadModalDialogs++;
                    DialogResult result = MsgBox.Show("Save alert message changes for "+ alert.getTitle() +"?", "Save Changes?", MessageBoxButtons.YesNoCancel);
                    Reminder19.threadModalDialogs--;
                    if (result == DialogResult.Yes)
                    {
                        //actual saving happens from Reminder19.cs
                        alert.setMessage(messageBox.RichTextBox.Rtf);
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }                    
                }
            }
            //catch (InvalidOperationException)
            catch (Exception)
            {
                //Ignore errors.  It doesn't matter if this fails and there will be an exception
                //here if Reminder 19 is closed while an alert dialog is open due to accessing
                //controls from a different thread.
            }

            //Cleanup sound.
            cleanupSound();
        }

        public Alert getCurrentAlert()
        {
            return alert;
        }

        public void center()
        {
            this.CenterToScreen();
        }

        private void AlertDialog_Shown(object sender, EventArgs e)
        {
            this.Activate();
            this.TopMost = true;
            this.BringToFront();
            this.Focus();

            playSound(soundFile);
            launchCommand(alert.getCommand());
        }

        private void launchCommand(string cmd)
        {
            if (cmd.Trim().Equals(""))
                return;

            string[] command = cmd.Split(new Char[] { '?' }, 2);
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = command[0];
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            if (command.Length > 1)
                startInfo.Arguments = command[1];

            try
            {
                System.Diagnostics.Process exeProcess = System.Diagnostics.Process.Start(startInfo);
                exeProcess.Close();
            }
            catch
            {
                //Notify on error.
                MsgBox.Show("Failed to run associated command.  If you do not know why you got this message, " +
                    "make sure that your advanced schedule field only contains 6 fields separated by 5 spaces.", "Launch Command Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        #region Used for playing sounds.  MP3 part is unmanaged!
        private bool mciBeingUsed = false;
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(string lpstrCommand, 
            StringBuilder lpstrReturnString, int uReturnLength, IntPtr hwndCallback);

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        static extern bool mciGetErrorString(int error, StringBuilder desc, uint descLen);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetShortPathName(
                 [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)]
                   string path,
                 [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPTStr)]
                   StringBuilder shortPath,
                 int shortPathLength
                 );

        private void playSound(string defaultsound)
        {
            try
            {
                if (!soundFile.Equals("") && !soundFile.ToUpper().Equals("NONE"))
                {
                    if (soundFile.ToUpper().EndsWith(".WAV"))
                    {
                        System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
                        myPlayer.SoundLocation = soundFile;
                        myPlayer.Play();
                    }
                    else if (!Registration.isValid())
                    {
                        MsgBox.Show("Only a registered version of Reminder 19 can play non-wave files.");
                    }
                    else
                    {
                        StringBuilder shortPath = new StringBuilder(255);
                        GetShortPathName(soundFile, shortPath, shortPath.Capacity);
                        String CommandString = "play \"" + shortPath.ToString() + "\"";
                        int Err;
                        if ((Err = mciSendString(CommandString, null, 0, IntPtr.Zero)) != 0)
                        {
                            StringBuilder errorDesc = new StringBuilder(1024);
                            mciGetErrorString(Err, errorDesc, 1024);
                            MsgBox.Show("Failed to play sound file: " + errorDesc.ToString());
                        }
                        else
                        {
                            mciBeingUsed = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                MsgBox.Show("Failed to play sound file.");
            }
        }

        private void cleanupSound()
        {
            if (mciBeingUsed)
            {
                StringBuilder shortPath = new StringBuilder(255);
                GetShortPathName(soundFile, shortPath, shortPath.Capacity);
                String CommandString = "close \"" + shortPath.ToString() + "\"";
                int Err;
                if ((Err = mciSendString(CommandString, null, 0, IntPtr.Zero)) != 0)
                {
                    //Can't figure out why error message always shows up, so just ignoring it.
                    //It doesn't seem to be causing any memory leaks.
                    //StringBuilder errorDesc = new StringBuilder(1024);
                    //mciGetErrorString(Err, errorDesc, 1024);
                    //MessageBox.Show("Failed to close sound file: " + errorDesc.ToString());
                }
                else
                {
                    mciBeingUsed = false;
                }
            }
        }
        #endregion

        private void snoozeButton_Click(object sender, EventArgs e)
        {            
            DateTime dt = DateTime.Now;
            double howMuch = 0;
            try
            {
                howMuch = double.Parse(snoozeField.Text);
            }
            catch (Exception)
            {
                MsgBox.Show("You must specify a valid number!");
                return;
            }
            string howMany = snoozeComboBox.Text;

            if (howMany.Equals("Minute(s)"))
            {
                dt = dt.AddMinutes(howMuch);
            }
            else if (howMany.Equals("Hour(s)"))
            {
                dt = dt.AddHours(howMuch);
            }
            else if (howMany.Equals("Day(s)"))
            {
                dt = dt.AddDays(howMuch);
                //Zero out the hours and minutes
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            }
            else if (howMany.Equals("Week(s)"))
            {
                dt = dt.AddDays(howMuch*7);
                //Zero out the hours and minutes
                dt = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            }
            else
            {
                MsgBox.Show("You must specify a snooze period!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            alert.snoozeUntil(dt);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void acknowledgeButton_Click(object sender, EventArgs e)
        {            
            if (!snoozeField.Text.Equals("") && !snoozeComboBox.Text.Equals(""))
            {
                if (MsgBox.Show("Are you sure you meant acknowledge instead of snooze?",
                    "Verify Choice", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No )
                {
                    return;
                }
            }
            
            alert.acknowledge();

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void disableButton_Click(object sender, EventArgs e)
        {            
            alert.setValid(false);
            alert.setSnoozed(false);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void fiveMinSnooze_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddMinutes(5);
            alert.snoozeUntil(dt);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tenMinSnooze_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddMinutes(10);
            alert.snoozeUntil(dt);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void oneHourSnooze_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddMinutes(60);
            alert.snoozeUntil(dt);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void oneDaySnooze_Click(object sender, EventArgs e)
        {            
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(1);
            alert.snoozeUntil(dt);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {            
            alert.setRemoved(true);

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void alertLabel_Click(object sender, EventArgs e)
        {
            MsgBox.Show(alert.previewNextTenAlerts(), "Next Ten Alert Times",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}