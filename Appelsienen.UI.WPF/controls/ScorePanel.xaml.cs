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
    /// Interaction logic for ScorePanel.xaml
    /// </summary>
    public partial class ScorePanel : UserControl
    {
        public ScorePanel()
        {
            InitializeComponent();
        }

        int _currentItem = 1;

        public void ResetScore()
        {
            for (int i = 1; i <= 10; i++)
            {
                Image score = (Image)this.FindName("score" + i.ToString());

                var image = "../icons/Default.ico";
                var src = new Uri(image, UriKind.Relative);

                var ib = (Image)score;
                ib.Source = new BitmapImage(src);
                ib.ToolTip = "";
            }

            _currentItem = 1;
        }

        public void SetNextScore(bool correctAnswer, string comment)
        {
            if (_currentItem <= 10)
            {
                Image score = (Image)this.FindName("score" + _currentItem.ToString());

                score.ToolTip = comment;
                if (correctAnswer)
                {
                    var image = "../icons/CheckDealie.ico";
                    var src = new Uri(image, UriKind.Relative);

                    score.Source = new BitmapImage(src);
                }
                else
                {
                    var image = "../icons/Delete.ico";
                    var src = new Uri(image, UriKind.Relative);

                    score.Source = new BitmapImage(src);
                }

                _currentItem++;
            }
        }

    }
}
