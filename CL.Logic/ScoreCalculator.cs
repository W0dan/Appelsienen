using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Entity;

namespace CL
{
    class ScoreCalculator
    {

        public List<ScoreSet> CalculateScoreSet(List<Score> scores)
        {
            List<ScoreSet> scoreSetList = new List<ScoreSet>();
            List<Score> filteredScores = scores.FindAll(x => x.AantalOpgelost > 0);

            int defaultGrouping = filteredScores.Count / 10;
            int plusOneGroupingCount = filteredScores.Count % 10;
            List<int> grouping = GetGroupings(defaultGrouping, plusOneGroupingCount);

            int groupNumber = 0;
            decimal minimum = 10;
            decimal maximum = 0;
            decimal sumScores = 0;
            int totaalOpgelost = 0;
            int j = 0;
            for (int i = 0; i < filteredScores.Count; i++)
            {
                int aantalOpgelost = filteredScores[i].AantalOpgelost;
                decimal factor = (decimal)10 / (decimal)aantalOpgelost;
                int scoreOpTien = filteredScores[i].ScoreOpTien;

                //wat doen we met gedeeltelijk afgewerkte oefeningen voor de
                //afgeleide cijfers ?
                //al dan niet meerekenen voor minima en maxima ?
                //-> meerekenen, maar factor incalculeren
                decimal score = filteredScores[i].ScoreOpTien * factor;
                if (score < minimum)
                    minimum = score;
                if (score > maximum)
                    maximum = score;
                sumScores += scoreOpTien;
                totaalOpgelost += aantalOpgelost;

                if (j == (grouping[groupNumber] - 1))
                {
                    ScoreSet scoreSet = new ScoreSet();

                    scoreSet.Maximum = maximum;
                    scoreSet.Minimum = minimum;
                    scoreSet.Gemiddelde = (sumScores / totaalOpgelost) * 10;

                    scoreSetList.Add(scoreSet);

                    j = 0;
                    minimum = 10;
                    maximum = 0;
                    sumScores = 0;
                    totaalOpgelost = 0;

                    groupNumber++;
                }
                else
                {
                    j++;
                }
            }

            return scoreSetList;
        }

        private List<int> GetGroupings(int defaultGrouping, int pogc)
        {
            List<int> groupings = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                if (i < pogc)
                {
                    groupings.Add(defaultGrouping + 1);
                }
                else
                {
                    groupings.Add(defaultGrouping);
                }
            }

            return groupings;
        }

    }
}
