using System.Text.Json;
using ZamawianieRolek.Code.User;

namespace ZamawianieRolek.Code.System;

/// <summary>
/// Klasa odpowiedzialna za serializację i deserializację danych - zarówno dotyczących kont użytkowników, jak i dostępnych wiat.
/// </summary>
public static class Serialization
{
	/// <summary>
	/// Ścieżka do pliku zawierającego zapisane konta użytkowników.
	/// </summary>
	private const string AccountDataFileName = "Accounts.json";
	
	/// <summary>
	/// Ścieżka do pliku zawierającego zapisane wiaty.
	/// </summary>
	private const string ShedDataFileName = "Sheds.json";

	/// <summary>
	/// Funkcja serializująca dane kont użytkowników oraz wiat do plików o rozszerzeniu JSON.
	/// Funkcja ta jest wywoływana tuż przed zakończeniem działania programu.
	/// </summary>
	public static void SerializeDatabase()
	{
		string accountJson = JsonSerializer.Serialize(Database.Accounts);
		File.WriteAllText(AccountDataFileName, accountJson);

		string shedJson = JsonSerializer.Serialize(Database.Sheds);
		File.WriteAllText(ShedDataFileName, shedJson);
	}

	/// <summary>
	/// Funkcja deserializująca dane kont użytkowników oraz wiat z plików o rozszerzeniu JSON (jeżeli istnieją).
	/// Funkcja ta jest wywoływana tuż po rozpoczęciu działania programu.
	/// </summary>
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