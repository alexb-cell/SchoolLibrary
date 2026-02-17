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
using ModelLibrary;
using LibraryModels;
using LibraryWSClient;


namespace AdminApp
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : UserControl
    {

        List<Book> books;
        NewBook newBook;    
        public BooksPage()
        {
            InitializeComponent();
            GetBookList();


        }

        private void ViewNewBookWindow()
        {
            if (this.newBook == null)
                this.newBook = new NewBook();
            this.newBook.Owner = Window.GetWindow(this);
            bool? ok= this.newBook.ShowDialog();
            this.newBook = null;
        }

        private async Task GetBookList()
        {
            ApiClient<List<Book>> apiClient = new ApiClient<List<Book>>();
            apiClient.Scheme = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5273;
            apiClient.Path = "api/Admin/GetBooks";
            this.books = await apiClient.GetAsync();
            listViewBooks.ItemsSource = this.books;
            this.DataContext = this.books;
        }

        private async void buttonAddNewBook_Click(object sender, RoutedEventArgs e)
        {
            ViewNewBookWindow();
        }
    }


}
