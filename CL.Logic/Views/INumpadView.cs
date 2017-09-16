using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CL.Views
{
    public interface INumpadView:IViewBase
    {
        void SetNumpadButton(int number, int displayNumber);

        void SetNumpadButtonActive(int number);
        void SetNumpadButtonInactive(int number);
    }
}
