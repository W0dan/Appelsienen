using System.Collections.Generic;
using CL.Entity;

namespace CL.Views
{
    public interface IGebruikerDetailView : IViewBase
    {

        string UserName { get; set; }
        RolesEnum RoleName { get; set; }
        int CountOrangesMaxValue { get; set; }
        int RecogniseNumbersMaxValue { get; set; }

        void SetUserImage(int value);
        void CloseWindow();
        bool EdittingUsernameIsAllowed { set; }
        bool EdittingRolenameIsAllowed { set; }
        void ShowWarning(string text);
        void DisplayScores(List<ScoreSet> scores);

        void SetCountOrangesExclusionsValues(int[] values);
        void SetRecogniseNumbersExclusionsValues(int[] values);
    }
}
