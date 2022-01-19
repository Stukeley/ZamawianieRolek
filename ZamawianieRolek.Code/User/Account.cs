using ZamawianieRolek.Code.System;

namespace ZamawianieRolek.Code.User;

/// <summary>
/// Klasa przedstawiająca konto użytkownika.
/// </summary>
public class Account
{
	/// <summary>
	/// Email użytkownika, podany przy rejestracji.
	/// </summary>
	public string Email { get; set; }
	
	/// <summary>
	/// Ilość pieniędzy pozostałych na koncie użytkownika.
	/// </summary>
	public int MoneyLeft { get; set; }
	
	/// <summary>
	/// Imię użytkownika.
	/// </summary>
	public string Name { get; set; }
	
	/// <summary>
	/// Nazwisko użytkownika.
	/// </summary>
	public string Surname { get; set; }
	
	/// <summary>
	/// Numer telefonu użytkownika.
	/// </summary>
	public string PhoneNumber { get; set; }
	
	/// <summary>
	/// Hasło użytkownika.
	/// </summary>
	public string Password { get; set; }
	
	/// <summary>
	/// Lista zawierająca profile użytkownika utworzone w ramach jednego konta.
	/// </summary>
	public List<UserProfile> UserProfiles { get; set; }
	
	/// <summary>
	/// Zmienna przechowująca obecnie aktywny profil użytkownika.
	/// </summary>
	public UserProfile SelectedProfile { get; set; }

	/// <summary>
	/// Domyślny, prywatny konstruktor, inicijalizujący obiekt podanymi wartościami.
	/// </summary>
	/// <param name="email">Email nowopowstałego użytkownika.</param>
	/// <param name="name">Imię nowopowstałego użytkownika.</param>
	/// <param name="surname">Nazwisko nowopowstałego użytkownika.</param>
	/// <param name="phoneNumber">Numer telefonu nowopowstałego użytkownika.</param>
	/// <param name="password">Hasło nowopowstałego użytkownika.</param>
	private Account(string email, string name, string surname, string phoneNumber, string password)
	{
		Email = email;
		Name = name;
		Surname = surname;
		PhoneNumber = phoneNumber;
		Password = password;

		UserProfiles = new List<UserProfile>();
		MoneyLeft = 0;
	}

	/// <summary>
	/// Funkcja dokonująca zalogowania użytkownika.
	/// Podany adres email wyszukiwany jest w bazie danych, a następnie sprawdzane jest, czy podane hasło zgadza się z hasłem zapisanym w bazie danych.
	/// </summary>
	/// <param name="email">Email podany podczas próby logowania.</param>
	/// <param name="password">Hasło podane podczas próby logowania.</param>
	/// <returns>Odnaleziony w bazie danych użytkownik, jeżeli zgadzają się hasła.</returns>
	/// <exception cref="Exception">Wyjątek rzucany gdy użytkownik o podanym adresie email nie istnieje, lub gdy podane hasło jest niewłaściwe.</exception>
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

	/// <summary>
	/// Funkcja rejestrująca nowego użytkownika za pomocą danych wpisanych przy pomocy interfejsu użytkownika.
	/// </summary>
	/// <param name="email">Email podany przy rejestracji.</param>
	/// <param name="name">Imię podane przy rejestracji.</param>
	/// <param name="surname">Nazwisko podane przy rejestracji.</param>
	/// <param name="password">Hasło podane przy rejestracji.</param>
	/// <param name="phoneNumber">Numer telefonu podany przy rejestracji.</param>
	/// <returns></returns>
	public static Account RegisterWithUserData(string email, string name, string surname, string password, string phoneNumber)
	{
		var account = new Account(email, name, surname, phoneNumber, password);

		Database.AddAccountToDatabase(account);

		return LoginUser(email, password);
	}

	/// <summary>
	/// Funkcja rejestrująca nowego użytkownika za pomocą konta Google.
	/// Użytkownik podaje jedynie adres email i hasło, pozostałe niezbędne wartości są generowane automatycznie.
	/// </summary>
	/// <param name="email">Email podany przy rejestracji.</param>
	/// <param name="password">Hasło podane przy rejestracji.</param>
	/// <returns></returns>
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

	/// <summary>
	/// Funkcja dodająca nowy profil użytkownika do konta, oraz ustawiająca go jako obecnie wybrany profil.
	/// </summary>
	/// <param name="userProfile">Profil, który należy dodać do konta.</param>
	public void AddUserProfile(UserProfile userProfile)
	{
		UserProfiles.Add(userProfile);
		SelectedProfile = userProfile;
	}

	/// <summary>
	/// Funkcja wybierająca profil użytkownika o podanej nazwie jako obecnie wybrany profil.
	/// </summary>
	/// <param name="profileName">Nazwa profilu, który ma zostać wybrany.</param>
	/// <exception cref="Exception">Wyjątek rzucany gdy użytkownik poda nazwę profilu, która nie istnieje.</exception>
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