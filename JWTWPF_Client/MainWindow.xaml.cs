using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace JWTWPF_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AssotiateAndRaiseEvents();
        }

        private void AssotiateAndRaiseEvents()
        {
            btnRegister.Click += (s, e) =>
            {
                RegisterView view = new RegisterView();
                view.ShowDialog();
            };
            btnLogin.Click += (s, e) =>
            {
                LoginView view = new LoginView();
                view.ShowDialog();
            };
        }
    }
}
