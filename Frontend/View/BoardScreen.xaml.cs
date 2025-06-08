using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.ViewModel;
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

namespace IntroSE.Kanban.Frontend.View
{
    /// <summary>
    /// Interaction logic for BoardScreen.xaml
    /// </summary>
    public partial class BoardScreen : Window
    {
        private UserModel userModel;
        private BoardScreenVM boardScreenVM;
        private int[] boards;


        internal BoardScreen(UserModel userModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            this.boards = userModel.Boards;
            /*
            var boards = Enumerable.Range(1, userModel.)
                                   .Select(i => $"Board {i}")
                                   .ToList();
            BoardsList.ItemsSource = boards;
            */
            boardScreenVM = new BoardScreenVM(userModel.Email);
            this.DataContext = boardScreenVM;
        }

        private void CreateBoard_Click(object sender, RoutedEventArgs e)
        {
            CreateBoardScreen createBoardScreen = new CreateBoardScreen(userModel.Email);
            createBoardScreen.Show();
            this.Close();
        }

        private void DeleteBoard_Click(object sender, RoutedEventArgs e)
        {
            // Logic to delete the selected board
            // This could involve calling a method in the BoardController to delete the board
            // and then updating the UI to reflect the deletion.
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (boardScreenVM.Logout())
            {
                LoginScreen loginScreen = new LoginScreen();
                loginScreen.Show();
                this.Close();
            }
        }

        private void ViewBoard_Click(object sender, RoutedEventArgs e)
        {
            // Logic to view the selected board
            // This could involve navigating to a new window that displays the board details.
        }
    }
}
