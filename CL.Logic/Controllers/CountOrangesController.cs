using CL.Controllers.Helpers;
using CL.DAO;
using CL.Entity;
using CL.Views;
using System;

namespace CL
{
    public class CountOrangesController : Controllers.ControllerBase<IMainView>
    {
        private readonly AppSettings _appSettings = new AppSettings();
        private readonly CountOrangesControllerState _controllerState = new CountOrangesControllerState();
        private readonly ProfileDAO _profileDAO;
        private int[] _excludedValues;
        private int _maxValue;

        public CountOrangesController()
        {
            _profileDAO = ProfileDAO.Instance;
        }

        #region Form actions

        public override void LoadView()
        {
            _excludedValues = Gebruiker.CountOrangesExcludedValues ?? _appSettings.CountOrangesExcludedValues;
            _maxValue = Gebruiker.CountOrangesMaxValue ?? _appSettings.CountOrangesMaxValue;

            _controllerState.ExcerciseNumber = 1;
            _controllerState.ScreenModus = ScreenModus.NewGame;
            View.DrawGridLines = false;
            SetNextValue();

            SetViewState();
        }

        public void ControleerButtonClicked()
        {
            _controllerState.ScreenModus = View.CijferAnswered == _controllerState.Cijfer ? ScreenModus.CheckCorrect : ScreenModus.CheckFout;

            SetViewState();
        }

        public void NextButtonClicked()
        {
            if (_controllerState.ExcerciseNumber < 10) //controle op finished
            {
                _controllerState.ExcerciseNumber++;

                //nog niet finished:
                SetNextValue();
                _controllerState.ScreenModus = ScreenModus.Solve;
            }
            else
            {
                //score updaten
                UpdateScore();

                //finished:
                _controllerState.ExcerciseNumber = 1;
                _controllerState.ScreenModus = ScreenModus.Finished;
            }

            SetViewState();
        }

        private void SetNextValue()
        {
            var ng = new NumberGenerator(_maxValue, _excludedValues);

            int nieuwCijfer;
            do
            {
                nieuwCijfer = ng.GetNumber();
            } while (nieuwCijfer == _controllerState.Cijfer);
            _controllerState.Cijfer = nieuwCijfer;
        }

        public void OpnieuwButtonClicked()
        {
            _controllerState.Score = 0;
            _controllerState.ScreenModus = ScreenModus.NewGame;

            SetNextValue();

            SetViewState();
        }

        public void ExitButtonClicked()
        {
            if (_controllerState.ScreenModus != ScreenModus.Finished)
            {
                //score updaten
                UpdateScore();
            }

            _controllerState.ScreenModus = ScreenModus.ExitGame;

            SetViewState();
        }

        private void UpdateScore()
        {
            var myScore = new Score
            {
                Tijdstip = DateTime.Now,
                AantalOpgelost = _controllerState.ExcerciseNumber,
                ScoreOpTien = _controllerState.Score
            };

            Gebruiker.Scores(GamesEnum.countoranges).Add(myScore);

            _profileDAO.SaveScoresToProfile(Gebruiker.Games, Gebruiker.Name);
        }

        #endregion

        /// <summary>
        /// Instellen van de View op basis van de ControllerState
        /// </summary>
        protected override void SetViewState()
        {
            View.SetGebruiker(Gebruiker);

            switch (_controllerState.ScreenModus)
            {
                case ScreenModus.NewGame:
                    View.ResetScore();

                    View.ActivatingControlleerbuttonIsAllowed = true;
                    View.ActivatingNextbuttonIsAllowed = false;
                    View.ActivatingOpnieuwbuttonIsAllowed = false;
                    View.ViewingCijferIsAllowed = true;
                    View.Status = ExcerciseState.Solve;
                    View.ViewingAppelsienenIsAllowed = true;
                    View.EdittingAppelsienenIsAllowed = true;
                    View.CijferAsked = _controllerState.Cijfer;
                    break;
                case ScreenModus.Solve:
                    View.ActivatingControlleerbuttonIsAllowed = true;
                    View.ActivatingNextbuttonIsAllowed = false;
                    View.ActivatingOpnieuwbuttonIsAllowed = false;
                    View.ViewingCijferIsAllowed = true;
                    View.Status = ExcerciseState.Solve;
                    View.ResetAppelsienen();
                    View.ViewingAppelsienenIsAllowed = true;
                    View.EdittingAppelsienenIsAllowed = true;
                    View.CijferAsked = _controllerState.Cijfer;
                    break;
                case ScreenModus.CheckCorrect:
                    View.ActivatingNextbuttonIsAllowed = true;
                    View.ActivatingControlleerbuttonIsAllowed = false;
                    View.ActivatingOpnieuwbuttonIsAllowed = false;
                    View.ViewingCijferIsAllowed = true;
                    View.Status = ExcerciseState.Correct;
                    View.SetCijferModusCheck();
                    View.DisplayNextScore(true, "Het cijfer " + View.CijferAnswered + " was ingevuld. Dit was correct.");
                    View.ViewingAppelsienenIsAllowed = true;
                    View.EdittingAppelsienenIsAllowed = false;

                    _controllerState.Score++;
                    PlaySound(Resources.ready);

                    break;
                case ScreenModus.CheckFout:
                    View.ActivatingNextbuttonIsAllowed = true;
                    View.ActivatingControlleerbuttonIsAllowed = false;
                    View.ActivatingOpnieuwbuttonIsAllowed = false;
                    View.ViewingCijferIsAllowed = true;
                    View.Status = ExcerciseState.Error;
                    View.SetCijferModusCheck();
                    View.DisplayNextScore(false, "Het cijfer " + View.CijferAnswered + " was ingevuld. Dit was niet correct, het juiste antwoord was: " + _controllerState.Cijfer + ".");
                    View.ViewingAppelsienenIsAllowed = true;
                    View.EdittingAppelsienenIsAllowed = false;

                    PlaySound(Resources.malfound);

                    break;
                case ScreenModus.Finished:
                    View.ActivatingOpnieuwbuttonIsAllowed = true;
                    View.ActivatingNextbuttonIsAllowed = false;
                    View.ActivatingControlleerbuttonIsAllowed = false;
                    View.ViewingCijferIsAllowed = false;
                    View.Status = ExcerciseState.Again;
                    View.EdittingAppelsienenIsAllowed = false;
                    View.ViewingAppelsienenIsAllowed = false;
                    View.ResetAppelsienen();
                    View.DisplayFinalScore("Uw score is " + _controllerState.Score + " op 10");
                    break;
                case ScreenModus.ExitGame:
                    View.ApplicationExit();
                    break;
                default:
                    break;
            }
        }

    }
}
