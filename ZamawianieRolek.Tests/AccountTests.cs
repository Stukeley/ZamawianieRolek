using System;
using NUnit.Framework;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

namespace ZamawianieRolek.Tests;

[TestFixture]
public class AccountTests
{
	[SetUp]
	public void Setup()
	{
		Seeding.SeedData();
	}

	[Test]
	[TestCase("asdf@g.c", "A", "A", "A", "123456789")]
	[TestCase("b@g.c", "B", "B", "B", "987654321")]
	[TestCase("zyx@g.c", "C", "A B C", "3njiweogqfubrfeqiuw", "111111111")]
	public void TestRegister_WorksForValidInput(string email, string name, string surname, string password, string phoneNumber)
	{
		try
		{
			var account = Account.RegisterWithUserData(email, name, surname, password, phoneNumber);

			Assert.NotNull(account);
		}
		catch (Exception)
		{
			Assert.Fail();
		}
	}

	[Test]
	[TestCase("asdf@g.c", "password")]
	[TestCase("b@g.c", "B")]
	[TestCase("zyx@g.c", "xyzeinfeio")]
	public void TestRegisterGoogle_WorksForValidInput(string email, string password)
	{
		try
		{
			var account = Account.RegisterWithGoogleAccount(email, password);

			Assert.NotNull(account);
		}
		catch (Exception)
		{
			Assert.Fail();
		}
	}
	
	[Test]
	[TestCase("asdf@g.c", "6", "A", "A", "123456789")]
	[TestCase("b@g.c", "B", "4", "B", "987654321")]
	[TestCase("zyx@g.c", "C", "A B C", "", "111111111")]
	[TestCase("zyx@g.c", "C", "A B C", "sdfa", "xd")]
	[TestCase("zyxg.c", "C", "A B C", "cxvbcb", "987654321")]
	[TestCase("", "C", "A B C", "cxvbcb", "987654321")]
	public void TestRegister_ThrowsForInvalidInput(string email, string name, string surname, string password, string phoneNumber)
	{
		try
		{
			var account = Account.RegisterWithUserData(email, name, surname, password, phoneNumber);
			Assert.Fail();
		}
		catch (Exception)
		{
			Assert.Pass();
		}
	}

	[Test]
	public void TestRegister_ThrowsForNullInput()
	{
		try
		{
			var account = Account.RegisterWithUserData(null, null, null, null, null);
			Assert.Fail();
		}
		catch (Exception)
		{
			Assert.Pass();
		}
	}

	[Test]
	[TestCase("email@e.m", "passw")]
	[TestCase("police@g.c", "random")]
	public void TestLogin_ThrowsWhenAccountNotFound(string email, string password)
	{
		try
		{
			var account = Account.LoginUser(email, password);
			Assert.Fail();
		}
		catch (Exception)
		{
			Assert.Pass();
		}
	}
}