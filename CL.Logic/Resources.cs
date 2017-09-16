using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CL
{
    partial class Resources
    {

        public static System.IO.UnmanagedMemoryStream GetSound(int number)
        {
            return ResourceManager.GetStream("sound_" + number.ToString(), resourceCulture);
        }

    }
}
