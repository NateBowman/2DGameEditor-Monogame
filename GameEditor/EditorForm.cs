#region Usings

#endregion

namespace GameEditor {
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Properties;
    using SharedGameData;
    using SharedGameData.Assets;
    using SharedGameData.Editor;
    using SharedGameData.ExtensionMethods;
    using SharedGameData.LevelClasses;
    using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
    using Point = System.Drawing.Point;

    #endregion

    public partial class formEditor : Form {
        private readonly List<BaseActor> objectsUnderCursor;

        private bool _canRefreshPropertyGrid;
        private int _propertyGridRefreshInterval = 250;

        private Timer _propertyGridRefreshTimer;

        private bool _ScaleInprogress;

        public formEditor() {
            InitializeComponent();
            AllowDrop = true;
            StaticEditorMode.LevelInstance = new Level();
            objectsUnderCursor = new List<BaseActor>();
            MouseWheel += editorControl_GameView_OnMouseWheel;
            editorControl_GameView.MouseMove += editorControl_GameView.UpdateCoords;
            StaticEditorMode.SelectionChanged += o => {
                                                     if (propertyGrid1.SelectedObject is BaseActor actor) {
                                                         actor.PropertyChanged -= OnSelectedValueChanged;
                                                     }
                                                     propertyGrid1.SelectedObject = o;
                                                     if (propertyGrid1.SelectedObject is BaseActor newActor) {
                                                         newActor.PropertyChanged += OnSelectedValueChanged;
                                                         treeViewHierarchy.SelectedNode = treeViewHierarchy.Nodes.Find(newActor.Id, true).First() ?? treeViewHierarchy.TopNode;
                                                     }
                                                 };

            hScrollBar1.Minimum = editorControl_GameView.Width / 2;
            hScrollBar1.Maximum = 3000;
            vScrollBar1.Minimum = editorControl_GameView.Height / 2;
            vScrollBar1.Maximum = 3000;

            StaticGlobalInput.InitialiseInputHandlers(editorControl_GameView);

            _propertyGridRefreshTimer = new Timer();
            _propertyGridRefreshTimer.Interval = _propertyGridRefreshInterval;
            _propertyGridRefreshTimer.Tick += delegate { _canRefreshPropertyGrid = true; };
            _propertyGridRefreshTimer.Start();

            PopulateTree();
        }

        public static string[] GetXnaFileList() {
            return Directory.GetFiles(Path.Combine(Application.StartupPath, "Content"), "*.xnb", SearchOption.TopDirectoryOnly);
        }

