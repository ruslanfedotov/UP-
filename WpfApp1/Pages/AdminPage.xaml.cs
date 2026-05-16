using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace WpfApp1.Pages
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            UserGrid.ItemsSource = Core.Context.User.Include("Roles").ToList();
            ComplaintsGrid.ItemsSource = Core.Context.Complaints.Include("User").ToList();
            AuthorRequestsGrid.ItemsSource = Core.Context.RoleRequest
                .Include("User")
                .Where(r => r.Status == "На рассмотрении")
                .ToList();
        }

        private void AcceptAuthor_Click(object sender, RoutedEventArgs e)
        {
            var request = (sender as Button)?.DataContext as RoleRequest;
            if (request != null)
            {
                request.User.RoleId = 2;
                request.Status = "Принята";
                Core.Context.SaveChanges();
                LoadData();
                MessageBox.Show($"Пользователь {request.User.DisplayName} теперь автор!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RejectRequest_Click(object sender, RoutedEventArgs e)
        {
            var request = (sender as Button)?.DataContext as RoleRequest;
            if (request != null)
            {
                request.Status = "Отклонена";
                Core.Context.SaveChanges();
                LoadData();
                MessageBox.Show("Заявка отклонена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ToggleFreeze_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.DataContext as User;

            if (user != null)
            {
                if (user.UserId == Core.CurrentUser.UserId)
                {
                    MessageBox.Show("Вы не можете заморозить свою учетную запись!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                user.IsFrozen = !user.IsFrozen;

                try
                {
                    Core.Context.SaveChanges();
                    LoadData();
                    string status = user.IsFrozen ? "заморожен" : "разморожен";
                    MessageBox.Show($"Пользователь {user.DisplayName} {status}.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteComplaint_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)((Button)sender).Tag;
            var complaint = Core.Context.Complaints.FirstOrDefault(c => c.ComplaintId == id);

            if (complaint != null)
            {
                var result = MessageBox.Show("Удалить жалобу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Core.Context.Complaints.Remove(complaint);
                    Core.Context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Жалоба удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}