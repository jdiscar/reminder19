using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using Reminder19.src.messageboxes;
using Reminder19.src.cntrmsgbox.Dialog;
using IWshRuntimeLibrary;
using System.IO;

namespace Reminder19.src
{
    public partial class Reminder19 : Form
    {        
        private Controller controller;
        private Dictionary<string, Alert> htAlerts;
        private Dictionary<int, Thread> htAlertDialogs;
        private delegate void UpdateListBoxCallback(Alert alert);
        private AlertsListViewSorter lvwColumnSorter;

        //The use of this threadModalDialogs variable is very sloppy, but I don't feel like
        //spending the time to do it right for now.  It is used to verify that there are no
        //"Save Changes?" messageboxes waiting for responses on other threads.  A '-1' value
        //means that the appliction is closing and that other threads should not put up 
        //messageboxes.  There may be race conditions, but none that I can foresee.
        public static int threadModalDialogs = 0;

        public Reminder19()
        {
            InitializeComponent();
            controller = new Controller();
            htAlerts = new Dictionary<string, Alert>();
            htAlertDialogs = new Dictionary<int, Thread>();            

            //Take care of positioning before the app is shown
            string positionType = controller.getSetting("WindowsPositioning");
            if (positionType.Equals("center"))
            {
                this.CenterToScreen();
            } 
            else if( positionType.Equals("custom") ) 
            {
                Dictionary<string, int> settings = controller.getManagerSettings();
                this.Location = new Point(settings["xpos"], settings["ypos"]);
                this.Size = new Size(settings["width"],settings["height"]);
                alertsListView.Columns[0].Width = settings["width1"];
                alertsListView.Columns[1].Width = 0;
                alertsListView.Columns[2].Width = settings["width2"];
                alertsListView.Columns[3].Width = settings["width3"];
            }            

            //See if the current registration is valid
            if (Registration.checkRegistration(controller.getSetting("LicenseUser"),
                controller.getSetting("LicenseCode")))
            {
                Registration.markValid();
                registerReminder19ToolStripMenuItem.Text = "Update License";
            }

            //Try to create a program startup
            createStartupShortcut();

            //Used for testing registration
            //Registration.test();

            //Used for Testing alerts and getting the next update time
            //Leave commented unless you want to test
            //new Alert().tester();
            //if (true)
            //    Environment.Exit(0);
        }

        #region Generalized Private Methods
        //Kills all alert threads and removes them from htAlertDialogs
        private void removeAllAlertThreads()
        {
            foreach (Thread alertThread in htAlertDialogs.Values)
            {
                alertThread.Abort();
            }
            htAlertDialogs.Clear();
        }

        //Creates the alert thread and enters it into htAlertDialogs
        private void createAlertThread(Alert alert)
        {
            //Using an asynchronous delegate
            ThreadStart ts = delegate() { alertDialog(alert); };
            Thread alertThread = new Thread(ts);
            alertThread.Start();
            htAlertDialogs.Add(alert.getAlertId(), alertThread);
        }

        //Aborts and deletes the old thread
        private void removeAlertThread(Alert alert)
        {
            htAlertDialogs[alert.getAlertId()].Abort();
            htAlertDialogs.Remove(alert.getAlertId());
        }

        //Aborts and deletes the old thread, then recreates it with the updated data
        private void recreateAlertThread(Alert alert)
        {
            removeAlertThread(alert);
            createAlertThread(alert);
        }

        //Adds the alert list view item
        private void addAlertListViewItem(Alert alert)
        {
            ListViewItem lvi = new ListViewItem(new string[4] { "", "" + alert.getAlertId(), 
                alert.getTitle(), "" + alert.getWakeUpTime() });
            if (alert.getValid()) lvi.Checked = true;
            alertsListView.Items.Add(lvi);
        }

        //Populates the alert list with the parameter data
        private void populateAlertList(ArrayList alerts)
        {
            removeAllAlertThreads();
            alertsListView.Items.Clear();
            htAlerts.Clear();

            foreach (Alert alert in alerts)
            {
                htAlerts.Add("" + alert.getAlertId(), alert);
                addAlertListViewItem(alert);
                createAlertThread( alert );
            }

            sortAlertListView();
        }

