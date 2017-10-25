#region Usings

using System.Collections.Generic;
using SharedGameData.Assets;
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