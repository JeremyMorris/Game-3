using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;

namespace Game3
{
    public class World
    {
        private Game1 _game;
        private Texture2D _background;
        private Tilemap _tileMap;
        public Rectangle Bounds { get; set; }

        public Player Player { get; set; }

        public PlatformManager PlatformManager { get; set; }

        public FlagManager FlagManager { get; set; }

        public bool GameComplete { get; set; }

        public World (Game1 g, int width, int height, ContentManager content)
        {
            _game = g;
            Bounds = new Rectangle(0, 0, width, height);
            GameComplete = false;

            LoadContent(content);

            // temporarily hardcoded map
            _tileMap = new Tilemap(50, 20, new int[]
            {
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,1,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,
                0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
                0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,1,1,1,1,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
                1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
            });
        }

        public void InitializeMap(ContentManager content)
        {
            PlatformManager = new PlatformManager(_tileMap, this, Player, content);
            FlagManager = new FlagManager(_tileMap, this, Player, content);
        }

        public void LoadContent (ContentManager content)
        {
            _background = content.Load<Texture2D>("Game3-Background");
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);

            FlagManager.Update(gameTime);

            if (FlagManager.CompletedCount >= FlagManager.FlagCount)
            {
                GameComplete = true;
            }

            // Platform collision logic
            Tuple<bool, Platform, bool> platformResults = PlatformManager.PlayerOnPlatform();
            
            if (platformResults.Item1 && (Player.State == Player.States.running || Player.State == Player.States.standing))
            {
                if (platformResults.Item2.Bounds.X < Player.X)
                {
                    Player.SetX(platformResults.Item2.Bounds.X + platformResults.Item2.Bounds.Width - 5);
                }
                else
                {
                    Player.SetX(platformResults.Item2.Bounds.X - Player.CollisionBox.Width - 1);
                }
            }

            if (platformResults.Item1 && Player.State == Player.States.jumping)
            {
                if (platformResults.Item2.Bounds.Y < Player.CollisionBox.Y)
                {
                    Player.VerticalSpeed = 0;
                    Player.SetY(platformResults.Item2.Bounds.Y + platformResults.Item2.Bounds.Height);
                }
            }

            if (platformResults.Item1 && (Player.State == Player.States.falling || Player.State == Player.States.jumping))
            {
                if (platformResults.Item2.Bounds.Y - (Player.Y + Player.Height) > -27)
                {
                    Player.State = Player.States.standing;
                    Player.SetY(platformResults.Item2.Bounds.Y - Player.Height - 1);
                }
                else if (platformResults.Item2.Bounds.Y + platformResults.Item2.Bounds.Height > Player.Y)
                {
                    if (platformResults.Item2.Bounds.X < Player.X)
                    {
                        Player.SetX(platformResults.Item2.Bounds.X + platformResults.Item2.Bounds.Width);
                    } 
                    else
                    {
                        Player.SetX(platformResults.Item2.Bounds.X - Player.CollisionBox.Width - 1);
                    }
                }
            } 
            else if (Player.State != Player.States.jumping && platformResults.Item3 == false)
            {
                Player.State = Player.States.falling;
            }

            Player.UpdateAnimation(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, Bounds, Color.White);

            PlatformManager.Draw(spriteBatch);
            FlagManager.Draw(spriteBatch);

            Player.Draw(spriteBatch);
        }
    }
}
