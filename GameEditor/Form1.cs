#region

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SharedGameData;
using SharedGameData.Editor;
using SharedGameData.ExtensionMethods;
using SharedGameData.LevelClasses;
using WinFormsGraphicsDevice;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

#endregion

namespace GameEditor
{
    public partial class Form1 : Form
    {
        private List<BaseActor> objectsUnderCursor;

        public Form1()
        {
            InitializeComponent();
            StaticEditorMode.LevelInstance = new Level();
            objectsUnderCursor = new List<BaseActor>();
            MouseWheel += new MouseEventHandler(editorControl1_OnMouseWheel);

            StaticGlobalInput.InitialiseInputHandlers(editorControl1);
        }

        private void btnMoveMode_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            btnMoveMode.CheckState = CheckState.Checked;
            StaticEditorMode.EditorMode = EditorMode.MoveAsset;
        }

        private void btnPlaceMode_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            btnPlaceMode.CheckState = CheckState.Checked;
            StaticEditorMode.EditorMode = EditorMode.AssetPlacement;
        }

        private void btnSelectMode_Click(object sender, EventArgs e)
        {
            UncheckToolStripButtons(Controls);
            btnSelectMode.CheckState = CheckState.Checked;
            StaticEditorMode.EditorMode = EditorMode.Selectmode;
        }

        private void editorControl1_MouseDown(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseButtons(e, true);

            var p = editorControl1.PointToClient(Cursor.Position);
            if (!StaticGlobalInput.IsNewLeftClick())
            {
                return;
            }

            switch (StaticEditorMode.EditorMode)
            {
                case EditorMode.Selectmode:
                {
                    StaticEditorMode.SelectedObject = objectsUnderCursor.Count > 0 ? objectsUnderCursor[0] : null;
                }
                    break;
                case EditorMode.AssetPlacement:
                {
                    StaticEditorMode.LevelInstance.Assets.Add(
                        new Asset(StaticEditorMode.ContentManager, "Images/icon", p.ToVector2()));
                }
                    break;
            }
        }

        private void editorControl1_MouseMove(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseMovement(e);
            var p = editorControl1.PointToClient(Cursor.Position);
            objectsUnderCursor.Clear();

            switch (StaticEditorMode.EditorMode)
            {
                case EditorMode.Selectmode:
                {
                    foreach (var actor in StaticEditorMode.LevelInstance.Assets)
                    {
                        if (actor.BoundingBox.Contains(p))
                        {
                            objectsUnderCursor.Add(actor);
                        }
                    }

                    break;
                }
                case EditorMode.MoveAsset:
                {
                    if (IsSomethingSelected())
                    {
                        if (StaticGlobalInput.currentMouse.LeftButton == ButtonState.Pressed)
                        {
                            StaticEditorMode.SelectedObject.Position = p.ToVector2();
                        }
                    }
                }
                    break;
                case EditorMode.AssetPlacement:
                {
                    //TODO Add placement
                }
                    break;
            }
        }

        private void editorControl1_MouseUp(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseButtons(e, false);
        }

        private void editorControl1_OnMouseWheel(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseWheel(e);
        }

        private bool IsSomethingSelected()
        {
            return StaticEditorMode.SelectedObject != null;
        }

        private void UncheckToolStripButtons(Control.ControlCollection controls)
        {
            btnSelectMode.Checked = btnMoveMode.Checked = btnPlaceMode.Checked = false;
        }
    }
}