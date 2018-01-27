#region Usings

#endregion

namespace SharedGameData.LevelClasses {
    #region Usings

    using System.Collections.Generic;
    using Assets;

    #endregion

    public class Level {
        public Level() {
            Assets = new List<BaseActor>();
        }

        public List<BaseActor> Assets { get; set; }
    }
}