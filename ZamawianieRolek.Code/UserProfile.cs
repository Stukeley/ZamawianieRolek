namespace ZamawianieRolek.Code
{
	public class UserProfile
	{
		public string Name { get; set; }
		public float FootSize { get; set; }
		public PaymentMethod PaymentMethod{ get; set; }

		public UserProfile(string name, float footSize, PaymentMethod paymentMethod)
		{
			Name = name;
			FootSize = footSize;
			PaymentMethod = paymentMethod;
		}

		public static UserProfile CreateUser(string name, float footsize, PaymentMethod paymentMethod)
		{
			var userProfile = new UserProfile(name, footsize, paymentMethod);

			return userProfile;
		}
	}
}
