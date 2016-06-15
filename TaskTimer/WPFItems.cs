using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace TaskTimer.WPF
{
	[DebuggerDisplay("{TotalHours}",Name="{DateLabel}")]
	public class Day : ObservableCollection<Task>, INotifyPropertyChanged,
		IComparable<Day>, IEquatable<Day>
	{
		public bool Expanded { get { return true; } }
		
		private DateTime _Date;
		public DateTime Date
		{
			get
			{
				return _Date;
			}
			set
			{
				_Date = value;
				OnPropertyChanged(new PropertyChangedEventArgs("Date"));
			}
		}
		public double TotalHours
		{
			get
			{
				double total = 0.0;
				foreach (var task in this)
				{
					total += task.TotalHours;
				}
				return total;
			}
		}
		public string DateLabel
		{
			get
			{
				return _Date.ToShortDateString();
			}
		}

		public Day(DateTime date, Task[] tasks)
			: base(new SortedSet<Task>())
		{

			foreach (var task in tasks)
			{
				this.Add(task);
			}
			_Date = date;
			this.CollectionChanged += new NotifyCollectionChangedEventHandler(My_CollectionChanged);
		}

		void My_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("TotalHours"));
		}

		void task_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender));
		}

		#region -- Item Add/Remove Event Handling --

		protected override void ClearItems()
		{
			foreach (var task in this)
			{
				task.CollectionChanged -= task_CollectionChanged;
			}

			base.ClearItems();
		}

		protected override void InsertItem(int index, Task task)
		{
			task.CollectionChanged -= task_CollectionChanged;
			task.CollectionChanged += task_CollectionChanged;

			base.InsertItem(index, task);
		}

		protected override void RemoveItem(int index)
		{
			if (0 <= index && index < this.Count)
			{
				var oldTask = this[index];
				oldTask.CollectionChanged -= task_CollectionChanged;
			}

			base.RemoveItem(index);
		}

		protected override void SetItem(int index, Task task)
		{
			if (0 <= index && index < this.Count)
			{
				var oldTask = this[index];
				oldTask.CollectionChanged -= task_CollectionChanged;
			}
			task.CollectionChanged += task_CollectionChanged;

			base.SetItem(index, task);
		}

		#endregion

		#region -- Comparisons --

		#region IComparable<Task> Members
		public int CompareTo(Day other)
		{
			if (Object.ReferenceEquals(null, other))
			{
				return -1;
			}
			else if (Object.ReferenceEquals(this, other))
			{
				return 0;
			}
			else
			{
				var comp = this.Date.CompareTo(other.Date);
				if (comp == 0)
				{
					comp = this.TotalHours.CompareTo(other.TotalHours);
				}
				if (comp == 0)
				{
					comp = this.GetHashCode().CompareTo(other.GetHashCode());
				}
				return comp;
			}
		}
		#endregion

		#region IEquatable<Day> Members
		public bool Equals(Day other)
		{
			if (Object.ReferenceEquals(null, other))
			{
				return false;
			}
			else if (Object.ReferenceEquals(this, other))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region -- Operators --
		public static bool operator < (Day day1, Day day2)
		{
			return (day1.CompareTo(day2) < 0) ;
		}
		public static bool operator > (Day day1, Day day2)
		{
			return (day1.CompareTo(day2) > 0);
		}
		#endregion

		#endregion
	}
	[DebuggerDisplay("{TotalHours}", Name = "{DateLabel}")]
	public class Task : ObservableCollection<Item>, INotifyPropertyChanged,
		IComparable<Task>, IEquatable<Task>
	{
		public bool Expanded { get { return false; } }
		private string _TaskName;
		private double? _OverrideHours;

		public string TaskName
		{
			get
			{
				return _TaskName;
			}
			set
			{
				_TaskName = value;
				OnPropertyChanged(new PropertyChangedEventArgs("TaskName"));
			}
		}
		public double? OverrideHours
		{
			get
			{
				return _OverrideHours;
			}
			set
			{
				_OverrideHours = value;
				OnPropertyChanged(new PropertyChangedEventArgs("OverrideHours"));
				OnPropertyChanged(new PropertyChangedEventArgs("TotalHours"));
			}
		}
		public double TotalHours
		{
			get
			{
				if (OverrideHours.HasValue)
				{
					return OverrideHours.Value;
				}
				else
				{
					double total = 0.0;
					foreach (var item in Items)
					{
						total += item.TotalHours;
					}
					return total;
				}
			}
		}

		public Task(string taskName, Item[] items)
			: base(new SortedSet<Item>())
		{

			foreach (var item in items)
			{
				this.Add(item);
			}
			_TaskName = taskName;
			this.CollectionChanged += new NotifyCollectionChangedEventHandler(My_CollectionChanged);
		}

		public Task(string taskName, double overrideHours)
		{
			_TaskName = taskName;
			_OverrideHours = overrideHours;
			this.CollectionChanged += new NotifyCollectionChangedEventHandler(My_CollectionChanged);
		}

		void My_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged(new PropertyChangedEventArgs("TotalHours"));
		}

		void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, sender, sender));
		}

		#region -- Item Add/Remove Event Handling --

		protected override void ClearItems()
		{
			foreach (var item in this)
			{
				item.PropertyChanged -= item_PropertyChanged;
			}

			base.ClearItems();
		}

		protected override void InsertItem(int index, Item item)
		{
			item.PropertyChanged -= item_PropertyChanged;
			item.PropertyChanged += item_PropertyChanged;

			base.InsertItem(index, item);
		}

		protected override void RemoveItem(int index)
		{
			if (0 <= index && index < this.Count)
			{
				var oldItem = this[index];
				oldItem.PropertyChanged -= item_PropertyChanged;
			}

			base.RemoveItem(index);
		}

		protected override void SetItem(int index, Item item)
		{
			if (0 <= index && index < this.Count)
			{
				var oldItem = this[index];
				oldItem.PropertyChanged -= item_PropertyChanged;
			}
			item.PropertyChanged += item_PropertyChanged;

			base.SetItem(index, item);
		}

		#endregion

		#region -- Comparisons --

		#region IComparable<Task> Members

		public int CompareTo(Task other)
		{
			if (Object.ReferenceEquals(null, other))
			{
				return -1;
			}
			else if (Object.ReferenceEquals(this, other))
			{
				return 0;
			}
			else
			{
				var comp = this.TaskName.CompareTo(other.TaskName);
				if (comp == 0)
				{
					comp = this.TotalHours.CompareTo(other.TotalHours);
				}
				if (comp == 0)
				{
					comp = this.GetHashCode().CompareTo(other.GetHashCode());
				}
				return comp;
			}
		}

		#endregion

		#region IEquatable<Task> Members

		public bool Equals(Task other)
		{
			if (Object.ReferenceEquals(null, other))
			{
				return false;
			}
			else if (Object.ReferenceEquals(this, other))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region -- Operators --
		public static bool operator <(Task task1, Task task2)
		{
			return (task1.CompareTo(task2) < 0);
		}
		public static bool operator >(Task task1, Task task2)
		{
			return (task1.CompareTo(task2) > 0);
		}
		#endregion

		#endregion
	}
	public class Item : INotifyPropertyChanged,
		IComparable<Item>, IEquatable<Item>
	{
		public bool Expanded { get { return true; } }
		private DateTime _Begin;
		private DateTime _End;
		public DateTime Begin
		{
			get
			{
				return _Begin;
			}
			set
			{
				_Begin = value;
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Begin"));
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TotalHours"));
			}
		}
		public DateTime End
		{
			get
			{
				return _End;
			}
			set
			{
				_End = value;
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("End"));
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TotalHours"));
			}
		}
		public double TotalHours
		{
			get
			{
				if (End > Begin)
				{
					return (End - Begin).TotalHours;
				}
				else
				{
					return (Begin - End).TotalHours;
				}
			}
		}

		public string BeginLabel
		{
			get
			{
				return _Begin.ToShortTimeString();
			}
		}
		public string EndLabel
		{
			get
			{
				return _End.ToShortTimeString();
			}
		}

		public Item(DateTime begin, DateTime end)
		{
			_Begin = begin;
			_End = end;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region -- Comparisons --

		#region IComparable<Item> Members

		public int CompareTo(Item other)
		{
			if (Object.ReferenceEquals(null, other))
			{
				return -1;
			}
			else if (Object.ReferenceEquals(this, other))
			{
				return 0;
			}
			else
			{
				var comp = this.Begin.CompareTo(other.Begin);
				if (comp == 0)
				{
					comp = this.End.CompareTo(other.End);
				}
				if (comp == 0)
				{
					comp = this.GetHashCode().CompareTo(other.GetHashCode());
				}
				return comp;
			}
		}

		#endregion

		#region IEquatable<Item> Members

		public bool Equals(Item other)
		{
			if (Object.ReferenceEquals(null, other))
			{
				return false;
			}
			else if (Object.ReferenceEquals(this, other))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region -- Operators --
		public static bool operator <(Item item1, Item item2)
		{
			return (item1.CompareTo(item2) < 0);
		}
		public static bool operator >(Item item1, Item item2)
		{
			return (item1.CompareTo(item2) > 0);
		}
		#endregion

		#endregion
	}
}
