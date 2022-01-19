namespace ZamawianieRolek.Code.System;

/// <summary>
/// Klasa przedstawiająca wiatę o danej lokalizacji, zawierającą wrotki, które można wypożyczyć.
/// </summary>
public class Shed
{
	/// <summary>
	/// Id wiaty.
	/// </summary>
	public int Id { get; private set; }
	
	/// <summary>
	/// Łączna ilość wrotek, które znajdują się w wiacie.
	/// </summary>
	public int TotalSkates { get; set; }
	
	/// <summary>
	/// Ilość wrotek, które zostały wypożyczone i są obecnie niedostępne.
	/// </summary>
	public int TakenSkates { get; set; }
	
	/// <summary>
	/// Lokalizacja wiaty.
	/// </summary>
	public string Localisation { get; set; }
	
	/// <summary>
	/// Lista zawierająca wszystkie dostępne do wypożyczenia w wiacie wrotki.
	/// </summary>
	public List<Skates> AvailableSkates{ get; set; }

	/// <summary>
	/// Domyślny konstruktor, inicijalizujący obiekt podanymi wartościami.
	/// </summary>
	/// <param name="totalSkates">Ilość wrotek, które znajdują się w nowopowstałej wiacie.</param>
	/// <param name="id">Id nowopowstałej wiaty.</param>
	/// <param name="localisation">Lokalizacja nowopowstałej wiaty.</param>
	public Shed(int totalSkates, int id, string localisation)
	{
		TotalSkates = totalSkates;
		Id = id;
		Localisation = localisation;

		AvailableSkates = new List<Skates>();
	}

	/// <summary>
	/// Funkcja otwierająca szafkę z wrotkami, wywoływana w momencie wypożyczenia wrotek przez użytkownika.
	/// </summary>
	public void OpenLocker()
	{
	}

	/// <summary>
	/// Funkcja zamykająca szafkę z wrotkami, wywoływana bezpośrednio po funkcji OpenLocker.
	/// </summary>
	public void LockLocker()
	{
	}

	/// <summary>
	/// Przeciążona funkcja, zwracająca reprezentację obiektu i wartości jego pól jako napis.
	/// </summary>
	/// <returns>Napis, przedstawiający wiatę, gotowy do wyświetlenia np. w oknie konsoli.</returns>
	public override string ToString()
	{
		return $"Id: {Id} - {Localisation}; Total Skates: {TotalSkates}, Taken Skates: {TakenSkates}";
	}
}