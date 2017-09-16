using System;
using System.Collections.Generic;
using System.Linq;
using CL.Views;

namespace CL.Controllers
{
    public class NumpadController : ControllerBase<INumpadView>
    {
        private readonly Random _rnd = new Random();
        private readonly List<int> _numpadNumbers = new List<int>();
        private const int MaxNumber = 11;
        private bool _randomized = true;

        private int _selectedNumber = -1;
        private readonly IList<int> _selectedNumbers = new List<int>();

        public override void LoadView()
        {
            if (Randomized)
                RandomizeNumpad();
            else
                InitializeNumpad();
        }

        protected override void SetViewState()
        {

        }

        public void ButtonClick(int number)
        {
            if (Multiselect)
            {
                if (_selectedNumbers.Contains(number))
                    View.SetNumpadButtonInactive(_selectedNumber);
                else
                {
                    View.SetNumpadButtonActive(number);
                    _selectedNumbers.Add(number);
                }
            }
            else
            {
                if (_selectedNumber > -1)
                    View.SetNumpadButtonInactive(_selectedNumber);

                Value = _numpadNumbers[number];
                View.SetNumpadButtonActive(number);
                _selectedNumber = number;
            }
        }

        public void Reset()
        {
            RandomizeNumpad();
        }

        public int Value { get; private set; } = -1;
        public int[] Values => _selectedNumbers.ToArray();

        public bool Randomized
        {
            get => _randomized;
            set
            {
                _randomized = value;
                LoadView();
            }
        }

        public bool Multiselect { get; set; } = false;

        private void RandomizeNumpad()
        {
            _numpadNumbers.Clear();

            for (var i = 0; i <= MaxNumber; i++)
            {
                var number = _rnd.Next(MaxNumber + 1);

                while (_numpadNumbers.Contains(number))
                {
                    number = _rnd.Next(MaxNumber + 1);
                }

                _numpadNumbers.Add(number);
                View.SetNumpadButton(i, number);
                View.SetNumpadButtonInactive(i);
            }
        }

        private void InitializeNumpad()
        {
            _numpadNumbers.Clear();

            for (var i = 0; i <= MaxNumber; i++)
            {
                _numpadNumbers.Add(i);
                View.SetNumpadButton(i, i);
                View.SetNumpadButtonInactive(i);
            }
        }
    }
}
