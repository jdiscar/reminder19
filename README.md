This is a project I made and sold back in 2008. It's a simple alert application, made in Visual Studio 2005 using CSharp. I think it's pretty useful and still use it today. A cellphone is nice for important alerts, but Reminder19 is nice for things that can wait until you log onto your computer. I also think Reminder19 has a very nice and convenient interface.

Anyway, I decided to put up the code. It's really old, has some embarrassing design decision, and I don't think I've touched it since 2009... but it can still be useful. I occasionally reference the code whenever I need to do desktop gui stuff with .net. It might be a decent base for a more modern app, I don't know. There aren't too many comments and there's no test code, but it should be easy to figure things out. It's a fairly straightforward desktop app.

One of the aspects I'm still happy with is the cron extension I designed for this app. You can read about it below.

For more information on this app, please see: 


About

Reminder 19 is a reminder / alarm / alert / notification program.  Reminder 19 is used to alert, remind, and notify you of any important (or even unimportant) events.  Use it to tell you to take out the trash, wake you up in the morning, remind you of your mom's birthday, notify you when your bills need to be paid, and more!

Reminder 19 consists of an Alert Manager and Alerts.  The Alert Manager is used to configure the messages and schedules of Alerts, which can be one time or reoccuring.  Reminder 19 automatically figures out the alert times based on the schedule.  When the next alert time is reached, it shows a pop up window with a customizable message and optionally plays a sound clip.  Reminder 19 supports a huge number of alerts that are very easily managable and configurable.

Reminder 19 supports several ways of describing the schedule of an alarm.

	1. Set a one time alert at a specific time and date.
	2. Set a one time alert to go off in a specific number of minutes, hours, or days.
	3. Set a reoccuring alert to go off at a specific time on a specific day of every month.
	4. Set a reoccuring alert to go off at a specific time on a specific day of every week.
	5. Set an advanced alert similarly to creating a unix cron job.
	
When the specified time is reached, the alert comes up and won't go away until you acknowledge the alert or tell it to snooze.  Using the Alert Manager, you can easily see when the next alert time for an alert is scheduled.

So if you want an advanced, easy to use alarm clock like program, give Reminder 19 a try.


Features

- Alerts are easy to create and manage.
- Schedules are easily set and can be very flexible.
- Alerts can be disabled and enabled instead of being removed and re-created.
- Alert Windows pop up on top and stay there.  They won't let you ignore them!
- Alerts can be customized with background colors, sounds, and individual messages.
- Alerts can be easily snoozed with preset and customizable snooze times.
- Alerts can be easily found from your long lists using sorting or searchs.
- Reminder 19 can be set to start up on system boot.
- Reminder 19 can remember its the last position.
- Reminder 19 minimizes to tray, so is unobtrusive.
- Reminder 19 knows when you change the system date and time, so it is easily portable.
- Reminder 19 does not need to be installed and stores all its information in a single, easily transferable, file
- Reminder 19 is small and lightweight, so requires little memory and cpu time.


Extended Cron

