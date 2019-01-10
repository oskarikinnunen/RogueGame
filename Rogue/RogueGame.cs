using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Rogue
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RogueGame : Game
    {
        private static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private static SpriteBatch uiSpriteBatch;
        private static ContentManager content2;
        private AnimatedTexture2D playerAnim;
        private GameObject player;
        private Vector2 mouseScreenPos;

        public static GraphicsDeviceManager Graphics { get => graphics; }

        public static SpriteFont SprFont;

        public static WorldScene LoadedWorldScene;

        public static SpriteBatch UISpriteBatch { get => uiSpriteBatch; }

        public RogueGame()
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
            AnimationEngine.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ContentHelper.Initialize();
            ContentHelper.LoadTerrainTextures(Content);
            
            spriteBatch = new SpriteBatch(GraphicsDevice); // Create a new SpriteBatch, which can be used to draw textures. 
            AnimationEngine.SpriteBatch = spriteBatch;

            //UI
            uiSpriteBatch = new SpriteBatch(GraphicsDevice);
            UI.InitializeTextures();

            SprFont = Content.Load<SpriteFont>("Default");
            
            LoadedWorldScene = WorldGenerator.NormalTerrain(new Vector2(10f, 10f)); //Needs to be added before any gameobjects since they are added to LoadedWorldScene
            
            //Initialize Player related objects etc.
            Texture2D playerSprite = Content.Load<Texture2D>("HeroSheet1");

            playerAnim = new AnimatedTexture2D(playerSprite, 32);
            playerAnim.PingPong = true;

            Vector2 worldCenter = new Vector2(LoadedWorldScene.Terrain.GetLength(0) / 2, LoadedWorldScene.Terrain.GetLength(0) / 2) * 32;
            player = new GameObject(worldCenter, playerAnim);

            //Init Camera
            Camera.Primary = new Camera(spriteBatch, graphics.GraphicsDevice.Viewport);
            Camera.Primary.Follow(player);
            Coroutines.Start(Camera.Primary.InventoryZoom());
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
            Input.Update(Keyboard.GetState());

            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CheckMovement();

            MouseState mState = Mouse.GetState();
            mouseScreenPos = mState.Position.ToVector2();

            Coroutines.Enumerate(gameTime);
            base.Update(gameTime);
        }

        private void CheckMovement()
        {
            if (Input.KeyPushed(Keys.Left))
            {       //TODO: Make a CheckInput method
                player.Position += new Vector2(-32f, 0f);
            }
            else if (Input.KeyPushed(Keys.Right))
            {
                player.Position += new Vector2(+32f, 0f);
            }
            else if (Input.KeyPushed(Keys.Up))
            {
                player.Position += new Vector2(0f, -32f);
            }
            else if (Input.KeyPushed(Keys.Down))
            {
                player.Position += new Vector2(0f, +32f);
            }

            if (Input.KeyPushed(Keys.Z))
            {
                Camera.Primary.Zoom -= 0.5f;
            }

            if (Input.KeyPushed(Keys.X))
            {
                Camera.Primary.Zoom += 0.5f;
            }
            
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Game drawing
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.FrontToBack,BlendState.AlphaBlend,SamplerState.PointClamp,null,RasterizerState.CullCounterClockwise,null,Camera.Primary.TransformMatrix);

            Camera.Primary.DrawTerrain();
            Camera.Primary.DrawGameObjects();
            //Mouse To World testing
            //GameObject go = new GameObject(Vector2.Zero);
            //AnimatedTexture2D

            //playerAnim.Draw(); //TODO: Call this in cameras draw method

            spriteBatch.End();


            //UI
            uiSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            Vector2 textLocation = new Vector2(0f, graphics.GraphicsDevice.Viewport.Height - 20f);
            
            UI.DrawBoxForString(textLocation, "Test String drawing");
            UI.DrawStringOnScreen(textLocation, "Test String drawing");

            UI.DrawBox(mouseScreenPos, new Vector2(2f, 2f));

            uiSpriteBatch.End();

            //Gametime reliant update calls
            base.Draw(gameTime);
            AnimationEngine.Update(gameTime); //Pass the gametime to the animationengine which then determines if any animation frames need to be advanced
        }

    }
}
