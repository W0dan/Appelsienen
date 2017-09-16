using System;

namespace CL.Controllers
{
    public class AppelsienenRandomizerController
    {
        private Random _rnd = new Random();

        private AppelsienenControllerState _controllerState = new AppelsienenControllerState();
        public CL.Views.IAppelsienenView View { get; set; }

        #region Form actions

        public void LoadView()
        {
            Action<int> setVisibility = _controllerState.DrawGridLines ?
                (Action<int>)View.SetGridLineOn :
                (Action<int>)View.SetGridLineOff;
            for (int i = 0; i < 8; i++)
            {
                setVisibility(i);
            }

            SetViewState();
        }

        public void Reset()
        {
            for (int i = 0; i < _controllerState.AppelsienenList.Count; i++)
            {
                View.SetAppelsienOff(_controllerState.AppelsienenList[i]);
            }
            _controllerState.AppelsienenList.Clear();
        }

        public bool DrawGridLines
        {
            get { return _controllerState.DrawGridLines; }
            set
            {
                _controllerState.DrawGridLines = value;
            }
        }

        public int GetBinaryValue()
        {
            int result = 0;

            _controllerState.AppelsienenList.Sort();
            for (int i = 0; i < _controllerState.AppelsienenList.Count; i++)
            {
                int value = _controllerState.AppelsienenList[i];
                result += (int)Math.Pow(2, value);
            }

            return result;
        }

        public int Value
        {
            get
            {
                var result = _controllerState.AppelsienenList.Count;

                return result;
            }
            set
            {
                SelectOranges(value);
            }
        }

        private void SelectOranges(int number)
        {
            Reset();
            int counter = 0;

            while (counter < number)
            {
                for (int i = 1; i <= 21; i++)
                {
                    if (counter == number)
                        break;

                    if (_rnd.Next(5) == 0)
                    {
                        if (!_controllerState.AppelsienenList.Contains(--i))
                        {
                            View.SetAppelsienOn(i);
                            _controllerState.AppelsienenList.Add(i);
                            counter++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Instellen van de View op basis van de ControllerState
        /// </summary>
        private void SetViewState()
        {


        }

        #endregion
    }
}
