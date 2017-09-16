using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Views;

namespace CL.Controllers
{
    public abstract class MainControllerBase : ControllerBase<IMainViewBase>
    {

        public abstract void ControleerButtonClicked();
        public abstract void NextButtonClicked();
        public abstract void OpnieuwButtonClicked();

        public void ExitButtonClicked()
        {
            View.ApplicationExit();
        }

    }
}
