using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapLibrary;
using Microsoft.Xna.Framework.Content;

namespace Game3
{
    public class PlatformManager
    {
        public List<Platform> platformList;
        public List<Texture2D> platformTextures;
        private Player player;
        private World world;
        private Random r;

        public PlatformManager(Tilemap map, World w, Player p, ContentManager content)
        {
            world = w;
            player = p;

            r = new Random();

            platformList = new List<Platform>();
            platformTextures = new List<Texture2D>();

            LoadContent(content);
            CreatePlatforms(map);
        }

        void CreatePlatforms(Tilemap map)
        {
            int horizontalScale = world.Bounds.Width / map.Width;
            int verticalScale = world.Bounds.Height / map.Height;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.MapData[(y * map.Width) + x] == 1)
                    {
                        var textureNum = r.Next(0, 4);

                        var newPlatform = new Platform(new Rectangle(x * horizontalScale, y * horizontalScale, horizontalScale, verticalScale), platformTextures[textureNum]);
                        platformList.Add(newPlatform);
                    }
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            platformTextures.Add(content.Load<Texture2D>("Platforms/GroundBlock1"));
            platformTextures.Add(content.Load<Texture2D>("Platforms/GroundBlock2"));
            platformTextures.Add(content.Load<Texture2D>("Platforms/GroundBlock3"));
            platformTextures.Add(content.Load<Texture2D>("Platforms/GroundBlock4"));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform platform in platformList)
            {
                platform.Draw(spriteBatch);
            }
        }

        public Tuple<bool,Platform, bool> PlayerOnPlatform()
        {
            bool playerAbovePlatform = false;
            int playerX = player.CollisionBox.X + (int)(player.HorizontalSpeed * 16);
            int playerY = player.CollisionBox.Y + (int)(player.VerticalSpeed * 16);

            foreach (Platform platform in platformList)
            {
                if (!(playerX > platform.Bounds.X + platform.Bounds.Width
                    || playerX + player.CollisionBox.Width < platform.Bounds.X))
                {
                    if (!(playerY > platform.Bounds.Y + platform.Bounds.Width
                    || playerY + player.CollisionBox.Width < platform.Bounds.Y))
                    {
                        return new Tuple<bool, Platform, bool>(true, platform, true);
                    }
                    else if (platform.Bounds.Y - player.Y - player.Height < 20)
                    {
                        playerAbovePlatform = true;
                    }
                }

            }
            return new Tuple<bool, Platform, bool>(false, null, playerAbovePlatform);
        }
    }
}
