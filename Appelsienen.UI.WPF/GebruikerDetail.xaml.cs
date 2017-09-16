using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Appelsienen_MVP_WPF.Dialogs;
using CL.Entity;
using Appelsienen_MVP_WPF.graphics;

namespace Appelsienen_MVP_WPF
{
    public partial class GebruikerDetail : Window, CL.Views.IGebruikerDetailView
    {
        private readonly CL.Controllers.GebruikerDetailController _controller = new CL.Controllers.GebruikerDetailController();

        public GebruikerDetail()
        {
            InitializeComponent();
        }

        public Profile Gebruiker
        {
            get => _controller.Gebruiker;
            set => _controller.Gebruiker = value;
        }

        public Profile TeEditerenGebruiker
        {
            get => _controller.TeEditerenGebruiker;
            set => _controller.TeEditerenGebruiker = value;
        }

        public bool NewUser
        {
            set => _controller.NewUser = value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.View = this;

            _controller.LoadView();
        }

        public void SetUserImage(int value)
        {
            {
                UserImagebox.Source = Shared.GetUserImage(value);
            }
        }

        public string UserName
        {
            get => NaamTextBox.Text;
            set => NaamTextBox.Text = value;
        }

        public RolesEnum RoleName
        {
            get => RoleComboBox.SelectedIndex == 0 ? RolesEnum.admin : RolesEnum.user;
            set => RoleComboBox.SelectedIndex = value == RolesEnum.admin ? 0 : 1;
        }

        public int CountOrangesMaxValue
        {
            get
            {
                int.TryParse(CountOrangesMaxValueTextBox.Text, out var value);

                return value;
            }
            set => CountOrangesMaxValueTextBox.Text = value.ToString("0");
        }

        public int RecogniseNumbersMaxValue
        {
            get
            {
                int.TryParse(RecogniseNumbersMaxValueTextBox.Text, out var value);

                return value;
            }
            set => RecogniseNumbersMaxValueTextBox.Text = value.ToString("0");
        }

        public void CloseWindow()
        {
            Close();
        }

        public void ShowWarning(string text)
        {
            WarningLabel.Content = text;
        }

        private void UserImagebox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectUserImageWindow = new SelectUserImage { Gebruiker = _controller.Gebruiker };


            selectUserImageWindow.ShowDialog();

            _controller.ImageChanged(selectUserImageWindow.Image);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.OkPressed();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.CancelPressed();
        }

        private void NaamTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _controller.NameChanged();
        }

        public bool EdittingUsernameIsAllowed
        {
            set => NaamTextBox.IsReadOnly = !value;
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _controller.RoleChanged();
        }

        public bool EdittingRolenameIsAllowed
        {
            set => RoleComboBox.IsEnabled = value;
        }

        public void DisplayScores(List<ScoreSet> scores)
        {
            var chartGenerator = new ScoreChart();
            ChartImage.Source = chartGenerator.CreateChart(scores);
        }

        private void CopyCatScoresbutton_Click(object sender, RoutedEventArgs e)
        {
            _controller.ShowCopycatScoresPressed();
        }

        private void NumbersScoresbutton_Click(object sender, RoutedEventArgs e)
        {
            _controller.ShowNumbersScoresPressed();
        }

        private void OrangesScoresbutton_Click(object sender, RoutedEventArgs e)
        {
            _controller.ShowOrangesScoresPressed();
        }

        private void CountOrangesMaxValueTextBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _controller.CountOrangesMaxValueChanged();
        }

        private void RecogniseNumbersMaxValueTextBox_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _controller.RecogniseNumbersMaxValueChanged();
        }

        private void CountOrangesExclusionsValuesLabel_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var numbers = NumberPicker.PickNumbers();

            _controller.CountOrangesExcludedValuesChanged(numbers);
        }

        public void SetCountOrangesExclusionsValues(int[] values)
        {
            if (values == null)
                return;

            CountOrangesExclusionsValuesLabel.Content = values.Select(x => x.ToString("0")).Aggregate((x, y) => $"{x},{y}");
        }

        private void RecogniseNumbersExclusionsValuesLabel_OnMousedDown(object sender, MouseButtonEventArgs e)
        {
            var numbers = NumberPicker.PickNumbers();

            _controller.RecogniseNumbersExcludedValuesChanged(numbers);
        }

        public void SetRecogniseNumbersExclusionsValues(int[] values)
        {
            if (values == null)
                return;

            RecogniseNumbersExclusionsValuesLabel.Content = values.Select(x => x.ToString("0")).Aggregate((x, y) => $"{x},{y}");
        }
    }
}
