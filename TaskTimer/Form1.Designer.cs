namespace TaskTimer
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.btnDock = new System.Windows.Forms.Button();
			this.btnBeginTask = new System.Windows.Forms.Button();
			this.txtTaskName = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnReport = new System.Windows.Forms.Button();
			this.btnHide = new System.Windows.Forms.Button();
			this.btnNewFilename = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.quickShowWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.txtQuickNewTask = new System.Windows.Forms.ToolStripTextBox();
			this.btnQuickNewTask = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.btnQuickNewFilename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.quickExit = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveTasksTimer = new System.Windows.Forms.Timer(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.flowLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.trayMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnDock
			// 
			this.btnDock.Location = new System.Drawing.Point(2, 4);
			this.btnDock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnDock.Name = "btnDock";
			this.btnDock.Size = new System.Drawing.Size(56, 28);
			this.btnDock.TabIndex = 1;
			this.btnDock.Text = "Dock";
			this.btnDock.UseVisualStyleBackColor = true;
			this.btnDock.Click += new System.EventHandler(this.btnDock_Click);
			// 
			// btnBeginTask
			// 
			this.btnBeginTask.Location = new System.Drawing.Point(62, 4);
			this.btnBeginTask.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnBeginTask.Name = "btnBeginTask";
			this.btnBeginTask.Size = new System.Drawing.Size(56, 28);
			this.btnBeginTask.TabIndex = 2;
			this.btnBeginTask.Text = "Begin";
			this.btnBeginTask.UseVisualStyleBackColor = true;
			this.btnBeginTask.Click += new System.EventHandler(this.btnBeginTask_Click);
			// 
			// txtTaskName
			// 
			this.txtTaskName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtTaskName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtTaskName.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtTaskName.Location = new System.Drawing.Point(4, 4);
			this.txtTaskName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtTaskName.Name = "txtTaskName";
			this.txtTaskName.Size = new System.Drawing.Size(114, 20);
			this.txtTaskName.TabIndex = 3;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnDock);
			this.flowLayoutPanel1.Controls.Add(this.btnBeginTask);
			this.flowLayoutPanel1.Controls.Add(this.btnReport);
			this.flowLayoutPanel1.Controls.Add(this.btnHide);
			this.flowLayoutPanel1.Controls.Add(this.btnNewFilename);
			this.flowLayoutPanel1.Controls.Add(this.btnExit);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 28);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(122, 107);
			this.flowLayoutPanel1.TabIndex = 4;
			// 
			// btnReport
			// 
			this.btnReport.Location = new System.Drawing.Point(2, 36);
			this.btnReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(56, 28);
			this.btnReport.TabIndex = 4;
			this.btnReport.Text = "Report";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// btnHide
			// 
			this.btnHide.Location = new System.Drawing.Point(62, 36);
			this.btnHide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnHide.Name = "btnHide";
			this.btnHide.Size = new System.Drawing.Size(56, 28);
			this.btnHide.TabIndex = 5;
			this.btnHide.Text = "Hide";
			this.btnHide.UseVisualStyleBackColor = true;
			this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
			// 
			// btnNewFilename
			// 
			this.btnNewFilename.Location = new System.Drawing.Point(2, 68);
			this.btnNewFilename.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnNewFilename.Name = "btnNewFilename";
			this.btnNewFilename.Size = new System.Drawing.Size(56, 28);
			this.btnNewFilename.TabIndex = 6;
			this.btnNewFilename.Text = "New File";
			this.btnNewFilename.UseVisualStyleBackColor = true;
			this.btnNewFilename.Click += new System.EventHandler(this.btnNewFilename_Click);
			// 
			// btnExit
			// 
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(62, 68);
			this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(56, 28);
			this.btnExit.TabIndex = 7;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.quickExit_Click);
			// 
			// panel1
			// 
			this.panel1.AutoSize = true;
			this.panel1.Controls.Add(this.txtTaskName);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Size = new System.Drawing.Size(122, 28);
			this.panel1.TabIndex = 5;
			// 
			// trayIcon
			// 
			this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.trayIcon.BalloonTipText = "TaskTimer";
			this.trayIcon.ContextMenuStrip = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "TaskTimer";
			this.trayIcon.Visible = true;
			this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
			this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
			// 
			// trayMenu
			// 
			this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickShowWindow,
            this.toolStripSeparator1,
            this.txtQuickNewTask,
            this.btnQuickNewTask,
            this.toolStripSeparator2,
            this.btnQuickNewFilename,
            this.toolStripSeparator3,
            this.quickExit});
			this.trayMenu.Name = "contextMenuStrip1";
			this.trayMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.trayMenu.ShowImageMargin = false;
			this.trayMenu.Size = new System.Drawing.Size(136, 135);
			this.trayMenu.Text = "TaskTimer";
			this.trayMenu.MouseLeave += new System.EventHandler(this.contextMenuStrip1_MouseLeave);
			// 
			// quickShowWindow
			// 
			this.quickShowWindow.Name = "quickShowWindow";
			this.quickShowWindow.Size = new System.Drawing.Size(135, 22);
			this.quickShowWindow.Text = "Show";
			this.quickShowWindow.Click += new System.EventHandler(this.quickShowWindow_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
			// 
			// txtQuickNewTask
			// 
			this.txtQuickNewTask.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.txtQuickNewTask.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.txtQuickNewTask.Name = "txtQuickNewTask";
			this.txtQuickNewTask.Size = new System.Drawing.Size(100, 23);
			this.txtQuickNewTask.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuickNewTask_KeyPress);
			// 
			// btnQuickNewTask
			// 
			this.btnQuickNewTask.Name = "btnQuickNewTask";
			this.btnQuickNewTask.Size = new System.Drawing.Size(135, 22);
			this.btnQuickNewTask.Text = "Start New Task";
			this.btnQuickNewTask.Click += new System.EventHandler(this.btnQuickNewTask_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(132, 6);
			// 
			// btnQuickNewFilename
			// 
			this.btnQuickNewFilename.Name = "btnQuickNewFilename";
			this.btnQuickNewFilename.Size = new System.Drawing.Size(135, 22);
			this.btnQuickNewFilename.Text = "New File";
			this.btnQuickNewFilename.Click += new System.EventHandler(this.btnNewFilename_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(132, 6);
			// 
			// quickExit
			// 
			this.quickExit.Name = "quickExit";
			this.quickExit.Size = new System.Drawing.Size(135, 22);
			this.quickExit.Text = "Exit";
			this.quickExit.Click += new System.EventHandler(this.quickExit_Click);
			// 
			// SaveTasksTimer
			// 
			this.SaveTasksTimer.Enabled = true;
			this.SaveTasksTimer.Interval = 30000;
			this.SaveTasksTimer.Tick += new System.EventHandler(this.SaveTasksTimer_Tick);
			// 
			// timer1
			// 
			this.timer1.Interval = 10000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// Form1
			// 
			this.AcceptButton = this.btnBeginTask;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnExit;
			this.ClientSize = new System.Drawing.Size(122, 135);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MinimumSize = new System.Drawing.Size(79, 169);
			this.Name = "Form1";
			this.Text = "Task Timer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.trayMenu.ResumeLayout(false);
			this.trayMenu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDock;
		private System.Windows.Forms.Button btnBeginTask;
		private System.Windows.Forms.TextBox txtTaskName;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenuStrip trayMenu;
		private System.Windows.Forms.ToolStripMenuItem quickShowWindow;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripTextBox txtQuickNewTask;
		private System.Windows.Forms.ToolStripMenuItem btnQuickNewTask;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem quickExit;
		private System.Windows.Forms.Button btnReport;
		private System.Windows.Forms.Timer SaveTasksTimer;
		private System.Windows.Forms.ToolStripMenuItem btnQuickNewFilename;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Button btnHide;
		private System.Windows.Forms.Button btnNewFilename;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Timer timer1;
	}
}

