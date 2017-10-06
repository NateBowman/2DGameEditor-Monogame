#region Usings

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using WinFormsGraphicsDevice;

#endregion

namespace SharedGameData {
    public class Asset : BaseActor {
        public Asset() { }

        public Asset(ContentManager contentManager, string assetName, Vector2 pos) : base(contentManager, assetName, pos) { }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch, bool drawHighlighted) {
            if (drawHighlighted) {
                spriteBatch.Draw(Texture, Position, Color.Red);
            }
            else {
                Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime) { }
    }
}