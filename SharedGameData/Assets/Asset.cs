#region Usings

#endregion

namespace SharedGameData.Assets {
    #region Usings

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    public class Asset : BaseActor {
        public Asset() { }

        public Asset(ContentManager contentManager, string assetName, Vector2 pos) : base(contentManager, assetName, pos) { }

        public override void Draw(SpriteBatch spriteBatch) {
            Draw(spriteBatch, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Color colour) {
            spriteBatch.Draw(Texture, Position, null, colour, Rotation, new Vector2(0.5f, 0.5f), Scale, SpriteEffects.None, RealZDepth());
        }


        public override void Draw(SpriteBatch spriteBatch, bool drawHighlighted) {
            if (drawHighlighted) {
                Draw(spriteBatch, Color.Red);
            }
            else {
                Draw(spriteBatch, Color.White);
            }
        }

        public override void Update(GameTime gameTime) { }
    }
}