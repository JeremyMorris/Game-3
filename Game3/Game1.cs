using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MapLibrary;
using Microsoft.Xna.Framework.Media;

namespace Game3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        World world;
        SpriteFont font;
        SpriteFont bigFont;
        Song song;
        bool songPlaying;
        int timer;

        Player player;
        float friction;
        float gravity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            friction = 0.04f;
            gravity = 0.06f;
            timer = 0;

            world = new World(this, 2000, 800, this.Content);
            player = new Player(new Vector2((world.Bounds.Width / 2) - 200, world.Bounds.Height - 110), this, world, friction, gravity);
            world.Player = player;
            world.InitializeMap(this.Content);
            camera = new Camera(world, graphics.GraphicsDevice.Viewport);

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.075f;
            songPlaying = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("RumbleBrave");
            bigFont = Content.Load<SpriteFont>("RumbleBraveBig");
            song = Content.Load<Song>("Game3");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!world.GameComplete)
            {
                timer += gameTime.ElapsedGameTime.Milliseconds;
            }

            if (!songPlaying)
            {
                MediaPlayer.Play(song);
                songPlaying = true;
            }

            world.Update(gameTime);
            camera.Position = new Vector2(player.X, player.Y - player.Height - 10);

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                timer = 0;
                player.SetX((world.Bounds.Width / 2) - 200);
                player.SetY(world.Bounds.Height - 110);
                world.GameComplete = false;
                world.FlagManager.ResetFlags();
                player.State = player.States.falling;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            // spriteBatch Begin arguments from Stack Overflow post https://stackoverflow.com/questions/25145377/xna-blurred-sprites-when-scaled
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, 
                    DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.GetTransformation(this.GraphicsDevice));

            // Draw world
            world.Draw(spriteBatch);
            
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            if (!world.GameComplete)
            {
                spriteBatch.DrawString(font, "Current Time: " + System.TimeSpan.FromMilliseconds(timer).ToString(@"m\:ss\:ff"), new Vector2(10, 15), Color.Gold);
                spriteBatch.DrawString(font, "Flags Remaining: " + (world.FlagManager.FlagCount - world.FlagManager.CompletedCount), new Vector2(10, 40), Color.Gold);
            } else
            {
                spriteBatch.DrawString(bigFont, "Game Complete!", new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 150, (graphics.GraphicsDevice.Viewport.Height / 2) - 50), Color.SpringGreen);
                spriteBatch.DrawString(bigFont, System.TimeSpan.FromMilliseconds(timer).ToString(@"m\:ss\:ff"), new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 60, (graphics.GraphicsDevice.Viewport.Height / 2)), Color.SpringGreen);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
