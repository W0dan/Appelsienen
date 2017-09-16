using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Views;
using CL.Entity;
using CL.DAO;
using System.Diagnostics;
using CL.Controllers.Helpers;

namespace CL.Controllers
{
    public class MainMenuController : ControllerBase<IMainMenuView>
    {
        public override void LoadView()
        {

        }

        protected override void SetViewState()
        {

            View.SetGebruiker(Gebruiker);
        }

        #region Form actions

        public void GebruikerGeselecteerd(Profile gebruiker)
        {
            this.Gebruiker = gebruiker;

            SetViewState();
        }

        #endregion
    }
}
