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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryWSClient;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void textBoxPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void buttonLoin_Click(object sender, RoutedEventArgs e)
        {
            string nickName = this.textBoxNickName.Text;
            string password = this.textBoxPassword.Text;
            //  Ws request
            string id = "1234";
            if(id !=null)
            {
                MainWindow mw = Application.Current.MainWindow as MainWindow;
                mw.SetAdmin(true);

            }
            else
            {
                // message 
            }
        }
    }
}
