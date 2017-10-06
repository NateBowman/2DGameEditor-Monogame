#region

using System.Collections.Generic;
using System.Windows.Forms;
using SharedGameData;
using SharedGameData.Editor;
using SharedGameData.LevelClasses;
using WinFormsGraphicsDevice;

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

        private void editorControl1_MouseDown(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseButtons(e, true);
        }

        private void editorControl1_MouseMove(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseMovement(e);
        }

        private void editorControl1_MouseUp(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseButtons(e, false);
        }

        private void editorControl1_OnMouseWheel(object sender, MouseEventArgs e)
        {
            StaticGlobalInput.HandleMouseWheel(e);
        }

    }
}