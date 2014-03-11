using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Reminder19.src.messageboxes;
using Reminder19.src.cntrmsgbox.Dialog;

namespace Reminder19.src
{
    public partial class AdvancedSchedule : Form
    {
        private Alert alert;

        public AdvancedSchedule( string advancedField )
        {
            InitializeComponent();

            alert = new Alert();
            alert.setSchedule(advancedField);
            alert.setWakeUpTime(new DateTime());
            
            setByDatesTimes();
            setByTimeDelay();
        }

        public string getAdvancedString()
        {
            return alert.getSchedule();
        }

        private void manualButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
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

        #region Set By Dates Times
        private void setByDatesTimes()
        {
            if (alert.getDayOfWeek().StartsWith("+"))
                return;

            datesTimesHoursField.Text = alert.getHour();
            datesTimesMinutesField.Text = alert.getMinute();
            datesTimesDomField.Text = alert.getDayOfMonth();
            datesTimesMonthsField.Text = alert.getMonth();
            datesTimesYearsField.Text = alert.getYear();
            datesTimesDowField.Text = alert.getDayOfWeek();

            if (alert.getCommand().Trim().Equals(""))
                return;
            string[] command = alert.getCommand().Split(new Char[] { '?' }, 2);
            datesTimesCommandField.Text = command[0];
            datesTimesArgumentsField.Text = command[1];
        }

        private string getDateTimesField(string field)
        {
            field = field.Trim();
            field = field.Replace(" ", "");
            if (field.Equals(""))
                return "*";
            return field;
        }

