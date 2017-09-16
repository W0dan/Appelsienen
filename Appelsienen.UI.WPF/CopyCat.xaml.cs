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

namespace Appelsienen_MVP_WPF
{
    /// <summary>
    /// Interaction logic for CopyCat.xaml
    /// </summary>
    public partial class CopyCat : Window,CL.Views.IMainView
    {
        private CL.CopyCatController _controller = new CL.CopyCatController();

        public CopyCat()
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

        private void ExitImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _controller.ExitButtonClicked();
        }

        private void Next_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _controller.NextButtonClicked();
        }

        private void Check_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _controller.ControleerButtonClicked();
        }

        private void Again_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _controller.OpnieuwButtonClicked();
        }

        #region IMainView Members

        public void ApplicationExit()
        {
            //Application.Current.Shutdown();
            this.Close();
        }

        public int CijferAsked
        {
            set
            {
                this.Cijfer1.Value = value;
            }
            get
            {
                return this.Cijfer1.GetBinaryValue();
            }
        }

        public int CijferAnswered
        {
            get
            {
                return this.Appelsienen1.GetBinaryValue();
            }
        }

        public CL.ExcerciseState Status
        {
            set
            {
                string image = "";

                switch (value)
                {
                    case CL.ExcerciseState.Solve:
                        image = "../icons/Question.png";
                        break;
                    case CL.ExcerciseState.Correct:
                        image = "../icons/CheckDealie.ico";
                        break;
                    case CL.ExcerciseState.Error:
                        image = "../icons/Delete.ico";
                        break;
                    case CL.ExcerciseState.Again:
                        image = "../icons/buttonRefresh.ico";
                        break;
                    default:
                        break;
                }
                var src = new Uri(image, UriKind.Relative);

                this.StatusImage.Source = new BitmapImage(src);
            }
        }

        public void DisplayNextScore(bool correctAnswer, string comment)
        {
            this.ScorePanel1.SetNextScore(correctAnswer, comment);
        }

        public void DisplayFinalScore(string text)
        {
            MessageBox.Show(text);
        }

        public void ResetAppelsienen()
        {
            this.Appelsienen1.Reset();
        }

        public void ResetScore()
        {
            this.ScorePanel1.ResetScore();
        }

        public void SetCijferModusCheck()
        {
            //TODO: aanduiding voorzien om fouten weer te geven
        }

        public bool ActivatingControlleerbuttonIsAllowed
        {
            set
            {
                this.Check.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool ActivatingNextbuttonIsAllowed
        {
            set
            {
                this.Next.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool ActivatingOpnieuwbuttonIsAllowed
        {
            set
            {
                this.Again.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool ViewingCijferIsAllowed
        {
            set
            {
                this.Cijfer1.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool ViewingAppelsienenIsAllowed
        {
            set
            {
                this.Appelsienen1.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool EdittingAppelsienenIsAllowed
        {
            set
            {
                this.Appelsienen1.IsEnabled = value;
            }
        }

        public bool DrawGridLines
        {
            set { 
                this.Appelsienen1.DrawGridLines = value;
                this.Cijfer1.DrawGridLines = value;
            }
        }

        public void SetGebruiker(Profile gebruiker)
        {
            Label profileLabel = new Label();
            profileLabel.Name = "profileLabel";
            profileLabel.Content = gebruiker.Name;
            profileLabel.MinWidth = 128;
            profileLabel.Width = 128;
            profileLabel.HorizontalAlignment = HorizontalAlignment.Center;

            Image profileImage = new Image();
            profileImage.Name = "profileImage";
            profileImage.Source = Shared.GetUserImage(gebruiker.Image);
            profileImage.Height = 128;
            profileImage.Width = 128;

            StackPanel profileStack = new StackPanel();
            profileStack.Name = "profileStack";
            profileStack.Children.Add(profileImage);
            profileStack.Children.Add(profileLabel);

            this.GebruikerLabel.Content = profileStack; //gebruiker.Name;
        }

        #endregion

    }
}
