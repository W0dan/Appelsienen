using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CL.Views;
using CL.Controllers;
using CL.Entity;

namespace Appelsienen_MVP_WPF
{
    public partial class Main : Window, IMainMenuView
    {
        private readonly MainMenuController _controller = new MainMenuController();

        public Main()
        {
            InitializeComponent();

            _controller.View = this;

            _controller.LoadView();

            var loginWindow = new Login();

            loginWindow.ShowDialog();

            if (loginWindow.GeselecteerdeGebruiker != null)
            {
                _controller.GebruikerGeselecteerd(loginWindow.GeselecteerdeGebruiker);
            }
        }

        private void CopycatButton_Click(object sender, RoutedEventArgs e)
        {
            var copycatWindow = new CopyCat
            {
                Gebruiker = _controller.Gebruiker
            };

            copycatWindow.ShowDialog();
        }

        private void ExitImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AppelsienentellenButton_Click(object sender, RoutedEventArgs e)
        {
            var countorangesWindow = new CountOranges
            {
                Gebruiker = _controller.Gebruiker
            };

            countorangesWindow.ShowDialog();
        }

        private void CijfersherkennenButton_Click(object sender, RoutedEventArgs e)
        {
            var recognisenumbersWindow = new RecogniseNumbers
            {
                Gebruiker = _controller.Gebruiker
            };

            recognisenumbersWindow.ShowDialog();
        }

        private void ChangeUserButton_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Login();

            loginWindow.ShowDialog();

            _controller.GebruikerGeselecteerd(loginWindow.GeselecteerdeGebruiker);
        }

        private void Adminbutton_Click(object sender, RoutedEventArgs e)
        {
            if (_controller.Gebruiker.Role == RolesEnum.admin)
            {
                var gebruikersWindow = new Gebruikers { Gebruiker = _controller.Gebruiker };

                gebruikersWindow.ShowDialog();
            }
            else
            {
                var userDetailWindow = new GebruikerDetail
                {
                    Gebruiker = _controller.Gebruiker,
                    TeEditerenGebruiker = _controller.Gebruiker,
                    NewUser = false
                };

                userDetailWindow.ShowDialog();

                SetGebruiker(_controller.Gebruiker);
            }
        }

        #region IMainMenuView Members

        public void SetGebruiker(Profile gebruiker)
        {
            var profileLabel = new Label
            {
                Name = "profileLabel",
                Content = gebruiker.Name,
                MinWidth = 128,
                Width = 128,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var profileImage = new Image
            {
                Name = "profileImage",
                Source = Shared.GetUserImage(gebruiker.Image),
                Height = 128,
                Width = 128
            };

            var profileStack = new StackPanel { Name = "profileStack" };
            profileStack.Children.Add(profileImage);
            profileStack.Children.Add(profileLabel);

            GebruikerLabel.Content = profileStack;

            var adminButtonImage = new Image
            {
                Name = "adminButtonImage",
                Source = gebruiker.Role == RolesEnum.admin ? Shared.GetAdminImage() : Shared.GetUserImage(gebruiker.Image),
                Height = 128,
                Width = 128
            };
            Adminbutton.Content = adminButtonImage;
        }

        #endregion
    }
}
