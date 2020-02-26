using MapLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class FlagManager
    {
        public List<Flag> FlagList { get; set; }
        public int FlagCount { get; set; }
        public int CompletedCount { get; set; }

        private Player player;
        private World world;
        private Texture2D greenSprite;
        private Texture2D redSprite;
        private SoundEffect flagSound;

        public FlagManager(Tilemap map, World w, Player p, ContentManager content)
        {
            world = w;
            player = p;

            FlagList = new List<Flag>();

            LoadContent(content);
            CreateFlags(map);

            CompletedCount = 0;
            FlagCount = FlagList.Count;
        }

        void CreateFlags(Tilemap map)
        {
            int horizontalScale = world.Bounds.Width / map.Width;
            int verticalScale = world.Bounds.Height / map.Height;

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.MapData[(y * map.Width) + x] == 2)
                    {
                        var newFlag = new Flag(new Rectangle(x * horizontalScale, y * horizontalScale, horizontalScale, verticalScale), redSprite, greenSprite);
                        FlagList.Add(newFlag);
                    }
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            greenSprite = content.Load<Texture2D>("Flags/Game3-GreenFlag");
            redSprite = content.Load<Texture2D>("Flags/Game3-RedFlag");
            flagSound = content.Load<SoundEffect>("SoundEffects/Flag");
        }

        public void Update(GameTime gameTime)
        {
            Tuple<bool, Flag> flagResults = PlayerTouchingFlag();

            if (flagResults.Item1)
            {
                if (!flagResults.Item2.Completed)
                {
                    flagSound.Play(0.1f, 0, 0);
                    flagResults.Item2.Completed = true;
                    CompletedCount++;
                }
            }
        }

        public Tuple<bool, Flag> PlayerTouchingFlag()
        {
            foreach (Flag flag in FlagList)
            {
                if (flag.Bounds.Intersects(player.CollisionBox))
                {
                    return new Tuple<bool, Flag>(true, flag);
                }
            }
            return new Tuple<bool, Flag>(false, null);
        }

        public void ResetFlags()
        {
            foreach (Flag flag in FlagList)
            {
                flag.Completed = false;
                CompletedCount = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Flag flag in FlagList)
            {
                flag.Draw(spriteBatch);
            }
        }
    }
}
