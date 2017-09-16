using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Entity;

namespace CL.Views
{
    public interface IGebruikersView : IViewBase
    {

        void ClearProfiles();
        void AddProfile(Profile profile);
        void ShowUserDetail(Profile profile, bool newUser);

    }
}
