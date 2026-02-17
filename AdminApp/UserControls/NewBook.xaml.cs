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
          
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {

        }


       
        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; 
            this.Close();
        }

        private void buttonSelectImage_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
