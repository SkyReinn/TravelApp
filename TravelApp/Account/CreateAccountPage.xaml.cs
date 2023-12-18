namespace TravelApp;

public partial class CreateAccountPage : ContentPage
{
	public event EventHandler<Account> AccountCreated;
	private Account account;
	public CreateAccountPage()
	{
		InitializeComponent();
		account = new Account();
	}
	private async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		account.Username = username.Text;
		account.Password = password.Text;

		AccountCreated?.Invoke(this, account);

		await Navigation.PopAsync();
	}
}