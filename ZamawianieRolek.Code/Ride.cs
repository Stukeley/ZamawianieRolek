namespace ZamawianieRolek.Code
{
	public class Ride
	{
		public Skates Skates{ get; set; }
		public DateTime StartTime { get; set; }
		public DateTime FinishTime { get; set; }
		public int TurboTime { get; set; }
		public int Distance { get; set; }
		public float RidePrice { get; set; }

		private const float PricePerMinuteRatio = 0.10f;

		public float EvaluatePrice()
		{
			int time = EvaluateTime();
			RidePrice = time * PricePerMinuteRatio;

			return RidePrice;
		}

		public int EvaluateTime()
		{
			return (FinishTime - StartTime).Minutes;
		}
	}
}
