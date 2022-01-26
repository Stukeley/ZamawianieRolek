namespace ZamawianieRolek.Tests;

using System;
using NUnit.Framework;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

/// <summary>
/// Klasa zawierająca testy związane z klasą UserProfile - profilem użytkownika w ramach konta.
/// </summary>
[TestFixture]
public class UserProfileTests
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
	/// Test sprawdzający, czy po utworzeniu profilu użytkownika jest on przypisany do konta, oraz czy jest on aktualnie wybranym profilem.
	/// </summary>
	[Test]
	public void ProfileExistsAfterCreating()
	{
		var newAccount = Account.RegisterWithUserData("email@e.m", "name", "surname", "password", "123456789");

		var newProfile = UserProfile.CreateUser("Name", 36, PaymentMethod.CreditCard);

		newAccount.AddUserProfile(newProfile);

		newAccount.SelectUserProfile("Name");

		Assert.AreEqual(newProfile, newAccount.SelectedProfile);
	}

	/// <summary>
	/// Test sprawdzający, czy funkcja tworząca profil użytkownika wyrzuca wyjątek dla niepoprawnych danych wejściowych.
	/// </summary>
	[Test]
	public void ThrowsForInvalidInput()
	{
		var newAccount = Account.RegisterWithUserData("email@e.m", "name", "surname", "password", "123456789");

		try
		{
			var newProfile = UserProfile.CreateUser("", 36, PaymentMethod.CreditCard);
			Assert.Fail();
		}
		catch (Exception)
		{
			Assert.Pass();
		}
	}
}