using System.Windows;

namespace Appelsienen_MVP_WPF.Dialogs
{
    /// <summary>
    /// Interaction logic for NumberPicker.xaml
    /// </summary>
    public partial class NumberPicker : Window
    {
        public NumberPicker()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            OkClicked = false;
            Close();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            OkClicked = true;
            Close();
        }

        public static int[] PickNumbers()
        {
            var dialog = new NumberPicker();

            dialog.ShowDialog();

            return dialog.OkClicked ? dialog.Numpad1.Values : null;
        }

        public bool OkClicked { get; set; }
    }
}
