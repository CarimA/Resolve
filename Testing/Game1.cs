using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Resolve;
using System.Collections.Generic;

namespace Testing
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D pixel;

        Vector2 position = Vector2.Zero;

        Polygon poly1;
        Polygon poly2;
        Polygon poly3;

        Polygon circle;
        Polygon rect;

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
            // TODO: Add your initialization logic here

            poly1 = new Polygon(Vector2.Zero, new List<Vector2>()
            {
                new Vector2(100, 0),
                new Vector2(150, 50),
                new Vector2(100, 150),
                new Vector2(0, 100)
            });

            poly2 = new Polygon(new Vector2(80, 80), new List<Vector2>()
            {
                new Vector2(50, 50),
                new Vector2(100, 0),
                new Vector2(150, 150)
            });

            poly3 = new Polygon(new Vector2(400, 200), new List<Vector2>()
            {
                new Vector2(0, 50),
                new Vector2(50, 0),
                new Vector2(150, 80),
                new Vector2(160, 200),
                new Vector2(-10, 190)
            });

            circle = ShapePrimitives.Circle(new Vector2(400, 200), 60, 10);
            rect = ShapePrimitives.BezelRectangle(new Vector2(60, 60), new Vector2(160, 220), 15);

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

            // TODO: use this.Content to load your game content here
            // create 1x1 texture for line drawing
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(
                new Color[] { Color.White });// fill the texture with white
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

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState ks = Keyboard.GetState();
            Vector2 velocity = Vector2.Zero;
            float speed = 90; // a link to the past link moves at 90 pixels per second!
            if (ks.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -speed * deltaTime);
            }
            if (ks.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, speed * deltaTime);
            }
            if (ks.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-speed * deltaTime, 0);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(speed * deltaTime, 0);
            }
            
            poly1.Move(new List<IPolygon>() { poly2, poly3, circle, rect }, velocity);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Action<Vector2, Vector2> drawLine = (p1, p2) => { DrawLine(spriteBatch, p1, p2, Color.White, 1); };
            Action<string, Vector2> drawString = (text, pos) => { };

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            
            poly1.Draw(drawLine, drawString);
            poly2.Draw(drawLine, drawString);
            poly3.Draw(drawLine, drawString);
            circle.Draw(drawLine, drawString);
            rect.Draw(drawLine, drawString);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end, Color color, int thickness = 1)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(pixel,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    thickness), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
