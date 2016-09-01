using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Oxide.Core;
using System.Collections.Generic;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive.Config
{
	public class OxideConfiguration
	{

		private static string config;

		public OxideConfiguration()
		{
		}

		public static void setConfigKey(string file, string key, string value)
		{
			string json = readConfig(file);
			OxideUtils.Puts("Json sagt: " + json);

			Dictionary<string, string> conf;

			if (String.IsNullOrEmpty(json))
			{
				conf = new Dictionary<string, string>();
			}
			else {
				conf = getConfigMap(json);
			}

			if (conf.ContainsKey(key))
			{
				conf[key] = value;
			}
			else
				conf.Add(key, value);

			WriteConfig(file, conf);

			// Clear cached config
			config = null;

		}


		public static string getConfigKey(string file, string key)
		{
			string json = readConfig(file);

			if (String.IsNullOrEmpty(json))
			{
				return null;
			}

			Dictionary<string, string> conf = getConfigMap(json);

			if (conf.ContainsKey(key))
			{
				return conf[key];
			}

			return null;
		}

		private static Dictionary<string, string> getConfigMap(string json)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			if (!String.IsNullOrEmpty(json))
			{
				JsonTextReader reader = new JsonTextReader(new StringReader(json));
				while (reader.Read())
				{
					if (reader.Value != null)
					{
						string key = reader.Value.ToString();
						reader.Read();
						result.Add(key, reader.Value.ToString());
					}
				}
			}

			return result;
		}

		// Writes a dictionary to a config
		private static void WriteConfig(string file, Dictionary<string, string> conf)
		{
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);

			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				writer.Formatting = Formatting.Indented;

				writer.WriteStartObject();

				foreach (string key in conf.Keys)
				{
					writer.WritePropertyName(key);
					writer.WriteValue(conf[key]);
				}
				writer.WriteEndObject();
			}


			OxideUtils.Puts("Sb sieht so aus: " + sb.ToString());

			// Complete file name
			string folder = Interface.Oxide.ConfigDirectory;
			string fileName = folder + Path.DirectorySeparatorChar + file + ".json";
			OxideUtils.Puts(fileName);
			File.WriteAllText(fileName, sb.ToString());
		}
		private static string readConfig(string fileName)
		{
			// Check if config is already loaded
			if (!String.IsNullOrEmpty(config))
			{
				return config;
			}

			// Load config file
			string folder = Interface.Oxide.ConfigDirectory;
			string file = folder + Path.DirectorySeparatorChar + fileName + ".json";

			// Create file if not existent
			if (!File.Exists(file))
			{
				File.Create(file);
				return "";
			}

			try
			{
				using (StreamReader sr = new StreamReader(file))
				{
					config = sr.ReadToEnd();
					return config;

				}

			}
			catch (Exception e)
			{
				OxideUtils.PrintError("hive","Error while reading to config");
				return null;
			}
		}


		public static bool exists(string file)
		{
			return File.Exists(getPath(file));

		}

		public static bool isEmpty(string file)
		{
			string json = readConfig(file);

			if (String.IsNullOrEmpty(json))
			{
				return true;
			}

			return false;
		}

		private static string getPath(string fileName)
		{
			// Load config file
			string folder = Interface.Oxide.ConfigDirectory;
			return folder + Path.DirectorySeparatorChar + fileName + ".json";
		}
	}
}


