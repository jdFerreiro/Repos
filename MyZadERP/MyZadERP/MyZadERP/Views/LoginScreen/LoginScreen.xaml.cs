using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyZadERP.Views.LoginScreen.Views;

namespace MyZadERP.Views.LoginScreen
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginScreen : ContentPage, INavigationHandler
	{
	    public LoginScreen()
		{
		    InitializeComponent();
		    this.BindingContext = new LoginPageViewModel { NavigationHandler = this };
            this.Content = new LoginView();
        }

	    public void LoadView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.LoginView:
                    this.Content = new LoginView();
                    break;
                case ViewType.SignUpView:
                    this.Content = new SignUpView();
                    break;
                case ViewType.PasswordResetView:
                    this.Content = new PasswordResetView();
                    break;
            }
        }
        
        protected override bool OnBackButtonPressed()
        {
            var viewType = this.Content.GetType();
            if (viewType == typeof(SignUpView) || viewType == typeof(PasswordResetView))
            {
                this.Content = new LoginView();
                return true;
            }
            return base.OnBackButtonPressed();
        }
    }
}