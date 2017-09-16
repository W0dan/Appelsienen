using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CL.Views
{
    public interface IAppelsienenView : IViewBase
    {
        void SetAppelsienOn(int number);
        void SetAppelsienOff(int number);
        void SetGridLineOn(int number);
        void SetGridLineOff(int number);
    }
}
