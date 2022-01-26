using ZamawianieRolek.Code.System;

namespace ZamawianieRolek.Code.User;

/// <summary>
/// Klasa przedstawiająca profil użytkownika w ramach konta.
/// </summary>
public class UserProfile
{
	/// <summary>
	/// Nazwa profilu użytkownika.
	/// </summary>
	public string Name { get; private set; }
	
	/// <summary>
	/// Rozmiar stopy użytkownika.
	/// </summary>
	public float FootSize { get; private set; }
	
	/// <summary>
	/// Metoda płatności podłączona do profilu.
	/// </summary>
	public PaymentMethod PaymentMethod{ get; private set; }
	
	/// <summary>
	/// Obecnie aktywny przejazd użytkownika.
	/// </summary>
	public Ride Ride{ get; set; }

	/// <summary>
	/// Domyślny, prywatny konstruktor, inicijalizujący obiekt podanymi wartościami.
	/// </summary>
	/// <param name="name">Nazwa nowopowstałego profilu.</param>
	/// <param name="footSize">Rozmiar stopy użytkownika.</param>
	/// <param name="paymentMethod">Wybrany przez użytkownika sposób płatności.</param>
	private UserProfile(string name, float footSize, PaymentMethod paymentMethod)
	{
		Name = name;
		FootSize = footSize;
		PaymentMethod = paymentMethod;
	}

	/// <summary>
	/// Funkcja tworząca nowy profil użytkownika za pomocą danych wpisanych przy pomocy interfejsu użytkownika.
	/// </summary>
	/// <param name="name">Nazwa podana przy tworzeniu profilu.</param>
	/// <param name="footSize">Rozmiar stopy podany przy tworzeniu profilu.</param>
	/// <param name="paymentMethod">Metoda płatności wybrana przy tworzeniu profilu.</param>
	/// <returns>Utworzony profil użytkownika.</returns>
	public static UserProfile CreateUser(string name, float footSize, PaymentMethod paymentMethod)
	{
		if (name?.Length == 0)
		{
			throw new Exception("User Profile name cannot be null or empty!");
		}

		var userProfile = new UserProfile(name, footSize, paymentMethod);

		return userProfile;
	}
}