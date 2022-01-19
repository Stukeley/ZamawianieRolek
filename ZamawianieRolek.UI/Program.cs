using System.Globalization;
using System.Text;
using ZamawianieRolek.Code.System;
using ZamawianieRolek.Code.User;

namespace ZamawianieRolek.UI;

/// <summary>
/// Główna klasa interfejsu użytkownika, zawierająca funkcję Main oraz funkcje odpowiedzialne za komunikację aplikacji z użytkownikiem.
/// </summary>
public static class Program
{
	/// <summary>
	/// Obecnie aktywne konto użytkownika.
	/// </summary>
	private static Account _currentAccount;
	
	/// <summary>
	/// Właściwość zwracająca wartość true, jeżeli użytkownik jest zalogowany.
	/// </summary>
	private static bool _isLoggedIn => _currentAccount != null;
	
	/// <summary>
	/// Właściwość zwracająca wartość true, jeżeli użytkownik wybrał profil.
	/// </summary>
	private static bool _isUserProfileSelected => _currentAccount?.SelectedProfile != null;

	/// <summary>
	/// Punkt wejściowy aplikacji.
	/// Funkcja dokonuje serializacji oraz deserializacji bazy danych, oraz wyświetla użytkownikowi menu aplikacji.
	/// </summary>
	public static void Main()
	{
		Serialization.DeserializeDatabase();
		Seeding.SeedData();
		bool isRepeated = true;

		while (isRepeated)
		{
			Console.WriteLine("=========================================");
			if (_isLoggedIn)
			{
				Console.WriteLine($"ACCOUNT: {_currentAccount.Name} {_currentAccount.Surname}");
				if (_isUserProfileSelected)
				{
					Console.WriteLine($"USER PROFILE: {_currentAccount.SelectedProfile.Name}");
				}
				else
				{
					Console.WriteLine("USER PROFILE: ---not selected---");
				}
			}
			else
			{
				Console.WriteLine("ACCOUNT: ---not logged in---");
			}
			Console.WriteLine("=========================================");
			Console.WriteLine("CHOOSE AN OPTION:");
			Console.WriteLine("1  - Register a new account");
			Console.WriteLine("2  - Log in");
			Console.WriteLine("3  - Create a new user profile");
			Console.WriteLine("4  - Select a user profile");
			Console.WriteLine("=========================================");

			if (_isLoggedIn && _isUserProfileSelected)
			{
				Console.WriteLine("5  - Display all sheds");
				Console.WriteLine("6  - Lend skates");
				Console.WriteLine("7  - Finish ride");
				Console.WriteLine("=========================================");

				if (_currentAccount.SelectedProfile.Ride != null)
				{
					Console.WriteLine("8 - Activate Turbo mode");
					Console.WriteLine("=========================================");
				}
			}

			Console.WriteLine("X - Exit application");

			string chosenOption = Console.ReadLine();

			switch (chosenOption.ToLower())
			{
				case "1":
					RegisterNewUser();
					break;

				case "2":
					LoginUser();
					break;

				case "3":
					CreateUserProfile();
					break;

				case "4":
					SelectUserProfile();
					break;

				case "5":
					DisplayAllSheds();
					break;

				case "6":
					LendSkates();
					break;

				case "7":
					FinishRide();
					break;

				case "8":
					SwitchTurbo();
					break;

				case "x":
					isRepeated = false;
					break;
			}

			Console.WriteLine("Press any key to continue.");
			Console.ReadKey();
		}

		Serialization.SerializeDatabase();
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby rejestracji użytkownika.
	/// Wyświetla na ekranie konsoli stosowne informacje oraz pobiera od użytkownika niezbędne do rejestracji dane.
	/// </summary>
	private static void RegisterNewUser()
	{
		string name, surname, email, phoneNumber, password, registerWithGoogle;
		var passwordBuilder = new StringBuilder();

		Console.WriteLine("Register with Google? [y/n]: ");
		registerWithGoogle = Console.ReadLine();
		
		Console.WriteLine("Type in your email: ");
		email = Console.ReadLine();

		Console.WriteLine("Choose a password: ");
		while (true)
		{
			var key = Console.ReadKey(true);
			if (key.Key == ConsoleKey.Enter)
			{
				break;
			}
			passwordBuilder.Append(key.KeyChar);
		}
		password = passwordBuilder.ToString();
		Console.WriteLine();

		if (registerWithGoogle.ToLower() == "y")
		{
			try
			{
				var account = Account.RegisterWithGoogleAccount(email, password);
				
				_currentAccount = account;
				Console.WriteLine("Account created successfully!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		else
		{
			Console.WriteLine("What is your name?");
			name = Console.ReadLine();

			Console.WriteLine("What is your surname?");
			surname = Console.ReadLine();

			Console.WriteLine("Type in your phone number: ");
			phoneNumber = Console.ReadLine();

			try
			{
				var account = Account.RegisterWithUserData(email, name, surname, password, phoneNumber);

				_currentAccount = account;
				Console.WriteLine("Account created successfully!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby zalogowania użytkownika.
	/// Wyświetla na ekranie konsoli stosowne informacje oraz pobiera od użytkownika niezbędne do logowania dane.
	/// </summary>
	private static void LoginUser()
	{
		string email, password;
		var passwordBuilder = new StringBuilder();

		Console.WriteLine("Type in your email: ");
		email = Console.ReadLine();

		Console.WriteLine("Type in your password: ");
		while (true)
		{
			var key = Console.ReadKey(true);
			if (key.Key == ConsoleKey.Enter)
			{
				break;
			}
			passwordBuilder.Append(key.KeyChar);
			Console.Write("*");
		}
		password = passwordBuilder.ToString();
		Console.WriteLine();

		try
		{
			var account = Account.LoginUser(email, password);
			_currentAccount = account;
			Console.WriteLine("Logged in successfully!");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby stworzenia nowego profilu użytkownika.
	/// Wyświetla na ekranie konsoli stosowne informacje oraz pobiera od użytkownika niezbędne do utworzenia profilu dane.
	/// </summary>
	private static void CreateUserProfile()
	{
		if (!_isLoggedIn)
		{
			Console.WriteLine("Log in first!");
		}
		else
		{
			string name;
			float footSize;
			string paymentChoice;
			PaymentMethod paymentMethod;

			Console.WriteLine("What is your name?");
			name = Console.ReadLine();

			Console.WriteLine("What is your foot size?");
			footSize = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

			Console.WriteLine("Type in your payment method (1 - Credit Card, else - PayPal): ");
			paymentChoice = Console.ReadLine();
			if (paymentChoice == "1")
			{
				paymentMethod = PaymentMethod.CreditCard;
			}
			else
			{
				paymentMethod = PaymentMethod.PayPal;
			}

			var newUserProfile = UserProfile.CreateUser(name, footSize, paymentMethod);
			_currentAccount.AddUserProfile(newUserProfile);
		}
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby wyboru profilu użytkownika.
	/// Wyświetla na ekranie konsoli stosowne informacje oraz pobiera od użytkownika niezbędne dane.
	/// </summary>
	private static void SelectUserProfile()
	{
		if (!_isLoggedIn)
		{
			Console.WriteLine("Log in first!");
		}
		else
		{
			if (_currentAccount.UserProfiles.Count == 0)
			{
				Console.WriteLine("There are no user profiles on this account. Create one first!");
				return;
			}

			string selectedName;

			Console.WriteLine("Available user profiles on this account:");

			foreach (UserProfile profile in _currentAccount.UserProfiles)
			{
				Console.WriteLine($"- {profile.Name}");
			}

			selectedName = Console.ReadLine();

			try
			{
				_currentAccount.SelectUserProfile(selectedName);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby wyświetlenia na ekranie listy dostępnych wiat.
	/// </summary>
	private static void DisplayAllSheds()
	{
		if (!(_isLoggedIn && _isUserProfileSelected))
		{
			Console.WriteLine("Log in AND select user profile first!");
			return;
		}

		foreach (var shed in Database.Sheds)
		{
			Console.WriteLine(shed.ToString());
		}
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby rozpoczęcia wypożyczenia wrotek przez użytkownika.
	/// Wyświetla na ekranie konsoli stosowne informacje oraz pobiera od użytkownika niezbędne do wypożyczenia dane.
	/// </summary>
	private static void LendSkates()
	{
		if (!(_isLoggedIn && _isUserProfileSelected))
		{
			Console.WriteLine("Log in AND select user profile first!");
			return;
		}

		int shedId;
		string selectedSkatesModelName;

		Console.WriteLine("Choose shed Id: ");

		shedId = int.Parse(Console.ReadLine());

		var shed = Database.Sheds.FirstOrDefault(x => x.Id == shedId);

		if (shed != null)
		{
			Console.WriteLine("Choose skates model name from the list:");

			foreach (var skts in shed.AvailableSkates)
			{
				Console.WriteLine(skts.ToString());
			}

			selectedSkatesModelName = Console.ReadLine();
			var skates = shed.AvailableSkates.FirstOrDefault(x => x.ModelName == selectedSkatesModelName);

			var newRide = new Ride();
			newRide.Skates = skates;
			newRide.Skates.IsLent = true;
			newRide.StartTime = DateTime.Now;
			_currentAccount.SelectedProfile.Ride = newRide;

			shed.OpenLocker();
			shed.LockLocker();
		}
		else
		{
			Console.WriteLine("Shed of this Id does not exist.");
		}
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby oddania przez użytkownika wypożyczonych wrotek.
	/// Wyświetla na ekranie konsoli stosowne informacje, w tym koszt przejazdu.
	/// </summary>
	private static void FinishRide()
	{
		if (!(_isLoggedIn && _isUserProfileSelected))
		{
			Console.WriteLine("Log in AND select user profile first!");
			return;

		}
		if (_currentAccount.SelectedProfile.Ride == null)
		{
			Console.WriteLine("This profile isn't currently lending skates!");
			return;
		}

		_currentAccount.SelectedProfile.Ride.FinishTime = DateTime.Now;
		Console.WriteLine($"Ride price: {_currentAccount.SelectedProfile.Ride.EvaluatePrice()}");
		_currentAccount.SelectedProfile.Ride.Skates.IsLent = false;
		_currentAccount.SelectedProfile.Ride = null;
	}

	/// <summary>
	/// Funkcja wywoływana w przypadku próby przełączenia trybu turbo w wypożyczonych wrotkach.
	/// Jeżeli tryb turbo jest włączony, zostanie on wyłączony; jeżeli nie, wówczas zostanie on włączony.
	/// </summary>
	private static void SwitchTurbo()
	{
		if (_currentAccount.SelectedProfile.Ride == null)
		{
			Console.WriteLine("This profile isn't currently lending skates!");
			return;
		}
		
		var ride = _currentAccount.SelectedProfile.Ride;
		ride.Skates.Turbo();

		Console.WriteLine($"Turbo mode is now {(ride.Skates.IsTurboActive ? "ON" : "OFF")}");
	}
}