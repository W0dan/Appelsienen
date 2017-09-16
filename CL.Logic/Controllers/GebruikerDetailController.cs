using CL.Views;
using CL.Entity;
using CL.DAO;

namespace CL.Controllers
{
    public class GebruikerDetailController : ControllerBase<IGebruikerDetailView>
    {
        private readonly Profile _gebruiker = new Profile();
        private readonly ProfileDAO _profileDAO;

        public bool NewUser { get; set; } = false;

        public Profile TeEditerenGebruiker { get; set; }

        public GebruikerDetailController()
        {
            _profileDAO = ProfileDAO.Instance;
        }

        public void ShowCopycatScoresPressed()
        {
            ShowScores(0);
        }

        public void ShowNumbersScoresPressed()
        {
            ShowScores(1);
        }
        
        public void ShowOrangesScoresPressed()
        {
            ShowScores(2);
        }

        private void ShowScores(int number)
        {
            if (TeEditerenGebruiker.Games.Count <= number) return;

            //al dan niet grafisch tonen van scores:
            var calculator = new ScoreCalculator();
            var result = calculator.CalculateScoreSet(TeEditerenGebruiker.Games[number].Scores);

            View.DisplayScores(result);
        }

        public override void LoadView()
        {
            _gebruiker.Name = TeEditerenGebruiker.Name;
            _gebruiker.Image = TeEditerenGebruiker.Image;
            _gebruiker.Role = TeEditerenGebruiker.Role;
            _gebruiker.CountOrangesMaxValue = TeEditerenGebruiker.CountOrangesMaxValue;
            _gebruiker.CountOrangesExcludedValues = TeEditerenGebruiker.CountOrangesExcludedValues;
            _gebruiker.RecogniseNumbersMaxValue = TeEditerenGebruiker.RecogniseNumbersMaxValue;
            _gebruiker.RecogniseNumbersExcludedValues = TeEditerenGebruiker.RecogniseNumbersExcludedValues;

            View.SetUserImage(_gebruiker.Image);
            View.UserName = _gebruiker.Name;
            View.RoleName = _gebruiker.Role;
            View.CountOrangesMaxValue = _gebruiker.CountOrangesMaxValue ?? 10;
            View.RecogniseNumbersMaxValue = _gebruiker.RecogniseNumbersMaxValue ?? 10;
            View.EdittingUsernameIsAllowed = NewUser;
            View.EdittingRolenameIsAllowed = Gebruiker.Role == RolesEnum.admin;

            View.SetRecogniseNumbersExclusionsValues(_gebruiker.RecogniseNumbersExcludedValues);
            View.SetCountOrangesExclusionsValues(_gebruiker.CountOrangesExcludedValues);

            ShowScores(0);
        }

        public void OkPressed()
        {
            TeEditerenGebruiker.Name = _gebruiker.Name;
            TeEditerenGebruiker.Image = _gebruiker.Image;
            TeEditerenGebruiker.Role = _gebruiker.Role;
            TeEditerenGebruiker.CountOrangesMaxValue = _gebruiker.CountOrangesMaxValue;
            TeEditerenGebruiker.CountOrangesExcludedValues = _gebruiker.CountOrangesExcludedValues;
            TeEditerenGebruiker.RecogniseNumbersMaxValue = _gebruiker.RecogniseNumbersMaxValue;
            TeEditerenGebruiker.RecogniseNumbersExcludedValues= _gebruiker.RecogniseNumbersExcludedValues;

            if (NewUser)
            {
                var success = _profileDAO.SaveNewProfile(TeEditerenGebruiker);

                if (success)
                {
                    View.CloseWindow();
                }
                else
                {
                    //melding tonen dat profielnaam reeds bestaat !!
                    View.ShowWarning("Bewaren mislukt: Profielnaam bestaat reeds.");
                }
            }
            else
            {
                _profileDAO.SaveProfile(TeEditerenGebruiker);
                View.CloseWindow();
            }
        }

        public void CancelPressed()
        {
            View.CloseWindow();
        }

        public void NameChanged()
        {
            _gebruiker.Name = View.UserName;
        }

        public void RoleChanged()
        {
            _gebruiker.Role = View.RoleName;
        }

        public void ImageChanged(int image)
        {
            _gebruiker.Image = image;

            View.SetUserImage(image);
        }

        protected override void SetViewState()
        {

        }

        public void CountOrangesMaxValueChanged()
        {
            _gebruiker.CountOrangesMaxValue = View.CountOrangesMaxValue;
        }

        public void RecogniseNumbersMaxValueChanged()
        {
            _gebruiker.RecogniseNumbersMaxValue = View.RecogniseNumbersMaxValue;
        }

        public void CountOrangesExcludedValuesChanged(int[] numbers)
        {
            _gebruiker.CountOrangesExcludedValues = numbers;

            View.SetCountOrangesExclusionsValues(_gebruiker.CountOrangesExcludedValues);
        }

        public void RecogniseNumbersExcludedValuesChanged(int[] numbers)
        {
            _gebruiker.RecogniseNumbersExcludedValues = numbers;

            View.SetRecogniseNumbersExclusionsValues(_gebruiker.RecogniseNumbersExcludedValues);
        }
    }
}
