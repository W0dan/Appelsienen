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
using CL.Controllers;
using CL.Views;
using CL.Entity;

namespace Appelsienen_MVP_WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, ILoginView
    {
        private LoginController _controller = new LoginController();

        public Login()
        {
            InitializeComponent();

            _controller.View = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.LoadView();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_controller.SelectedUser == null)
            {
                Application.Current.Shutdown();
            }
        }

        public Profile GeselecteerdeGebruiker
        {
            get
            {
                return _controller.SelectedUser;
            }
        }

        void profileLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement profileControl = (FrameworkElement)sender;
            string userName = "";

            switch (profileControl.Name)
            {
                case "profileLabel":
                    userName = (string)((Label)profileControl).Content;
                    break;
                case "profileImage":
                    userName = (string)((Label)((StackPanel)profileControl.Parent).Children[1]).Content;
                    break;
                case "profileStack":
                    userName = (string)((Label)((StackPanel)profileControl).Children[1]).Content;
                    break;
                default:
                    break;
            }

            _controller.UserSelected(userName);
            this.Close();
        }

        void profileLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label profileLabel = (Label)sender;

            _controller.UserSelected((string)profileLabel.Content);
            this.Close();
        }

        #region ILoginView Members

        public void AddProfile(Profile profile)
        {
            Label profileLabel = new Label();
            profileLabel.Name = "profileLabel";
            profileLabel.Content = profile.Name;
            profileLabel.MinWidth = 128;
            profileLabel.Width = 128;
            profileLabel.HorizontalAlignment = HorizontalAlignment.Center;
            profileLabel.MouseDoubleClick += new MouseButtonEventHandler(profileLabel_MouseDoubleClick);

            Image profileImage = new Image();
            profileImage.Name = "profileImage";
            profileImage.Source = Shared.GetUserImage(profile.Image);
            profileImage.Height = 128;
            profileImage.Width = 128;
            profileImage.Margin = new Thickness(5);

            StackPanel profileStack = new StackPanel();
            profileStack.Name = "profileStack";
            profileStack.Children.Add(profileImage);
            profileStack.Children.Add(profileLabel);
            profileStack.MouseLeftButtonUp += new MouseButtonEventHandler(profileLabel_MouseLeftButtonUp);

            UsersWrapPanel.Items.Add(profileStack);
        }

        #endregion
    }
}
