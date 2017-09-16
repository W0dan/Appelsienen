using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CL.Controllers.Helpers
{
    public class LogHelper
    {

        public static void DebugPrint(string text)
        {
            Debug.Print(DateTime.Now.ToString("hh:mm:ss") + " (-) " + text);
        }

    }
}
