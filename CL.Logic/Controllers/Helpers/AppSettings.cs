using System.Configuration;
using System.Linq;

namespace CL.Controllers.Helpers
{
    public class AppSettings
    {
        public int CountOrangesMaxValue => int.Parse(ConfigurationManager.AppSettings[nameof(CountOrangesMaxValue)]);
        public int[] CountOrangesExcludedValues
        {
            get
            {
                var setting = ConfigurationManager.AppSettings[nameof(CountOrangesExcludedValues)];

                return !string.IsNullOrEmpty(setting) ? setting.Split(',').Select(int.Parse).ToArray() : new int[] { };
            }
        }

        public int RecogniseNumbersMaxValue => int.Parse(ConfigurationManager.AppSettings[nameof(RecogniseNumbersMaxValue)]);
        public int[] RecogniseNumbersExcludedValues
        {
            get
            {
                var setting = ConfigurationManager.AppSettings[nameof(RecogniseNumbersExcludedValues)];

                return !string.IsNullOrEmpty(setting) ? setting.Split(',').Select(int.Parse).ToArray() : new int[] { };
            }
        }
    }
}
