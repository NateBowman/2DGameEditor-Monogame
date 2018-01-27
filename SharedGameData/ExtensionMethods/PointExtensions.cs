#region Usings

#endregion

namespace SharedGameData.ExtensionMethods {
    #region Usings

    using Microsoft.Xna.Framework;
    using Point = System.Drawing.Point;

    #endregion

    public static class PointExtensions {
        public static Vector2 ToVector2(this Point p) {
            return new Vector2(p.X, p.Y);
        }

        public static Microsoft.Xna.Framework.Point ToXnaPoint(this Point p) {
            return new Microsoft.Xna.Framework.Point(p.X, p.Y);
        }
    }
}