#region

using Microsoft.Xna.Framework.Content;
using SharedGameData.LevelClasses;
using WinFormsGraphicsDevice;

#endregion

namespace SharedGameData.Editor
{
    public enum EditorMode
    {
        Selectmode,
        MoveAsset,
        AssetPlacement
    }

    public static class StaticEditorMode
    {
        public static ContentManager ContentManager;
        public static EditorMode EditorMode = EditorMode.Selectmode;

        public static Level LevelInstance;

        public static BaseActor SelectedObject;
    }
}