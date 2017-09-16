using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Appelsienen_MVP_WPF.controls
{
    public partial class Numpad : UserControl, CL.Views.INumpadView
    {
        private readonly CL.Controllers.NumpadController _controller = new CL.Controllers.NumpadController();

        public Numpad()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            _controller.View = this;

            _controller.LoadView();
        }

        public int Value => _controller.Value;
        public int[] Values => _controller.Values;

        public bool Randomized {
            get => _controller.Randomized;
            set => _controller.Randomized = value;
        }

        public bool Multiselect
        {
            get => _controller.Multiselect;
            set => _controller.Multiselect = value;
        }

        public void Reset()
        {
            _controller.Reset();
        }

        private static string GetNumpadButtonName(int number)
        {
            return $"Button{number}";
        }

        private Button GetNumpadButton(int number)
        {
            return (Button)this.FindName(GetNumpadButtonName(number));
        }

        private static int GetNumpadButtonNumber(IFrameworkInputElement button)
        {
            return int.Parse(button.Name.Substring(6));
        }

        public void SetNumpadButton(int number, int displayNumber)
        {
            var numpadButton = GetNumpadButton(number);

            numpadButton.Content = displayNumber.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;

            _controller.ButtonClick(GetNumpadButtonNumber(b));
        }

        public void SetNumpadButtonActive(int number)
        {
            var b = GetNumpadButton(number);

            b.Background = new SolidColorBrush(Color.FromRgb(0, 0, 128));
            b.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
        }

        public void SetNumpadButtonInactive(int number)
        {
            var b = GetNumpadButton(number);

            b.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            b.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        }

    }
}
