using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TaskTimer
{
    public partial class TaskRenameDialog : Form
    {
        public TaskLineItem Task;
        public TaskRenameDialog(TaskLineItem task)
        {
            InitializeComponent();
            this.Task = task;
            txtOldName.Text = task.TaskName;
            txtNewName.Text = task.TaskName;
            this.Text = task.Begin.ToShortTimeString() + " - " + task.End.ToShortTimeString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Task.TaskName = txtNewName.Text;
            this.Close();
        }

        private void TaskRenameDialog_Load(object sender, EventArgs e)
        {
            txtNewName.Focus();
            txtNewName.Select();
        }

        private void TaskRenameDialog_Activated(object sender, EventArgs e)
        {
            txtNewName.Focus();
            txtNewName.Select();
        }
    }
}
