using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Reminder19.src.cntrmsgbox.Dialog;

namespace Reminder19.src
{
    public partial class Setup : Form
    {
        private Alert currentAlert;
        private bool backgroundChanged;

        public Setup( Alert alert, string defaultColor )
        {
            InitializeComponent();
            init(alert,defaultColor);
        }

        public Setup(string defaultColor,int fontSize)
        {
            InitializeComponent();
            messageBox.RichTextBox.Font = new Font(messageBox.RichTextBox.Font.FontFamily, fontSize);
            init(new Alert(),defaultColor);
        }

        public void init( Alert alert, string defaultColor )
        {
            if (!Registration.isValid())
            {
                label7.Visible = false;
                soundField.Visible = false;
                browseButton.Visible = false;
                messageBox.ShowStamp = false;
            }

            backgroundChanged = false;

            currentAlert = alert;
            soundField.Text = alert.getSound();
            try
            {
                string background = alert.getBackground();
                if (background.ToUpper().Equals("DEFAULT"))
                {
                    background = defaultColor;
                }
                else
                {
                    backgroundChanged = true;
                }
                messageBox.RichTextBox.BackColor = Color.FromArgb(System.Convert.ToInt32(background));
            }
            catch
            {
                messageBox.RichTextBox.BackColor = Color.FromName("Window");
            }

            titleField.Text = alert.getTitle();
            messageBox.RichTextBox.Rtf = alert.getMessage();
            
            advancedField1.Text = alert.getSchedule();
            oneTimeField3.SelectedIndex = 0;
            timeDelayField2.SelectedIndex = 0;
            dayOfMonthField1.SelectedIndex = 0;
            dayOfMonthField3.SelectedIndex = 0;
            dayOfMonthField4.SelectedIndex = 0;
            dayOfWeekField1.SelectedIndex = 0;
            dayOfWeekField3.SelectedIndex = 0;
            positionalDayOfWeekField1.SelectedIndex = 0;
            positionalDayOfWeekField2.SelectedIndex = 0;
            positionalDayOfWeekField4.SelectedIndex = 0;
            positionalDayOfWeekField6.SelectedIndex = 0;
            this.setRadioButton();

            if (alert.getSnoozed())
            {
                MsgBox.Show("If you save, this alert will no longer be on snooze.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region This section sets the initial checkboxes
        private int getNumber(string field)
        {
            try
            {
                return System.Convert.ToInt32(field);
            }
            catch
            {
                return -1;
            }
        }

        private void setRadioButton()
        {
            if( currentAlert.getAlertId() == -1 )
            {
                oneTimeRadio.Checked = true;
                return;
            }
            else if (!currentAlert.getCommand().Equals(""))
            {
                advancedRadio.Checked = true;
                return;
            }

            //Get the single value if possible.  If the operation fails, -1 will be
            //returned.  -1 is assumed to be the "*" character.
            int year = getNumber(currentAlert.getYear());
            int month = getNumber(currentAlert.getMonth());
            int dow = getNumber(currentAlert.getDayOfWeek());
            int dom = getNumber(currentAlert.getDayOfMonth());
            int hour = getNumber(currentAlert.getHour());
            int minute = getNumber(currentAlert.getMinute());

            //Order of setting the time is very important
            //Assume all am/pm fields are set to am
            if (year > -1 && month > -1 && dow < 0 && dom > -1 && hour > -1 && minute > -1)
            {
                oneTimeRadio.Checked = true;
                oneTimeField1.Value = new DateTime( year, month, dom, hour, minute, 0 );
                if (hour > 11) oneTimeField3.SelectedIndex = 1;
                if (hour > 12) hour = hour - 12;
                if (hour == 0) hour = 12;
                oneTimeField2.Text = hour + ":" + minute;
                if (minute < 10) oneTimeField2.Text = oneTimeField2.Text.Replace(":", ":0");                
            }
            else if (year < 0 && month > -2 && dow < 0 && dom > -1 && hour > -1 && minute > -1)
            {
                dayOfMonthRadio.Checked = true;
                dayOfMonthField1.SelectedIndex = dom-1;
                if (hour > 11) dayOfMonthField3.SelectedIndex = 1;
                if (hour > 12) hour = hour - 12;
                if (hour == 0) hour = 12;
                dayOfMonthField2.Text = hour + ":" + minute;                
                if (minute < 10) dayOfMonthField2.Text = dayOfMonthField2.Text.Replace(":", ":0");
                if (month < 1) dayOfMonthField4.SelectedIndex = 0;
                else dayOfMonthField4.SelectedIndex = month;
            }
            else if (year < 0 && month < 0 && dow > -1 && dow < 10 && dom < 0 && hour > -1 && minute > -1)
            {
                dayOfWeekRadio.Checked = true;
                dayOfWeekField1.SelectedIndex = dow;
                if (dow == 0) dayOfWeekField1.SelectedIndex = 7;
                if (hour > 11) dayOfWeekField3.SelectedIndex = 1;
                if (hour > 12) hour = hour - 12;
                if (hour == 0) hour = 12;
                dayOfWeekField2.Text = hour + ":" + minute;
                if (minute < 10) dayOfWeekField2.Text = dayOfWeekField2.Text.Replace(":", ":0");                
            }
            else if (year < 0 && month > -2 && dow >= 10 && dom < 0 && hour > -1 && minute > -1)
            {
                int position = dow / 10;
                dow = dow % 10;
                positionalDayOfWeekRadio.Checked = true;
                positionalDayOfWeekField1.SelectedIndex = position-1;
                positionalDayOfWeekField2.SelectedIndex = dow-1;
                if (dow == 0) positionalDayOfWeekField2.SelectedIndex = 6;
                if (hour > 11) positionalDayOfWeekField4.SelectedIndex = 1;
                if (hour > 12) hour = hour - 12;
                if (hour == 0) hour = 12;
                positionalDayOfWeekField3.Text = hour + ":" + minute;
                if (minute < 10) positionalDayOfWeekField3.Text = positionalDayOfWeekField3.Text.Replace(":", ":0");
                if (month < 1) positionalDayOfWeekField6.SelectedIndex = 0;
                else positionalDayOfWeekField6.SelectedIndex = month;
            }
            else if (year < 0 && month < 0 && dow < 0 && dom < 0 && hour > -1 && minute > -1)
            {
                dayOfWeekRadio.Checked = true;
                dayOfWeekField1.SelectedIndex = 0;
                if (hour > 11) dayOfWeekField3.SelectedIndex = 1;
                if (hour > 12) hour = hour - 12;
                if (hour == 0) hour = 12;
                dayOfWeekField2.Text = hour + ":" + minute;
                if (minute < 10) dayOfWeekField2.Text = dayOfWeekField2.Text.Replace(":", ":0");                
            }
            else
            {
                advancedRadio.Checked = true;
            }
        }
        #endregion

        public Alert getAlert()
        {
            return currentAlert;
        }

        private bool save()
        {
            bool returnValue = false;
            Alert alert = new Alert();
            int alertId = currentAlert.getAlertId();
            alert.setAlertId(alertId);
            alert.setTitle(titleField.Text);
            alert.setMessage(messageBox.RichTextBox.Rtf);
            alert.setValid(currentAlert.getValid());
            alert.setCommand(currentAlert.getCommand());
            if (!soundField.Text.Equals(alert.getSound()))
            {
                alert.setSound(soundField.Text);
            }
            if (backgroundChanged)
            {
                alert.setBackground(""+messageBox.RichTextBox.BackColor.ToArgb());
            }

            //Determine the schedule
            if (oneTimeRadio.Checked)
            {
                DateTime dt = oneTimeField1.Value;
                string[] time = extractTime(oneTimeField2.Text, oneTimeField3.Text);
                dt = new DateTime(dt.Year, dt.Month, dt.Day, System.Convert.ToInt32(time[0]), 
                    System.Convert.ToInt32(time[1]), 0);
                returnValue = alert.setSchedule(dt);
            }
            else if (timeDelayRadio.Checked)
            {
                DateTime dt = DateTime.Now;
                double howMuch = 0;
                try
                {
                    howMuch = double.Parse(timeDelayField1.Text);
                }
                catch (Exception)
                {
                    throw new Exception("Time Delay Field must be a number!");
                }
                string howMany = timeDelayField2.Text;

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
                returnValue = alert.setSchedule(dt);
            }
            else if (dayOfMonthRadio.Checked)
            {
                string dayOfMonth = "" + (dayOfMonthField1.SelectedIndex + 1);
                string[] time = extractTime(dayOfMonthField2.Text, dayOfMonthField3.Text);
                string month = "" + (dayOfMonthField4.SelectedIndex);
                if (month.Equals("0")) month = "*";
                if (System.Convert.ToInt32(dayOfMonth) > 28)
                    MsgBox.Show("Warning!  No alert will be shown for months without the specified day!", 
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                returnValue = alert.setSchedule("*", month, dayOfMonth, "*", time[0], time[1]);
            }
            else if (dayOfWeekRadio.Checked)
            {
                string dayOfWeek = "" + dayOfWeekField1.SelectedIndex;
                if (dayOfWeek.Equals("0"))
                    dayOfWeek = "*";
                else if (dayOfWeek.Equals("7"))
                    dayOfWeek = "0";
                string[] time = extractTime(dayOfWeekField2.Text, dayOfWeekField3.Text);
                returnValue = alert.setSchedule("*", "*", "*", dayOfWeek, time[0], time[1]);
            }
            else if (advancedRadio.Checked)
            {
                returnValue = alert.setSchedule(advancedField1.Text);
            }
            else if (positionalDayOfWeekRadio.Checked)
            {
                int position = positionalDayOfWeekField1.SelectedIndex + 1;
                int dayOfWeek = ((positionalDayOfWeekField2.SelectedIndex+1) % 7);
                string month = "" + (positionalDayOfWeekField6.SelectedIndex);
                if (month.Equals("0")) month = "*";
                string positionalDayOfWeek = ""+((position * 10) + dayOfWeek);
                string[] time = extractTime(positionalDayOfWeekField3.Text, positionalDayOfWeekField4.Text);
                returnValue = alert.setSchedule("*", month, "*", positionalDayOfWeek, time[0], time[1]);
            }
            else
            {
                throw new Exception("Nothing Selected");
            }

            if (!returnValue)
                return false;

            alert.setValid(true);

            currentAlert = alert;

            return true;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                if (save())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message.Replace("Input string", "Advanced Field"), "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] extractTime(string clock, string ampm)
        {
            string[] time = clock.Split(':');

            try
            {
                if (time.Length > 2)
                {
                    throw new Exception("Time must be in the format HH:MM");
                }
                else if (System.Convert.ToInt32(time[0]) > 12)
                {
                    throw new Exception("Hours must be between 1 and 12.");
                }
                else if (System.Convert.ToInt32(time[1]) > 60)
                {
                    throw new Exception("Minutes must be between 0 and 59.");
                }
            }
            catch (Exception)
            {
                throw new Exception("Time must be in the format HH:MM");
            }

            if (ampm.Equals("PM"))
                time[0] = "" + ((System.Convert.ToInt32(time[0]) % 12) + 12);
            else
                time[0] = "" + ((System.Convert.ToInt32(time[0]) % 12));

            return time;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #region Toggle Radio Buttons
        private void oneTimeField1_Enter(object sender, EventArgs e)
        {
            oneTimeRadio.Checked = true;
        }

        private void oneTimeField2_Enter(object sender, EventArgs e)
        {
            oneTimeRadio.Checked = true;
        }

        private void oneTimeField3_Enter(object sender, EventArgs e)
        {
            oneTimeRadio.Checked = true;
        }

        private void timeDelayField1_Enter(object sender, EventArgs e)
        {
            timeDelayRadio.Checked = true;
        }

        private void timeDelayField2_Enter(object sender, EventArgs e)
        {
            timeDelayRadio.Checked = true;
        }

        private void dayOfMonthField1_Enter(object sender, EventArgs e)
        {
            dayOfMonthRadio.Checked = true;
        }

        private void dayOfMonthField2_Enter(object sender, EventArgs e)
        {
            dayOfMonthRadio.Checked = true;
        }

        private void dayOfMonthField3_Enter(object sender, EventArgs e)
        {
            dayOfMonthRadio.Checked = true;
        }

        private void dayOfMonthField4_Enter(object sender, EventArgs e)
        {
            dayOfMonthRadio.Checked = true;
        }

        private void dayOfWeekField1_Enter(object sender, EventArgs e)
        {
            dayOfWeekRadio.Checked = true;
        }

        private void dayOfWeekField2_Enter(object sender, EventArgs e)
        {
            dayOfWeekRadio.Checked = true;
        }

        private void dayOfWeekField3_Enter(object sender, EventArgs e)
        {
            dayOfWeekRadio.Checked = true;
        }

        private void advancedField1_Enter(object sender, EventArgs e)
        {
            advancedRadio.Checked = true;
        }

        private void positionalDayOfWeekField1_Enter(object sender, EventArgs e)
        {
            positionalDayOfWeekRadio.Checked = true;
        }

        private void positionalDayOfWeekField2_Enter(object sender, EventArgs e)
        {
            positionalDayOfWeekRadio.Checked = true;
        }

        private void positionalDayOfWeekField3_Enter(object sender, EventArgs e)
        {
            positionalDayOfWeekRadio.Checked = true;
        }

        private void positionalDayOfWeekField4_Enter(object sender, EventArgs e)
        {
            positionalDayOfWeekRadio.Checked = true;
        }

        private void positionalDayOfWeekField6_Enter(object sender, EventArgs e)
        {
            positionalDayOfWeekRadio.Checked = true;
        }
        #endregion

        private void helpButton_Click(object sender, EventArgs e)
        {
            AdvancedSchedule advancedSchedule;
            try
            {
                advancedSchedule = new AdvancedSchedule(advancedField1.Text);
            }
            catch (Exception)
            {
                advancedSchedule = new AdvancedSchedule(currentAlert.getSchedule());
            }
            if (advancedSchedule.ShowDialog() == DialogResult.OK)
            {
                advancedField1.Text = advancedSchedule.getAdvancedString();
                advancedRadio.Checked = true;
            }
        }

        //I'm hijacking this display to change the background color
        private void messageBox_Stamp(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = messageBox.RichTextBox.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    messageBox.RichTextBox.BackColor = colorDialog.Color;
                    backgroundChanged = true;
                }
                catch
                {
                    MsgBox.Show("Invalid Color Selected");
                }
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose Alarm Sound";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFileDialog.Filter = "Wav (*.wav)|*.wav|Mp3 (*.mp3)|*.mp3";
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                soundField.Text = openFileDialog.FileName;
            }
            if (!soundField.Text.Equals("") && !soundField.Text.ToUpper().EndsWith(".WAV"))
            {
                MsgBox.Show("Warning: Wave sound format is preferred.");
            }
        }

        #region Callbacks to nicely match TitleField/MessageBox Contents.
        private void titleField_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && messageBox.RichTextBox.Text.Equals(""))
            {
                messageBox.RichTextBox.Text = titleField.Text;
            }
        }

        private void titleField_Leave(object sender, EventArgs e)
        {
            if (messageBox.RichTextBox.Text.Equals(""))
            {
                messageBox.RichTextBox.Text = titleField.Text;
            }
        }

        private void messageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && titleField.Text.Equals(""))
            {
                titleField.Text = messageBox.RichTextBox.Text;
            }
        }

        private void messageBox_Leave(object sender, EventArgs e)
        {
            if (titleField.Text.Equals(""))
            {
                titleField.Text = messageBox.RichTextBox.Text;
            }
        }

        //Automatically select message box text if it's the same as the title field
        private void messageBox_Enter(object sender, EventArgs e)
        {
            if (titleField.Text.Equals(messageBox.RichTextBox.Text))
            {
                messageBox.RichTextBox.SelectAll();
            }
        }

        //Automatically select title field text if it's the same as the message box
        private void titleField_Enter(object sender, EventArgs e)
        {
            if (titleField.Text.Equals(messageBox.RichTextBox.Text))
            {
                titleField.SelectAll();
            }            
        }


        #endregion
    }
}