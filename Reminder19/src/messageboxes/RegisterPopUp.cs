using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Reminder19.src.messageboxes
{
    public partial class RegisterPopUp : Form
    {
        public RegisterPopUp()
        {
            InitializeComponent();

            string message = "  Thank you for evaluating Reminder 19, I hope that you find it useful.  The " +
            "only limitations of the evaluation version of Reminder 19 are a maximum limit of 19 alerts at any " +
            "given time and this annoying pop up whenever you start the application.  This evaluation version " +
            "will never time out and you're welcome to use it indefinitely.\n\n  However, if you would like to " +
            "thank me for developing Reminder 19, please purchase a license for it from http://reminder.relic19.net.  " +
            "A license costs only $5.00 dollars and entitles you to all future updates.  In addition to knowing " +
            "you supported the developer of this application, you will unlock the following features:\n\n        " +
            "1. Support for an unlimited amount of alerts.\n        2. No annoying pop-up on start-up.\n        " +
            "3. Ability to set the background color of individual alerts.\n        4. Ability to set the " +
            "sound of individual alerts.\n        5. Ability to use MP3 files for alert sounds.\n\n  If you've " +
            "purchased a license, please select the \"Register Reminder 19\" option under the \"Help\" file menu and " +
            "then enter the e-mail address you used to purchase the license and the license code you " +
            "received by e-mail.";

            richTextBoxExtended1.RichTextBox.Text = message;

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