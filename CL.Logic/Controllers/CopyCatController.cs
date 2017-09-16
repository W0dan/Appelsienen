using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Views;
using CL.Entity;
using CL.DAO;

namespace CL
{
    public class CopyCatController : Controllers.ControllerBase<IMainView>
    {
        private CountOrangesControllerState _controllerState = new CountOrangesControllerState();
        private ProfileDAO _profileDAO;

        public CopyCatController()
        {
            _profileDAO = ProfileDAO.Instance;
        }

        #region Form actions

        public override void LoadView()
        {
            _controllerState.ExcerciseNumber = 1;
            _controllerState.ScreenModus = ScreenModus.NewGame;
            View.DrawGridLines = true;
            SetNextValue();

            SetViewState();
        }

        public void ControleerButtonClicked()
        {
            if (View.CijferAnswered == View.CijferAsked) //controle op correct
            {
                //correct
                _controllerState.ScreenModus = ScreenModus.CheckCorrect;
                _controllerState.Score++;
                PlaySound(Resources.ready);
            }
            else
            {
                //fout
                _controllerState.ScreenModus = ScreenModus.CheckFout;
                PlaySound(Resources.malfound);
            }

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
            var ng = new NumberGenerator(21, new int[] { });

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


            Gebruiker.Scores(GamesEnum.copycat).Add(myScore);

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
