namespace ZamawianieRolek.Code.System;

/// <summary>
/// Klasa przedstawiająca przejazd, rozpoczęty i zakończony przez użytkownika.
/// </summary>
public class Ride
{
	/// <summary>
	/// Wrotki, które są wykorzystywane przez użytkownika do przejazdu.
	/// </summary>
	public Skates Skates{ get; set; }
	
	/// <summary>
	/// Data początkowa przejazdu.
	/// </summary>
	public DateTime StartTime { get; set; }
	
	/// <summary>
	/// Data końcowa przejazdu.
	/// </summary>
	public DateTime FinishTime { get; set; }
	
	/// <summary>
	/// Łączny czas spędzony przez użytkownika w trybie turbo.
	/// </summary>
	public int TurboTime { get; set; }
	
	/// <summary>
	/// Łączny dystans przejechany przez użytkownika w ramach jednego przejazdu.
	/// </summary>
	public int Distance { get; set; }
	
	/// <summary>
	/// Cena przejazdu.
	/// </summary>
	public float RidePrice { get; set; }
	
	/// <summary>
	/// Funkcja obliczająca koszt przejazdu na podstawie czasu jego rozpoczęcia, zakończenia oraz stałego przelicznika ceny.
	/// Funkcja ustawia wartość pola RidePrice.
	/// </summary>
	/// <returns>Obliczona cena przejazdu.</returns>
	public float EvaluatePrice()
	{
		const float PricePerMinuteRatio = 0.10f;
		
		int time = EvaluateTime();
		RidePrice = time * PricePerMinuteRatio;

		return RidePrice;
	}

	/// <summary>
	/// Funkcja obliczająca i zwracająca łączny czas przejazdu w minutach.
	/// </summary>
	/// <returns>Czas przejazdu w minutach, bez uwzględnienia części ułamkowej.</returns>
	public int EvaluateTime()
	{
		return (FinishTime - StartTime).Minutes;
	}
}