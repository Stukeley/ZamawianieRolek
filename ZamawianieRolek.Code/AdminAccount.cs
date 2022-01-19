namespace ZamawianieRolek.Code
{
	public class AdminAccount : Account
	{
		public AdminAccount(string email, string name, string surname, string phoneNumber, string password) : base(email, name, surname, phoneNumber, password)
		{
		}
		
		public static Account RegisterAdminAccount(string email, string name, string surname, string password, string phoneNumber)
		{
			var account = new AdminAccount(email, name, surname, password, phoneNumber);

			Database.AddAccountToDatabase(account);

			return Account.LoginUser(email, password);
		}

		public void EditAccount()
		{

		}

		public void DeleteAccount()
		{

		}

		public void AddNewShed()
		{

		}

		public void AddNewSkates()
		{

		}

		public void ModifySkates()
		{

		}
	}
}
