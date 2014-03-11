using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Reminder19.src.cntrmsgbox.Dialog;
using System.Windows.Forms;

namespace Reminder19.src
{
    public class Alert
    {
        private int alertId;
        private string title;
        private string message;
        private string year;
        private string dayOfMonth;
        private string month;
        private string dayOfWeek;
        private string hour;
        private string minute;
        private bool snoozed;
        private bool valid;
        private DateTime wakeUpTime;
        private bool removed;
        private string sound;
        private string command;
        private string background;

        public Alert() 
        {
            alertId = -1;
            title = "";
            message = "";
            year = "*";
            dayOfMonth = "*";
            month = "*";
            dayOfWeek = "*";
            hour = "*";
            minute = "*";
            snoozed = false;
            valid = true;
            wakeUpTime = new DateTime();
            removed = false;
            sound = "Use Default";
            command = "";
            background = "Default";
        }

        public override string ToString()
        {
            String schedule = year + " " + month + " " + dayOfMonth + " " + dayOfWeek + " " + hour + " " + minute;
            return String.Format("ID={0}\nTitle={1}\nMessage={2}\nSchedule={3}\nSnoozed={4}\nValid=[{5}]\nWakeUpTime=[{6}]", alertId,
                title, message, schedule, snoozed, valid, wakeUpTime);
        }

        //This method is not meant to be used to set the snooze wake up time.  Use
        //snoozeUntil() instead.
        public void updateWakeUpTime()
        {
            wakeUpTime = new Scheduler().getNextDateTime(year, month, dayOfMonth, dayOfWeek, hour, minute);
            if (DateTime.Now > wakeUpTime)
                valid = false;
            else
                valid = true;

        }

        //Use this method to acknowledge an alert.
        public void acknowledge()
        {
            updateWakeUpTime();
            snoozed = false;
            return;
        }

        //Use this method to set a snooze wake up time.
        public void snoozeUntil( DateTime snoozeWakeUp )
        {
            wakeUpTime = snoozeWakeUp;
            snoozed = true;
        }

        #region Used for schedule validation
        private int[] toIntArray(string value)
        {
            if (value.Equals("*"))
                return new int[0];
            string[] values = value.Split(new Char[] { ',' });
            int[] intArray = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                intArray[i] = System.Convert.ToInt32(values[i]);
            }
            return intArray;
        }