        //Edits the alert that is selected
        private void editSelectedAlert()
        {
            if (alertsListView.SelectedItems.Count == 0) return;
            int selectedIndex = alertsListView.SelectedIndices[0];
            string selectedItem = alertsListView.SelectedItems[0].SubItems[1].Text;
            Alert alert = htAlerts[selectedItem];
            Setup setup = new Setup(alert, controller.getSetting("AlertBackground"));
            if (setup.ShowDialog() == DialogResult.OK)
            {
                alert = setup.getAlert();

                //Update alert
                controller.updateAlert(alert);

                //Recreate Alert Dialog using asynchronous delegate
                recreateAlertThread(alert);

                //Recreate Alert in htAlerts hashtable
                htAlerts.Remove(selectedItem);
                htAlerts.Add(selectedItem, alert);

                //Update List View Record
                ListViewItem lvi = alertsListView.Items[selectedIndex];
                if (alert.getValid()) lvi.Checked = true;
                else lvi.Checked = false;
                lvi.SubItems[1].Text = "" + alert.getAlertId();
                lvi.SubItems[2].Text = alert.getTitle();
                lvi.SubItems[3].Text = "" + alert.getWakeUpTime();
            }
            alertsListView.Items[selectedIndex].Selected = true;
            alertsListView.Select();

            sortAlertListView();
        }

        //Change the sorting options
        private void sortAlertListView()
        {
            //Perform the sort with these new sort options
            this.alertsListView.Sort();

            bySearchToolStripMenuItem.Checked = false;
            byNextAletToolStripMenuItem.Checked = false;
            byStatusToolStripMenuItem.Checked = false;
            byTitleToolStripMenuItem.Checked = false;
            byToolStripMenuItem.Checked = false;

            int column = lvwColumnSorter.SortColumn;
            switch (column)
            {
                case 0:
                    //Status
                    byStatusToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    //Creation Date
                    byToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    //Title
                    byTitleToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    //Next Alert
                    byNextAletToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    //Search
                    bySearchToolStripMenuItem.Checked = true;
                    break;
                default:
                    break;
            }
        }

        //by column
        private void sortAlertListView(int columntoSort)
        {
            if (lvwColumnSorter.SortColumn == columntoSort)
            {
                //Reverse the current sort direction for this column
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                //Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = columntoSort;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            sortAlertListView();
        }
        #endregion

        #region Form Related Callbacks
        private void Reminder19_Shown(object sender, EventArgs e)
        {
            //If user is not registered, show message
            if (!Registration.isValid())
            {
                new RegisterPopUp().ShowDialog();
            }

            lvwColumnSorter = new AlertsListViewSorter();
            this.alertsListView.ListViewItemSorter = lvwColumnSorter;
            lvwColumnSorter.SortColumn = System.Convert.ToInt32( controller.getSetting("ColumnToSort") );
            if (controller.getSetting("ColumnOrder").Equals("Descending"))
            {
                lvwColumnSorter.Order = SortOrder.Descending;
            }

            populateAlertList(controller.getAlerts());
            this.alertsListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.alertsListView_ItemChecked);

            //The following line is probably windows only.
            Microsoft.Win32.SystemEvents.TimeChanged += new EventHandler(SystemEvents_TimeChanged);
        }

        private void Reminder19_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool center = true;
            if (WindowState != FormWindowState.Normal)
                center = false;

            //Make sure the window is positioned on screen before saving the position
            //otherwise the sizes will be returned as negative
            Show();
            WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.TopMost = false;
            this.Focus();
            
