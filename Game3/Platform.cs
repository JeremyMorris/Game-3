using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game3
{
    public class Platform
    {
        public Rectangle Bounds { get; set; }

        public Texture2D Sprite { get; set; }

        public Platform(Rectangle bounds, Texture2D sprite)
        {
            Bounds = bounds;
            Sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Bounds, Color.White);
        }
    }
}