        public bool validate(string field, int min, int max)
        {
            try
            {
                if (field.Equals("*")) return true;
                string[] test = field.Split(new Char[] { ',' });
                foreach (string str in test)
                {
                    int value = System.Convert.ToInt32(str);
                    if (value < min || value > max)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool isIncrementalAlert(string year, string month, string dayOfMonth, string time, string hour, string minute)
        {
            try
            {
                if (!time.StartsWith("+"))
                {
                    return false;
                }

                System.Convert.ToInt32(time.Substring(1));
                System.Convert.ToInt32(year);
                System.Convert.ToInt32(month);
                System.Convert.ToInt32(dayOfMonth);
                System.Convert.ToInt32(time);
                System.Convert.ToInt32(hour);
                System.Convert.ToInt32(minute);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool isValid(string year, string month, string dayOfMonth, string dayOfWeek, string hour, string minute)
        {
            //Hours between 0-23
            if (!validate(hour, 0, 23))
            {
                MsgBox.Show("Invalid Hour Field.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Minutes between 0-59
            if (!validate(minute, 0, 59))
            {
                MsgBox.Show("Invalid Minute Field.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Check if the alert is incremental or by schedule
            if(dayOfWeek.StartsWith("+"))
            {
                try
                {
                    //Make sure the increment minutes is a number
                    System.Convert.ToInt32(dayOfWeek.Substring(1));

                    //Make sure all the fields are single numbers.
                    //Their values are validated elsewhere
                    System.Convert.ToInt32(year);
                    System.Convert.ToInt32(month);
                    System.Convert.ToInt32(dayOfMonth);
                    System.Convert.ToInt32(hour);
                    System.Convert.ToInt32(minute);
                } 
                catch 
                {
                    MsgBox.Show("Invalid Incremental Schedule Specified.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            } else {
                //dayOfWeek between 0-6, Position between 0-4
                try
                {
                    int[] iaDayOfWeek = toIntArray(dayOfWeek);
                    foreach (int positionOfWeek in iaDayOfWeek)
                    {
                        int position = positionOfWeek / 10;
                        int dow = positionOfWeek % 10;
                        if (dow > 6)
                        {
                            MsgBox.Show("Invalid Day Of Week.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                        else if (position > 4)
                        {
                            MsgBox.Show("Invalid Position of Week.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                } 
                catch 
                {
                    MsgBox.Show("Invalid Day Of Week Field.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            //Years must be greater than 1900
            int[] iaYears;
            try
            {
                iaYears = toIntArray(year);
                foreach (int yearValue in iaYears)
                {
                    if (yearValue < 1900)
                    {
                        MsgBox.Show("Invalid Year Field.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch
            {
                MsgBox.Show("Invalid Year Field.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Months between 1-12
            if (!validate(month, 1, 12))
            {
                MsgBox.Show("Invalid Month Field.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //Days between 1-28, else:
            //  if month=2, day=29: Must be leap year
            //  month=2, day=30: Invalid Date
            //  day=31: month must contain be 1,3,5,7,8,10,12
            if (!validate(dayOfMonth, 1, 31))
            {
                MsgBox.Show("Invalid day of month.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                bool noLeapYearProblem = false;
                bool containsThirtyDays = false;
                bool containsThirtyOneDays = false;

                if (year.Equals("*"))
                {
                    noLeapYearProblem = true;
                }
                else
                {
                    foreach (int yearValue in iaYears)
                    {
                        if (DateTime.IsLeapYear(yearValue))
                        {
                            noLeapYearProblem = true;
                        }
                    }
                }

                if (month.Equals("*"))
                {
                    containsThirtyDays = true;
                    containsThirtyOneDays = true;
                }
                else
                {
                    int[] iaMonths = toIntArray(month);
                    foreach (int monthValue in iaMonths)
                    {
                        if (monthValue != 2)
                        {
                            containsThirtyDays = true;
                            noLeapYearProblem = true;
                        }
                        if (monthValue != 9 && monthValue != 4 && monthValue != 6 && monthValue != 12 && monthValue != 2)
                        {
                            containsThirtyOneDays = true;
                        }
                    }
                }

                int[] iaDayOfMonths = toIntArray(dayOfMonth);
                ArrayList values = new ArrayList(iaDayOfMonths);
                if (values.Contains(29) && !noLeapYearProblem)
                {
                    MsgBox.Show("Invalid leap year date.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (values.Contains(30) && !containsThirtyDays)
                {
                    MsgBox.Show("No scheduled month contains 30 days.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (values.Contains(31) && !containsThirtyOneDays)
                {
                    MsgBox.Show("No scheduled month contains 31 days.", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }
        #endregion

        /** Abnormal setters */
        public bool setSchedule(string year, string month, string dayOfMonth, string dayOfWeek, string hour, string minute)
        {
            if (!setSchedule(year, month, dayOfMonth, dayOfWeek, hour, minute, true))
                return false;
            command = "";
            return true;
        }

        public bool setSchedule(string year, string month, string dayOfMonth, string dayOfWeek, string hour, string minute, bool setNextUpdateTime) 
        {
            if (!isValid(year, month, dayOfMonth, dayOfWeek, hour, minute))
                return false;

            setYear(year);
            setMonth(month);
            setDayOfMonth(dayOfMonth);
            setDayOfWeek(dayOfWeek);
            setHour(hour);
            setMinute(minute);

            snoozed = false;
            valid = true;

            if( setNextUpdateTime )
                updateWakeUpTime();

            return true;
        }

        public bool setSchedule(string schedule) 
        {
            string[] fields = schedule.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (fields.Length < 6)
            {
                MsgBox.Show("Invalid number of fields!", "Schedule Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!setSchedule(fields[4], fields[3], fields[2], fields[5].Replace('7', '0'), fields[1], fields[0]))
                return false;

            //Set the command
            command = "";
            for( int i = 6; i < fields.Length; i++ )
            {
                command += fields[i] + " ";
            }

            return true;
        }

        public bool setSchedule(DateTime dateTime) 
        {
            if (!setSchedule("" + dateTime.Year, "" + dateTime.Month, "" + dateTime.Day, "*", "" + dateTime.Hour, "" + dateTime.Minute))
                return false;
            
            //This case is special where we actually care about the seconds
            wakeUpTime = wakeUpTime.AddSeconds(dateTime.Second);
            if (wakeUpTime < DateTime.Now)
            {
                valid = false;
            }

            return true;
        }

        public string previewNextTenAlerts()
        {
            string result = "";
            DateTime nextWakeUpTime;
            wakeUpTime = DateTime.Now;
            for (int i = 0; i < 10; i++)
            {
                nextWakeUpTime = new Scheduler().getNextDateTime(wakeUpTime, year, month,
                    dayOfMonth, dayOfWeek, hour, minute);
                if (wakeUpTime >= nextWakeUpTime)                
                    break;
                wakeUpTime = nextWakeUpTime;
                result += wakeUpTime.ToString() + "\n";
            }
            if (result.Equals(""))
                result = "There are no future alerts scheduled.";
            return result;
        }

        /** Abnormal getters */
        public string getSchedule() { return minute + " " + hour + " " + dayOfMonth + " " + month + " " + year + " " + dayOfWeek + " " + command; }

        /** Normal Setters */
        public void setAlertId(int alertId) { this.alertId = alertId;  }
        public void setTitle(string title) { this.title = title; }
        public void setMessage(string message) { this.message = message; }
        private void setYear(string year) { this.year = year; }
        private void setDayOfMonth(string dayOfMonth) { this.dayOfMonth = dayOfMonth; }
        private void setMonth(string month) { this.month = month; }
        private void setDayOfWeek(string dayOfWeek) { this.dayOfWeek = dayOfWeek; }
        private void setHour(string hour) { this.hour = hour; }
        private void setMinute(string minute) { this.minute = minute; }
        public void setSnoozed(bool snoozed) { this.snoozed = snoozed; }
        public void setValid(bool valid) { this.valid = valid; }
        public void setWakeUpTime(DateTime wakeUpTime) 
        {
            if (wakeUpTime < DateTime.Now) valid = false;
            this.wakeUpTime = wakeUpTime; 
        }
        public void setRemoved(bool removed) { this.removed = removed; }
        public void setSound(string sound) { this.sound = sound; }
        public void setCommand(string command) { this.command = command; }
        public void setBackground(string background) { this.background = background; }

        /** Normal Getters **/
        public int getAlertId() { return alertId;  }
        public string getTitle() { return title; }
        public string getMessage() { return message;  }
        public string getYear() { return year;  }
        public string getDayOfMonth() { return dayOfMonth;  }
        public string getMonth() { return month;  }
        public string getDayOfWeek() { return dayOfWeek; }
        public string getHour() { return hour; }
        public string getMinute() { return minute; }
        public bool getSnoozed() { return snoozed; }
        public bool getValid() { return valid; }
        public DateTime getWakeUpTime() { return wakeUpTime; }
        public bool getRemoved() { return removed; }
        public string getSound() { return sound; }
        public string getCommand() { return command; }
        public string getBackground() { return background; }

        public void tester()
        {           
            this.setSchedule("0 0 29,30,31 2,3 2009 11,21,1");
            wakeUpTime = new Scheduler().getNextDateTime(year, month, dayOfMonth, 
                dayOfWeek, hour, minute);

            while (true)
            {
                wakeUpTime = new Scheduler().getNextDateTime(wakeUpTime, year, month, 
                    dayOfMonth, dayOfWeek, hour, minute);
                if (DateTime.Now > wakeUpTime)
                    valid = false;
                else
                    valid = true;

                MsgBox.Show(this.ToString());
            }
        }
    }
}
