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
using AdminApp.UserControls;
using AdminApp;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SartPage startPage;
        LoginPage loginPage;
        BooksPage booksPage;    
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
        }

        private void ViewStartPage()
        {
            if (this.startPage == null)
                this.startPage = new SartPage();
            this.frameMain.Content = this.startPage;
        }
        private void ViewLoginPage()
        {
            if (this.booksPage == null)
                this.booksPage = new BooksPage();
            this.frameMain.Content = this.booksPage;
        }
        private void ViewBookPage()
        {
            if (this.loginPage == null)
                this.loginPage = new LoginPage();
            this.frameMain.Content = this.loginPage;
        }

        private void hyperlinkLogin_Click(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }

        private void hyperlinkStartPage_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage();
        }

        private void hyperlinkBooks_Click(object sender, RoutedEventArgs e)
        {
            ViewLoginPage();
        }
    }
}