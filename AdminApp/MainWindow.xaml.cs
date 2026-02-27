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
        AuthorsPage authorsPage;
        GenresPage genresPage;
        ReaderPage readerPage;
        BorrowsPage borrowsPage;

        bool isLogin;

        public void SetAdmin(bool isadmin)
        {
            this.isLogin = isadmin;
            HyperlinkState();
            if (this.isLogin == true)
                ViewStartPage();
        }
        public MainWindow()
        {
            InitializeComponent();
            ViewStartPage();
            this.isLogin = false;
            HyperlinkState();
        }
        private void HyperlinkState()
        {
            this.hyperlinkAuthors.IsEnabled = this.isLogin;
            this.hyperlinkBooks.IsEnabled = this.isLogin;
            this.hyperlinkBorrows.IsEnabled = this.isLogin;
            this.hyperlinkReaders.IsEnabled = this.isLogin;
        }
        private void ViewStartPage()
        {
            if (this.startPage == null)
                this.startPage = new SartPage();
            this.frameMain.Content = this.startPage;
        }
        private void ViewLoginPage()
        {
            if (this.loginPage == null)
                this.loginPage = new LoginPage();
            this.frameMain.Content = this.loginPage;
        }
        private void ViewBookPage()
        {
            if (this.booksPage == null)
                this.booksPage = new BooksPage();
            this.frameMain.Content = this.booksPage;
        }

        private void ViewAuthorsPage()
        {
            if (this.authorsPage == null)
                this.authorsPage = new AuthorsPage();
            this.frameMain.Content = this.authorsPage;
        }

        private void ViewGenresPage()
        {
            if (this.genresPage == null)
                this.genresPage = new GenresPage();
            this.frameMain.Content = this.genresPage;
        }

        private void ViewReaderssPage()
        {
            if (this.readerPage == null)
                this.readerPage = new ReaderPage();
            this.frameMain.Content = this.readerPage;
        }

        private void ViewBorrowsPage()
        {
            if (this.borrowsPage == null)
                this.borrowsPage = new BorrowsPage();
            this.frameMain.Content = this.borrowsPage;
        }

        private void hyperlinkLogin_Click(object sender, RoutedEventArgs e)
        {
            if(this.isLogin==false)
            {

                ViewLoginPage();
                this.hyperlinkLogin.Inlines.Clear();
                this.hyperlinkLogin.Inlines.Add("Logout");
            }
            
            else
            {
                this.isLogin = false;
                this.hyperlinkLogin.Inlines.Clear();
                this.hyperlinkLogin.Inlines.Add("Login");
                HyperlinkState();
            }
        }

        private void hyperlinkStartPage_Click(object sender, RoutedEventArgs e)
        {
            ViewStartPage();
        }

        private void hyperlinkBooks_Click(object sender, RoutedEventArgs e)
        {
            ViewBookPage();
        }

        private void hyperlinkAuthors_Click(object sender, RoutedEventArgs e)
        {
            ViewAuthorsPage();
        }

        private void hyperLinkGanres_Click(object sender, RoutedEventArgs e)
        {
            ViewGenresPage();
        }

        private void hyperlinkReaders_Click(object sender, RoutedEventArgs e)
        {
            ViewReaderssPage();
        }

        private void hyperlinkBorrows_Click(object sender, RoutedEventArgs e)
        {
            ViewBorrowsPage();
        }

        private void hyperlinkExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}