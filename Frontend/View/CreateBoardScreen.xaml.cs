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
    /// Interaction logic for CreateBoardScreen.xaml  
    /// </summary>  
    public partial class CreateBoardScreen : Window
    {
        private CreateBoardVM createBoardVM;
        private string email;
        public CreateBoardScreen(string email)
        {
            InitializeComponent();
            this.email = email;
            this.createBoardVM = new CreateBoardVM(email);
            this.DataContext = createBoardVM;
        }

        private void CreateBoard_Click(object sender, RoutedEventArgs e)
        {
            BoardModel? board = createBoardVM.CreateBoard();
            if (board != null)
            {
                BoardScreen boardScreen = new BoardScreen(new UserModel(this.email));
                boardScreen.Show();
                this.Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BoardScreen boardScreen = new BoardScreen(new UserModel(this.email));
            boardScreen.Show();
            this.Close();
        }
    }
}