            //Show the warning message if necessary
            if (controller.getSetting("WarnBeforeClose").Equals("true") && e.CloseReason != CloseReason.WindowsShutDown
                && e.CloseReason != CloseReason.TaskManagerClosing)
            {
                if (MsgBox.Show(center, "Are you sure you want to close Reminder 19?\n(Use the minimize button" +
                    " to send Reminder 19 to the system tray.)\n(Turn off this message in the options menu.)",
                    "Reminder 19 Closing Warning.", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (Reminder19.threadModalDialogs > 0)
            {
                MsgBox.Show(center, "All Alert Dialog \"Save Changes\" dialogs must be closed before this application can be closed.",
                    "Close Application Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            Reminder19.threadModalDialogs--;
        }

        private void Reminder19_FormClosed(object sender, FormClosedEventArgs e)
        {
            removeAllAlertThreads();

            string positionType = controller.getSetting("WindowsPositioning");
            if (positionType.Equals("custom"))
            {
                controller.updateManagerSettings(this.Location.X, this.Location.Y,
                    this.Size.Width, this.Size.Height, alertsListView.Columns[0].Width,
                    alertsListView.Columns[2].Width, alertsListView.Columns[3].Width);
            }
            controller.updateSettings("ColumnToSort", ""+lvwColumnSorter.SortColumn);
            controller.updateSettings("ColumnOrder", ""+lvwColumnSorter.Order);
            Application.Exit();
        }

        //Called when user changes the system time
        void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            //Restart all the alert threads
            foreach (int alertId in htAlertDialogs.Keys)
            {
                if (htAlerts.ContainsKey("" + alertId) &&
                    htAlertDialogs.ContainsKey(alertId))
                {
                    Alert alert = htAlerts["" + alertId];
                    recreateAlertThread(alert);
                }
            }

            sortAlertListView();
        }
        #endregion

        #region System Tray Related Callbacks
        private void Reminder19_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                this.Hide();
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.TopMost = false;
            this.Focus();
        }

        private void notifyIconRestoreItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void notifyIconCloseItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Startup Stuff
        //create startup shortcut if necessary
        private void createStartupShortcut()
        {
            try
            {
                if (controller.getSetting("AskedCreateShortcut").Equals("true"))
                    return;
            }
            catch (Exception)
            {
                //Don't care, it just means don't ask about startup.  This is for backwards compatibility.
                return;
            }
            try
            {
                DialogResult dialogResult = MsgBox.Show("Would you like Reminder 19 to be run on system start up?  Pressing Yes will create a shortcut to Reminder19 " +
                    "in the startup folder for the current user.  Pressing Cancel will cause this dialog to appear in the future.  You may learn how to " +
                    "manually create a shortcut at http://reminder.relic19.net.", "Create a Startup Shortcut?",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dialogResult.Equals(DialogResult.Cancel))
                    return;
                controller.updateSettings("AskedCreateShortcut", "true");
                if (dialogResult.Equals(DialogResult.No))
                    return;
                WshShellClass wshShell = new WshShellClass();
                IWshRuntimeLibrary.IWshShortcut myShortcut = null;
                String startupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                if (!Directory.Exists(startupFolder))
                    throw new Exception("Startup Directory Not Found");
                myShortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(startupFolder + @"\Reminder19Shortcut.lnk");
                myShortcut.WorkingDirectory = System.IO.Directory.GetCurrentDirectory();
                myShortcut.TargetPath = Application.ExecutablePath;
                myShortcut.Description = "Launch Reminder 19.";
                myShortcut.WindowStyle = 7; //minimized
                myShortcut.Save();
            }
            catch (Exception)
            {
                MsgBox.Show("Sorry, Reminder19 can not create a shortcut for you.  Please visit http://reminder.relic19.net " +
                    "to learn how to manually create a shortcut.", "Failed to Create Shortcut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region AlertsListView Callbacks
        private void alertsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                //Reverse the current sort direction for this column
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                //Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            sortAlertListView();
        }

        private void alertsListView_DoubleClick(object sender, EventArgs e)
        {
            //TODO: This is a temporary solution to undo the checkbox
            //being set on a double click.  This is not a good solution
            //because the itemcheck logic ends up going off twice, which
            //causes a database write twice and thread resetting twice.
            int selectedIndex = alertsListView.SelectedIndices[0];
            if (alertsListView.Items[selectedIndex].Checked)
                alertsListView.Items[selectedIndex].Checked = false;
            else
                alertsListView.Items[selectedIndex].Checked = true;

            //Allow the user to edit the selected alert.
            editSelectedAlert();
        }

        private void alertsListView_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                return;
            }

            long width1 = alertsListView.Columns[0].Width;
            long width2 = alertsListView.Columns[2].Width;
            long width3 = alertsListView.Columns[3].Width;

            long oldWidth = width1 + width2 + width3;
            long newWidth = alertsListView.Width - 30;

            //Resize width.  -30 to account for the possible scrollbar
            alertsListView.Columns[0].Width = (int)((width1 * newWidth) / oldWidth);
            alertsListView.Columns[1].Width = 0;
            alertsListView.Columns[2].Width = (int)((width2 * newWidth) / oldWidth);
            alertsListView.Columns[3].Width = (int)((width3 * newWidth) / oldWidth);
        }

        //This callback forces the zeroth column to always be hidden
        private void alertsListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.NewWidth > 0)
            {
                alertsListView.Columns[0].Width = e.NewWidth;
                alertsListView.Columns[1].Width = 0;
                e.Cancel = true;
            }
        }

