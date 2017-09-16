using System.Collections.Generic;

namespace CL.Entity
{
    public class Game
    {
        public string Name { get; set; }
        public List<Score> Scores { get; set; } = new List<Score>();
    }
}
