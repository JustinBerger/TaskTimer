using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Combine_Old_Files
{
	class Program
	{
		static void Main(string[] args)
		{
			bool valid = false;
			string outFName = "";
			while (!valid)
			{
				Console.WriteLine("Please enter the output filename");
				outFName = Console.ReadLine();
				if (File.Exists(outFName))
				{
					Console.WriteLine("File already exists");
				}
				else
				{
					try
					{
						outFName = Path.GetFileName(outFName);
						valid = true;
					}
					catch (Exception)
					{
						valid = false;
					}
				}
			}
			FileWorker.CombineFiles(args, outFName);
		}
	}
}