Extended Cron
I wrote a Reminder/Notification program for Windows called Reminder 19 (you can check it out at http://reminder.relic19.net.)  I wanted to be able to cleanly define many different kinds of alert schedules and decided to use cron as my starting point.  I ended up extending the format because I wanted to have things like time delayed alerts and a year field.  This is the format I came up with... I think it's pretty expressive.  The only way I think it would be more expressive is if I added conditional alerts (for example, an alert should be shown on the 31st if the month has 31 days, otherwise it should be shown on the 28th).

Format 1: Advanced Schedule By Dates and Times
  +------------------ minute (0 - 59)
  |  +--------------- hour (0 - 23)
  |  |  +------------ day of month (0 - 31)
  |  |  |  +--------- month (1 - 12)
  |  |  |  |  +------ year (number between 1700 and 9999)
  |  |  |  |  |  +--- dayOfWeek (0 - 7, 10-17, 20-27, 30-37, 40-47)
  |  |  |  |  |  |  + optional command and parameters
  *  *  *  *  *  *  *

Each of the patterns from the first six fields may be either a single in range number, * (the asterisk character, which means the field matches all legal values), or a list of in range numbers separated by commas (such as 2,3). There may be no spaces following commas and fields may not end with a comma.  There is also an optional special seventh field.

The hour field (field 1) is specified using the 24-hour format.  0 is 12 AM, 4 is 4 AM, and 15 is 3 PM.

The month field (field 4) is defined as a number, where 1 is January and 11 is November.  Alerts will not be shown for non existent days, such as September 31.  Please note that an alert will not show on September 30 instead.

For the day of the week field (field 6), both 0 and 7 are considered Sunday.  The dayOfWeek field can be two digits, where the first digit represents its position in the month and the second digit is the day of the week.  For example, 31 would mean the third Monday of every month.  A position of 0 means every postion, so 03 means every Wednesday.  In deciding the next wake up time between, for example, the 1st and 31st, the soonest wake up date is chosen.

An alert is shown when the time/date specification fields all match the current time and date. There is one exception: if both "day of month" and "day of week" are restricted (not "*"), then either the "day of month" field (field 3) or the "day of week" field (field 6) must match the current day (even though the other of the two fields need not match the current day).

The command field (field 7) is completely optional.  If it is present, then an attempt will be made to execute the command.

Example 1:
 00 16 1,2,31 2,3 2008 1,45
 This alert specifies the following date and times:
  February 1, 2008 at 4:00 PM
  February 2, 2008 at 4:00 PM
  February 4, 2008 at 4:00 PM (A Monday)
  February 11, 2008 at 4:00 PM (A Monday)
  February 18, 2008 at 4:00 PM (A Monday)
  February 22, 2008 at 4:00 PM (The fourth Friday)
  February 25, 2008 at 4:00 PM (A Monday)
  March 1, 2008 at 4:00 PM
  March 2, 2008 at 4:00 PM
  March 3, 2008 at 4:00 PM (A Monday)
  March 10, 2008 at 4:00 PM (A Monday)
  March 17, 2008 at 4:00 PM (A Monday)
  March 24, 2008 at 4:00 PM (A Monday)
  March 28, 2008 at 4:00 PM (The fourth Friday)
  March 31, 2008 at 4:00 PM (The 31st and a Monday)

Example 2:
 00 4,16 * * * * "C:\Program Files\Internet Explorer\IEXPLORE.EXE" ? http://www.google.com
 Every day at 4 AM and 4 PM, an instance of Internet Explorer will be launched that navigates to http://www.google.com.
 
 
Format 2: Advanced Schedules by Time Delays
  +------------------ the start minute (0 - 59)
  |  +--------------- the start hour (0 - 23)
  |  |  +------------ the start day of month (0 - 31)
  |  |  |  +--------- the start month (1 - 12)
  |  |  |  |  +------ the start year (number between 1700 and 9999)
  |  |  |  |  |  +--- increment in minutes (+[number of minutes to delay])
  |  |  |  |  |  |  + optional command and parameters
  * * * * *  *  *

No field in this format may be * (an asterisk character.)  Field 7 (the command field) is optional, but all other fields must be defined.

Each of the first 5 fields must be a single number in the specified ranges.  They will be used to define the start date.

The hour field is specified using the 24-hour format.  0 is 12 AM, 4 is 4 AM, and 15 is 3 PM.

The months are defined as number, where 1 is January and 11 is November.

For "increment in minutes" field (field 6), a non-negative number must be specified, prefixed by a '+' character.  The number represents the number of minutes to wait between alerts.  For example, +59 means that the alert will be triggered every 59 minutes after the start date.

The command field (field 7) is completely optional.  If it is present, then an attempt will be made to execute the command.

Example 1:
 00 00 31 3 2008 +30
 This alert will be shown every 30 minutes after March 31, 2008 at 12:00 AM.  So the first alert will go off at March 31, 2008 at 12:30 AM, the next alert will go off at March 31, 2008 at 1:00 AM, and so on.
 
Example 2:
 00 00 31 3 2008 +60 "C:\Program Files\Internet Explorer\IEXPLORE.EXE" ? http://www.google.com
 This alert will be shown every 60 minutes (one hour) after March 31, 2008 at 12:00 AM.  So the first alert will go off at March 31, 2008 at 1:00 AM, the next alert will go off at March 31, 2008 at 2:00 AM, and so on.  Whenever an alert goes off, http://www.google.com will be launched in Internet Explorer.