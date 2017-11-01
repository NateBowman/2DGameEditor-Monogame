#region Usings

using Microsoft.Xna.Framework.Content;
using SharedGameData.Assets;
using SharedGameData.LevelClasses;
using WinFormsGraphicsDevice;

#endregion

namespace SharedGameData.Editor {

    public delegate void ObjectSelectionChange(object obj);

    public static class StaticEditorMode {
        public static ContentManager ContentManager;
        public static EditorMode EditorMode = EditorMode.Selectmode;

        public static Level LevelInstance;

        private static BaseActor _selectedObject;

        public static event ObjectSelectionChange SelectionChanged;

        public static BaseActor SelectedObject {
            get => _selectedObject;
            set {
                _selectedObject = value;
                SelectionChanged?.Invoke(SelectedObject);

            }
        }
    }
}