using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CL.Views;

namespace Appelsienen_MVP_WPF
{
    public class ViewBase : Window, IViewBase
    {

        #region IViewBase Members

        public void ShowMessage(string text)
        {
            MessageBox.Show(text, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}
