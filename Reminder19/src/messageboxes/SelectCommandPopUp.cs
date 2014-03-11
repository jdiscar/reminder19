using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Reminder19.src.messageboxes
{
    public partial class SelectCommandPopUp : Form
    {
        public SelectCommandPopUp(string file, string arguments)
        {
            InitializeComponent();
            fileField.Text = file;
            argumentsField.Text = arguments;
        }

        public string getCommand()
        {
            return fileField.Text;
        }

        public string getArguments()
        {
            return argumentsField.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fileField.Text = "";
            argumentsField.Text = "";
            DialogResult = DialogResult.OK;
        }

        private void cancelButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Choose File To Execute";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            openFileDialog.CheckFileExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileField.Text = openFileDialog.FileName;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}