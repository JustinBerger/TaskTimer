using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace TaskTimer
{
	public partial class Form1 : ShellLib.ApplicationDesktopToolbar, IDisposable
	{
		private enum FormState
		{
			Visible = 0,
			Hidden,
			Docked
		}

		public Form1()
		{
			string last = LastFileName();
			handler = new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
			SystemEvents.SessionSwitch += handler;
			items = new List<TaskLineItem>();
			fileLock = new object();
			fileName = LastFileName();

			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			LoadTasks(LastFileName());
		}

		#region -- Lock, Sleep, Hibernate --
		
		private SessionSwitchEventHandler handler;
		private bool handlerDead = false;

		void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
		{
			Console.WriteLine(e.Reason.ToString());
			this.txtTaskName.Text += e.Reason.ToString() + '\n';
			TaskLineItem PreviousTask;
			TaskRenameDialog rename;
			switch (e.Reason)
			{
				case SessionSwitchReason.SessionLock:
				case SessionSwitchReason.SessionLogoff:
				case SessionSwitchReason.RemoteDisconnect:
				case SessionSwitchReason.ConsoleDisconnect:
					System.Console.WriteLine(items.Count);
					BeginTask(AFKValue);
					System.Console.WriteLine(items.Count);
					System.Console.WriteLine("Lock");
					break;
				case SessionSwitchReason.RemoteConnect:
				case SessionSwitchReason.SessionRemoteControl:
				case SessionSwitchReason.ConsoleConnect:
					System.Console.WriteLine(items.Count);
					BeginTask(AFKValue);
					PreviousTask = current;
					BeginTask(Prior);
					System.Console.WriteLine(items.Count);
					System.Console.WriteLine("Remote-Unlock");
					rename = new TaskRenameDialog(PreviousTask);
					rename.Show(this);
					rename.Focus();
					rename.Select();
					break;

				case SessionSwitchReason.SessionUnlock:
				case SessionSwitchReason.SessionLogon:
					PreviousTask = current;
					System.Console.WriteLine(items.Count);
					BeginTask(Prior);
					System.Console.WriteLine(items.Count);
					System.Console.WriteLine("Unlock");
					rename = new TaskRenameDialog(PreviousTask);
					rename.Show(this);
					rename.Focus();
					rename.Select();
					break;
			}
		}

		#endregion
		
		#region -- Tasks --
		
		private const string AFKValue = "AFK";
		private string Prior = "NULL";
		private List<TaskLineItem> items;
		private TaskLineItem current;

		private void btnBeginTask_Click(object sender, EventArgs e)
		{
			this.BeginTask(txtTaskName.Text);
		}
		
		private void btnQuickNewTask_Click(object sender, EventArgs e)
		{
			this.BeginTask(txtQuickNewTask.Text);
			ToggleFormHidden(true);
		}

		void BeginTask(string taskName)
		{
			if (taskName != AFKValue)
			{
				txtTaskName.Text = taskName;
				txtQuickNewTask.Text = taskName;
			}

			if (current == null)
			{
				var t = new TaskLineItem(taskName);
				items.Add(t);
				current = t;
			}
			else if (current.TaskName != taskName)
			{
				if (current.TaskName != AFKValue)
				{
					Prior = current.TaskName;
				}
				var end = current.EndTask();
				var t = new TaskLineItem(taskName, end);
				items.Add(t);
				current = t;
			}
			else
			{
				current.End = DateTime.Now;
				System.Console.WriteLine("Same task already started.");
			}
			ReloadTaskSuggestions();
		}

		#region -- Save/Load --
		
		string fileName;
		private object fileLock;

		private string LastFileName()
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				return fileName;
			}
			else
			{
				var last = "";
				foreach (var path in Directory.EnumerateFiles("./", "Tasks_*_*_*.time"))
				{
					var fName = Path.GetFileName(path);
					if (fName.CompareTo(last) > 0)
					{
						last = fName;
					}
				}
				if (last == "")
				{
					return NextFileName();
				}
				else
				{
					return last;
				}
			}
		}

		private string NextFileName()
		{
			DateTime now = DateTime.Now;
			return string.Format("Tasks_{0:D4}_{1:D2}_{2:D2}.time", now.Year, now.Month, now.Day);
		}

		private void SaveTasksTimer_Tick(object sender, EventArgs e)
		{
			SaveTasks(items);
		}

		private void SaveTasks(List<TaskLineItem> currentItems)
		{
			lock (fileLock)
			{
				if (current != null)
				{
					current.End = DateTime.Now;
				}
				bool fileExists = false;
				bool bakExists = false;
				bool success = false;
				if (File.Exists(fileName))
				{
					fileExists = true;
					if (File.Exists(fileName + ".bak"))
					{
						bakExists = true;
						if (File.Exists(fileName + ".bak2"))
						{
							File.Delete(fileName + ".bak2");
						}
						File.Copy(fileName + ".bak", fileName + ".bak2");
					}
				}

				try
				{
					using (var writer = new StreamWriter(fileName, false))
					{
						foreach (var item in currentItems)
						{
							writer.WriteLine(item.ToString());
						}
						writer.Close();
					}
					success = true;
				}
				catch (Exception)
				{
					success = false;
				}

				if (!success && fileExists)
				{
					//roll back
					if (File.Exists(fileName))
					{
						File.Delete(fileName);
					}
					File.Copy(fileName + ".bak", fileName);

					if (bakExists)
					{
						File.Delete(fileName + ".bak");
						File.Copy(fileName + ".bak2", fileName + ".bak");
					}
				}
			}
		}
		
		private void LoadTasks(string fileName)
		{
			lock (fileLock)
			{
				if (File.Exists(fileName))
				{
					using (var reader = new StreamReader(fileName))
					{
						while (!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							var item = TaskLineItem.FromString(line);
							if (item != null)
							{
								items.Add(item);
							}
						}
						reader.Close();
					}
				}
			}
			ReloadTaskSuggestions();
		}
		
		private void ReloadTaskSuggestions()
		{
			var suggestions = new SortedSet<string>();

			txtTaskName.AutoCompleteCustomSource.Clear();
			txtQuickNewTask.AutoCompleteCustomSource.Clear();
			if (items != null)
			{
				foreach (var item in items)
				{
					suggestions.Add(item.TaskName);				
				}
			}
			foreach (var suggestion in suggestions)
			{
				txtTaskName.AutoCompleteCustomSource.Add(suggestion);
				txtQuickNewTask.AutoCompleteCustomSource.Add(suggestion);
			}
			txtTaskName.AutoCompleteSource = AutoCompleteSource.CustomSource;
			txtQuickNewTask.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}

		private void btnNewFilename_Click(object sender, EventArgs e)
		{
			var next = NextFileName();
			if (fileName != next)
			{
				SaveTasksTimer.Enabled = false;
				var oldItems = new List<TaskLineItem>();
				var newItems = new List<TaskLineItem>();
				var now = DateTime.Now.Date;

				foreach (var item in items)
				{
					if (item.Begin.Date >= now)
					{
						newItems.Add(item);
					}
					else
					{
						oldItems.Add(item);
					}
				}

				items = newItems;

				SaveTasks(oldItems);
				fileName = next;
				SaveTasks(newItems);

				SaveTasksTimer.Enabled = true;
			}
		}

		#endregion

		#region -- Report --

		private void btnReport_Click(object sender, EventArgs e)
		{
			Report r = new Report(items);
			if (current != null)
			{
				current.End = DateTime.Now;
			}
			//Report r = new Report();
			r.Show(this);
		}

		#endregion

		#endregion

		#region -- UI Functions --

		#region -- Docking --
		bool _Docked = false;
		private void btnDock_Click(object sender, EventArgs e)
		{
			ToggleDock();
		}
		private void ToggleDock(bool? forced = null)
		{
			if (forced.HasValue)
			{
				_Docked = forced.Value;
			}
			else
			{
				_Docked = !_Docked;
			}

			SetFormState();
		}
		#endregion

		#region -- Visibility --
		
		bool _Hidden = false;
		
		private void ToggleFormHidden(bool? forced = null)
		{
			if (forced.HasValue)
			{
				_Hidden = forced.Value;
			}
			else
			{
				_Hidden = !_Hidden;
			}

			SetFormState();
		}

		private void SetFormState()
		{
			if (_Hidden)
			{
				SetFormState(FormState.Hidden);
			}
			else if (_Docked)
			{
				SetFormState(FormState.Docked);
			}
			else
			{
				SetFormState(FormState.Visible);
			}
		}
		
		private void SetFormState(FormState state)
		{
			switch(state)
			{
				case FormState.Hidden:
					this.Visible = false;
					this.Edge = AppBarEdges.Float;
					btnDock.Text = "Dock";
					trayIcon.Visible = true;
					break;
				case FormState.Docked:
					trayIcon.Visible = false;
					this.Edge = AppBarEdges.Left;
					btnDock.Text = "Undock";
					this.Visible = true;
					break;
				case FormState.Visible:
				default:
					trayIcon.Visible = false;
					this.Edge = AppBarEdges.Float;
					btnDock.Text = "Dock";
					this.Visible = true;
					break;
			}
		}
		
		private void quickShowWindow_Click(object sender, EventArgs e)
		{
			ToggleFormHidden(false);
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			ToggleFormHidden(true);
		}

		#endregion

		#region -- Exit --
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!handlerDead)
			{
				SystemEvents.SessionSwitch -= handler;
				handlerDead = true;
			}
			if (current != null)
			{
				if (current.End == DateTime.MinValue || current.End == current.Begin)
				{
					current.End = DateTime.Now;
				}
			}

			this.SaveTasks(items);
			
		}
		private void quickExit_Click(object sender, EventArgs e)
		{
			var temp = MessageBox.Show("Are you sure you want to exit?", "Are you sure?", MessageBoxButtons.YesNo);
			if (temp == System.Windows.Forms.DialogResult.Yes)
			{
				this.Close();
			}
		}
		#endregion

		#region -- Tray Menu Show / Hide --

		private void trayIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				var temp = MousePosition;
				trayMenu.Show(temp);
			}
		}

		private void contextMenuStrip1_MouseLeave(object sender, EventArgs e)
		{
			var pos = System.Windows.Forms.Cursor.Position;
			var cPos = trayMenu.PointToClient(pos);
			
			if ((cPos.X <= 0 || cPos.X >= this.Width) || (cPos.Y <= 0 || cPos.Y >= this.Height))
			{
				trayMenu.Hide();
			}
		}

		#endregion
		
		#endregion

		#region -- Implements IDisposable --

		void IDisposable.Dispose()
		{
			if (!handlerDead)
			{
				SystemEvents.SessionSwitch -= handler;
			}
			base.Dispose();
		}

		#endregion

		private void txtQuickNewTask_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n')
			{
				this.BeginTask(txtQuickNewTask.Text);
				ToggleFormHidden(true);
				e.Handled = true;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (_Hidden || this.Visible == false || this.WindowState == FormWindowState.Minimized)
			{
				trayIcon.Visible = true;
			}
		}

	}
}
