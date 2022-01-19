namespace ZamawianieRolek.Code
{
	public class Account
	{
		public string Email { get; set; }
		public int MoneyLeft { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public List<UserProfile> UserProfiles { get; set; }
		public UserProfile SelectedProfile { get; set; }

		public Account(string email, string name, string surname, string phoneNumber, string password)
		{
			Email = email;
			Name = name;
			Surname = surname;
			PhoneNumber = phoneNumber;
			Password = password;

			UserProfiles = new List<UserProfile>();
			MoneyLeft = 0;
		}

		public static Account LoginUser(string email, string password)
		{
			var databaseAccount = Database.Accounts.FirstOrDefault(x => x.Email == email);

			if (databaseAccount is null)
			{
				throw new Exception("Account with this email not found.");
			}

			if (databaseAccount.Password == password)
			{
				return databaseAccount;
			}

			throw new Exception("Incorrect password.");
		}

		public static Account RegisterWithUserData(string email, string name, string surname, string password, string phoneNumber)
		{
			var account = new Account(email, name, surname, password, phoneNumber);

			Database.AddAccountToDatabase(account);

			return LoginUser(email, password);
		}

		public static Account RegisterWithGoogleAccount(string email, string password)
		{
			var rng = new Random();
			string name = "Name" + rng.Next(10000);
			string surname = "Surname" + rng.Next(10000);
			string phoneNumber = "123456789";

			var account = new Account(email, name, surname, password, phoneNumber);

			Database.AddAccountToDatabase(account);

			return LoginUser(email, password);
		}

		public void AddUserProfile(UserProfile userProfile)
		{
			UserProfiles.Add(userProfile);
			SelectedProfile = userProfile;
		}

		public void SelectUserProfile(string profileName)
		{
			var selectedProfile = UserProfiles.FirstOrDefault(x => x.Name == profileName);

			if (selectedProfile is null)
			{
				throw new Exception("User profile with that name doesn't exist.");
			}

			SelectedProfile = selectedProfile;
		}
	}
}
