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


        internal BoardScreen(UserModel userModel)
        {
            InitializeComponent();
            this.userModel = userModel;
            boardScreenVM = new BoardScreenVM(userModel);
            this.DataContext = boardScreenVM;
        }

        /// <summary>
        /// Handles double-click events on a board row in the data grid.
        /// Opens the <see cref="TaskScreen"/> for the selected board and closes the current window.
        /// </summary>
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BoardModel selectedBoard = (BoardModel)((DataGridRow)sender).Item;
            TaskScreen taskScreen = new TaskScreen(userModel, selectedBoard);
            taskScreen.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Create Board button click event.
        /// Opens the <see cref="CreateBoardScreen"/> and closes the current window.
        /// </summary>
        private void CreateBoard_Click(object sender, RoutedEventArgs e)
        {
            CreateBoardScreen createBoardScreen = new CreateBoardScreen(userModel.Email);
            createBoardScreen.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Delete Board button click event.
        /// Deletes the selected board using the view model.
        /// </summary>
        private void DeleteBoard_Click(object sender, RoutedEventArgs e)
        {
            boardScreenVM.DeleteBoard(this.dataGridView.SelectedItem);
        }

        /// <summary>
        /// Handles the Logout button click event.
        /// Logs out the user and returns to the login screen if successful.
        /// </summary>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            if (boardScreenVM.Logout())
            {
                LoginScreen loginScreen = new LoginScreen();
                loginScreen.Show();
                this.Close();
            }
        }
    }
}
