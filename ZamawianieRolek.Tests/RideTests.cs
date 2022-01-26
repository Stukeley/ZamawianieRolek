namespace ZamawianieRolek.Tests;

using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

/// <summary>
/// Klasa zawierająca testy związane z klasą Ride - przejazdem, obliczaniem ceny i długości przejazdu, oraz trybem Turbo.
/// </summary>
[TestFixture]
public class RideTests
{
	private Account _account;

	/// <summary>
	/// Funkcja setup, inicjalizująca bazę danych przed każdym wywołanym testem, oraz inicjalizująca obiekt Account, wykorzystywany przez testy.
	/// </summary>
	[SetUp]
	public void Setup()
	{
		Seeding.SeedData();
		_account = Account.RegisterWithUserData("asdf@gmail.com", "Name", "Surname", "123", "123456789");
		_account.AddUserProfile(UserProfile.CreateUser("ProfileName", 39, PaymentMethod.CreditCard));
	}

	/// <summary>
	/// Test sprawdzający, czy cena za przejazd wrotkami jest obliczana poprawnie.
	/// </summary>
	[Test]
	public void EvaluatesPriceCorrectly()
	{
		var shed = Database.Sheds.First();
		var skates = shed.AvailableSkates.First();

		var ride = new Ride();

		ride.Skates = skates;
		ride.Skates.IsLent = true;
		ride.StartTime = DateTime.Now;
		_account.SelectedProfile.Ride = ride;

		Thread.Sleep(1000);

		ride.FinishTime = DateTime.Now.AddMinutes(1);

		Assert.AreEqual(ride.EvaluatePrice(), 5.10f);
	}

	/// <summary>
	/// Test sprawdzający, czy czas przejazdu wrotkami (w minutach) jest obliczany poprawnie.
	/// </summary>
	[Test]
	public void EvaluatesTimeCorrectly()
	{
		var shed = Database.Sheds.First();
		var skates = shed.AvailableSkates.First();

		var ride = new Ride();

		ride.Skates = skates;
		ride.Skates.IsLent = true;
		ride.StartTime = DateTime.Now;
		_account.SelectedProfile.Ride = ride;

		Thread.Sleep(1000);

		ride.FinishTime = DateTime.Now.AddMinutes(1);

		Assert.AreEqual(ride.EvaluateTime(), 1);
	}

	/// <summary>
	/// Test sprawdzający, czy przełączanie trybu Turbo we wrotkach działa poprawnie.
	/// </summary>
	[Test]
	public void TurboModeSwitchesCorrectly()
	{
		var shed = Database.Sheds.First();
		var skates = shed.AvailableSkates.First();

		var ride = new Ride();

		ride.Skates = skates;
		ride.Skates.IsLent = true;
		ride.StartTime = DateTime.Now;
		_account.SelectedProfile.Ride = ride;

		skates.Turbo();

		Assert.AreEqual(skates.IsTurboActive, true);
	}
	
}