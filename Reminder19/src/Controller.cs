//If you ever need to do patches, just use the concept of a version setting to figure out
//what changes need to be made.  The first version will be that the field is not present.

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Collections;
using System.IO;
using Reminder19.src.cntrmsgbox.Dialog;
using System.Windows.Forms;

namespace Reminder19.src
{
    public class Controller
    {
        //Provides the connection to SQLite.  One connection at any given time.
        private SQLiteConnection conn;

        private string createAlertsTable = "CREATE TABLE Alerts ( " +
                                            "AlertID integer not null primary key autoincrement, " +
                                            "Title text not null, " +
                                            "Background text not null, " +
                                            "Sound text not null, " +
                                            "Message text not null, " +
                                            "Year text not null, " +
                                            "DayOfMonth text not null, " +
                                            "Month text not null, " +
                                            "DayOfWeek text not null, " +
                                            "Hour text not null, " +
                                            "Minute text not null, " +
                                            "Command text not null, " +
                                            "Snoozed boolean not null, " +
                                            "Valid boolean not null, " +
                                            "WakeUpTime datetime not null" +
                                           ")";

        private string createSettingsTable = "CREATE TABLE Settings ( " +
                                               "SettingID integer not null primary key autoincrement, " +
                                               "Setting text not null, " +
                                               "Value text not null" +
                                             ")";

