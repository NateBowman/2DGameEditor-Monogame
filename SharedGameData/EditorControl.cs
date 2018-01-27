#region

#endregion

namespace WinFormsGraphicsDevice {
    #region Usings

    using System;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using SharedGameData;
    using SharedGameData.Camera2D;
    using SharedGameData.Editor;
    using SharedGameData.ExtensionMethods;

    #endregion

    public class EditorControl : GraphicsDeviceControl {
        public bool DoNotDraw = false;

        private Texture2D background;
        private string camCoords = "";
        private ContentManager content;
        private TimeSpan elapsed;
        private float fElapsed = 0.0f;
        private GameTime gameTime;
        private TimeSpan lastUpdate;
        private string localCoords = "";
        private PrimitiveBatch primitiveBatch;
        private Random randomizer;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        private Stopwatch timer;
        private TimeSpan total;
        private string worldCoords = "";

        public Camera2D Camera { get; set; }

        public void Update() {
            UpdateTime();
        }

        public void UpdateCoords(object sender, EventArgs e) {
            localCoords = $"screen: x: {StaticGlobalInput.currentMouse.X} / y: {StaticGlobalInput.currentMouse.Y}";

            var transformedMousePos = Vector2.Transform(StaticGlobalInput.currentMouse.Position.ToVector2(), Matrix.Invert(Camera.get_Transformation(GraphicsDevice)));

            worldCoords = $"world : x: {transformedMousePos.X:F2} / y: {transformedMousePos.Y:F2}";

            camCoords = $"x: {Camera.Pos.X} / y: {Camera.Pos.Y}";
        }

        protected override void Draw() {
            if (DoNotDraw) {
                return;
            }

            var convertedPos = PointToClient(MousePosition);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.get_Transformation(GraphicsDevice));
            spriteBatch.Draw(background, Vector2.Zero, null, Color.White, 0f, new Vector2(0f, 0f), new Vector2(1f, 1f), SpriteEffects.None, 1f);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, null, null, null, null, Camera.get_Transformation(GraphicsDevice));

            foreach (var asset in StaticEditorMode.LevelInstance.Assets) {
                asset.Draw(spriteBatch);

                DrawBox(asset.BoundingBox.GetCorners(), StaticEditorMode.SelectedObject == asset);
            }

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, localCoords, new Vector2(50, 10), Color.Black, 0f, Vector2.Zero, Vector2.One * 0.5f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(spriteFont, worldCoords, new Vector2(50, 25), Color.Black, 0f, Vector2.Zero, Vector2.One * 0.5f, SpriteEffects.None, 1f);

            spriteBatch.End();
        }

        protected override void Initialize() {
            content = new ContentManager(Services, "Content");
            StaticEditorMode.ContentManager = content;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = content.Load<SpriteFont>("Fonts/hudfont");

            primitiveBatch = new PrimitiveBatch(GraphicsDevice);

            Application.Idle += delegate { Update(); };
            Application.Idle += delegate { Invalidate(); };

            Camera = new Camera2D {Pos = new Vector2(Width / 2, Height / 2)};
            background = content.Load<Texture2D>("Images/ScrollingTexture");

            timer = new Stopwatch();
            timer.Start();

            //if (Microsoft.Xna.Framework.Input.Mouse.WindowHandle != this.Handle)
            //    Microsoft.Xna.Framework.Input.Mouse.WindowHandle = this.Handle;

            randomizer = new Random();
        }

        private void DrawBox(Vector2[] lineList, bool selected) {
            var colour = (selected ? Color.Blue : Color.White);

            lineList[0].X -= 1;
            lineList[0].Y -= 1;

            lineList[1].X += 1;
            lineList[1].Y -= 1;

            lineList[2].X += 1;
            lineList[2].Y += 1;

            lineList[3].X -= 1;
            lineList[3].Y += 1;

            primitiveBatch.Begin(PrimitiveType.LineList, Camera.get_Transformation(GraphicsDevice));

            primitiveBatch.AddVertex(lineList[0], colour);
            primitiveBatch.AddVertex(lineList[1], colour);

            primitiveBatch.AddVertex(lineList[1], colour);
            primitiveBatch.AddVertex(lineList[2], colour);

            primitiveBatch.AddVertex(lineList[2], colour);
            primitiveBatch.AddVertex(lineList[3], colour);

            primitiveBatch.AddVertex(lineList[3], colour);
            primitiveBatch.AddVertex(lineList[0], colour);

            primitiveBatch.End();
        }

        private void UpdateTime() {
            total = timer.Elapsed;
            elapsed = total - lastUpdate;
            gameTime = new GameTime(total, elapsed, false);
            lastUpdate = total;
        }
    }
}