namespace ZamawianieRolek.Tests;

using System;
using System.Linq;
using NUnit.Framework;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

/// <summary>
/// Klasa zawierająca testy związane z klasą Database - bazą danych, przechowującą informacje o kontach użytkowników i wiatach.
/// </summary>
[TestFixture]
public class DatabaseTests
{
	/// <summary>
	/// Funkcja setup, inicjalizująca bazę danych przed każdym wywołanym testem.
	/// </summary>
	[SetUp]
	public void Setup()
	{
		Seeding.SeedData();
	}

	/// <summary>
	/// Test sprawdzający, czy po rejestracji konta znajduje się ono w bazie danych.
	/// </summary>
	[Test]
	public void AccountExistsAfterRegistration()
	{
		var account = Account.RegisterWithUserData("email@e.m", "name", "surname", "password", "123456789");

		var accountFromDatabase = Database.Accounts.FirstOrDefault(x => x.Email == "email@e.m");

		Assert.IsNotNull(accountFromDatabase);
		Assert.AreEqual(account, accountFromDatabase);
	}

	/// <summary>
	/// Test sprawdzający, czy po dodaniu nowej wiaty do bazy danych jest ona zapisana.
	/// </summary>
	[Test]
	public void ShedExistsAfterAdd()
	{
		var shed = new Shed(5, 101, "Random Location");
		Database.AddShedToDatabase(shed);

		var shedFromDatabase = Database.Sheds.FirstOrDefault(x => x.Id == 101);

		Assert.IsNotNull(shedFromDatabase);
		Assert.AreEqual(shed, shedFromDatabase);
	}

	/// <summary>
	/// Test sprawdzający, czy funkcja odpowiedzialna za dodanie obiektu do bazy danych wyrzuca wyjątek w przypadku danych wejściowych będących null.
	/// </summary>
	[Test]
	public void ThrowsForNullParameter()
	{
		try
		{
			Database.AddAccountToDatabase(null);
			Assert.Fail();
		}
		catch (Exception)
		{
			Assert.Pass();
		}
	}
}