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
	public partial class Report : Form
	{
		List<WPF.Day> wpfDays;
		public Report()
		{
			InitializeComponent();
		}
		public Report(IEnumerable<TaskLineItem> items)
		{
			var days = new Dictionary<DateTime, HashSet<string>>();
			var tasks = new Dictionary<string, List<TaskLineItem>>();
			foreach (var item in items)
			{
				var day = item.Begin.Date;
				var task = item.TaskName;
				if (string.IsNullOrWhiteSpace(task))
				{
					task = "BLANK";
				}

				if (days.ContainsKey(day))
				{
					days[day].Add(task);
				}
				else
				{
					days[day] = new HashSet<string>() { task };
				}

				if (tasks.ContainsKey(task))
				{
					tasks[task].Add(item);
				}
				else
				{
					tasks[task] = new List<TaskLineItem>() { item };
					var list = new List<TaskLineItem>();
				}
			}

			wpfDays = new List<WPF.Day>();
			foreach (var day in days.Keys)
			{
				var wpfTasks = new List<WPF.Task>();
				foreach (var task in days[day])
				{
					var wpfItems = new List<WPF.Item>();
					foreach (var item in tasks[task])
					{
						var itemDay = item.Begin.Date;
						if (itemDay == day)
						{
							if ((item.End - item.Begin).TotalHours < 8)
							{
								wpfItems.Add(new WPF.Item(item.Begin,item.End));
							}
						}
					}
					wpfTasks.Add(new WPF.Task(task, wpfItems.ToArray()));
				}
				wpfDays.Add(new WPF.Day(day, wpfTasks.ToArray()));
			}

			InitializeComponent();
		}

		private void Report_Load(object sender, EventArgs e)
		{
			wpfReport1.AddDays(wpfDays);
		}
	}
}
