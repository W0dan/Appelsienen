using System;
using System.Linq;

namespace CL
{
    class NumberGenerator
    {
        private Random _rnd = new Random();

        private int? _targetNumber = new int?();
        private int _maxNumber = 10;
        private int[] _excludeNumbers = { };

        public NumberGenerator(int maxNumber, int[] excludeNumbers)
        {
            constructor(maxNumber, excludeNumbers, new int?());
        }

        public NumberGenerator(int maxNumber, int[] excludeNumbers, int targetNumber)
        {
            constructor(maxNumber, excludeNumbers, targetNumber);
        }

        private void constructor(int maxNumber, int[] excludeNumbers, int? targetNumber)
        {
            _maxNumber = maxNumber;
            _excludeNumbers = excludeNumbers;
            _targetNumber = targetNumber;
        }

        public int GetNumber()
        {
            if (_targetNumber.HasValue)
            {
                //een beduidend hogere kans om een <_targetNumber> te krijgen !
                if (_rnd.Next(2) == 0)
                {
                    return GetRandomNumber();
                }
                else
                    return _targetNumber.Value;
            }
            else
            {
                return GetRandomNumber();
            }
        }

        /// <summary>
        /// rekening houden met range en exclusions
        /// </summary>
        /// <returns></returns>
        private int GetRandomNumber()
        {
            var number = _rnd.Next(_maxNumber + 1);

            while (_excludeNumbers.Contains<int>(number))
            {
                number = _rnd.Next(_maxNumber + 1);
            }

            return number;
        }
    }
}
