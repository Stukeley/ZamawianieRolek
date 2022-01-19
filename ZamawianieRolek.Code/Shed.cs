namespace ZamawianieRolek.Code
{
	public class Shed
	{
		public int TotalSkates { get; set; }
		public int TakenSkates { get; set; }
		public int Id { get; set; }
		public string Localisation { get; set; }
		public List<Skates> AvailableSkates{ get; set; }

		public Shed(int totalSkates, int id, string localisation)
		{
			TotalSkates = totalSkates;
			Id = id;
			Localisation = localisation;

			AvailableSkates = new List<Skates>();
		}

		public void OpenLocker()
		{

		}

		public void LockLocker()
		{

		}

		public override string ToString()
		{
			return $"{Id} - {Localisation}; Total Skates: {TotalSkates}, Taken Skates: {TakenSkates}";
		}
	}
}
