using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Reminder19.src
{
    class Scheduler
    {
        private int[] years;
        private int[] months;
        private int[] dayOfMonths;
        private int[] dayOfWeeks;
        private int[] hours;
        private int[] minutes;

        private bool anyYear;
        private bool anyMonth;
        private bool anyDayOfMonth;
        private bool anyDayOfWeek;
        private bool anyHour;
        private bool anyMinute;

        public Scheduler() { }

        public DateTime getNextDateTime(string sYear, string sMonth, string sDayOfMonth, string sDayOfWeek, string sHour, string sMinute)
        {
            return getNextDateTime(DateTime.Now, sYear, sMonth, sDayOfMonth, sDayOfWeek, sHour, sMinute);
        }

        public DateTime getNextDateTime(DateTime now, string sYear, string sMonth, string sDayOfMonth, 
            string sDayOfWeek, string sHour, string sMinute)
        {
            if (sDayOfWeek.StartsWith("+"))
            {
                int increment = System.Convert.ToInt32(sDayOfWeek.Substring(1));
                int year = System.Convert.ToInt32(sYear);
                int month = System.Convert.ToInt32(sMonth);
                int dayOfMonth = System.Convert.ToInt32(sDayOfMonth);
                int hour = System.Convert.ToInt32(sHour);
                int minute = System.Convert.ToInt32(sMinute);

                DateTime wakeUpTime = new DateTime(year, month, dayOfMonth, hour, minute, 0);
                while (wakeUpTime <= now)
                {
                    wakeUpTime = wakeUpTime.AddMinutes(increment);
                }
                return wakeUpTime;
            }

            years = toIntArray(sYear);
            months = toIntArray(sMonth);
            dayOfMonths = toIntArray(sDayOfMonth);
            dayOfWeeks = toIntArray(sDayOfWeek);
            hours = toIntArray(sHour);
            minutes = toIntArray(sMinute);

            anyYear = trueIfAsterisk(sYear);
            anyMonth = trueIfAsterisk(sMonth);
            anyDayOfMonth = trueIfAsterisk(sDayOfMonth);
            anyDayOfWeek = trueIfAsterisk(sDayOfWeek);
            anyHour = trueIfAsterisk(sHour);
            anyMinute = trueIfAsterisk(sMinute);

            DateTime startTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);

            return getNextDateTime2(startTime);
        }

        #region Helper Methods
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
            Array.Sort(intArray);
            return intArray;
        }

        //Assume the list is sorted
        private int getNextValue(int[] values, int current)
        {
            foreach (int value in values)
            {
                if (value > current)
                    return value;
            }
            //Since nothing is greater, default to lowest value
            return values[0];
        }

        //Checks if the current parameter is present in the comma delimited list
        private bool notValid(int[] list, int current)
        {
            if (list.Length == 0)
                return false;
            ArrayList values = new ArrayList(list);
            return !values.Contains(current);
        }

        private bool trueIfAsterisk(string test)
        {
            if( test.Equals("*") )
                return true;
            return false;
        }

        private bool invalidDate(int year, int month, int day)
        {
            try
            {
                DateTime dt = new DateTime(year, month, day);
                return false;
            }
            catch
            {
                return true;
            }
        }
        #endregion

        #region Wrap Time
        private int wrapMinutes()
        {
            if (anyMinute)
                return 0;
            else
                return minutes[0];
        }

        private int wrapHours()
        {
            if (anyHour)
                return 0;
            else
                return hours[0];
        }

        private DateTime wrapDays(DateTime wakeUpTime)
        {
            wakeUpTime = new DateTime(wakeUpTime.Year, wakeUpTime.Month, 1, wakeUpTime.Hour, wakeUpTime.Minute, wakeUpTime.Second);
            if (!anyDayOfWeek && anyDayOfMonth)
            {
                int dow = dayOfWeeks[0];
                if (dow != (int)wakeUpTime.DayOfWeek)
                    wakeUpTime = incrementDayOfWeek(wakeUpTime);
            }
            else if (anyDayOfWeek && !anyDayOfMonth)
            {
                if( invalidDate( wakeUpTime.Year, wakeUpTime.Month, dayOfMonths[0] ) )
                    wakeUpTime = incrementMonth( wakeUpTime );
                else
                    wakeUpTime = new DateTime(wakeUpTime.Year, wakeUpTime.Month, dayOfMonths[0], wakeUpTime.Hour, wakeUpTime.Minute, wakeUpTime.Second);
            }
            else if( !anyDayOfWeek && !anyDayOfMonth )
            {
                int dow = dayOfWeeks[0];
                DateTime dayCandidate1 = wakeUpTime;
                if (dayOfWeeks[0] != (int)dayCandidate1.DayOfWeek)
                    dayCandidate1 = incrementDayOfWeek(dayCandidate1);
                DateTime dayCandidate2 = DateTime.Now;
                if (invalidDate(wakeUpTime.Year, wakeUpTime.Month, dayOfMonths[0]))
                    dayCandidate2 = incrementMonth(wakeUpTime);
                else
                    dayCandidate2 = new DateTime(wakeUpTime.Year, wakeUpTime.Month, dayOfMonths[0], wakeUpTime.Hour, wakeUpTime.Minute, wakeUpTime.Second);
                if (dayCandidate1 <= dayCandidate2)
                    wakeUpTime = dayCandidate1;
                else
                    wakeUpTime = dayCandidate2;
            }
            while (notValid(months, wakeUpTime.Month))
            {
                wakeUpTime = incrementMonth(wakeUpTime);
            }
            return wakeUpTime;
        }

        private int wrapMonths()
        {
            if (anyMonth)
                return 1;
            else
                return months[0];
        }
        #endregion

        #region Increment Time
        private DateTime incrementMinute(DateTime wakeUpTime)
        {
            if( anyMinute )
            {
                return wakeUpTime.AddMinutes(1);
            }
            
            int initMinute = wakeUpTime.Minute;
            int minute = getNextValue(minutes, initMinute);

            if (minute <= initMinute)
            {
                //Time Wrapped
                wakeUpTime = new DateTime(wakeUpTime.Year, wakeUpTime.Month, wakeUpTime.Day, wakeUpTime.Hour, minute, 0);
                return wakeUpTime.AddHours(1);
            }
            else
            {
                return new DateTime(wakeUpTime.Year, wakeUpTime.Month, wakeUpTime.Day, wakeUpTime.Hour, minute, 0);
            }
        }

        //Takes care of wrapping minutes
        private DateTime incrementHour(DateTime wakeUpTime)
        {
            int initHour = wakeUpTime.Hour;
            int hour = 0;

            if (anyHour)
            {
                hour = initHour + 1;
                if (hour > 23)
                    hour = 1;
            }
            else
            {
                hour = getNextValue(hours, initHour);
            }

            if (hour <= initHour)
            {
                //The time wrapped
                wakeUpTime = new DateTime(wakeUpTime.Year, wakeUpTime.Month, wakeUpTime.Day, hour, wrapMinutes(), 0);
                return wakeUpTime.AddDays(1);
            }
            else
            {
                return new DateTime(wakeUpTime.Year, wakeUpTime.Month, wakeUpTime.Day, hour, wrapMinutes(), 0);
            }
        }

        //Takes care of wrapping hours and minutes
        private DateTime incrementDay(DateTime wakeUpTime)
        {
            if (anyDayOfWeek && anyDayOfMonth)
            {
                return incrementDayOfMonth(wakeUpTime);
            }
            else if (!anyDayOfWeek && anyDayOfMonth)
            {
                return incrementDayOfWeek(wakeUpTime);
            }
            else if (anyDayOfWeek && !anyDayOfMonth)
            {
                return incrementDayOfMonth(wakeUpTime);
            }
            else
            {
                DateTime dayCandidate1 = incrementDayOfWeek(wakeUpTime);
                DateTime dayCandidate2 = incrementDayOfMonth(wakeUpTime);

                if (dayCandidate1 <= dayCandidate2)
                    return dayCandidate1;
                else
                    return dayCandidate2;
            }
        }

        private DateTime incrementDayOfMonth(DateTime wakeUpTime)
        {
            int initDay = wakeUpTime.Day;
            int day = 0;

            if (anyDayOfMonth)
            {
                day = wakeUpTime.AddDays(1).Day;
            }
            else
            {
                day = getNextValue(dayOfMonths, initDay);
            }

            if (invalidDate(wakeUpTime.Year, wakeUpTime.Month, day))
            {               
                return incrementMonth(wakeUpTime);
            }
            else if (day <= initDay)
            {
                //Time wrapped
                wakeUpTime = new DateTime(wakeUpTime.Year, wakeUpTime.Month, day, wrapHours(), wrapMinutes(), 0);
                return wakeUpTime.AddMonths(1);
            }
            else
            {
                return new DateTime(wakeUpTime.Year, wakeUpTime.Month, day, wrapHours(), wrapMinutes(), 0);
            }            
        }

        //The day of week is very complicated
        private DateTime incrementDayOfWeek(DateTime wakeUpTime)
        {
            int initDay = wakeUpTime.Day;

            if (anyDayOfWeek)
            {
                return wakeUpTime.AddDays(1);
            }

            DateTime dayCandidate1 = wakeUpTime;
            DateTime dayCandidate2 = wakeUpTime;
            bool invalidDayCandidate1 = false;

            int daysToSkip = 0;

            int currentDayOfWeek = (int)wakeUpTime.DayOfWeek;
            
            //Look for the date if using the next day of week (values 0-7)
            int newDayOfWeek = getNextValue(dayOfWeeks, (int)wakeUpTime.DayOfWeek);
            if (newDayOfWeek > 7 && (newDayOfWeek = dayOfWeeks[0]) > 7 )
            {
                invalidDayCandidate1 = true;
            }
            else
            {
                daysToSkip = newDayOfWeek - currentDayOfWeek;
                if (daysToSkip < 0)
                    daysToSkip = 7 + daysToSkip;
                dayCandidate1 = dayCandidate1.AddDays(daysToSkip);
            }

            //Look for the date if using the next positions of week (values 10-47)
            int currentPositionOfWeek = getPositionOfWeek(wakeUpTime);
            int newPositionOfWeek = getNextValue(dayOfWeeks, currentPositionOfWeek);
            if (newPositionOfWeek <= 7 && (newPositionOfWeek = getNextValue(dayOfWeeks, 7)) <= 7)
            {
                return new DateTime(dayCandidate1.Year, dayCandidate1.Month, dayCandidate1.Day, wrapHours(), wrapMinutes(), 0);
            }

            int positionalDistance = ((newPositionOfWeek / 10) - 1) - ((currentPositionOfWeek / 10) - 1);
            daysToSkip = (newPositionOfWeek % 10) - currentDayOfWeek;
            if (daysToSkip < 0)
            {
                daysToSkip = 7 + daysToSkip;
                //This also means that we moved to the next week,
                //so the positional distance is now changed
                positionalDistance--;
            }
            if (positionalDistance >= 0)
            {
                daysToSkip += (positionalDistance) * 7;
                dayCandidate2 = dayCandidate2.AddDays(daysToSkip);
            }
            else
            {
                int startMonth = dayCandidate2.Month;   
                dayCandidate2 = dayCandidate2.AddDays(daysToSkip);
                while (dayCandidate2.Month == startMonth)
                {
                    dayCandidate2 = dayCandidate2.AddDays(7);
                }
                daysToSkip = ((newPositionOfWeek / 10) - 1) * 7;
                dayCandidate2 = dayCandidate2.AddDays(daysToSkip);
            }

            //Because I'm using Add Days, I won't need to worry about time wrapping
            if (invalidDayCandidate1 || dayCandidate2 <= dayCandidate1)
            {
                return new DateTime(dayCandidate2.Year, dayCandidate2.Month, dayCandidate2.Day, wrapHours(), wrapMinutes(), 0);
            }
            else
            {
                return new DateTime(dayCandidate1.Year, dayCandidate1.Month, dayCandidate1.Day, wrapHours(), wrapMinutes(), 0);
            }
        }

        private DateTime incrementMonth(DateTime wakeUpTime)
        {
            int initMonth = wakeUpTime.Month;
            int month = 0;

            if (anyMonth)
            {
                month = wakeUpTime.AddMonths(1).Month;
            }
            else
            {
                month = getNextValue(months, initMonth);
            }

            if( invalidDate(wakeUpTime.Year, month, wakeUpTime.Day) )
            {
                wakeUpTime = new DateTime(wakeUpTime.Year, month, 1, wrapHours(), wrapMinutes(), 0);
                return incrementMonth(wakeUpTime.AddMonths(1).AddDays(-1));
            }
            else if (month <= initMonth)
            {
                wakeUpTime = new DateTime(wakeUpTime.Year+1, month, 1, wrapHours(), wrapMinutes(), 0);
                return wrapDays(wakeUpTime);
            }
            else
            {
                wakeUpTime = new DateTime(wakeUpTime.Year, month, 1, wrapHours(), wrapMinutes(), 0);
                return wrapDays(wakeUpTime);
            }
        }

        private DateTime incrementYear(DateTime wakeUpTime)
        {
            if (anyYear)
            {
                return wakeUpTime.AddYears(1);
            }

            int year = wakeUpTime.Year;
            year = getNextValue(years, year);

            if (invalidDate(year, wakeUpTime.Month, wakeUpTime.Day))
            {
                wakeUpTime = new DateTime(year, wakeUpTime.Month, 1, wrapHours(), wrapMinutes(), 0);
                return incrementMonth(wakeUpTime.AddMonths(1).AddDays(-1));
            }

            wakeUpTime = new DateTime(year, wrapMonths(), 1, wrapHours(), wrapMinutes(), 0);
            return wrapDays(wakeUpTime);
        }
        #endregion

        #region Special functions for day of week
        private int getPositionOfWeek(DateTime wakeUpTime)
        {
            int currentDayOfWeek = (int)wakeUpTime.DayOfWeek;
            int initMonth = wakeUpTime.Month;
            int currentPositionInMonth = 0;
            do
            {
                currentPositionInMonth++;
                wakeUpTime = wakeUpTime.AddDays(-7);
            }
            while (wakeUpTime.Month == initMonth);
            return 10 * currentPositionInMonth + currentDayOfWeek;
        }

        private bool invalidDayOfWeek(DateTime wakeUpTime)
        {
            return notValid(dayOfWeeks, (int)wakeUpTime.DayOfWeek) 
                && notValid(dayOfWeeks, getPositionOfWeek(wakeUpTime));
        }
        #endregion

        private DateTime getNextDateTime2( DateTime startTime )
        {
            DateTime now = startTime;
            DateTime wakeUpTime = now;
            
            wakeUpTime = incrementMinute(wakeUpTime);

            while (notValid(hours, wakeUpTime.Hour))
            {
                wakeUpTime = incrementHour(wakeUpTime);
            }

            while ( (!anyDayOfWeek && !anyDayOfMonth && invalidDayOfWeek(wakeUpTime) && notValid(dayOfMonths, wakeUpTime.Day))
                    || (!anyDayOfWeek && anyDayOfMonth && invalidDayOfWeek(wakeUpTime))
                    || (anyDayOfWeek && !anyDayOfMonth && notValid(dayOfMonths, wakeUpTime.Day)) )
            {
                wakeUpTime = incrementDay(wakeUpTime);
            }

            while (notValid(months, wakeUpTime.Month))
            {
                wakeUpTime = incrementMonth(wakeUpTime);
            }

            while (notValid(years, wakeUpTime.Year))
            {
                wakeUpTime = incrementYear(wakeUpTime);        
            }

            return wakeUpTime;
        }
    }
}
