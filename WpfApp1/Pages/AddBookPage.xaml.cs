using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    public partial class AddBookPage : Page
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(ContentBox.Text))
            {
                MessageBox.Show("Введите текст книги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Book newBook = new Book()
            {
                Title = TitleBox.Text.Trim(),
                Description = DescBox.Text.Trim(),
                Content = ContentBox.Text,
                AuthorId = Core.CurrentUser.UserId,
                IsFrozen = false
            };

            Core.Context.Book.Add(newBook);
            Core.Context.SaveChanges();

            MessageBox.Show("Книга успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }
    }
}