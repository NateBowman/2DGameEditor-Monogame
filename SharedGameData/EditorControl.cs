#region

using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedGameData;
using SharedGameData.Editor;

#endregion

namespace WinFormsGraphicsDevice
{
    public class EditorControl : GraphicsDeviceControl
    {
        private ContentManager content;
        private Random randomizer;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        public void Update()
        {
        }

        protected override void Draw()
        {
            var convertedPos = PointToClient(MousePosition);

            var message = $"X: {StaticGlobalInput.currentMouse.X} Y: {StaticGlobalInput.currentMouse.Y}";
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, message, Vector2.Zero, Color.White);
            spriteBatch.End();

            Console.WriteLine(Mouse.GetState().X + " / " + Mouse.GetState().Y);

            //if(OnDraw != null)
            //{
            //    OnDraw(this, null);
            //}
        }

        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            StaticEditorMode.ContentManager = content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = content.Load<SpriteFont>("Fonts/hudfont");

            Application.Idle += delegate { Update(); };
            Application.Idle += delegate { Invalidate(); };

            //if (Microsoft.Xna.Framework.Input.Mouse.WindowHandle != this.Handle)
            //    Microsoft.Xna.Framework.Input.Mouse.WindowHandle = this.Handle;

            randomizer = new Random();
        }
    }
}