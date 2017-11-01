#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedGameData;
using SharedGameData.Assets;
using SharedGameData.Editor;
using SharedGameData.ExtensionMethods;
using SharedGameData.LevelClasses;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Point = System.Drawing.Point;

#endregion

namespace GameEditor {
    public partial class Form1 : Form {
        private List<BaseActor> objectsUnderCursor;

        public Form1() {
            InitializeComponent();
            AllowDrop = true;
            StaticEditorMode.LevelInstance = new Level();
            objectsUnderCursor = new List<BaseActor>();
            MouseWheel += editorControl1_OnMouseWheel;
            editorControl1.MouseMove += editorControl1.UpdateCoords;
            StaticEditorMode.SelectionChanged += o => propertyGrid1.SelectedObject = o;

            hScrollBar1.Minimum = editorControl1.Width / 2;
            hScrollBar1.Maximum = 3000;
            vScrollBar1.Minimum = editorControl1.Height / 2;
            vScrollBar1.Maximum = 3000;

            StaticGlobalInput.InitialiseInputHandlers(editorControl1);
        }

        public Point GetMouseVPos() {
            var transformedMousePos = Vector2.Transform(StaticGlobalInput.currentMouse.Position.ToVector2(),
                                                        Matrix.Invert(editorControl1.Camera.get_Transformation(editorControl1.GraphicsDevice)));

            return transformedMousePos.ToSystemPoint();
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

            var p = GetMouseVPos();
            //if (!StaticGlobalInput.IsNewLeftClick()) {
            //    return;
            //}

            // Right click should not place an object
            if (StaticGlobalInput.IsNewRightClick()) {
                editorControl1.Camera.Zoom = 1.0f;
                return;
            }

            switch (StaticEditorMode.EditorMode) {
                case EditorMode.Selectmode: {
                    StaticEditorMode.SelectedObject = objectsUnderCursor.Count > 0 ? objectsUnderCursor[0] : null;
                }
                    break;
                case EditorMode.AssetPlacement: {
                    if (IsValidContent(listBox_Assets.SelectedItem.ToString(), typeof(Texture2D))) {
                        if (listBox_Assets.SelectedItem != null) {
                            StaticEditorMode.LevelInstance.Assets.Add(new Asset(StaticEditorMode.ContentManager, listBox_Assets.SelectedItem.ToString(), p.ToVector2()));
                        }
                    }
                    else {
                        MessageBox.Show("This is not a Texture2D asset", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                    break;
            }

            
        }

        public bool IsValidContent(string fileToLoad, params Type[] desiredTypes) {
            object o;
            try {
                o = StaticEditorMode.ContentManager.Load<object>(fileToLoad);
                return desiredTypes.Any(type => o.GetType() == type);
            }
            catch (Exception e) {
                if (e is ContentLoadException) { }
                else {
                    throw e;
                }
            }

            return false;
        }

        private void editorControl1_MouseEnter(object sender, EventArgs e) {
            editorControl1.Focus();
        }

        private void editorControl1_MouseMove(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseMovement(e);
            var p = GetMouseVPos();
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

            if (StaticGlobalInput.currentMouse.MiddleButton == ButtonState.Pressed) {
                editorControl1.Camera.Pos -= StaticGlobalInput.GetMousePosDelta().ToVector2();
                editorControl1.Camera.Pos = Vector2.Clamp(editorControl1.Camera.Pos, new Vector2(hScrollBar1.Minimum, vScrollBar1.Minimum),
                                                          new Vector2(hScrollBar1.Maximum, vScrollBar1.Maximum));

                hScrollBar1.Value = (int) editorControl1.Camera.Pos.X;
                vScrollBar1.Value = (int) editorControl1.Camera.Pos.Y;
            }
        }

        private void editorControl1_MouseUp(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseButtons(e, false);
        }

        private void editorControl1_OnMouseWheel(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseWheel(e);

            if (editorControl1.Focused) {
                if (e.Delta > 0) {
                    editorControl1.Camera.Zoom += 0.05f;
                }

                else if (e.Delta < 0) {
                    editorControl1.Camera.Zoom -= 0.05f;
                }
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            var droppedFiles = ((string[]) e.Data.GetData((DataFormats.FileDrop))).ToList();

            if (!droppedFiles.Any()) {
                return;
            }

            // Load the first XML of the DnD operation
            for (var i = 0; i < droppedFiles.Count; i++) {
                // check for wanted file types
                if (Path.GetExtension(droppedFiles[i]) == ".xml") {
                    LoadLevel(droppedFiles[i]);

                    // exit loop
                    break;
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        private List<Type> GetAssemblyTypesFromType(params Type[] types) {
            var knownTypes = new List<Type>();
            foreach (var type in types) {
                foreach (var t in Assembly.GetAssembly(type).GetTypes()) {
                    if (type.IsAssignableFrom(t)) {
                        knownTypes.Add(t);
                    }
                }
            }

            return knownTypes;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) {
            editorControl1.Camera.Pos = new Vector2(hScrollBar1.Value, editorControl1.Camera.Pos.Y);
            Refresh();
        }

        private bool IsSomethingSelected() {
            return StaticEditorMode.SelectedObject != null;
        }

        private bool LoadLevel(string fileName) {
            try {
                // load the xml
                Type[] baseTypes = {
                    typeof(BaseActor),
                    typeof(Level)
                };

                var assemblyTypes = GetAssemblyTypesFromType(baseTypes).ToArray();

                var x = new XmlSerializer(StaticEditorMode.LevelInstance.GetType(), assemblyTypes);

                using (TextReader reader = new StreamReader(fileName)) {
                    editorControl1.DoNotDraw = true;
                    StaticEditorMode.LevelInstance = x.Deserialize(reader) as Level;

                    foreach (var asset in StaticEditorMode.LevelInstance.Assets) {
                        asset.LoadAsset(StaticEditorMode.ContentManager, asset.TextureName);
                    }

                    editorControl1.DoNotDraw = false;
                    return true;
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"{ex.Message}", "File Info Box", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.Filter = "Xml Files|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                if (LoadLevel(openFileDialog1.FileName)) {
                    MessageBox.Show("Level Loaded!", "Level loaded info box", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show($"Level Load failed!", "Level loaded info box", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveFileDialog1.Filter = @"Xml Files|*.xml";
            saveFileDialog1.AddExtension = true;

            Type[] baseTypes = {
                typeof(BaseActor),
                typeof(Level)
            };
            var assemblyTypes = GetAssemblyTypesFromType(baseTypes).ToArray();

            try {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                    var x = new XmlSerializer(StaticEditorMode.LevelInstance.GetType(), assemblyTypes);
                    using (TextWriter writer = new StreamWriter(saveFileDialog1.FileName)) {
                        x.Serialize(writer, StaticEditorMode.LevelInstance);
                        MessageBox.Show("Save Successful");
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Level Save failed! \n {ex.Message}", "Level loaded info box", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UncheckToolStripButtons(Control.ControlCollection controls) {
            btnSelectMode.Checked = btnMoveMode.Checked = btnPlaceMode.Checked = false;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
            editorControl1.Camera.Pos = new Vector2(editorControl1.Camera.Pos.X, vScrollBar1.Value);
            Refresh();
        }

        private void button_ImportAsset_Click(object sender, EventArgs e)
        {
            frm_Import frmImport = new frm_Import();
            frmImport.ShowDialog();
            Do_Refresh_XNB_Asset_List();
        }

        private void Do_Refresh_XNB_Asset_List() {
            listBox_Assets.Items.Clear();
            var files = Directory.GetFiles(Path.Combine(Application.StartupPath, "Content"), "*.xnb", SearchOption.TopDirectoryOnly);
            foreach (var file in files) {
                listBox_Assets.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Do_Refresh_XNB_Asset_List();
        }
    }
}