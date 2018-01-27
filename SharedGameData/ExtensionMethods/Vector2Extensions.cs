namespace SharedGameData.ExtensionMethods {
    #region Usings

    using Microsoft.Xna.Framework;
    using Point = System.Drawing.Point;

    #endregion

    public static class Vector2Extensions {
        public static Point ToSystemPoint(this Vector2 v2) {
            return new Point((int) v2.X, (int) v2.Y);
        }
    }
}