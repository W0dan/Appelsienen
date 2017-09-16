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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Appelsienen_MVP_WPF.controls
{
    /// <summary>
    /// Interaction logic for AppelsienenRandomizer.xaml
    /// </summary>
    public partial class AppelsienenRandomizer : UserControl, CL.Views.IAppelsienenView
    {
        CL.Controllers.AppelsienenRandomizerController _controller = new CL.Controllers.AppelsienenRandomizerController();

        public AppelsienenRandomizer()
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

        public int Value
        {
            get
            {
                return _controller.Value;
            }
            set
            {
                _controller.Value= value;
            }
        }

        public int GetBinaryValue()
        {
            return _controller.GetBinaryValue();
        }

        private string GetAppelsienName(int number)
        {
            return "appelsien" + (number + 1).ToString();
        }

        private string GetGridLineName(int number)
        {
            return "line" + (number + 1).ToString();
        }

        public bool DrawGridLines
        {
            get { return _controller.DrawGridLines; }
            set { _controller.DrawGridLines = value; }
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
            appelsien.Source = null;// Shared.GetImageResource("../icons/Pointless Bw Circle, I use it for iEx.ico");
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
