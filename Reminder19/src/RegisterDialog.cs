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
    public partial class RegisterDialog : Form
    {
        public RegisterDialog(string email, string code)
        {
            InitializeComponent();
            emailField.Text = email;
            licenseField.Text = code;
        }

        public string getUser() { return emailField.Text; }

        public string getCode() { return licenseField.Text; }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string user = emailField.Text;
            string license = licenseField.Text;

            if (Registration.checkRegistration(user, license))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                MsgBox.Show("Invalid Email and/or License Code Entered.  Please try again","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}