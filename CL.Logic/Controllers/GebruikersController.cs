using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.DAO;
using CL.Entity;

namespace CL.Controllers
{
    public class GebruikersController : ControllerBase<CL.Views.IGebruikersView>
    {
        private ProfileDAO _profileDAO;
        private Profile _selectedUser = null;

        public GebruikersController()
        {
            _profileDAO = ProfileDAO.Instance;
        }

        public void NieuweGebruikerMakenButtonClicked()
        {
            Profile newProfile = new Profile();

            newProfile.Role = RolesEnum.user;
            newProfile.Image = 0;
            newProfile.Name = "";

            View.ShowUserDetail(newProfile, true);

            LoadProfiles();
        }

        public void GebruikerVerwijderenButtonClicked()
        {
            _profileDAO.DeleteProfile(_selectedUser);

            LoadProfiles();
        }

        public override void LoadView()
        {
            //initiele staat van de view instellen
            LoadProfiles();
        }

        public void LoadProfiles()
        {
            View.ClearProfiles();
            var profiles = _profileDAO.GetProfiles();

            foreach (var profile in profiles)
            {
                View.AddProfile(profile);
            }
        }

        protected override void SetViewState()
        {

        }

        public void GebruikerGeselecteerd(string userName)
        {
            _selectedUser = _profileDAO.GetProfile(userName);
            _selectedUser.Games = _profileDAO.GetScoresByUsername(userName);
        }

        public Profile SelectedUser
        {
            get 
            {
                return _selectedUser;
            }
        }
    }
}
