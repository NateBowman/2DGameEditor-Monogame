#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using SharedGameData;
using SharedGameData.Assets;
using SharedGameData.Editor;
using SharedGameData.ExtensionMethods;
using SharedGameData.LevelClasses;
using WinFormsGraphicsDevice;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

#endregion

namespace GameEditor {
    public partial class Form1 : Form {
        private List<BaseActor> objectsUnderCursor;

        public Form1() {
            InitializeComponent();
            StaticEditorMode.LevelInstance = new Level();
            objectsUnderCursor = new List<BaseActor>();
            MouseWheel += editorControl1_OnMouseWheel;

            StaticGlobalInput.InitialiseInputHandlers(editorControl1);
        }

        private void btnMoveMode_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            btnMoveMode.CheckState = CheckState.Checked;
            StaticEditorMode.EditorMode = EditorMode.MoveAsset;
        }

        private void btnPlaceMode_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            btnPlaceMode.CheckState = CheckState.Checked;
            StaticEditorMode.EditorMode = EditorMode.AssetPlacement;
        }

        private void btnSelectMode_Click(object sender, EventArgs e) {
            UncheckToolStripButtons(Controls);
            btnSelectMode.CheckState = CheckState.Checked;
            StaticEditorMode.EditorMode = EditorMode.Selectmode;
        }

        private void editorControl1_MouseDown(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseButtons(e, true);

            var p = editorControl1.PointToClient(Cursor.Position);
            if (!StaticGlobalInput.IsNewLeftClick()) {
                return;
            }

            switch (StaticEditorMode.EditorMode) {
                case EditorMode.Selectmode: {
                    StaticEditorMode.SelectedObject = objectsUnderCursor.Count > 0 ? objectsUnderCursor[0] : null;
                }
                    break;
                case EditorMode.AssetPlacement: {
                    StaticEditorMode.LevelInstance.Assets.Add(new Asset(StaticEditorMode.ContentManager, "Images/icon", p.ToVector2()));
                }
                    break;
            }
        }

        private void editorControl1_MouseMove(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseMovement(e);
            var p = editorControl1.PointToClient(Cursor.Position);
            objectsUnderCursor.Clear();

            switch (StaticEditorMode.EditorMode) {
                case EditorMode.Selectmode: {
                    foreach (var actor in StaticEditorMode.LevelInstance.Assets) {
                        if (actor.BoundingBox.Contains(p)) {
                            objectsUnderCursor.Add(actor);
                        }
                    }

                    break;
                }
                case EditorMode.MoveAsset: {
                    if (IsSomethingSelected()) {
                        if (StaticGlobalInput.currentMouse.LeftButton == ButtonState.Pressed) {
                            StaticEditorMode.SelectedObject.Position = p.ToVector2();
                        }
                    }
                }
                    break;
                case EditorMode.AssetPlacement: {
                    //TODO Add placement
                }
                    break;
            }
        }

        private void editorControl1_MouseUp(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseButtons(e, false);
        }

        private void editorControl1_OnMouseWheel(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseWheel(e);
        }

        private bool IsSomethingSelected() {
            return StaticEditorMode.SelectedObject != null;
        }

        private void UncheckToolStripButtons(Control.ControlCollection controls) {
            btnSelectMode.Checked = btnMoveMode.Checked = btnPlaceMode.Checked = false;
        }

        private List<Type> GetAssemblyTypesFromType(params Type[] types)
        {
            var knownTypes = new List<Type>();
            foreach (var type in types)
            {
                foreach (var t in System.Reflection.Assembly.GetAssembly(type).GetTypes())
                {
                    knownTypes.Add(t);
                }
            }
            return knownTypes;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Xml Files|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Type[] baseTypes = {typeof(BaseActor), typeof(Level)};
                    Type[] assemblyTypes = GetAssemblyTypesFromType(baseTypes).ToArray();

                    XmlSerializer x = new XmlSerializer(StaticEditorMode.LevelInstance.GetType(), assemblyTypes);

                    using (System.IO.TextReader reader = new StreamReader(openFileDialog1.FileName))
                    {
                        editorControl1.DoNotDraw = true;
                        StaticEditorMode.LevelInstance = x.Deserialize(reader) as Level;

                        for (var i = 0; i < StaticEditorMode.LevelInstance.Assets.Count; i++)
                        {
                            var asset = StaticEditorMode.LevelInstance.Assets[i];
                            asset.LoadAsset(StaticEditorMode.ContentManager, asset.TextureName);
                        }
                        editorControl1.DoNotDraw = false;
                        MessageBox.Show("Level Loaded!", "Level loaded info box", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Level Load failed! \n {ex.Message}", "Level loaded info box", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = @"Xml Files|*.xml";
            saveFileDialog1.AddExtension = true;

            Type[] baseTypes = { typeof(BaseActor), typeof(Level) };
            Type[] assemblyTypes = GetAssemblyTypesFromType(baseTypes).ToArray();

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    XmlSerializer x = new XmlSerializer(StaticEditorMode.LevelInstance.GetType(), assemblyTypes);
                    using (System.IO.TextWriter writer = new StreamWriter(saveFileDialog1.FileName))
                    {
                        x.Serialize(writer, StaticEditorMode.LevelInstance);
                        MessageBox.Show("Save Successful");
                    }
                }
            }
            catch( Exception ex)
            {
                MessageBox.Show($"Level Save failed! \n {ex.Message}", "Level loaded info box", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}