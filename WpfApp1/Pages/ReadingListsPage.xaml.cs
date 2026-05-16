using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    public partial class ReadingListsPage : Page
    {
        public ReadingListsPage()
        {
            InitializeComponent();
            StatusComboBox.SelectedIndex = 0;
            RefreshData();
        }

        private void RefreshData()
        {
            // Если таблицы ReadingLists нет - показываем пустой список
            if (Core.CurrentUser == null) return;

            EmptyStateBorder.Visibility = Visibility.Visible;
            CountTextBlock.Text = "Всего: 0 книг";
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void MoveToRead_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция временно недоступна", "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция временно недоступна", "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}