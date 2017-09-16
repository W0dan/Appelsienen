using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Entity;

namespace CL.Views
{
    public interface IMainMenuView : IViewBase
    {

        void SetGebruiker(Profile gebruiker);

    }
}
