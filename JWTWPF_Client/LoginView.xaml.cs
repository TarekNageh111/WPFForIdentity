using JWTWPF_Client.HelperManagers;
using RegistrationsLiberary.Registerations;
using Telerik.Windows.Controls;

namespace JWTWPF_Client
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : RadWindow
    {
        public LoginView()
        {
            InitializeComponent();
            btnLogin.Click += async (s, e) =>
            {
                LoginModel loginModel = new LoginModel()
                {
                    Email = txtEmail.Text,
                    Password = txtPassword.Password,
                };
                var res = await AuthManager.LoginAsync(loginModel);
                if (res is not null && !string.IsNullOrEmpty(res.Token))
                {
                    HomeView view = new HomeView();
                    view.ShowDialog();
                }
            };
        }
    }
}
