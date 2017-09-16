using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CL.Entity;
using CL.Controllers;
using CL.Views;

namespace Appelsienen_MVP_WPF
{
    /// <summary>
    /// Interaction logic for Gebruikers.xaml
    /// </summary>
    public partial class Gebruikers : Window, IGebruikersView
    {
        private GebruikersController _controller = new GebruikersController();

        public Gebruikers()
        {
            InitializeComponent();

            _controller.View = this;
        }

        public Profile Gebruiker
        {
            get
            {
                return _controller.Gebruiker;
            }
            set
            {
                _controller.Gebruiker = value;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.LoadView();
        }

        private void NieuweGebruikerMakenButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.NieuweGebruikerMakenButtonClicked();
        }

        #region IGebruikersView Members

        public void ClearProfiles()
        {
            UsersWrapPanel.Children.Clear();
        }

        public void AddProfile(Profile profile)
        {
            Label profileLabel = new Label();
            profileLabel.Content = profile.Name;
            profileLabel.MinWidth = 128;
            profileLabel.Width = 128;
            profileLabel.HorizontalAlignment = HorizontalAlignment.Center;

            Image profileImage = new Image();
            profileImage.Source = Shared.GetUserImage(profile.Image);
            profileImage.Height = 128;
            profileImage.Width = 128;
            profileImage.Margin = new Thickness(5);

            StackPanel profileStack = new StackPanel();
            profileStack.Name = "profile_" + profile.Name;
            profileStack.Children.Add(profileImage);
            profileStack.Children.Add(profileLabel);
            profileStack.MouseUp += new MouseButtonEventHandler(profileStack_MouseUp);
            profileStack.MouseDown += new MouseButtonEventHandler(profileStack_MouseDown);

            UsersWrapPanel.Children.Add(profileStack);
        }

        void profileStack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel profileStack = (StackPanel)sender;

            string profileName = profileStack.Name.Split('_')[1];

            _controller.GebruikerGeselecteerd(profileName);
        }

        void profileStack_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowSelectedUser();
        }

        public void ShowSelectedUser()
        {
            ShowUserDetail(_controller.SelectedUser, false);
        }

        public void ShowUserDetail(Profile profile, bool newUser)
        {

            GebruikerDetail userDetailWindow = new GebruikerDetail();

            userDetailWindow.NewUser = newUser;
            userDetailWindow.Gebruiker = _controller.Gebruiker;
            userDetailWindow.TeEditerenGebruiker = profile;

            userDetailWindow.ShowDialog();

            _controller.LoadProfiles();

        }

        #endregion

        private void UsersWrapPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GebruikersContextmenu.PlacementTarget = (UIElement)sender;
            GebruikersContextmenu.IsOpen = true;
        }

        private void verwijderenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _controller.GebruikerVerwijderenButtonClicked();
        }
    }
}
