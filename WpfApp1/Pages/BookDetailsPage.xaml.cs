using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    public partial class BookDetailsPage : Page
    {
        private Book _currentBook;

        public BookDetailsPage(Book book)
        {
            InitializeComponent();
            _currentBook = book;
            DataContext = _currentBook;

            TitleTxt.Text = book.Title;
            AuthorTxt.Text = "Автор: " + (book.User?.DisplayName ?? "Неизвестен");
            DescTxt.Text = book.Description;
            ContentTxt.Text = book.Content;

            LoadReviews();
        }

        private void LoadReviews()
        {
            // Если таблицы Reviews нет - просто показываем пустой список
            ReviewsList.ItemsSource = null;
        }

        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция добавления в список временно недоступна", "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SendReview_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция отправки отзыва временно недоступна", "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция жалобы временно недоступна", "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}