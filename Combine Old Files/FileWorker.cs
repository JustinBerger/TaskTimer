using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Combine_Old_Files
{
	public class TaskLine
	{
		public readonly string Name;
		public readonly DateTime Begin;
		public readonly DateTime End;
		public override string ToString()
		{
			return string.Format("{0};{1};{2}", Name, Begin.ToUniversalTime().ToString("o"), End.ToUniversalTime().ToString("o"));
		}
		public static TaskLine TryParse(string line)
		{
			var parts = line.Split(';', '|');
			if (parts.Length == 3)
			{
				return TryParse(parts[0], parts[1], parts[2]);
			}
			else
			{
				return null;
			}
		}
		public static TaskLine TryParse(string name, string begin, string end)
		{
			DateTime beginDT, endDT;

			if (!DateTime.TryParse(begin, null, DateTimeStyles.RoundtripKind, out beginDT))
			{
				return null;
			}
			else if (!DateTime.TryParse(end, null, DateTimeStyles.RoundtripKind, out endDT))
			{
				return null;
			}
			else
			{
				return new TaskLine(name, beginDT, endDT);
			}
		}
		public TaskLine(string name, DateTime begin, DateTime end)
		{
			Name = Cleanup(name);
			Begin = begin;
			End = end;
		}
		private string Cleanup(string orig)
		{
			string fix = orig.ToLower();
			return MultiRemove(MultiRemove(fix, "the"),"attempt to")
				.Replace("talking","talk")
				.Replace("afk","break");
		}
		private string MultiRemove(string orig, string toRemove)
		{
			string fix = orig;
			if (fix.StartsWith(toRemove + " "))
			{
				fix = fix.Substring(toRemove.Length+1);
			}
			if (fix.EndsWith(" " + toRemove))
			{
				fix = fix.Substring(0,fix.Length - (toRemove.Length+1));
			}
			if (fix.Contains(" " + toRemove + " " ))
			{
				fix = fix.Replace(" " + toRemove + " " ," ");
			}
			if (fix.Contains(toRemove))
			{
				fix = fix.Replace(toRemove,"");
			}
			return fix;
		}
	}

	public static class FileWorker
	{
		public static void CombineFiles(string[] fileNames, string outFileName)
		{
			var lines = new List<TaskLine>();
			foreach (string fileName in fileNames)
			{
				if (File.Exists(fileName) && fileName.Contains("Tasks_"))
				{
					var someLines = ReadFile(fileName);
					lines.AddRange(someLines);
				}
				else
				{
					Console.WriteLine("Invalid file {0}", fileName);
				}
			}
			lines = new List<TaskLine>(Dedup(lines));
			WriteFile(outFileName, lines);
		}

		public static List<TaskLine> ReadFile(string fileName)
		{
			var lines = new List<TaskLine>();

			using (var reader = File.OpenText(fileName))
			{
				int i = 1;
				for (var line = reader.ReadLine(); !reader.EndOfStream; line = reader.ReadLine())
				{
					var item = TaskLine.TryParse(line);
					if (item == null)
					{
						Console.WriteLine("Invalid line {0}:{1}", fileName, i);
					}
					else
					{
						lines.Add(item);
					}
					i++;
				}
			}

			return lines;
		}

		public static void WriteFile(string fileName, List<TaskLine> lines)
		{
			using (var writer = File.CreateText(fileName))
			{
				foreach (var line in lines)
				{
					if (line != null)
					{
						writer.WriteLine(line.ToString());
					}
				}
			}
		}

		public static IEnumerable<TaskLine> Dedup(List<TaskLine> lines)
		{
			//var NameIndex = new Dictionary<string, List<TaskLine>>();
			var DateIndex = new Dictionary<DateTime, List<TaskLine>>();
			var newLines = new List<TaskLine>();

			foreach (var line in lines)
			{
				if (line != null)
				{
					var date = line.Begin.Date;
					if (DateIndex.ContainsKey(date))
					{
						var list = DateIndex[date];
						bool found = false;
						foreach (var line2 in list)
						{
							if (line.Name == line2.Name)
							{
								if (line.Begin <= line2.End && line2.Begin <= line.End)
								{
									found = true;
									break;
								}
							}
						}
						if (!found)
						{
							list.Add(line);
							yield return line;
						}
					}
					else
					{
						DateIndex[date] = new List<TaskLine>() { line };
						yield return line;
					}
				}
			}
		}
	}
}
