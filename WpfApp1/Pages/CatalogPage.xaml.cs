using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp1.Pages
{
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
            LoadGenres();
            LoadBooks();
        }

        private void LoadGenres()
        {
            // Проверяем, существует ли свойство Genres в контексте
            // Если нет - закомментируйте этот метод
        }

        private void LoadBooks()
        {
            var books = Core.Context.Book
                .Where(b => b.IsFrozen == false)
                .OrderBy(b => b.Title)
                .ToList();
            BookListView.ItemsSource = books;
        }

        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Core.Context == null) return;

            var books = Core.Context.Book.Where(b => b.IsFrozen == false).ToList();

            if (SearchTextBox != null && !string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                string search = SearchTextBox.Text.ToLower();
                books = books.Where(b => b.Title.ToLower().Contains(search)).ToList();
            }

            BookListView.ItemsSource = books;
        }

        private void BookListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedBook = BookListView.SelectedItem as Book;
            if (selectedBook != null)
            {
                NavigationService.Navigate(new BookDetailsPage(selectedBook));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FilterChanged(null, null);
        }
    }
}