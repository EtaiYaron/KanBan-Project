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
using IntroSE.Kanban.Frontend.Model;
using IntroSE.Kanban.Frontend.ViewModel;

namespace IntroSE.Kanban.Frontend.View
{
    /// <summary>
    /// Interaction logic for TaskScreen.xaml
    /// </summary>
    public partial class TaskScreen : Window
    {
        private UserModel userModel;
        private BoardModel boardModel;
        private TasksVM taskScreenVM;

        internal TaskScreen(UserModel user, BoardModel board)
        {
            InitializeComponent();
            this.boardModel = board;
            this.userModel = user;
            taskScreenVM = new TasksVM(boardModel);
            this.DataContext = taskScreenVM;
        }

        /// <summary>
        /// Handles the Back button click event.
        /// Closes the current task screen window.
        /// </summary>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the window closing event.
        /// Opens the board screen for the user when the task screen is closed.
        /// </summary>
        private void TaskScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BoardScreen boardScreen = new BoardScreen(new UserModel(userModel.Email));
            boardScreen.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
