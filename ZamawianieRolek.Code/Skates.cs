namespace ZamawianieRolek.Code
{
	public class Skates
	{
		public string ModelName { get; set; }
		public byte[] Image { get; set; }
		public float Size { get; set; }
		public bool Condition { get; set; }
		public bool IsTurboActive { get; set; }
		public float BatteryPercentage { get; set; }
		public bool IsLent { get; set; }

		public Skates(string modelName, byte[] image, float size, bool condition)
		{
			ModelName = modelName;
			Image = image;
			Size = size;
			Condition = condition;
		}

		public void Turbo(bool mode)
		{
			IsTurboActive = mode;
		}

		public override string ToString()
		{
			return $"{ModelName} - size: {Size}, battery: {BatteryPercentage}.";
		}
	}
}
