using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedGameData.ExtensionMethods
{
    public static class Vector2Extensions
    {
        public static System.Drawing.Point ToSystemPoint(this Microsoft.Xna.Framework.Vector2 v2) {
            return new Point((int)v2.X, (int)v2.Y);
        }
    }
}
