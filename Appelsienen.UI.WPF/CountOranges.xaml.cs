using CL.Entity;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Appelsienen_MVP_WPF
{
    public partial class CountOranges : Window, CL.Views.IMainView
    {
        private readonly CL.CountOrangesController _controller = new CL.CountOrangesController();

        public CountOranges()
        {
            InitializeComponent();

            _controller.View = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.LoadView();
        }

        private void ExitImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _controller.ExitButtonClicked();
        }

        private void Next_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e.LeftButton & MouseButtonState.Pressed) != MouseButtonState.Pressed)
                return;

            _controller.NextButtonClicked();
        }

        private void Check_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e.LeftButton & MouseButtonState.Pressed) != MouseButtonState.Pressed)
                return;

            _controller.ControleerButtonClicked();
        }

        private void Again_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e.LeftButton & MouseButtonState.Pressed) != MouseButtonState.Pressed)
                return;

            _controller.OpnieuwButtonClicked();
        }

        public Profile Gebruiker
        {
            get => _controller.Gebruiker;
            set => _controller.Gebruiker = value;
        }

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

            var profileStack = new StackPanel {Name = "profileStack"};
            profileStack.Children.Add(profileImage);
            profileStack.Children.Add(profileLabel);

            this.GebruikerLabel.Content = profileStack; //gebruiker.Name;
        }

        #region IMainView Members

        public void ApplicationExit()
        {
            Close();
        }

        public int CijferAsked
        {
            set => Cijfer1.Value = value;
            get => 0;
        }

        public int CijferAnswered => Numpad1.Value;

        public CL.ExcerciseState Status
        {
            set
            {
                var image = "";

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
            this.Numpad1.Reset();
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
                this.Numpad1.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public bool EdittingAppelsienenIsAllowed
        {
            set
            {
                this.Numpad1.IsEnabled = value;
            }
        }

        public bool DrawGridLines
        {
            set
            {
                //this.Appelsienen1.DrawGridLines = value; 
            }
        }

        #endregion
    }
}
