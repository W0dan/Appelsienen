using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CL.Controllers
{
    public class AppelsienenController
    {
        private AppelsienenControllerState _controllerState = new AppelsienenControllerState();
        public CL.Views.IAppelsienenView View { get; set; }

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
        }

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
                View.SetAppelsienOff(_controllerState.AppelsienenList[i] );
            }
            _controllerState.AppelsienenList.Clear();
        }

        public bool DrawGridLines
        {
            get { return _controllerState.DrawGridLines; }
            set
            {
                _controllerState.DrawGridLines = value;
                if (_controllerState.DrawGridLines)
                {
                    for (int i = 0; i < 21; i++)
                    {
                        View.SetAppelsienOff(i);
                    }
                }
            }
        }

        public void AppelsienClicked(string numberString)
        {
            int number;

            if (int.TryParse(numberString, out number))
            {
                number--;

                if (_controllerState.AppelsienenList.Contains(number))
                {
                    _controllerState.AppelsienenList.Remove(number);
                    View.SetAppelsienOff(number);
                }
                else
                {
                    _controllerState.AppelsienenList.Add(number);
                    View.SetAppelsienOn(number);
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
