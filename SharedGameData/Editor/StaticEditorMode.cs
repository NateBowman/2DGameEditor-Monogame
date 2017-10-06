#region Usings

using Microsoft.Xna.Framework.Content;
using SharedGameData.LevelClasses;
using WinFormsGraphicsDevice;

#endregion

namespace SharedGameData.Editor {
    public static class StaticEditorMode {
        public static ContentManager ContentManager;
        public static EditorMode EditorMode = EditorMode.Selectmode;

        public static Level LevelInstance;

        public static BaseActor SelectedObject;
    }
}