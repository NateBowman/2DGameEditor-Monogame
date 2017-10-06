using System.Collections.Generic;
using WinFormsGraphicsDevice;

namespace SharedGameData.LevelClasses
{
    public class Level
    {
        public List<BaseActor> Assets { get; set; }

        public Level()
        {
            Assets = new List<BaseActor>();
        }
    }
}