        public Controller() 
        {
            try
            {
                string name = System.IO.Directory.GetCurrentDirectory() + "/alerts";
                if (!name.EndsWith(".reminder19"))
                    name += ".reminder19";

                if (File.Exists(name))
                {
                    if (conn != null)
                        conn.Close();
                    conn = new SQLiteConnection("Data Source=" + name + ";Password=reminder19!!!");
                    conn.Open();
                }
                else
                {
                    createNewDb(name, true);
                }

                if (new FileInfo(name).IsReadOnly)
                {
                    MsgBox.Show(true, name+" must be writable!",
                        "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
            catch (Exception)
            {
                MsgBox.Show(true, "Failed to connect to database!",
                    "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }                
        }

        /**
         * Warning: Implicitly overwrites existing file!
         */
        private void createNewDb(String name, bool overwrite)
        {
            try
            {
                if (!name.EndsWith(".reminder19"))
                    name += ".reminder19";

                if (File.Exists(name) && overwrite)
                {
                    File.Delete(name);
                }
                else if (File.Exists(name) && !overwrite)
                {
                    throw new Exception("Database already exists.");
                }

                SQLiteConnection.CreateFile(name);
                conn = new SQLiteConnection("Data Source=" + name);
                conn.Open();
                conn.ChangePassword("reminder19!!!"); 

                //Create the tables
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = createAlertsTable;
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                initSettingsTable();
            }
            catch (Exception)
            {
                MsgBox.Show(true, "Failed to create database!  Please make sure the Reminder 19 directory can be written to!",
                    "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }        
        }

        public void close()
        {
            conn.Close();
        }

        #region settings
        public void initSettingsTable()
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = createSettingsTable;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('ManagerSettings','100,100,449,338,200,135,55')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('AlertSettings','100,100,448,379')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('WindowsPositioning','center')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('AlertSound','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('AlertBackground','"+System.Drawing.Color.FromName("Window").ToArgb()+"')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('AlertFont','10')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('WarnBeforeClose','true')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('ColumnToSort','1')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('ColumnOrder','" + System.Windows.Forms.SortOrder.Ascending + "')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('LicenseUser','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('LicenseCode','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('LastQuery','')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Settings (Setting, Value) VALUES ('AskedCreateShortcut','false')";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void updateManagerSettings( int xpos, int ypos, int width, int height, int width1, int width2, int width3 )
        {
            //Update Entry
            updateSettings("ManagerSettings", xpos + "," + ypos + "," + width + "," + height + "," + width1 + "," + width2 + "," + width3);
        }

        public void updateAlertSettings( int xpos, int ypos, int width, int height )
        {
            //Update Entry
            updateSettings("AlertSettings", xpos + "," + ypos + "," + width + "," + height);
        }

        public void updateSettings(string setting, string value)
        {
            //Update Entry
            SQLiteCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Settings SET Value = ? WHERE Setting = ?";
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters[0].Value = value;
            cmd.Parameters[1].Value = setting;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public Dictionary<string,int> getManagerSettings()
        {
            Dictionary<string, int> settings = new Dictionary<string, int>();
            string value = getSetting("ManagerSettings");
            String[] values = value.Split(new Char[] { ',' });
            settings["xpos"] = System.Convert.ToInt32(values[0]);
            settings["ypos"] = System.Convert.ToInt32(values[1]);
            settings["width"] = System.Convert.ToInt32(values[2]);
            settings["height"] = System.Convert.ToInt32(values[3]);
            settings["width1"] = System.Convert.ToInt32(values[4]);
            settings["width2"] = System.Convert.ToInt32(values[5]);
            settings["width3"] = System.Convert.ToInt32(values[6]);
            return settings;
        }

        public Dictionary<string,int> getAlertSettings()
        {
            Dictionary<string, int> settings = new Dictionary<string, int>();
            string value = getSetting("AlertSettings");
            String[] values = value.Split(new Char[] { ',' });
            settings["xpos"] = System.Convert.ToInt32(values[0]);
            settings["ypos"] = System.Convert.ToInt32(values[1]);
            settings["width"] = System.Convert.ToInt32(values[2]);
            settings["height"] = System.Convert.ToInt32(values[3]);
            return settings;
        }

        public string getSetting(string setting)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Value FROM Settings WHERE Setting = ?";
            cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters[0].Value = setting;
            string value = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            return value;
        }
        #endregion

        /** ArrayList contains Alert objects */
        public ArrayList getAlerts()
        {
            return getAlerts("SELECT AlertId, Title, Message, Year, DayOfMonth, Month, " +
                "DayOfWeek, Hour, Minute, Snoozed, Valid, WakeUpTime, Background, " +
                "Sound, Command FROM Alerts");
        }

        /** ArrayList contains Alert objects */
        public ArrayList getSortedAlerts( String query )
        {
            return getAlerts(query);
        }

        /** ArrayList contains Alert objects */
        private ArrayList getAlerts( String query ) 
        {
            ArrayList alerts = new ArrayList();

            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            SQLiteDataReader results = cmd.ExecuteReader();
            while (results.Read())
            {
                int alertId = results.GetInt32(0);
                string title = results.GetString(1);
                string message = results.GetString(2);
                string year = results.GetString(3);
                string dayOfMonth = results.GetString(4);
                string month = results.GetString(5);
                string dayOfWeek = results.GetString(6);
                string hour = results.GetString(7);
                string minute = results.GetString(8);
                bool snoozed = results.GetBoolean(9);
                bool valid = results.GetBoolean(10);
                DateTime wakeUpTime = results.GetDateTime(11);
                string background = results.GetString(12);
                string sound = results.GetString(13);
                string command = results.GetString(14);

                //Do not change the order of the following block,
                //otherwise snoozed and valid values may be set
                //incorrectly.
                Alert alert = new Alert();
                alert.setAlertId(alertId);
                alert.setTitle(title);
                alert.setMessage(message);
                alert.setSchedule(year, month, dayOfMonth, dayOfWeek, hour, minute, false);
                alert.setWakeUpTime(wakeUpTime);
                alert.setSnoozed(snoozed);
                alert.setValid(valid);
                alert.setBackground(background);
                alert.setSound(sound);
                alert.setCommand(command);

                alerts.Add(alert);
            }
            cmd.Dispose();

            return alerts;        
        }

        /** ArrayList contains Alert objects */
        public Alert getUpdatedAlert( Alert alert )
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT AlertId, Title, Message, Year, DayOfMonth, Month, " +
                "DayOfWeek, Hour, Minute, Snoozed, Valid, WakeUpTime Background, " +
                "Sound, Command FROM Alerts WHERE AlertID = ?";
            cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters[0].Value = alert.getAlertId();
            SQLiteDataReader results = cmd.ExecuteReader();
            if (results.Read())
            {
                int alertId = results.GetInt32(0);
                string title = results.GetString(1);
                string message = results.GetString(2);
                string year = results.GetString(3);
                string dayOfMonth = results.GetString(4);
                string month = results.GetString(5);
                string dayOfWeek = results.GetString(6);
                string hour = results.GetString(7);
                string minute = results.GetString(8);
                bool snoozed = results.GetBoolean(9);
                bool valid = results.GetBoolean(10);
                DateTime wakeUpTime = results.GetDateTime(11);
                string background = results.GetString(12);
                string sound = results.GetString(13);
                string command = results.GetString(14);

                //Do not change the order of the following block,
                //otherwise snoozed and valid values may be set
                //incorrectly.
                alert.setAlertId(alertId);
                alert.setTitle(title);
                alert.setMessage(message);
                alert.setSchedule(year, month, dayOfMonth, dayOfWeek, hour, minute, false);
                alert.setWakeUpTime(wakeUpTime);
                alert.setSnoozed(snoozed);
                alert.setValid(valid);
                alert.setBackground(background);
                alert.setSound(sound);
                alert.setCommand(command);
            }
            cmd.Dispose();

            return alert;
        }

        public Alert createNewAlert( Alert alert ) 
        {
            SQLiteCommand cmd;

            //Create a new Entry
            cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Alerts (Title, Message, Year, DayOfMonth, Month, DayOfWeek, Hour, " +
                "Minute, Snoozed, Valid, WakeUpTime, Background, Sound, Command) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters[0].Value = alert.getTitle();
            cmd.Parameters[1].Value = alert.getMessage();
            cmd.Parameters[2].Value = alert.getYear();
            cmd.Parameters[3].Value = alert.getDayOfMonth();
            cmd.Parameters[4].Value = alert.getMonth();
            cmd.Parameters[5].Value = alert.getDayOfWeek();
            cmd.Parameters[6].Value = alert.getHour();
            cmd.Parameters[7].Value = alert.getMinute();
            cmd.Parameters[8].Value = alert.getSnoozed();
            cmd.Parameters[9].Value = alert.getValid();
            cmd.Parameters[10].Value = alert.getWakeUpTime();
            cmd.Parameters[11].Value = alert.getBackground();
            cmd.Parameters[12].Value = alert.getSound();
            cmd.Parameters[13].Value = alert.getCommand();
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            //Get the newly created AlertId.  Rely on sqlite, hope for no multiple access of the table.
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT last_insert_rowid()";
            int newAlertId = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.Dispose();
            alert.setAlertId(newAlertId);

            return alert;
        }

        public void updateAlert( Alert alert ) 
        {
            if (alert.getAlertId() < 0)
            {
                throw new Exception("Invalid Alert Id");
            }

            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Alerts SET Title = ?, Message = ?, Year = ?, DayOfMonth = ?, Month = ?, " +
                "DayOfWeek = ?, Hour = ?, Minute = ?, Snoozed = ?, Valid = ?, WakeUpTime = ?, Background = ?, " +
                "Sound = ?, Command = ? WHERE AlertId = ?";
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter()); cmd.Parameters.Add(cmd.CreateParameter());
            cmd.Parameters[0].Value = alert.getTitle();
            cmd.Parameters[1].Value = alert.getMessage();
            cmd.Parameters[2].Value = alert.getYear();
            cmd.Parameters[3].Value = alert.getDayOfMonth();
            cmd.Parameters[4].Value = alert.getMonth();
            cmd.Parameters[5].Value = alert.getDayOfWeek();
            cmd.Parameters[6].Value = alert.getHour();
            cmd.Parameters[7].Value = alert.getMinute();
            cmd.Parameters[8].Value = alert.getSnoozed();
            cmd.Parameters[9].Value = alert.getValid();
            cmd.Parameters[10].Value = alert.getWakeUpTime();
            cmd.Parameters[11].Value = alert.getBackground();
            cmd.Parameters[12].Value = alert.getSound();
            cmd.Parameters[13].Value = alert.getCommand();
            cmd.Parameters[14].Value = alert.getAlertId();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void deleteAlert( Alert alert ) 
        {
            if (alert.getAlertId() < 0)
                throw new Exception("Invalid Alert Id");

            SQLiteCommand cmd;

            cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Alerts WHERE AlertID = " + alert.getAlertId();
            cmd.ExecuteNonQuery(); 
        }
    }
}
