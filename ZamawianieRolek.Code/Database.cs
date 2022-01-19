using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZamawianieRolek.Code
{
	public static class Database
	{
		public static List<Account> Accounts{ get; set; }
		public static List<Shed> Sheds{ get; set; }

		public static void AddAccountToDatabase(Account newAccount)
		{
			Accounts.Add(newAccount);
		}

		public static void AddShedToDatabase(Shed newShed)
		{
			Sheds.Add(newShed);
		}
	}
}
