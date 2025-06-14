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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BoardScreen boardScreen = new BoardScreen(new UserModel(userModel.Email));
            boardScreen.Show();
            this.Close();
        }

        private void TaskScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BoardScreen boardScreen = new BoardScreen(new UserModel(userModel.Email));
            boardScreen.Show();
        }
    }
}
