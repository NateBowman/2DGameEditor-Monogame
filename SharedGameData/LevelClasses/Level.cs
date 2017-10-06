#region Usings

using System.Collections.Generic;
using WinFormsGraphicsDevice;

#endregion

namespace SharedGameData.LevelClasses {
    public class Level {
        public Level() {
            Assets = new List<BaseActor>();
        }

        public List<BaseActor> Assets { get; set; }
    }
}