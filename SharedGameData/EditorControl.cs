#region

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedGameData;
using SharedGameData.Camera2D;
using SharedGameData.Editor;
using SharedGameData.ExtensionMethods;

#endregion

namespace WinFormsGraphicsDevice {
    public class EditorControl : GraphicsDeviceControl {
        private ContentManager content;
        private PrimitiveBatch primitiveBatch;
        private Random randomizer;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        private Texture2D background;
        private string localCoords = "";
        private string worldCoords = "";
        private string camCoords = "";

        private Stopwatch timer;
        private TimeSpan lastUpdate;
        private TimeSpan total;
        private TimeSpan elapsed;
        private GameTime gameTime;
        private float fElapsed = 0.0f;

        public Camera2D Camera { get; set; }

        public bool DoNotDraw = false;

        public void Update() {
            UpdateTime();
        }

        private void UpdateTime() {
            total = timer.Elapsed;
            elapsed = total - lastUpdate;
            gameTime = new GameTime(total, elapsed, false);
            lastUpdate = total;
        }

        protected override void Draw() {
            if (DoNotDraw)
            {
                return;
            }

            var convertedPos = PointToClient(MousePosition);
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null,Camera.get_Transformation(GraphicsDevice));
            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            foreach (var asset in StaticEditorMode.LevelInstance.Assets) {
                asset.Draw(spriteBatch, asset == StaticEditorMode.SelectedObject);

                DrawLines(asset.BoundingBox.GetCorners());
            }
            spriteBatch.End();
            
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, localCoords, Vector2.Zero, Color.Black);
            spriteBatch.DrawString(spriteFont, worldCoords, new Vector2(0, 20), Color.Black);
            
            if(gameTime != null)
            {
                spriteBatch.DrawString(spriteFont, total.TotalSeconds.ToString(CultureInfo.InvariantCulture), new Vector2(0, 60), Color.Black);
                spriteBatch.DrawString(spriteFont, elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture), new Vector2(0, 80), Color.Black);
                spriteBatch.DrawString(spriteFont, gameTime.ElapsedGameTime.TotalSeconds.ToString(CultureInfo.InvariantCulture), new Vector2(0, 100), Color.Black);
            }

            spriteBatch.End();

            //if(OnDraw != null)
            //{
            //    OnDraw(this, null);
            //}
        }

        public void UpdateCoords(object sender, EventArgs e) {
            localCoords = $"x: {StaticGlobalInput.currentMouse.X} / y: {StaticGlobalInput.currentMouse.Y}";

            Vector2 transformedMousePos = Vector2.Transform(StaticGlobalInput.currentMouse.Position.ToVector2(),
                Matrix.Invert(Camera.get_Transformation(GraphicsDevice)));

            worldCoords = $"x: {transformedMousePos.X} / y: {transformedMousePos.Y}";

            camCoords = $"x: {Camera.Pos.X} / y: {Camera.Pos.Y}";
            
        }

        protected override void Initialize() {
            content = new ContentManager(Services, "Content");
            StaticEditorMode.ContentManager = content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = content.Load<SpriteFont>("Fonts/hudfont");
            primitiveBatch = new PrimitiveBatch(GraphicsDevice);

            Application.Idle += delegate { Update(); };
            Application.Idle += delegate { Invalidate(); };

            Camera = new Camera2D {Pos = new Vector2(this.Width / 2, this.Height / 2)};
            background = content.Load<Texture2D>("Images/ScrollingTexture");


            timer = new Stopwatch();
            timer.Start();

            //if (Microsoft.Xna.Framework.Input.Mouse.WindowHandle != this.Handle)
            //    Microsoft.Xna.Framework.Input.Mouse.WindowHandle = this.Handle;

            randomizer = new Random();
        }

        private void DrawLines(Vector2[] lineList) {
            primitiveBatch.Begin(PrimitiveType.LineList, Camera.get_Transformation(GraphicsDevice));

            primitiveBatch.AddVertex(lineList[0], Color.White);
            primitiveBatch.AddVertex(lineList[1], Color.White);

            primitiveBatch.AddVertex(lineList[1], Color.White);
            primitiveBatch.AddVertex(lineList[2], Color.White);

            primitiveBatch.AddVertex(lineList[2], Color.White);
            primitiveBatch.AddVertex(lineList[3], Color.White);

            primitiveBatch.AddVertex(lineList[3], Color.White);
            primitiveBatch.AddVertex(lineList[0], Color.White);

            primitiveBatch.End();
        }
    }
}