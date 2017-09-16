using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CL.Views
{
    public interface IMainViewBase : IViewBase
    {
        void ApplicationExit();
    }
}
