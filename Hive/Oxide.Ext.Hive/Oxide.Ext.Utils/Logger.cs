using System;
using System.IO;
using Oxide.Ext.Hive;

namespace Oxide.Ext.Utils
{
	public class Logger
	{

		private string file;
		private bool active;



		public Logger(string file)
		{
			this.file = file;
			if (!File.Exists(file))
			{
				try{
					File.Create(file);
					active = true;
					Print("Log created!", LogLevel.OP);
				} catch (Exception ex)
				{
					active = false;
				}


			}
		}

		public bool Clear()
		{
			try {
				File.Delete(file);
				File.Create(file);
				return true;
			} catch (Exception ex)
			{
				return false;
			}

		}


		public bool Print(string text, LogLevel lvl)
		{
			if (active)
			{
				try
				{
					File.AppendAllText(this.file, "[" + DateTime.Now.ToString("dd-MM-yy hh:mm:ss") + "] [" + getLogTag(lvl) + "] " + text + "\r\n");
					return true;
				}
				catch (Exception ex)
				{
					return false;
			}
			}
			return false;
		}

		private string getLogTag(LogLevel lvl)
		{
			switch (lvl)
			{
				case LogLevel.OP:
					return "Operation";
					break;
				case LogLevel.FatalError:
					return "Fatal Error";
					break;
				case LogLevel.Error:
					return "Error";
					break;
				case LogLevel.Warning:
					return "Warning";
					break;
				default:
					return "";
					break;
			}
		}

	}
}

