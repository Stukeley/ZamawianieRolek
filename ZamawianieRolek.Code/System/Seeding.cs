namespace ZamawianieRolek.Code.System;

/// <summary>
/// Statyczna klasa wykorzystywana na potrzeby seedowania bazy danych przy pierwszym uruchomieniu aplikacji.
/// </summary>
public static class Seeding
{
	/// <summary>
	/// Funkcja seedująca listę Sheds bazy danych, przy pierwszym uruchomieniu aplikacji, za pomocą przykładowych danych wiat i wypożyczalnych wrotek.
	/// </summary>
	public static void SeedData()
	{
		if (Database.Sheds.Count != 0)
		{
			return;
		}

		var shed1 = new Shed(10, 1, "Location 1");

		shed1.AvailableSkates = new List<Skates>()
		{
			new Skates("A", new byte[100], 43, true),
			new Skates("B", new byte[100], 45, true),
			new Skates("C", new byte[100], 39, false)
		};

		var shed2 = new Shed(7, 2, "Location 2");

		shed2.AvailableSkates = new List<Skates>()
		{
			new Skates("A", new byte[100], 43, true),
			new Skates("B", new byte[100], 45, true),
			new Skates("C", new byte[100], 39, false)
		};

		var shed3 = new Shed(13, 3, "Location 3");

		shed3.AvailableSkates = new List<Skates>()
		{
			new Skates("A", new byte[100], 43, true),
			new Skates("B", new byte[100], 45, true),
			new Skates("C", new byte[100], 39, false)
		};

		Database.AddShedToDatabase(shed1);
		Database.AddShedToDatabase(shed2);
		Database.AddShedToDatabase(shed3);
	}
}