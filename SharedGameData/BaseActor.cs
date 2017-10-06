#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace WinFormsGraphicsDevice
{
    public abstract class BaseActor
    {
        protected Texture2D Texture;

        private Vector2 position;

        protected BaseActor()
        {
            Position = Vector2.Zero;
        }

        protected BaseActor(ContentManager contentManager, string assetName, Vector2 pos) : this()
        {
            Position = pos;

            // Virtual member call in constructor
            LoadAsset(contentManager, assetName);
        }

        public Rectangle BoundingBox { get; set; }

        public int Height { get; set; }

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                UpdateParameters();
            }
        }

        public int Width { get; set; }
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Draw(SpriteBatch spriteBatch, bool drawHighlighted);

        public virtual void LoadAsset(ContentManager contentManager, string assetName)
        {
            Texture = contentManager.Load<Texture2D>(assetName);
            UpdateParameters();
        }

        public abstract void Update(GameTime gameTime);

        public virtual void UpdateParameters()
        {
            if (Texture != null)
            {
                Width = Texture.Width;
                Height = Texture.Height;
            }

            BoundingBox = new Rectangle((int) Position.X, (int) Position.Y, Width, Height);
        }
    }
}