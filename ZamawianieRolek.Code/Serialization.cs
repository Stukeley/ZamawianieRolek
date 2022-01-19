using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZamawianieRolek.Code
{
	public static class Serialization
	{
		private const string AccountDataFileName = "Accounts.json";
		private const string ShedDataFileName = "Sheds.json";

		public static void SerializeDatabase()
		{
			string accountJson = JsonSerializer.Serialize(Database.Accounts);
			File.WriteAllText(AccountDataFileName, accountJson);

			string shedJson = JsonSerializer.Serialize(Database.Sheds);
			File.WriteAllText(ShedDataFileName, shedJson);
		}

		public static void DeserializeDatabase()
		{
			if (File.Exists(AccountDataFileName))
			{
				string accountJson = File.ReadAllText(AccountDataFileName);
				Database.Accounts = JsonSerializer.Deserialize<List<Account>>(accountJson);
			}
			else
			{
				Database.Accounts = new List<Account>();
			}

			if (File.Exists(ShedDataFileName))
			{
				string shedJson = File.ReadAllText(ShedDataFileName);
				Database.Sheds = JsonSerializer.Deserialize<List<Shed>>(shedJson);
			}
			else
			{
				Database.Sheds = new List<Shed>();
			}
		}
	}
}
