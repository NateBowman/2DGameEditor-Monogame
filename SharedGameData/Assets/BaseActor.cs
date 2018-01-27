#region Usings

#endregion

namespace SharedGameData.Assets {
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;
    using Annotations;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Rectangle = Microsoft.Xna.Framework.Rectangle;

    #endregion

    [XmlInclude(typeof(Asset))]
    public abstract class BaseActor : INotifyPropertyChanged {
        [XmlIgnore, Browsable(true)]
        protected Texture2D Texture;

        [XmlIgnore, Browsable(true)]
        private readonly ContentManager _contentManager;

        private Rectangle _boundingBox;
        private float _depthInLayer = 1;
        private int _height;

        private string _id;

        private string _name;

        private string _parentId;

        private Vector2 _position;
        private Vector2 _scale = new Vector2(1f, 1f);
        private DrawingLayer _sortingLayer;

        private string _textureName;
        private int _width;
        private float _rotation;

        protected BaseActor() {
            Position = Vector2.Zero;
        }

        protected BaseActor(ContentManager contentManager, string assetName, Vector2 pos) : this() {
            Position = pos;
            _contentManager = contentManager;

            // Virtual member call in constructor
            LoadAsset(contentManager, assetName);
            TextureName = assetName;
            Name = $"{TextureName}:{new Random().Next(9999)}";
            Id = Guid.NewGuid().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public enum DrawingLayer {
            Background,
            Foreground
        }

        /// <summary>
        ///     Hacky conversion between system Rect for editing in the property grid
        ///     Note: decided to disable editing in favour of other options
        /// </summary>
        [TypeConverter(typeof(RectangleConverter)), Category("Size Data"), Description("The size of the bounding Box"), DisplayName("Bounding Box"), ReadOnly(true)]
        public System.Drawing.Rectangle _BoundingBox {
            get => new System.Drawing.Rectangle(_boundingBox.X, _boundingBox.Y, _boundingBox.Width, _boundingBox.Height);
            set {
                if ((_boundingBox.X == value.X) && (_boundingBox.Y == value.Y) && (_boundingBox.Width == value.Width) && (_boundingBox.Height == value.Height)) {
                    return;
                }

                _boundingBox = new Rectangle(value.X, value.Y, value.Width, value.Height);
                OnPropertyChanged();
            }
        }

        [XmlIgnore, Browsable(false)]
        public Rectangle BoundingBox {
            get => _boundingBox;
            set {
                if (value.Equals(_boundingBox)) {
                    return;
                }

                _boundingBox = value;
                OnPropertyChanged();
            }
        }

        [Category("Drawing Data"), Description("z-depth in the scene"), DisplayName("Z-Depth"), ReadOnly(true)]
        public float Depth => RealZDepth();

        [Category("Drawing Data"), Description("Depth relative to the objects in the layer"), DisplayName("Sorting Fudge")]
        public float DepthInLayer {
            get => _depthInLayer;
            set {
                if (value > 1) {
                    value = 1f;
                }
                else if (value < 0) {
                    value = 0f;
                }

                if (value.Equals(_depthInLayer)) {
                    return;
                }

                _depthInLayer = value;
                OnPropertyChanged();
            }
        }

        [XmlIgnore]
        [Category("Drawing Data"), Description("Height of the sprite")]
        public int Height {
            get => _height;
            set {
                if (value == _height) {
                    return;
                }

                var s = Scale;
                s.Y = value / (float) Texture.Height;
                _scale = s;

                _height = value;
                OnPropertyChanged();
            }
        }

        [Browsable(true), Category("~ ID"), Description("The ID of the Object"), ReadOnly(true), DisplayName("ID (Self)")]
        public string Id {
            get => _id;
            set {
                if (value == _id) {
                    return;
                }

                _id = value;
                OnPropertyChanged();
            }
        }

        [Category("~ ID"), Description("name of the object")]
        public string Name {
            get => _name;
            set {
                if (value == _name) {
                    return;
                }

                _name = value;
                OnPropertyChanged();
            }
        }

        [Browsable(true), Category("~ ID"), Description("The ID of the Parent Object"), ReadOnly(true), DisplayName("ID (Parent)")]
        public string ParentId {
            get => _parentId;
            set {
                if (value == _parentId) {
                    return;
                }

                _parentId = value;
                OnPropertyChanged();
            }
        }

        [Category("Positioning Data"), Description("The Position of the object")]
        public Vector2 Position {
            get => _position;
            set {
                if (value.Equals(_position)) {
                    return;
                }

                _position = value;
                OnPropertyChanged();
            }
        }

        [Category("Positioning Data"), Description("The Scale of the object")]
        public Vector2 Scale {
            get => _scale;
            set {
                if (value.Equals(_scale)) {
                    return;
                }

                if (value.X < 0) {
                    value.X = 0;
                }
                if (value.Y < 0) {
                    value.Y = 0;
                }
                _scale = value;
                OnPropertyChanged();
            }
        }

        [Category("Drawing Data"), Description("The Sorting layer of the sprite"), DisplayName("Layer")]
        public DrawingLayer SortingLayer {
            get => _sortingLayer;
            set {
                if (value == _sortingLayer) {
                    return;
                }

                _sortingLayer = value;
                OnPropertyChanged();
            }
        }

        [Category("Drawing Data"), Description("The name of the texture object \n Editable only to an existing texture name")]
        public string TextureName {
            get => _textureName;
            set {
                if (value == _textureName) {
                    return;
                }

                if ((_contentManager != null) && !LoadAsset(_contentManager, value)) {
                    return;
                }

                _textureName = value;
                OnPropertyChanged();
            }
        }

        [Category("Drawing Data"), Description("Height of the sprite")]
        [XmlIgnore]
        public int Width {
            get => _width;
            set {
                if (value == _width) {
                    return;
                }

                var s = Scale;
                s.X = value / (float) Texture.Width;
                _scale = s;

                _width = value;
                OnPropertyChanged();
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void Draw(SpriteBatch spriteBatch, bool drawHighlighted);

        public virtual bool LoadAsset(ContentManager contentManager, string assetName) {
            try {
                Texture = contentManager.Load<Texture2D>(assetName);
                UpdateParameters();
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

            return false;
        }

        public abstract void Update(GameTime gameTime);

        [Category("Positioning Data"), Description("The Rotation of the object in radians")]
        public float Rotation {
            get { return _rotation; }
            set {
                if (value.Equals(_rotation))
                    return;

                _rotation = value;
                OnPropertyChanged();
            }
        }

        public virtual void UpdateParameters() {
            if (Texture != null) {
                _width = (int) (Texture.Width * Scale.X);
                _height = (int) (Texture.Height * Scale.Y);
            }

            BoundingBox = new Rectangle((int) Position.X, (int) Position.Y, Width, Height);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            UpdateParameters();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected float RealZDepth() {
            // count the possible values
            var count = Enum.GetValues(typeof(DrawingLayer)).Length;
            var interval = 1f / count;

            var val = (int) SortingLayer * interval;

            return (DepthInLayer * interval) + val;
        }
    }
}