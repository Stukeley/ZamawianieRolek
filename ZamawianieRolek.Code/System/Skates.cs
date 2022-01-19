namespace ZamawianieRolek.Code.System;

/// <summary>
/// Klasa przedstawiająca wrotki, które można wypożyczyć w wiacie.
/// </summary>
public class Skates
{
	/// <summary>
	/// Nazwa modelu wrotek.
	/// </summary>
	public string ModelName { get; private set; }
	
	/// <summary>
	/// Obraz jako tablica bajtów, przedstawiający wrotki.
	/// </summary>
	public byte[] Image { get; private set; }
	
	/// <summary>
	/// Rozmiar wrotek.
	/// </summary>
	public float Size { get; private set; }
	
	/// <summary>
	/// Stan wrotek.
	/// </summary>
	public bool Condition { get; set; }
	
	/// <summary>
	/// Zmienna informująca, czy w danym momencie aktywny jest tryb turbo.
	/// </summary>
	public bool IsTurboActive { get; set; }
	
	/// <summary>
	/// Stan baterii wrotek - liczba z przedziału od 0.00 do 1.00.
	/// </summary>
	public float BatteryPercentage { get; set; }
	
	/// <summary>
	/// Zmienna informująca, czy wrotki są w danym momencie wypożyczone.
	/// </summary>
	public bool IsLent { get; set; }

	/// <summary>
	/// Domyślny konstruktor, inicijalizujący obiekt podanymi wartościami.
	/// Bateria jest początkowo ustawiana na wartość 1.00.
	/// </summary>
	/// <param name="modelName">Nazwa modelu nowopowstałych wrotek.</param>
	/// <param name="image">Zdjęcie nowopowstałych wrotek.</param>
	/// <param name="size">Rozmiar nowopowstałych wrotek.</param>
	/// <param name="condition">Stan nowopowstałych wrotek.</param>
	public Skates(string modelName, byte[] image, float size, bool condition)
	{
		ModelName = modelName;
		Image = image;
		Size = size;
		Condition = condition;
		BatteryPercentage = 1.0f;
	}

	/// <summary>
	/// Funkcja przełączająca tryb turbo we wrotkach.
	/// Jeżeli turbo jest aktywne, wówczas zostanie wyłączone; jeżeli nie, zostanie włączone.
	/// </summary>
	public void Turbo()
	{
		IsTurboActive = !IsTurboActive;
	}

	/// <summary>
	/// Przeciążona funkcja, zwracająca reprezentację obiektu i wartości jego pól jako napis.
	/// </summary>
	/// <returns>Napis, przedstawiający wrotki, gotowy do wyświetlenia np. w oknie konsoli.</returns>
	public override string ToString()
	{
		return $"{ModelName} - size: {Size}, battery: {BatteryPercentage}.";
	}
}