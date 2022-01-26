using ZamawianieRolek.Code.User;

namespace ZamawianieRolek.Code.System;

/// <summary>
/// Klasa reprezentująca bazę danych, przechowującą informacje o użytkownikach (zarejestrowanych kontach) oraz dostępnych wiatach z wrotkami.
/// </summary>
public static class Database
{
	/// <summary>
	/// Lista zarejestrowanych w aplikacji kont.
	/// Na początku działania programu, lista jest wczytywana z pliku. Po zakończeniu działania, lista jest zapisywana do pliku.
	/// Podczas działania programu jest na bieżąco aktualizowana.
	/// </summary>
	public static List<Account> Accounts { get; set; }
	
	/// <summary>
	/// Lista dostępnych w aplikacji wiat, w których można wypożyczyć wrotki.
	/// Na początku działania programu, lista jest wczytywana z pliku. Po zakończeniu działania, lista jest zapisywana do pliku.
	/// Przy pierwszym uruchomieniu programu lista jest automatycznie zapełniana przykładowymi wartościami.
	/// </summary>
	public static List<Shed> Sheds { get; set; }

	/// <summary>
	/// Funkcja odpowiedzialna za dodanie nowego konta do bazy danych.
	/// </summary>
	/// <param name="newAccount">Konto użytkownika do dodania.</param>
	public static void AddAccountToDatabase(Account newAccount)
	{
		if (newAccount is null)
		{
			throw new Exception("Account cannot be null!");
		}

		Accounts.Add(newAccount);
	}

	/// <summary>
	/// Funkcja odpowiedzialna za dodanie nowej wiaty do bazy danych.
	/// </summary>
	/// <param name="newShed">Wiata do dodania.</param>
	public static void AddShedToDatabase(Shed newShed)
	{
		if (newShed is null)
		{
			throw new Exception("Shed cannot be null!");
		}
		
		Sheds.Add(newShed);
	}
}