        public Point GetMouseVPos() {
            var transformedMousePos = Vector2.Transform(StaticGlobalInput.currentMouse.Position.ToVector2(),
                                                        Matrix.Invert(editorControl_GameView.Camera.get_Transformation(editorControl_GameView.GraphicsDevice)));

            return transformedMousePos.ToSystemPoint();
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

        public void OnSelectedValueChanged(object sender, PropertyChangedEventArgs e) {
            if (_canRefreshPropertyGrid) {
                propertyGrid1.Refresh();
                _canRefreshPropertyGrid = false;
            }
            if (sender is Asset asset) {
                if (sender.GetType().GetProperty(e.PropertyName).Name == "Name") {
                    UpdateTreeName(asset);
                }
            }
        }

        /// <summary>
        ///     Recursive population of the tree from the assets collection
        /// </summary>
        public void PopulateTree() {
            treeViewHierarchy.BeginUpdate();
            treeViewHierarchy.Nodes.Clear();

            void AddNodes(string id, TreeNode node) {
                foreach (var baseActor1 in StaticEditorMode.LevelInstance.Assets.Where(baseActor => baseActor.ParentId == id)) {
                    var childNode = node.Nodes.Add(baseActor1.Id, baseActor1.Name);
                    AddNodes(baseActor1.Id, childNode);
                }
            }

            var root = new TreeNode("root");
            AddNodes(null, root);

            treeViewHierarchy.Nodes.Add(root);
            treeViewHierarchy.TopNode = root;

            root.Expand();

            treeViewHierarchy.EndUpdate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case (Keys.Q):
                    toolStripMain.Items[0].PerformClick();
                    break;
                case (Keys.W):
                    toolStripMain.Items[1].PerformClick();
                    break;
                case (Keys.E):
                    toolStripMain.Items[2].PerformClick();
                    break;
                case (Keys.R):
                    toolStripMain.Items[3].PerformClick();
                    break;
                case (Keys.Delete):
                    DeleteSelectedActor();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AddTreeNodeFromAsset(Asset asset) {
            treeViewHierarchy.BeginUpdate();
            // Add to the treeview
            if (treeViewHierarchy.TopNode != null) {
                treeViewHierarchy.SelectedNode = treeViewHierarchy.TopNode.Nodes.Add(asset.Id, asset.Name);
            }
            else {
                treeViewHierarchy.SelectedNode = treeViewHierarchy.Nodes.Add(asset.Id, asset.Name);
            }
            treeViewHierarchy.EndUpdate();
        }

        private void btnMoveMode_Click(object sender, EventArgs e) {
            SetStateFromButton(sender, EditorMode.MoveAsset);
        }

        private void btnPlaceMode_Click(object sender, EventArgs e) {
            SetStateFromButton(sender, EditorMode.AssetPlacement);
        }

        private void btnScaleMode_Click(object sender, EventArgs e) {
            SetStateFromButton(sender, EditorMode.ScaleMode);
        }

        private void btnSelectMode_Click(object sender, EventArgs e) {
            SetStateFromButton(sender, EditorMode.Selectmode);
        }

        private void button_ImportAsset_Click(object sender, EventArgs e) {
            var frmImport = new frm_Import();
            frmImport.ShowDialog();
            Do_Refresh_XNB_Asset_List();
        }

        private void button_RemoveAsset_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Really remove the asset from the project? \r\n This will remove all assets of this type in the scene.", "Remove Asset", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes) {
                var nameToRemove = listBox_Assets.SelectedItem.ToString();
                StaticEditorMode.RemoveAssetsWithTextureName(nameToRemove);

                var files = GetXnaFileList();

                foreach (var file in files) {
                    if (Path.GetFileNameWithoutExtension(file) == nameToRemove) {
                        try {
                            File.Delete(file);
                        }
                        catch (Exception exception) {
                            Console.WriteLine(exception);
                            throw;
                        }
                    }
                }

                Do_Refresh_XNB_Asset_List();

                PopulateTree();
            }
        }

        private void DeleteActor(TreeNode node) {
//            StaticEditorMode.RemoveAsset(actor);

            if (node != null) {
                ExecuteOnActorAndAllTreeChildren(node, (n, actor) => StaticEditorMode.RemoveAsset(actor));
                node.Remove();
            }
        }

        private void DeleteActor(BaseActor actor) {
            if (actor != null) {
                DeleteActor(treeViewHierarchy.Nodes.Find(actor.Id, true).FirstOrDefault());
            }
        }

        private void DeleteSelectedActor() {
            DeleteActor(StaticEditorMode.SelectedObject);
        }

