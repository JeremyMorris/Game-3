using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        Player player;
        float friction;
        float gravity;

        Texture2D tempTexture;

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
            graphics.PreferredBackBufferHeight = 300;
            graphics.ApplyChanges();

            friction = 0.04f;
            gravity = 0.06f;

            player = new Player(new Vector2((GraphicsDevice.Viewport.Width / 2) - 200, GraphicsDevice.Viewport.Height - 64), this, friction, gravity);
            camera = new Camera();

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

            tempTexture = Content.Load<Texture2D>("Player/Pixel");
            // TODO: use this.Content to load your game content here
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

            player.Update(gameTime);
            camera.Position = new Vector2(player.X, player.Y - player.Height - 10);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            

            // spriteBatch Begin arguments from Stack Overflow post https://stackoverflow.com/questions/25145377/xna-blurred-sprites-when-scaled
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, 
                    DepthStencilState.None, RasterizerState.CullCounterClockwise, null, camera.GetTransformation(this.GraphicsDevice));

            // Draw player
            player.Draw(spriteBatch);
            spriteBatch.Draw(tempTexture, new Rectangle(20, 20, 10, 10), Color.Black);
            spriteBatch.Draw(tempTexture, new Rectangle(GraphicsDevice.Viewport.Width - 30, 30, 10, 10), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
