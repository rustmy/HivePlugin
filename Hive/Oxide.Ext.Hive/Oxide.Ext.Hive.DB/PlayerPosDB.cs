using System;
using System.Data.SQLite;
using System.IO;
using Oxide.Core;
using Oxide.Ext.Hive.Utils;

namespace Oxide.Ext.Hive {

	// TODO: Let it extend SQLiteDB.cs

	// Player position database - TODO: Optimize with prepared
	public class PlayerPosDB {
		private static string FILE = Interface.Oxide.DataDirectory + Path.DirectorySeparatorChar + "hive.sqlite";
		private SQLiteConnection conn;
		private static PlayerPosDB obj;
		private bool active;

		private PlayerPosDB()
		{
		}


		// Get Singleton Instance of this DB
		public static PlayerPosDB getInstance() {
			if(obj == null) {
				obj = new PlayerPosDB();
			}

			return obj;
		}


		// Init the database
		public void Init() {

			if(!File.Exists(FILE))
				Create(FILE);
			else
				Open();
			
			active = true;

		}

		public void Open() {
			conn = new SQLiteConnection("Data Source=" + FILE + "; Version=3;");
			conn.Open();
			active = true;
		}

		public void Close() {
			active = false;
			conn.Close();
		}


		public void ReOpen() {
			active = false;
			conn.Close();
			conn = new SQLiteConnection("Data Source=" + FILE + "; Version=3;");
			conn.Open();
			active = true;
		}


		// Creates the database if not existent
		private void Create(string path) {
			SQLiteConnection.CreateFile(path);
			conn = new SQLiteConnection("Data Source=" + path + "; Version=3;");
			conn.Open();

			string sql = "CREATE TABLE `players` (  `id` BIGINT NOT NULL, `x` DOUBLE NOT NULL,  `y` DOUBLE NULL, `z` DOUBLE NULL,  PRIMARY KEY (`id`));";
			SQLiteCommand command = new SQLiteCommand(sql, conn);
			command.ExecuteNonQuery();
		}




		// Saves a user in the database
		public void SaveUser(ulong steamid, double x, double y, double z) {

			if(conn == null) {
				OxideUtils.PrintError("Hive", "Could not save player in DB because no connection");
				if(GlobalVars.DEBUG) {
					
				}
				return;
			}


			if(active) {
				
				string query1 = "select count(1) from `players` where `id` = '" + steamid + "';";
				SQLiteCommand command = new SQLiteCommand(query1, conn);
				long result = 0;

				using(SQLiteDataReader reader = command.ExecuteReader()) {
					while(reader.Read())
						result = reader.GetInt64(0);

					reader.Close();
				}


				string query2 = "";
				if(result == 0) {
					query2 = "INSERT INTO `players` (`id`,`x`,`y`,`z`) VALUES (" + steamid + ", " + x + ", " + y + ", " + z + ");";
				} else {
					query2 = "UPDATE `players` SET `x`=" + x + ", `y`=" + y + ", `z`=" + z + " WHERE `id`=" + steamid;
				}


				SQLiteCommand command2 = new SQLiteCommand(query2, conn);

				command2.ExecuteNonQuery();

			}
		}


		// Gets and removes a user in the database
		public PlayerPosition PopUser(ulong steamid) {
			if(conn == null) {
				return null;
			}
			
			if(active) {
				// Get player from db
				string sql = "select * from `players` where `id` = '" + steamid + "';";
				SQLiteCommand command = new SQLiteCommand(sql, conn);
				SQLiteDataReader reader = command.ExecuteReader();

				// No player inside database
				if(!reader.HasRows) {
					reader.Close();
					return null;
				}

				// PlayerPosition object
				PlayerPosition pos = null;
				while(reader.Read()) {
					pos = new PlayerPosition(Convert.ToUInt64(reader["id"]), Convert.ToSingle(reader["x"]), Convert.ToSingle(reader["y"]), Convert.ToSingle(reader["z"]));

				}

				reader.Close();

				// Delete player after get
				string sql2 = "DELETE from `players` WHERE `id` = '" + steamid + "';";
				SQLiteCommand command2 = new SQLiteCommand(sql2, conn);
				command2.ExecuteNonQuery();


				return pos;
			}

			// DB not active
			return null;
		}

	}
}