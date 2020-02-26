using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class Flag
    {
        public Rectangle Bounds { get; set; }

        private Texture2D _redSprite;
        private Texture2D _greenSprite;

        public bool Completed { get; set; }

        public Flag (Rectangle b, Texture2D r, Texture2D g)
        {
            Bounds = b;
            Completed = false;
            _redSprite = r;
            _greenSprite = g;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Completed)
            {
                spriteBatch.Draw(_greenSprite, Bounds, Color.White);
            }
            else
            {
                spriteBatch.Draw(_redSprite, Bounds, Color.White);
            }
        }
    }
}