        private void Do_Refresh_XNB_Asset_List() {
            listBox_Assets.Items.Clear();

            foreach (var file in GetXnaFileList()) {
                listBox_Assets.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void editorControl_GameView_MouseDown(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseButtons(e, true);

            var p = GetMouseVPos();
            //if (!StaticGlobalInput.IsNewLeftClick()) {
            //    return;
            //}

            // Right click should not place an object
            if (StaticGlobalInput.IsNewRightClick()) {
                editorControl_GameView.Camera.Zoom = 1.0f;
                UpdateZoomTracker();
                return;
            }

            switch (StaticEditorMode.EditorMode) {
                case EditorMode.Selectmode: {
                    StaticEditorMode.SelectedObject = objectsUnderCursor.OrderByDescending(actor => actor.Depth).LastOrDefault();

                    break;
                }
                case EditorMode.AssetPlacement: {
                    if ((StaticGlobalInput.currentMouse.LeftButton == ButtonState.Pressed)) {
                        if (listBox_Assets.SelectedItem == null) {
                            MessageBox.Show(Resources.SelectAssetError, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (IsValidContent(listBox_Assets.SelectedItem.ToString(), typeof(Texture2D))) {
                            if (listBox_Assets.SelectedItem != null) {
                                var asset = new Asset(StaticEditorMode.ContentManager, listBox_Assets.SelectedItem.ToString(), p.ToVector2());
                                StaticEditorMode.LevelInstance.Assets.Add(asset);
                                AddTreeNodeFromAsset(asset);
                                StaticEditorMode.SelectedObject = asset;
                            }
                        }
                        else {
                            MessageBox.Show(string.Format(Resources.InvalidAssetType, typeof(Texture2D)), Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    break;
                }
                case EditorMode.MoveAsset: {
                    if (!objectsUnderCursor.Contains(StaticEditorMode.SelectedObject)) {
                        StaticEditorMode.SelectedObject = objectsUnderCursor.OrderByDescending(actor => actor.Depth).LastOrDefault();
                    }

                    break;
                }
            }
        }

        private void editorControl_GameView_MouseEnter(object sender, EventArgs e) {
            editorControl_GameView.Focus();
        }

        private void editorControl_GameView_MouseLeave(object sender, EventArgs e) {
            Cursor = Cursors.Default;
        }

        private void editorControl_GameView_MouseMove(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseMovement(e);
            var p = GetMouseVPos();
            objectsUnderCursor.Clear();
            SetObjectsUnderPoint(p);

            switch (StaticEditorMode.EditorMode) {
                case EditorMode.ScaleMode: {
                    if (IsSomethingSelected()) {
                        // Do not rescale unless the mouse is over the object
                        if (objectsUnderCursor.Contains(StaticEditorMode.SelectedObject) || _ScaleInprogress) {
                            Cursor = Cursors.SizeNWSE;

                            if ((StaticGlobalInput.currentMouse.LeftButton == ButtonState.Pressed)) {
                                _ScaleInprogress = true;
                                StaticEditorMode.SelectedObject.Scale += (StaticGlobalInput.GetMousePosDelta().ToVector2() * 0.01f);
                                StaticEditorMode.SelectedObject.Scale += (StaticGlobalInput.GetMousePosDelta().ToVector2() * 0.01f);
                                break;
                            }
                        }
                    }

                    if (objectsUnderCursor.Count == 0) {
                        Cursor = Cursors.Default;
                    }

                    break;
                }
                case EditorMode.Selectmode: {
                    Cursor = objectsUnderCursor.Count > 0 ? Cursors.Hand : Cursors.Default;
                    break;
                }
                case EditorMode.MoveAsset: {
                    Cursor = objectsUnderCursor.Count > 0 ? Cursors.NoMove2D : Cursors.Default;

                    if (IsSomethingSelected()) {
                        foreach (var actor in StaticEditorMode.LevelInstance.Assets) {
                            if (actor.BoundingBox.Contains(p)) {
                                objectsUnderCursor.Add(actor);
                            }
                        }

                        // Do not move unless the mouse is over the object
                        if ((StaticGlobalInput.currentMouse.LeftButton == ButtonState.Pressed) && objectsUnderCursor.Contains(StaticEditorMode.SelectedObject)) {
                            // As the treeview is already populated, use it to quickly get all children and move them
                            var itemRoot = treeViewHierarchy.Nodes.Find(StaticEditorMode.SelectedObject.Id, true).FirstOrDefault();
                            if (itemRoot != null) {
                                ExecuteOnActorAndAllTreeChildren(itemRoot,
                                                                 (node, actor) => {
                                                                     actor.Position = actor.Position + (StaticGlobalInput.GetMousePosDelta().ToVector2() *
                                                                                                        (1f / editorControl_GameView.Camera.Zoom));
                                                                 });
                            }
                        }
                    }
                    else {
                        Cursor = Cursors.Default;
                    }

                    break;
                }
                case EditorMode.AssetPlacement: {
                    //TODO Add placement

                    break;
                }
            }

            if (StaticGlobalInput.currentMouse.MiddleButton == ButtonState.Pressed) {
                editorControl_GameView.Camera.Pos -= StaticGlobalInput.GetMousePosDelta().ToVector2();
                editorControl_GameView.Camera.Pos = Vector2.Clamp(editorControl_GameView.Camera.Pos, new Vector2(hScrollBar1.Minimum, vScrollBar1.Minimum),
                                                                  new Vector2(hScrollBar1.Maximum, vScrollBar1.Maximum));

                hScrollBar1.Value = (int) editorControl_GameView.Camera.Pos.X;
                vScrollBar1.Value = (int) editorControl_GameView.Camera.Pos.Y;
            }
        }

        private void editorControl_GameView_MouseUp(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseButtons(e, false);
            _ScaleInprogress = false;
        }

        private void editorControl_GameView_OnMouseWheel(object sender, MouseEventArgs e) {
            StaticGlobalInput.HandleMouseWheel(e);

            if (editorControl_GameView.Focused) {
                if (e.Delta > 0) {
                    editorControl_GameView.Camera.Zoom += 0.05f;
                }

                else if (e.Delta < 0) {
                    editorControl_GameView.Camera.Zoom -= 0.05f;
                }
                UpdateZoomTracker();
            }
        }

        private void ExecuteOnActorAndAllTreeChildren(TreeNode node, Action<TreeNode, BaseActor> action) {
            // Move parent
            action(node, StaticEditorMode.LevelInstance.Assets.FirstOrDefault(actor => actor.Id == node.Name));
            ExecuteOnAllTreeChildren(node, action);
        }

        private void ExecuteOnActorAndAllTreeChildren(string id, Action<TreeNode, BaseActor> action) {
            var node = treeViewHierarchy.Nodes.Find(id, true).FirstOrDefault();
            ExecuteOnActorAndAllTreeChildren(node, action);
        }

        private void ExecuteOnAllTreeChildren(TreeNode node, Action<TreeNode, BaseActor> action) {
            if (node == null) {
                return;
            }

            // Recursively move children
            foreach (TreeNode n in node.Nodes) {
                ExecuteOnAllTreeChildren(n, action);
                action(n, StaticEditorMode.LevelInstance.Assets.FirstOrDefault(actor => actor.Id == n.Name));
            }
        }

        private void ExecuteOnAllTreeChildren(string id, Action<TreeNode, BaseActor> action) {
            var node = treeViewHierarchy.Nodes.Find(id, true).FirstOrDefault();
            ExecuteOnAllTreeChildren(node, action);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Exiting without saving will result in the loss of work", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                Application.Exit();
            }
        }

        private void formEditor_DragDrop(object sender, DragEventArgs e) {
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

        private void formEditor_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        private void formEditor_Load(object sender, EventArgs e) {
            Do_Refresh_XNB_Asset_List();
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
            editorControl_GameView.Camera.Pos = new Vector2(hScrollBar1.Value, editorControl_GameView.Camera.Pos.Y);
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
                    editorControl_GameView.DoNotDraw = true;
                    StaticEditorMode.LevelInstance = x.Deserialize(reader) as Level;

                    foreach (var asset in StaticEditorMode.LevelInstance.Assets) {
                        asset.LoadAsset(StaticEditorMode.ContentManager, asset.TextureName);
                    }

                    editorControl_GameView.DoNotDraw = false;

                    PopulateTree();

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
                    MessageBox.Show("Level Loaded succesfully!", "Level loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show($"Level Load failed!", "Level loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void SetObjectsUnderPoint(Point p) {
            foreach (var actor in StaticEditorMode.LevelInstance.Assets) {
                if (actor.BoundingBox.Contains(p)) {
                    objectsUnderCursor.Add(actor);
                }
            }
        }

        private void SetStateFromButton(object sender, EditorMode mode) {
            UncheckAllToolStripButtons();
            if (sender is ToolStripButton button) {
                button.CheckState = CheckState.Checked;
            }
            StaticEditorMode.EditorMode = mode;
        }

        private void toolStripMenuItemHelp_Click(object sender, EventArgs e) {
            var formAbout = new AboutForm();
            formAbout.Show();
        }

        private void trackBarZoom_ValueChanged(object sender, EventArgs e) {
            if (sender is TrackBar bar) {
                var mid = (bar.Maximum - bar.Minimum) / 2;

                var multiplier = 1f;

                //float mul = bar.Value / (float)mid;

                //if (bar.Value < 100) {

                //    multiplier = (bar.Value) / 100f;

                //}
                //else {

                //    multiplier = 1f + (bar.Value - 100) * 0.05f;
                //}

                multiplier = trackBarZoom.Value / 100f;

                editorControl_GameView.Camera.Zoom = 1f * multiplier;
                labelZoomFactor.Text = editorControl_GameView.Camera.Zoom.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void treeViewHierarchy_DragDrop(object sender, DragEventArgs e) {
            // Retrieve the client coordinates of the drop location.
            var targetPoint = treeViewHierarchy.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location.
            var targetNode = treeViewHierarchy.GetNodeAt(targetPoint);

            // Retrieve the node that was dragged.
            var draggedNode = (TreeNode) e.Data.GetData(typeof(TreeNode));

            // Confirm that the node at the drop location is not 
            // the dragged node and that target node isn't null
            // (for example if you drag outside the control)
            if (!draggedNode.Equals(targetNode) && (targetNode != null)) {
                // check the target node is not a child of the dragged node
                var child = false;
                var currentNode = targetNode;
                while (currentNode != null) {
                    if (currentNode == draggedNode) {
                        child = true;
                        break;
                    }

                    currentNode = currentNode.Parent;
                }

                if (child) {
                    return;
                }

                // Remove the node from its current 
                // location and add it to the node at the drop location.
                draggedNode.Remove();
                targetNode.Nodes.Add(draggedNode);

                // Expand the node at the location 
                // to show the dropped node.
                targetNode.Expand();

                // Update the parentID of the asset
                var draggedActor = StaticEditorMode.LevelInstance.Assets.First(actor => actor.Id == draggedNode.Name);
                draggedActor.ParentId = targetNode.Name;
            }
        }

        private void treeViewHierarchy_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void treeViewHierarchy_ItemDrag(object sender, ItemDragEventArgs e) {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void treeViewHierarchy_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node != null) {
                if (StaticEditorMode.LevelInstance.Assets.Count > 0) {
                    var baseActor = StaticEditorMode.LevelInstance.Assets.FirstOrDefault(act => act.Id == e.Node.Name);
                    StaticEditorMode.SelectedObject = baseActor;
                }
            }
        }

        private void UncheckAllToolStripButtons() {
            foreach (ToolStripItem item in toolStripMain.Items) {
                if (item is ToolStripButton button) {
                    button.Checked = false;
                }
            }
        }

        private void UpdateTreeName(BaseActor actor) {
            var node = treeViewHierarchy.Nodes.Find(actor.Id, true).FirstOrDefault();
            treeViewHierarchy.BeginUpdate();
            node.Text = actor.Name;
            treeViewHierarchy.EndUpdate();
        }

        private void UpdateZoomTracker() {
            var val = (int) Math.Max(trackBarZoom.Minimum, Math.Min((editorControl_GameView.Camera.Zoom * 100f), trackBarZoom.Maximum));

            // force an update, hacky way to limit 
            trackBarZoom.Value += (trackBarZoom.Value > 0) ? -1 : 1;
            trackBarZoom.Value = val;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e) {
            editorControl_GameView.Camera.Pos = new Vector2(editorControl_GameView.Camera.Pos.X, vScrollBar1.Value);
            Refresh();
        }
    }
}