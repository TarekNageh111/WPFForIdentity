using JWTWPF_Client.Credentials;
using JWTWPF_Client.HelperManagers;
using RegistrationsLiberary.Registerations;
using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace JWTWPF_Client
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : RadWindow
    {
        List<AddRoolModel> roleList = new List<AddRoolModel>();
        public HomeView()
        {
            InitializeComponent();

            btnAddRole.Click += async (s, e) =>
            {
                AddRoolModel addRoolModel = new AddRoolModel()
                {
                    Email = CredentialManager.EmailUserLogined,
                    Role = txtRole.Text,
                };
                var res = await AuthManager.AddRoleToUserAsync(addRoolModel);
                if (res != null)
                    roleList.Add(res);
                gridRoles.ItemsSource = null;
                gridRoles.ItemsSource = roleList;
            };
            btnGetNames.Click += async (s, e) =>
            {
                var res = await AuthManager.GetNamesAsync();
                gridNames.ItemsSource = res;
            };
        }
        private void LoadGridRoles()
        {
            gridRoles.Columns.Add(new GridViewDataColumn
            {
                Header = "Email",
                Name = "Email",
                UniqueName = "Email",
                Width = new GridViewLength(0.3, GridViewLengthUnitType.Star),
                DataMemberBinding = new System.Windows.Data.Binding(path: "Email"),
                IsVisible = false
            });
            gridRoles.Columns.Add(new GridViewDataColumn
            {
                Header = "Role",
                Name = "Role",
                UniqueName = "Role",
                Width = new GridViewLength(0.6, GridViewLengthUnitType.Star),
                DataMemberBinding = new System.Windows.Data.Binding(path: "Role"),
            });
        }
    }
}
