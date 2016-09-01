﻿using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Core;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace Oxide.Ext.Hive.Utils
{
	public class OxideUtils
	{
		private static string config;

		public OxideUtils()
		{
		}

		public static void Puts(string message)
		{
			Interface.Oxide.ServerConsole.AddMessage(message, ConsoleColor.White);
		}

		public static void PrintError(string tag, string message)
		{
			printConsole(tag, message, ConsoleColor.Red);
		}

		public static void PrintWarning(string message)
		{
			Interface.Oxide.ServerConsole.AddMessage(message, ConsoleColor.Yellow);
		}

		public static void PrintSuccess(string tag, string message)
		{
			printConsole(tag, message, ConsoleColor.Green);
		}

		private static void printConsole(string tag, string msg, ConsoleColor col)
		{
			Interface.Oxide.ServerConsole.AddMessage("[" + tag + "] " + msg, col);
		}

		protected static Dictionary<String, String> combine(Dictionary<String, String> baseDic, Dictionary<String, String> addDic)
		{
			if (addDic != null)
			{
				addDic.ToList().ForEach(x =>
				{
					baseDic.Add(x.Key, x.Value);
				});
			}
			return baseDic;
		}




	}
}
