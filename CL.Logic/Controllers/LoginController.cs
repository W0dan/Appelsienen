using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Views;
using CL.DAO;
using CL.Entity;
using CL.Controllers.Helpers;
using CL.ExtensionMethods;
using System.Windows;

namespace CL.Controllers
{
    public class LoginController : ControllerBase<ILoginView>
    {
        private readonly ProfileDAO _profileDAO;

        public LoginController()
        {
            _profileDAO = ProfileDAO.Instance;
        }

        public override void LoadView()
        {
            //initiele staat van de view instellen
            LoadProfiles();
        }

        public void UserSelected(string username)
        {
            Invoke(() =>
            {
                SelectedUser = _profileDAO.GetProfile(username);
                SelectedUser.Games = _profileDAO.GetScoresByUsername(username);
            });
        }

        public Profile SelectedUser { get; private set; }

        private void LoadProfiles()
        {
            Invoke(() =>
            {
                var profiles = _profileDAO.GetProfiles();

                if (profiles.Count == 0)
                {
                    //create default admin account
                    var defaultAdmin = new Profile
                    {
                        Image = 0,
                        Name = "admin",
                        Role = RolesEnum.admin,
                        CountOrangesMaxValue = 10,
                        CountOrangesExcludedValues = new[] { 0 },
                        RecogniseNumbersMaxValue = 10,
                        RecogniseNumbersExcludedValues = new[] { 0 }
                    };


                    _profileDAO.SaveNewProfile(defaultAdmin);

                    profiles = _profileDAO.GetProfiles();
                }

                foreach (var profile in profiles)
                {
                    View.AddProfile(profile);
                }
            });
        }

        protected override void SetViewState()
        {

        }
    }
}
