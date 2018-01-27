#region Usings

#endregion

namespace SharedGameData.Camera2D {
    #region Usings

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    #endregion

    public class Camera2D {
        private Vector2 pos;
        private float rotation;
        private Matrix transform;
        private float zoom;

        public Camera2D() {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
        }

        public Vector2 Pos { get => pos; set => pos = value; }

        public float Rotation { get => rotation; set => rotation = value; }

        public float Zoom {
            get => zoom;
            set {
                zoom = value;
                if (zoom < 0.1f) {
                    zoom = 0.1f;
                }
            }
        }

        /// <summary>
        ///     Gets a transformation that represents the camera
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <returns></returns>
        public Matrix get_Transformation(GraphicsDevice graphicsDevice) {
            transform = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) * Matrix.CreateRotationZ(Rotation) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                        Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));

            return transform;
        }

        public void Move(Vector2 amount) {
            Pos += amount;
        }
    }
}