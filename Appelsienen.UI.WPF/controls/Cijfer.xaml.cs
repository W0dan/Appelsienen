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
using System.Drawing;

namespace Appelsienen_MVP_WPF.controls
{
    /// <summary>
    /// Interaction logic for Cijfer.xaml
    /// </summary>
    public partial class Cijfer : UserControl
    {
        public Cijfer()
        {
            InitializeComponent();
        }

        private Random _rnd = new Random();
        private int _value = 0;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                this.CijferImage.Source = GetNumber(_value);
            }
        }

        public void Check()
        {
            this.CijferImage.Source = GetCheck(_value);
        }

        private ImageSource GetNumber(int number)
        {
            //set 1,2 en 3 random
            var image = "../images/number" + (_rnd.Next(3) + 1).ToString() + "_" + number.ToString() + ".png";
            var src = new Uri(image, UriKind.Relative);

            return new BitmapImage(src);
        }

        private ImageSource GetCheck(int number)
        {
            //voorlopig enkel set 2
            var image = "../images/check" + "2" + "_" + number.ToString() + ".png";
            var src = new Uri(image, UriKind.Relative);

            return new BitmapImage(src);
        }

    }
}
