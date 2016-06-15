using System;
using System.Globalization;

public class TaskLineItem
{
	private static char SplitCharacter = ';';
	public string TaskName;
	public DateTime Begin;
	public DateTime End;

	public TaskLineItem(string taskName)
	{
		this.TaskName = taskName;
		this.Begin = DateTime.Now;
		this.End = this.Begin;
	}
	public TaskLineItem(string taskName, DateTime begin)
	{
		this.TaskName = taskName;
		this.Begin = begin;
		this.End = this.Begin;
	}
	public DateTime EndTask()
	{
		End = DateTime.Now;
		return End;
	}

	public override string ToString()
	{
		return string.Format(@"{0}{1}{2}{3}{4}",TaskName,SplitCharacter.ToString(),Begin.ToUniversalTime().ToString("o"),SplitCharacter.ToString(),End.ToUniversalTime().ToString("o"));
	}
	public static TaskLineItem FromString(string val)
	{
		if (val != null && val != "" && val.Contains(SplitCharacter.ToString()))
		{
			var splitList = val.Split(new char[] {SplitCharacter});
			if (splitList.Length == 3 && splitList[0] != "")
			{
				DateTime b;
				if (DateTime.TryParse(splitList[1], null, DateTimeStyles.RoundtripKind, out b))
				{
					b = b.ToLocalTime();
					var result = new TaskLineItem(splitList[0], b);
					DateTime e;
					if (DateTime.TryParse(splitList[2], null, DateTimeStyles.RoundtripKind, out e))
					{
						e = e.ToLocalTime();
						if (e >= b)
						{
							result.End = e;
						}
					}
					return result;
				}
			}
		}
		return null;
	}
}