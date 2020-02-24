using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    // Matrix camera made with help from http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    public class Camera
    {
        protected float _zoom;
        public Matrix Transform { get; set; }
        public Vector2 Position { get; set; }
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }
        public float Rotation { get; set; }

        public Camera()
        {
            _zoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }
        
        public void Move(Vector2 movement)
        {
            Position += movement;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            Transform =
                Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return Transform;
        }
    }
}
