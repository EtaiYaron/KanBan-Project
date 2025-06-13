using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IntroSE.Kanban.Frontend.ViewModel;
using IntroSE.Kanban.Frontend.Model;

namespace IntroSE.Kanban.Frontend.View
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        private LoginVM loginVM;
        public LoginScreen()
        {
            InitializeComponent();
            this.loginVM = new LoginVM();
            this.DataContext = loginVM;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserModel? user = loginVM.Login();
            if (user != null)
            {
                BoardScreen boardScreen = new BoardScreen(user);
                boardScreen.Show();
                this.Close();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserModel? user = loginVM.Register();
            if (user != null)
            {
                BoardScreen boardScreen = new BoardScreen(user);
                boardScreen.Show();
                this.Close();
            }
        }
    }
}
