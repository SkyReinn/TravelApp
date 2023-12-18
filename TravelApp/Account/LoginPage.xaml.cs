using System.Runtime.CompilerServices;

namespace TravelApp;

public partial class LoginPage : ContentPage
{
	private AccountDatabase accountDB;
	public LoginPage()
	{
		InitializeComponent();
		accountDB = new AccountDatabase();
	}

	private async void OnLoginButtonClicked(object sender, EventArgs e)
	{
		bool isValid = false;

		foreach(var account in await accountDB.GetAccountsAsync())
            if(account.Username == username.Text && account.Password == password.Text)
				isValid = true;

		if(isValid == true)
			await Navigation.PushAsync(new MainPage());
		else
			await DisplayAlert("Error", "The username or password is incorrect", "Ok");
	}

	private async void OnCreateAccountButtonClicked(object sender, EventArgs e)
	{
		var createAccountPage = new CreateAccountPage();

		createAccountPage.AccountCreated += async (source, createdAccount) =>
		{
			await accountDB.SaveAccountAsync(createdAccount);
		};

		await Navigation.PushAsync(createAccountPage);
	}
}