        private bool setDatesTimesSchedule()
        {
            try
            {
                string schedule = "";
                schedule += getDateTimesField(datesTimesMinutesField.Text) + " ";
                schedule += getDateTimesField(datesTimesHoursField.Text) + " ";
                schedule += getDateTimesField(datesTimesDomField.Text) + " ";
                schedule += getDateTimesField(datesTimesMonthsField.Text) + " ";
                schedule += getDateTimesField(datesTimesYearsField.Text) + " ";
                schedule += getDateTimesField(datesTimesDowField.Text) + " ";

                string command = "";
                if (!datesTimesCommandField.Text.Trim().Equals(""))
                {
                    command = datesTimesCommandField.Text;
                    if (!datesTimesArgumentsField.Text.Trim().Equals(""))
                    {
                        command += " ? " + datesTimesArgumentsField.Text;
                    }
                }
                schedule += command;

                return alert.setSchedule(schedule);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void setDatesTimesCommand()
        {
            SelectCommandPopUp selectCommandDialog = new SelectCommandPopUp(datesTimesCommandField.Text,
                datesTimesArgumentsField.Text );
            if (selectCommandDialog.ShowDialog() == DialogResult.OK)
            {
                datesTimesCommandField.Text = selectCommandDialog.getCommand();
                datesTimesArgumentsField.Text = selectCommandDialog.getArguments();
            }
        }

        private void datesTimesSetCommandButton_Click(object sender, EventArgs e)
        {
            setDatesTimesCommand();
        }

        private void datesTimesCommandField_Click(object sender, EventArgs e)
        {
            setDatesTimesCommand();
        }

        private void datesTimesArgumentsField_Click(object sender, EventArgs e)
        {
            setDatesTimesCommand();
        }

        private void datesTimesPreviewButton_Click(object sender, EventArgs e)
        {
            if (setDatesTimesSchedule())
            {
                MsgBox.Show(alert.previewNextTenAlerts(), "Next Ten Alert Times",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void datesTimesSaveButton_Click(object sender, EventArgs e)
        {
            if( setDatesTimesSchedule() )
                DialogResult = DialogResult.OK;
        }

        private void datesTimesCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void dateTimesMinutesButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Enter a comma delimited list of numbers between 0 and 59."+
                "  Use '*' to denote all minutes.", "Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void datesTimesHoursButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Enter a comma delimited list of numbers between 0 and 23." +
                "  Use '*' to denote all hours.", "Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void datesTimesDomButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Enter a comma delimited list of numbers between 1 and 31." +
                "  Use '*' to denote all days of the month.", "Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void DatesTimesDowButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Enter a comma delimited list of numbers between 0 and 7." +
                "  0 is Sunday, 1 is Monday, 2 is Tuesday, and so on.  Use '*' to denote all days of the week.", 
                "Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void datesTimesMonthsButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Enter a comma delimited list of numbers between 1 and 12." +
                "  1 is January, 2 is February, and so on.  Use '*' to denote all months.", 
                "Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void datesTimesYearsButton_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Enter a comma delimited list of numbers between 1900 and 9999." +
                "  Use '*' to denote all values.", "Help", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        #endregion

        #region Time Delay Tab
        private void setByTimeDelay()
        {
            timeDelayAmPmField.SelectedIndex = 1;
            if (!alert.getDayOfWeek().StartsWith("+"))
                return;

            DateTime startTime = new DateTime(
                System.Convert.ToInt32(alert.getYear()),
                System.Convert.ToInt32(alert.getMonth()), 
                System.Convert.ToInt32(alert.getDayOfMonth()));
            timeDelayStartDateField.Value = startTime;

            int hour = System.Convert.ToInt32(alert.getHour());
            int minute = System.Convert.ToInt32(alert.getMinute());
            if (hour < 12) timeDelayAmPmField.SelectedIndex = 0;
            if (hour > 12) hour = hour - 12;
            if (hour == 0) hour = 12;
            timeDelayTimeField.Text = hour + ":" + minute;
            if (minute < 10) timeDelayTimeField.Text = timeDelayTimeField.Text.Replace(":", ":0");

            int timeDelay = System.Convert.ToInt32( alert.getDayOfWeek().Substring(1) );
            timeDelayWeekField.Text = ""+timeDelay/10080;
            timeDelay = timeDelay % 10080;
            timeDelayDayField.Text = "" + timeDelay/1440;
            timeDelay = timeDelay % 1440;
            timeDelayHourField.Text = "" + timeDelay/60;
            timeDelay = timeDelay % 60;
            timeDelayMinutesField.Text = ""+timeDelay;

            if (alert.getCommand().Trim().Equals(""))
                return;
            string[] command = alert.getCommand().Split(new Char[] { '?' }, 2);
            timeDelayCommandField.Text = command[0];
            timeDelayArgumentsField.Text = command[1];
        }

        public bool setTimeDelaySchedule()
        {
            int timeDelay = 0;
            try
            {
                timeDelay += System.Convert.ToInt32(timeDelayWeekField.Text) * 10080;
                timeDelay += System.Convert.ToInt32(timeDelayDayField.Text) * 1440;
                timeDelay += System.Convert.ToInt32(timeDelayHourField.Text) * 60;
                timeDelay += System.Convert.ToInt32(timeDelayMinutesField.Text);
            }
            catch (Exception)
            {
                MsgBox.Show("All values must be numbers!", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (timeDelay < 1)
            {
                MsgBox.Show("You must wait at least 1 minute!", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string[] time = extractTime(timeDelayTimeField.Text, timeDelayAmPmField.Text);

            string command = "";
            if( !timeDelayCommandField.Text.Trim().Equals("") )
            {
                command = timeDelayCommandField.Text;
                if (!timeDelayArgumentsField.Text.Trim().Equals(""))
                {
                    command += " ? " + timeDelayArgumentsField.Text;
                }
            }
            
            DateTime startTime = timeDelayStartDateField.Value;

            string schedule = time[1] + " " + time[0] + " " + startTime.Day + " " + startTime.Month +
                " " + startTime.Year + " +" + timeDelay + " " + command;

            return alert.setSchedule(schedule);
        }

        private void setTimeDelayCommand()
        {
            SelectCommandPopUp selectCommandDialog = new SelectCommandPopUp(timeDelayCommandField.Text,
                timeDelayArgumentsField.Text);
            if (selectCommandDialog.ShowDialog() == DialogResult.OK)
            {
                timeDelayCommandField.Text = selectCommandDialog.getCommand();
                timeDelayArgumentsField.Text = selectCommandDialog.getArguments();
            }
        }

        private void timeDelaySetCommandButton_Click(object sender, EventArgs e)
        {
            setTimeDelayCommand();
        }

        private void timeDelayCommandField_Click(object sender, EventArgs e)
        {
            setTimeDelayCommand();
        }

        private void timeDelayArgumentsField_Click(object sender, EventArgs e)
        {
            setTimeDelayCommand();
        }

        private void timeDelayPreviewButton_Click(object sender, EventArgs e)
        {
            if (setTimeDelaySchedule())
            {
                MsgBox.Show(alert.previewNextTenAlerts(), "Next Ten Alert Times",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timeDelaySaveButton_Click(object sender, EventArgs e)
        {
            if( setTimeDelaySchedule() )
                DialogResult = DialogResult.OK;
        }

        private void timeDelayCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}