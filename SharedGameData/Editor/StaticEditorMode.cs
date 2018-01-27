#region Usings

#endregion

namespace SharedGameData.Editor {
    #region Usings

    using Assets;
    using LevelClasses;
    using Microsoft.Xna.Framework.Content;

    #endregion

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

        public static void RemoveAsset(BaseActor asset) {
            LevelInstance.Assets.Remove(asset);
            ValidateSelection();
        }

        public static void RemoveAssetsWithTextureName(string textureName) {
            LevelInstance.Assets.RemoveAll(actor => actor.TextureName == textureName);
            ValidateSelection();
        }

        private static void ValidateSelection() {
            if (!LevelInstance.Assets.Contains(SelectedObject)) {
                SelectedObject = null;
            }
        }
    }
}