        private void alertsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int index = e.Item.Index;
            string alertId = alertsListView.Items[index].SubItems[1].Text;                   
            Alert alert = htAlerts[alertId];
            bool state = e.Item.Checked;
            if (alert.getValid() != state)
            {
                alert.acknowledge();
                alert.setValid(state);
                controller.updateAlert(alert);

                //Recreate Alert in htAlerts hashtable
                htAlerts.Remove(alertId);
                htAlerts.Add(alertId, alert);

                //Recreate Alert Dialog using asynchronous delegate
                if (htAlertDialogs.ContainsKey(alert.getAlertId()))
                {
                    recreateAlertThread(alert);
                }

                //Update the reported wake up time
                e.Item.SubItems[3].Text = "" + alert.getWakeUpTime();
            }

            //Deselect old item, then select new one
            if (alertsListView.SelectedItems.Count != 0)
            {
                alertsListView.SelectedItems[0].Selected = false;
            }
            alertsListView.Items[index].Selected = true;
            alertsListView.Select();

            //Without the condition, there is an error when added
            //a new list item while the title column is supposed
            //to be sorted
            if (lvwColumnSorter.SortColumn == 0)
            {
                sortAlertListView();
            }
        }
        #endregion

        #region Button Callbacks
        private void createNewAlertBut_Click(object sender, EventArgs e)
        {
            if (!Registration.isValid())
            {
                if( alertsListView.Items.Count >= 19 )
                {
                    new MaxAlertPopUp().ShowDialog();
                    return;
                }
            }

            Setup setup = new Setup(controller.getSetting("AlertBackground"), 
                System.Convert.ToInt32(controller.getSetting("AlertFont")));
            if (setup.ShowDialog() == DialogResult.OK)
            {
                Alert alert = setup.getAlert();
                
                //Create Alert in the Database
                alert = controller.createNewAlert(alert);

                //Add Alert to the Application
                htAlerts.Add("" + alert.getAlertId(), alert);
                addAlertListViewItem(alert);
                createAlertThread(alert);
                sortAlertListView();
            }            
        }

        private void editAlertBut_Click(object sender, EventArgs e)
        {
            editSelectedAlert();
        }

        private void removeAlertBut_Click(object sender, EventArgs e)
        {
            if (alertsListView.SelectedItems.Count == 0) return;
            string selectedItem = alertsListView.SelectedItems[0].SubItems[1].Text;
            int selectedIndex = alertsListView.SelectedItems[0].Index;

            //Remove all references to the selected item
            Alert alert = htAlerts[selectedItem];
            controller.deleteAlert(alert);
            htAlerts.Remove(selectedItem);
            removeAlertThread(alert);
            alertsListView.Items.Remove(alertsListView.SelectedItems[0]);

            //Select the next appropriate item
            if (alertsListView.Items.Count > selectedIndex)
                alertsListView.Items[selectedIndex].Selected = true;
            else if( alertsListView.Items.Count > 0 )
                alertsListView.Items[selectedIndex-1].Selected = true;
            alertsListView.Select();
        }
        #endregion

