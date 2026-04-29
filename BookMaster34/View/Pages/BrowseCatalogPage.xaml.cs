using BookMaster34.Models;
using BookMaster34.View.Windows;
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

namespace BookMaster34.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BrowseCatalogPage.xaml
    /// </summary>
    public partial class BrowseCatalogPage : Page
    {
        private readonly List<Book> _books;
        // Создаем поле для хранения выбранной книги
        private Book _selectedBook;
        public BrowseCatalogPage()
        {
            InitializeComponent();
            _books = App.Get34Context().Books.ToList();
         
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchResultsGrid.Visibility = Visibility.Visible;
            string bookTitle = BookTitleTb.Text;
            string bookAuthors = BookAuthorsTb.Text;
            string bookSubjects = BookSubjectsTb.Text;
            if (string.IsNullOrWhiteSpace(bookTitle) &&
            string.IsNullOrWhiteSpace(bookAuthors) &&
            string.IsNullOrWhiteSpace(bookSubjects))
            {
                LoadData(_books);
            }
            else
            {
                List <Book> filteredBooks = _books.Where(book => book.Title.Contains(bookTitle,StringComparison.OrdinalIgnoreCase)&&
                book.Authors.Contains(bookAuthors, StringComparison.OrdinalIgnoreCase) &&
                book.Subjects.Contains(bookSubjects, StringComparison.OrdinalIgnoreCase)).ToList();
                LoadData(filteredBooks);
            }
        }

     
        private void LoadData(List<Book> booksList)
        {
            BookAuthorsLV.ItemsSource = booksList;
        }

        private void BookAuthorsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBook =(Book)BookAuthorsLV.SelectedItem;

            BookDetailsGrid.DataContext = _selectedBook;
            if (_selectedBook == null)
            {
                BookDetailsGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                BookDetailsGrid.Visibility = Visibility.Visible;
            }
        }

        private void BookAuthorsDetailsHl_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook != null)
            {
                BookAuthorsDetailsWindow bookAuthorsDetailsWindow = new BookAuthorsDetailsWindow(_selectedBook.BookAuthors);
                bookAuthorsDetailsWindow.ShowDialog();
            }
        }

        private void PreviousPageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CurrentPageTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
