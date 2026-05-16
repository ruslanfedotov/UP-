using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadUserData();
            LoadUserReviews();
        }

        private void LoadUserData()
        {
            if (Core.CurrentUser != null)
            {
                NameTextBlock.Text = Core.CurrentUser.DisplayName;
                LoginTextBlock.Text = $"Логин: {Core.CurrentUser.Login}";
                EmailTextBlock.Text = $"Email: {Core.CurrentUser.Email}";

                string roleName = Core.CurrentUser.RoleId == 1 ? "Читатель" :
                                  Core.CurrentUser.RoleId == 2 ? "Автор" : "Администратор";
                RoleTextBlock.Text = $"Роль: {roleName}";

                if (Core.CurrentUser.RoleId == 2)
                {
                    ApplyAuthorButton.Visibility = Visibility.Collapsed;
                    ApplyAuthorButton.Content = "✓ Вы уже автор";
                    ApplyAuthorButton.IsEnabled = false;
                }
            }
        }

        private void LoadUserReviews()
        {
            if (Core.CurrentUser == null) return;

            var reviews = Core.Context.Reviews
                .Where(r => r.UserId == Core.CurrentUser.UserId)
                .OrderByDescending(r => r.ReviewDate)
                .ToList();

            ReviewsDataGrid.ItemsSource = reviews;

            if (reviews.Count == 0)
            {
                NoReviewsText.Visibility = Visibility.Visible;
            }
        }

        private void ApplyAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            if (Core.CurrentUser == null)
            {
                MessageBox.Show("Пожалуйста, войдите в систему", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existingRequest = Core.Context.RoleRequest
                .FirstOrDefault(r => r.UserId == Core.CurrentUser.UserId && r.Status == "На рассмотрении");

            if (existingRequest != null)
            {
                MessageBox.Show("Вы уже отправили заявку на статус автора. Ожидайте рассмотрения.",
                    "Заявка уже отправлена", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = MessageBox.Show("Вы хотите получить статус автора? После одобрения вы сможете публиковать свои книги.",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                RoleRequest newRequest = new RoleRequest()
                {
                    UserId = Core.CurrentUser.UserId,
                    RequestDate = System.DateTime.Now,
                    Status = "На рассмотрении"
                };

                Core.Context.RoleRequest.Add(newRequest);
                Core.Context.SaveChanges();

                MessageBox.Show("Заявка на статус автора отправлена администрации",
                    "Заявка отправлена", MessageBoxButton.OK, MessageBoxImage.Information);

                ApplyAuthorButton.IsEnabled = false;
                ApplyAuthorButton.Content = "⏳ Заявка отправлена";
            }
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция редактирования профиля будет доступна в следующей версии",
                "В разработке", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}