using System.Collections.Generic;

namespace CL.Entity
{
    public class Profile
    {
        public string Name { get; set; }
        public int Image { get; set; }
        public RolesEnum Role { get; set; }

        public int? CountOrangesMaxValue { get; set; }
        public int[] CountOrangesExcludedValues { get; set; }
        public int? RecogniseNumbersMaxValue { get; set; }
        public int[] RecogniseNumbersExcludedValues { get; set; }

        public GamesList Games { get; set; } = new GamesList();

        private void CreateGames()
        {
            var copycat = new Game {Name = "CopyCat"};
            var recogniseNumbers = new Game {Name = "RecogniseNumbers"};
            var countoranges = new Game {Name = "CountOranges"};

            Games.Add(copycat);
            Games.Add(recogniseNumbers);
            Games.Add(countoranges);
        }

        public List<Score> Scores(GamesEnum game)
        {
            if (Games.Count == 0)
            {
                CreateGames();
            }

            switch (game)
            {
                case GamesEnum.copycat:
                    return Games[0].Scores;
                case GamesEnum.recognisenumbers:
                    return Games[1].Scores;
                case GamesEnum.countoranges:
                    return Games[2].Scores;
                default:
                    return null;
            }
        }

    }
}
