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

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BoardModel selectedBoard = (BoardModel)((DataGridRow)sender).Item;
            TaskScreen taskScreen = new TaskScreen(userModel, selectedBoard);
            taskScreen.Show();
            this.Close();
        }

        private void CreateBoard_Click(object sender, RoutedEventArgs e)
        {
            CreateBoardScreen createBoardScreen = new CreateBoardScreen(userModel.Email);
            createBoardScreen.Show();
            this.Close();
        }

        private void DeleteBoard_Click(object sender, RoutedEventArgs e)
        {
            boardScreenVM.DeleteBoard(this.dataGridView.SelectedItem);
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
    }
}
