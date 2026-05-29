using BookMaster34.AppData;
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
        //Создаем контроллер пагинации 
        private readonly PaginationController _paginationController = new();
        public BrowseCatalogPage()
        {
            InitializeComponent();
            //
            _paginationController.Load(App.Get34Context().Books.ToList());
            RefreshUI();

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
                RefreshUI();
            }
            else
            {
                List<Book> filteredBooks = _books.Where(book => book.Title.Contains(bookTitle, StringComparison.OrdinalIgnoreCase) &&
                book.Authors.Contains(bookAuthors, StringComparison.OrdinalIgnoreCase) &&
                book.Subjects.Contains(bookSubjects, StringComparison.OrdinalIgnoreCase)).ToList();
                
                RefreshUI();
            }
        }


        

        private void BookAuthorsLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedBook = (Book)BookAuthorsLV.SelectedItem;

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
            _paginationController.GoToPage(_paginationController.CurrentPage - 1);
            RefreshUI();
        }

        private void CurrentPageTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(CurrentPageTb.Text, out int page))
            {
                _paginationController.CurrentPage = page;
                RefreshUI();
            }
        }

        private void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            _paginationController.GoToPage(_paginationController.CurrentPage + 1);
            RefreshUI();
        }
    
    public void RefreshUI()
        {
            BookAuthorsLV.ItemsSource = _paginationController.GetCurrentPage();
            TotalBooksTbl.Text = $"Найдено {_paginationController.BooksCount} книг";
            TotalPagesTbl.Text= $"из {_paginationController.TotalPages}";
            CurrentPageTb.Text = _paginationController.CurrentPage.ToString();

            PreviousPageBtn.IsEnabled = _paginationController.CanGoPrevious;
            NextPageBtn.IsEnabled = _paginationController.CanGoNext; 
        }
    }
}
