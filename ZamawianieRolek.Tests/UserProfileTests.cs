namespace ZamawianieRolek.Tests;

using System;
using NUnit.Framework;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

public class UserProfileTests
{
	[SetUp]
	public void Setup()
	{
		Seeding.SeedData();
	}

	[Test]
	public void ProfileExistsAfterCreating()
	{
		var newAccount = Account.RegisterWithUserData("email@e.m", "name", "surname", "password", "123456789");

		var newProfile = UserProfile.CreateUser("Name", 36, PaymentMethod.CreditCard);

		newAccount.AddUserProfile(newProfile);

		newAccount.SelectUserProfile("Name");

		Assert.AreEqual(newProfile, newAccount.SelectedProfile);
	}

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