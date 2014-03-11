using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Reminder19.src.messageboxes
{
    public partial class MaxAlertPopUp : Form
    {
        public MaxAlertPopUp()
        {
            InitializeComponent();

            richTextBoxExtended1.RichTextBox.Text = "The unregistered version of Reminder 19 supports a maximum of 19 alerts in the alerts list.  "+
                "Please purchase a license for Reminder 19 at http://reminder.relic19.net or remove an alert from your alert list.  A license only "+
                "costs $5.00 US dollars.";

            richTextBoxExtended1.RichTextBox.Font = new Font(richTextBoxExtended1.RichTextBox.Font.FontFamily, 10);
            richTextBoxExtended1.RichTextBox.BorderStyle = BorderStyle.None;
            richTextBoxExtended1.RichTextBox.BackColor = Color.FromName("Control");
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}