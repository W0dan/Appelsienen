using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Appelsienen_MVP_WPF.controls
{
    /// <summary>
    /// Interaction logic for Appelsienen_MVP_WPF.xaml
    /// </summary>
    public partial class Appelsienen : UserControl, CL.Views.IAppelsienenView
    {
        CL.Controllers.AppelsienenController _controller = new CL.Controllers.AppelsienenController();

        public Appelsienen()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            _controller.View = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _controller.LoadView();
        }

        private Dictionary<string, bool> _appelsienen = new Dictionary<string, bool>();

        private void appelsien_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e.LeftButton & MouseButtonState.Pressed) != MouseButtonState.Pressed)
                return;

            var appelsien = (Image)sender;
            string number = appelsien.Name.Substring(9);

            _controller.AppelsienClicked(number);
        }

        public bool DrawGridLines
        {
            get { return _controller.DrawGridLines; }
            set { _controller.DrawGridLines = value; }
        }

        public int GetBinaryValue()
        {
            return _controller.GetBinaryValue();
        }

        public int Value
        {
            get
            {
                return _controller.Value;
            }
        }

        public void Reset()
        {
            _controller.Reset();
        }

        private string GetAppelsienName(int number)
        {
            return "appelsien" + (number + 1).ToString();
        }

        private string GetGridLineName(int number)
        {
            return "line" + (number + 1).ToString();
        }

        #region IAppelsienenView Members

        public void SetAppelsienOn(int number)
        {
            var appelsien = (Image)this.FindName(GetAppelsienName(number));
            appelsien.Source = Shared.GetImageResource("../icons/Orange.ico");
        }

        public void SetAppelsienOff(int number)
        {
            var appelsien = (Image)this.FindName(GetAppelsienName(number));
            if (_controller.DrawGridLines)
            {
                appelsien.Source = Shared.GetImageResource("../images/void.png");
            }
            else
            {
                appelsien.Source = Shared.GetImageResource("../icons/Pointless Bw Circle, I use it for iEx.ico");
            }
        }

        public void SetGridLineOn(int number)
        {
            var gridline = (Line)this.FindName(GetGridLineName(number));
            gridline.Visibility = Visibility.Visible;
        }

        public void SetGridLineOff(int number)
        {
            var gridline = (Line)this.FindName(GetGridLineName(number));
            gridline.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}
