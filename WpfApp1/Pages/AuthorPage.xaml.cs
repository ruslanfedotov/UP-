using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    public partial class AuthorPage : Page
    {
        public AuthorPage()
        {
            InitializeComponent();
            LoadAuthorBook();
        }

        private void LoadAuthorBook()
        {
            if (Core.CurrentUser == null) return;

            var allAuthorBook = Core.Context.Book
                .Where(b => b.AuthorId == Core.CurrentUser.UserId)
                .ToList();

            ActiveBookGrid.ItemsSource = allAuthorBook.Where(b => b.IsFrozen == false).ToList();
            FrozenBookGrid.ItemsSource = allAuthorBook.Where(b => b.IsFrozen == true).ToList();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBookPage());
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            int bookId = (int)((Button)sender).Tag;
            MessageBox.Show($"Переход к редактированию книги #{bookId}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Appeal_Click(object sender, RoutedEventArgs e)
        {
            int bookId = (int)((Button)sender).Tag;

            // Проверяем, существует ли таблица UnfreezeRequest в модели
            MessageBox.Show("Функция обжалования временно недоступна", "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}