using LibraryModels;
using LibraryWSClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.IO;

namespace AdminApp
{
    /// <summary>
    /// Interaction logic for NewBook.xaml
    /// </summary>
    public partial class NewBook : Window
    {
       
        NewBookViewModel  newBookViewModel;
        string imagePath;
      
        public NewBook()
        {
            InitializeComponent();
            GetNewbookViewModel();
        }

        private async Task GetNewbookViewModel()
        {
            ApiClient<NewBookViewModel> apiClient = new ApiClient<NewBookViewModel>();
            apiClient.Scheme = "http";
            apiClient.Host = "localhost";
            apiClient.Port = 5273;
            apiClient.Path = "api/Admin/GetNewBookViewModel";
            this.newBookViewModel = await apiClient.GetAsync();
            this.newBookViewModel.Book = new Book();
            this.listBoxAuthors.ItemsSource = this.newBookViewModel.Authors;
            this.listBoxGenres.ItemsSource= this.newBookViewModel.Genres;
            this.DataContext = this.newBookViewModel;
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; 
            this.Close();
        }

        private void buttonSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Only(*.jpg, *.png, *.gif)|*.jpg; *.png; *.gif";
            bool? dialogResult= openFileDialog.ShowDialog();
            if(dialogResult==true)
            {
                string fileName = openFileDialog.FileName;
                imagePath = fileName;
                Uri uri = new Uri(fileName);
                BitmapImage bitmapImage = new BitmapImage(uri);
                imageBook.Source = bitmapImage;
                this.textBlockSelectImage.Visibility = Visibility.Hidden;
            }
        }

        private  async void buttonAddBook_Click(object sender, RoutedEventArgs e)
        {
            bool ok = false;
            NewBookViewModel newBook = new NewBookViewModel();
            newBook.Book = new Book();
            newBook.Book.BookId = "0";
            newBook.Book.BookName = textBoxBookName.Text;
            newBook.Book.BookDescription = textBoxBookDescription.Text;
            newBook.Book.BookImage = System.IO.Path.GetExtension(this.imagePath);
            newBook.Genres = listBoxGenres.SelectedItems.Cast<Ganre>().ToList<Ganre>();
            newBook.Authors = listBoxAuthors.SelectedItems.Cast<Author>().ToList<Author>();
            newBook.Book.Validate();
            if( newBook.Book.HasErrors == false)
            {
                ApiClient<NewBookViewModel> apiClient = new ApiClient<NewBookViewModel>();
                apiClient.Scheme = "http";
                apiClient.Host = "localhost";
                apiClient.Port = 5273;
                apiClient.Path = "api/Admin/AddNewBook";
                Stream stream = new FileStream(this.imagePath,
                                                FileMode.Open,
                                                FileAccess.Read);
                ok = await apiClient.PostAsync(newBook,stream);
            }
            if(ok==true)
            {
                MessageBox.Show("New book have been addad");
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
