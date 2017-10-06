#region Usings

using Microsoft.Xna.Framework;
using Point = System.Drawing.Point;

#endregion

namespace SharedGameData.ExtensionMethods {
    public static class RectangleExtensions {
        public static bool Contains(this Rectangle rect, Point p) {
            return rect.Contains(p.ToXnaPoint());
        }

        public static Vector2[] GetCorners(this Rectangle rect) {
            return new[] {
                // Top Left
                new Vector2(rect.X, rect.Y),

                // Top Right
                new Vector2(rect.X + rect.Width, rect.Y),

                // Bottom Left
                new Vector2(rect.X + rect.Width, rect.Y + rect.Height),

                // Bottom Right
                new Vector2(rect.X, rect.Y + rect.Height)
            };
        }
    }
}