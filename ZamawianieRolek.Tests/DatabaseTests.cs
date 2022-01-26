namespace ZamawianieRolek.Tests;

using System;
using System.Linq;
using NUnit.Framework;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

public class DatabaseTests
{
	[SetUp]
	public void Setup()
	{
		Seeding.SeedData();
	}

	[Test]
	public void AccountExistsAfterRegistration()
	{
		var account = Account.RegisterWithUserData("email@e.m", "name", "surname", "password", "123456789");

		var accountFromDatabase = Database.Accounts.FirstOrDefault(x => x.Email == "email@e.m");

		Assert.IsNotNull(accountFromDatabase);
		Assert.AreEqual(account, accountFromDatabase);
	}

	[Test]
	public void ShedExistsAfterAdd()
	{
		var shed = new Shed(5, 101, "Random Location");
		Database.AddShedToDatabase(shed);

		var shedFromDatabase = Database.Sheds.FirstOrDefault(x => x.Id == 101);

		Assert.IsNotNull(shedFromDatabase);
		Assert.AreEqual(shed, shedFromDatabase);
	}

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