using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using System.Windows.Forms;

namespace WinFormsGraphicsDevice
{
    public class EditorControl : GraphicsDeviceControl
    {
        ContentManager content;
        Random randomizer;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        protected override void Draw()
        {

            System.Drawing.Point convertedPos = PointToClient(MousePosition);

            string message = String.Format("X: {0} Y: {1}", convertedPos.X, convertedPos.Y);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = content.Load<SpriteFont>("Fonts/hudfont");

            Application.Idle += delegate { Update(); };
            Application.Idle += delegate { Invalidate(); };

            //if (Microsoft.Xna.Framework.Input.Mouse.WindowHandle != this.Handle)
            //    Microsoft.Xna.Framework.Input.Mouse.WindowHandle = this.Handle;

            randomizer = new Random();
        }

        public void Update() { }

        }
}
