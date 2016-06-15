using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace TaskTimer.WPF
{
	/// <summary>
	/// Interaction logic for WPFReport.xaml
	/// </summary>
	public partial class WPFReport : UserControl
	{
		public ObservableCollection<Day> Days
		{
			get;
			set;
		}

		public WPFReport()
		{
			DataContext = this;
			Days = new ObservableCollection<Day>(new SortedSet<Day>());
			InitializeComponent();
		}
		public WPFReport(IEnumerable<Day> days)
		{
			DataContext = this;
			var sorted = new SortedSet<Day>(days);
			Days = new ObservableCollection<Day>(sorted);
			InitializeComponent();
		}

		public void AddDays(IEnumerable<Day> days)
		{
			foreach (var day in days)
			{
				bool inserted = false;
				for (int i = 0; i < Days.Count; i++)
				{
					if (day < Days[i])
					{
						Days.Insert(i, day);
						inserted = true;
						break;
					}
				}
				if (!inserted)
				{
					Days.Add(day);
				}
			}
		}
	}
}