        #region FileMenuItems Callbacks
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options(controller);
            options.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enableAllAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in alertsListView.Items)
            {
                lvi.Checked = true;
            }
        }

        private void disableAllAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in alertsListView.Items)
            {
                lvi.Checked = false;
            }
        }

        private void removeAllDisabledAlertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in alertsListView.Items)
            {
                if (!lvi.Checked)
                {
                    int selectedIndex = lvi.Index;
                    string selectedItem = lvi.SubItems[1].Text;
                    Alert alert = htAlerts[selectedItem];

                    controller.deleteAlert(alert);

                    //Remove Alert Dialog using asynchronous delegate
                    if (htAlertDialogs.ContainsKey(alert.getAlertId()))
                    {
                        htAlertDialogs[alert.getAlertId()].Abort();
                        htAlertDialogs.Remove(alert.getAlertId());
                    }

                    //Remove from htAlerts
                    if (htAlerts.ContainsKey(selectedItem))
                    {
                        htAlerts.Remove(selectedItem);
                    }

                    alertsListView.Items.Remove(lvi);
                }
            }
        }

        private void bySearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlertSearch alertSearch = new AlertSearch();
            if (alertSearch.ShowDialog() == DialogResult.OK)
            {
                //Sort by the rank column
                lvwColumnSorter.SortColumn = 4;
                populateAlertList(controller.getSortedAlerts(alertSearch.getQuery()));
            }
        }

        private void byToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlertListView(1);
        }

        private void byStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlertListView(0);
        }

        private void byTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlertListView(2);
        }

        private void byNextAletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sortAlertListView(3);
        }

        private void registerReminder19ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterDialog rd = new RegisterDialog(controller.getSetting("LicenseUser"),
                controller.getSetting("LicenseCode"));
            if (rd.ShowDialog() == DialogResult.OK)
            {
                controller.updateSettings("LicenseUser", rd.getUser());
                controller.updateSettings("LicenseCode", rd.getCode());
                Registration.markValid();
                registerReminder19ToolStripMenuItem.Text = "Update License";
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Please visit http://reminder.relic19.net for help.","Help");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Reminder 19 was created by Relic 19 Software in 2008.","About");
        }
        #endregion

        #region Alert Dialog Thread Related
        //Called from the worker threads, don't need to worry about thread's updating
        //because the thread has taken care of itself by the time this is called.
        //This is the thread telling the main app to update itself.
        public void UpdateManagerAlert(Alert alert)
        {            
            string alertId = ""+alert.getAlertId();

            //Reset Alert in hashtable
            try
            {
                htAlerts.Remove(alertId);
                if (!alert.getRemoved())
                {
                    htAlerts.Add(alertId, alert);
                }
            }
            catch (Exception)
            {
                //ignore
            }

            //Updated List View Record
            foreach( ListViewItem lvi in alertsListView.Items )
            {                
                if( lvi.SubItems[1].Text.Equals( alertId ) )
                {
                    if (!alert.getRemoved())
                    {                        
                        if (alert.getValid()) lvi.Checked = true;
                        else lvi.Checked = false;
                        lvi.SubItems[1].Text = "" + alert.getAlertId();
                        lvi.SubItems[2].Text = alert.getTitle();
                        lvi.SubItems[3].Text = "" + alert.getWakeUpTime();
                        sortAlertListView();
                    }
                    else
                    {
                        alertsListView.Items[lvi.Index].Remove();
                    }
                    return;
                }
            }
        }

        private String getSound(Alert alert)
        {
            string soundFile = alert.getSound();
            if (soundFile.ToUpper().Equals("USE DEFAULT"))
            {
                soundFile = controller.getSetting("AlertSound");
            }
            return soundFile;
        }

        private String getBackground( Alert alert )
        {
            string background = alert.getBackground();
            if (background.ToUpper().Equals("DEFAULT"))
            {
                return controller.getSetting("AlertBackground");
            }
            return background;            
        }

        public void alertDialog(Alert alert)
        {
            while (true)
            {
                //Make sure it's time to show the dialog
                DateTime wakeup = alert.getWakeUpTime();
                DateTime now = DateTime.Now;
                if( !alert.getValid() && !alert.getSnoozed() )
                {
                    if (alertsListView.IsHandleCreated == true)
                    {
                        alertsListView.Invoke(new UpdateListBoxCallback(UpdateManagerAlert), new object[] { alert });
                    }
                    return;
                }
                else if (wakeup > now)
                {
                    double msDifference = wakeup.Subtract(now).TotalMilliseconds;
                    while (msDifference >= int.MaxValue)
                    {
                        Thread.Sleep(int.MaxValue);
                        msDifference = wakeup.Subtract(now).TotalMilliseconds;
                    }
                    Thread.Sleep((int)msDifference);
                }

                //Take care of color, positioning, command execution, and sound                
                AlertDialog alertDialog = new AlertDialog(alert, getBackground(alert), getSound(alert));                
                string positionType = controller.getSetting("WindowsPositioning");
                if (positionType.Equals("center"))
                {
                    alertDialog.center();
                }
                else if (positionType.Equals("custom"))
                {
                    Dictionary<string, int> settings = controller.getAlertSettings();
                    alertDialog.Location = new Point(settings["xpos"], settings["ypos"]);
                    alertDialog.Size = new Size(settings["width"], settings["height"]);
                }

                //Show Dialog
                if (alertDialog.ShowDialog() == DialogResult.OK)
                {
                    positionType = controller.getSetting("WindowsPositioning");
                    if (positionType.Equals("custom"))
                    {
                        controller.updateAlertSettings(alertDialog.Location.X, alertDialog.Location.Y,
                            alertDialog.Size.Width, alertDialog.Size.Height);
                    } 
                    alert = alertDialog.getCurrentAlert();
                    if (alertsListView.IsHandleCreated == true)
                    {
                        alertsListView.Invoke(new UpdateListBoxCallback(UpdateManagerAlert), new object[] { alert });
                    }
                    if (alert.getRemoved())
                    {
                        controller.deleteAlert(alert);
                        return;
                    }
                    else if (!alert.getValid() && !alert.getSnoozed())
                    {
                        controller.updateAlert(alert);
                        return;
                    }
                    else
                    {
                        controller.updateAlert(alert);
                        continue;
                    }                    
                }
            }
        }
        #endregion
    }
}