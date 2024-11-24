using JWTWPF_Client.HelperManagers;
using RegistrationsLiberary.Registerations;
using Telerik.Windows.Controls;

namespace JWTWPF_Client
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : RadWindow
    {
        public RegisterView()
        {
            InitializeComponent();
            btnLogin.Click += (s, e) =>
            {
                LoginView view = new LoginView();
                view.ShowDialog();
            };
            btnRegister.Click += async (s, e) =>
            {
                RegisterModel registerModel = new RegisterModel()
                {
                    Email = txtEmail.Text,
                    UserName = txtUserName.Text,
                    FullName = txtFullName.Text,
                    Password = txtPassword.Password
                };
                var res = await AuthManager.RegisterAsync(registerModel);
                if(res is not null && !string.IsNullOrEmpty(res.Token))
                {
                    LoginView view = new LoginView();
                    view.ShowDialog();
                }
            };
        }
    }
}
