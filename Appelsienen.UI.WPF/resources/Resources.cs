using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Appelsienen_MVP_WPF
{
    class Shared
    {
        private const int MAXUSERIMAGENUMBER = 56;

        public static ImageSource GetImageResource(string name)
        {
            var src = new Uri(name, UriKind.Relative);
            return new BitmapImage(src);
        }

        public static ImageSource GetAdminImage()
        {
            return GetImageResource(@"../icons/Administrator.png");
        }

        public static ImageSource GetSettingsImage()
        {
            return GetImageResource(@"../icons/Settings.png");
        }

        public static ImageSource GetUsersImage()
        {
            return GetImageResource(@"../icons/Users128.png");
        }

        public static ImageSource GetUserImage(int number)
        {
            if (number >= 0 && number <= MAXUSERIMAGENUMBER)
            {
                return GetImageResource(@"../userImages/user" + number.ToString("000") + ".png");
            }
            else
            {
                return GetUserImage(0);
            }
        }

        public static List<ImageSource> GetUserImageList()
        {
            List<ImageSource> retval = new List<ImageSource>();

            for (int i = 1; i <= MAXUSERIMAGENUMBER; i++)
            {
                retval.Add(GetUserImage(i));
            }

            return retval;
        }
    }
}
