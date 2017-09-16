using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CL.Views;

namespace CL.ExtensionMethods
{
    public static class IViewBaseExtensions
    {

        public static void ShowMessage(this IViewBase window, string text)
        {
            MessageBox.Show(text, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowError(this IViewBase window, Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
