using System.Globalization;
using System.Text;
using ZamawianieRolek.Code;

public class Program
{
	private static Account _currentAccount;

	private static bool IsLoggedIn
	{
		get
		{
			return _currentAccount != null;
		}
	}

	private static bool IsUserProfileSelected
	{
		get
		{
			return _currentAccount?.SelectedProfile != null;
		}
	}

	public static void Main()
	{
		Serialization.DeserializeDatabase();
		Seeding.SeedData();
		bool isRepeated = true;

		while (isRepeated)
		{
			Console.WriteLine("=========================================");
			if (IsLoggedIn)
			{
				Console.WriteLine($"ACCOUNT: {_currentAccount.Name} {_currentAccount.Surname}");
				if (IsUserProfileSelected)
				{
					Console.WriteLine($"USER PROFILE: {_currentAccount.SelectedProfile.Name}");
				}
				else
				{
					Console.WriteLine($"USER PROFILE: ---not selected---");
				}
			}
			else
			{
				Console.WriteLine($"ACCOUNT: ---not logged in---");
			}
			Console.WriteLine("=========================================");
			Console.WriteLine("CHOOSE AN OPTION:");
			Console.WriteLine("1  - Register a new account");
			Console.WriteLine("2  - Log in");
			Console.WriteLine("3  - Create a new user profile");
			Console.WriteLine("4  - Select a user profile");
			Console.WriteLine("=========================================");

			if (IsLoggedIn && IsUserProfileSelected)
			{
				Console.WriteLine("5  - Display all sheds");
				Console.WriteLine("6  - Lend skates");
				Console.WriteLine("7  - Finish ride");
				Console.WriteLine("=========================================");
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

				case "x":
					isRepeated = false;
					break;

				default:
					break;
			}

			Console.WriteLine("Press any key to continue.");
			Console.ReadKey();
		}

		Serialization.SerializeDatabase();
	}

	public static void RegisterNewUser()
	{
		string name, surname, email, phoneNumber, password, registerWithGoogle;
		var passwordBuilder = new StringBuilder();

		Console.WriteLine("Register with Google? [y/n]: ");
		registerWithGoogle = Console.ReadLine();

		if (registerWithGoogle.ToLower() == "y")
		{
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

	public static void LoginUser()
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
		}
		password = passwordBuilder.ToString();

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

	public static void CreateUserProfile()
	{
		if (!IsLoggedIn)
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

			Console.WriteLine("Type in your payment method (1 - credit card, else - paypal): ");
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

	public static void SelectUserProfile()
	{
		if (!IsLoggedIn)
		{
			Console.WriteLine("Log in first!");
		}
		else
		{
			if(_currentAccount.UserProfiles.Count == 0)
			{
				Console.WriteLine("There are no user profiles on this account. Create one first!");
				return;
			}

			string selectedName;
			UserProfile selectedProfile;

			Console.WriteLine("Available user profiles on this account:");

			foreach(UserProfile profile in _currentAccount.UserProfiles)
			{
				Console.WriteLine($"- {profile.Name}");
			}

			selectedName = Console.ReadLine();
			selectedProfile = _currentAccount.UserProfiles.FirstOrDefault(x => x.Name == selectedName);

			if(selectedProfile == null)
			{
				Console.WriteLine("There is no profile with this name!");
			}
			else
			{
				_currentAccount.SelectedProfile = selectedProfile;
			}
		}
	}

	public static void DisplayAllSheds()
	{
		if(!(IsLoggedIn && IsUserProfileSelected))
		{
			Console.WriteLine("Log in AND select user profile first!");
			return;
		}
		
		foreach (var shed in Database.Sheds)
		{
			Console.WriteLine(shed.ToString());
		}
	}

	public static void LendSkates()
	{
		if(!(IsLoggedIn && IsUserProfileSelected))
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

		}
		else
		{
			Console.WriteLine("Shed of this Id does not exist.");
		}
	}

	public static void FinishRide()
	{
		if(!(IsLoggedIn && IsUserProfileSelected))
		{
			Console.WriteLine("Log in AND select user profile first!");
			return;
			
		}
		if(_currentAccount.SelectedProfile.Ride == null)
		{
			Console.WriteLine("This profile isn't lending skates!");
			return;
		}

		_currentAccount.SelectedProfile.Ride.FinishTime = DateTime.Now;
		Console.WriteLine($"Ride price: {_currentAccount.SelectedProfile.Ride.EvaluatePrice()}");
		_currentAccount.SelectedProfile.Ride.Skates.IsLent = false;
		_currentAccount.SelectedProfile.Ride = null;
	}
	
}