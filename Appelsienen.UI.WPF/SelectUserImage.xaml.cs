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
using CL.Views;
using CL.Controllers;
using CL.Entity;

namespace Appelsienen_MVP_WPF
{
    /// <summary>
    /// Interaction logic for SelectUserImage.xaml
    /// </summary>
    public partial class SelectUserImage : Window, ISelectUserImageView
    {
        SelectUserImageController _controller = new SelectUserImageController();

        public SelectUserImage()
        {
            InitializeComponent();
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
            _controller.View = this;

            _controller.LoadView();
        }

        public void LoadImages()
        {
            List<ImageSource> images = Shared.GetUserImageList();

            for (int i = 0; i < images.Count; i++)
            {
                Image avatar = new Image();

                avatar.Source = images[i];
                avatar.Name = "avatar" + (i + 1).ToString("000");
                avatar.Height = 64;
                avatar.Width = 64;
                avatar.Margin = new Thickness(5);
                avatar.MouseUp += new MouseButtonEventHandler(avatar_MouseUp);

                ImagesWrapPanel.Children.Add(avatar);
            }
        }

        void avatar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image avatar = (Image)sender;

            int imageNr = 0;
            int.TryParse(avatar.Name.Substring(6), out imageNr);

            _controller.Image = imageNr;

            this.Close();
        }

        public int Image
        {
            get
            {
                return _controller.Image;
            }
        }
    }
}
