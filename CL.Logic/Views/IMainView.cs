using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CL.Entity;

namespace CL
{
    public enum ExcerciseState
    {
        Solve,
        Correct,
        Error,
        Again
    }

    namespace Views
    {
        public interface IMainView : IMainViewBase
        {
            int CijferAsked { set; get; }
            int CijferAnswered { get; }
            ExcerciseState Status { set; }
            void DisplayNextScore(bool correctAnswer, string comment);
            void DisplayFinalScore(string text);
            void ResetAppelsienen();
            void ResetScore();
            void SetCijferModusCheck();

            bool ActivatingControlleerbuttonIsAllowed { set; }
            bool ActivatingNextbuttonIsAllowed { set; }
            bool ActivatingOpnieuwbuttonIsAllowed { set; }
            bool ViewingCijferIsAllowed { set; }
            bool ViewingAppelsienenIsAllowed { set; }
            bool EdittingAppelsienenIsAllowed { set; }

            bool DrawGridLines { set; }

            void SetGebruiker(Profile gebruiker);
        }
    }
}
