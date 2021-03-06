﻿using Microsoft.Xna.Framework;
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

        Quadtree<Polygon> world;

        MouseState lastMouse;

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
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            world = new Quadtree<Polygon>(new RectangleF(0, 0, 800, 600));

            poly1 = new Polygon(Vector2.Zero, new List<Vector2>()
            {
                new Vector2(25, 0),
                new Vector2(38, 12),
                new Vector2(25, 38),
                new Vector2(0, 25)
            });

            world.Insert(new Polygon(new Vector2(80, 80), new List<Vector2>()
            {
                new Vector2(50, 50),
                new Vector2(100, 0),
                new Vector2(150, 150)
            }));

            world.Insert(new Polygon(new Vector2(400, 200), new List<Vector2>()
            {
                new Vector2(0, 50),
                new Vector2(50, 0),
                new Vector2(150, 80),
                new Vector2(160, 200),
                new Vector2(-10, 190)
            }));

            world.Insert(ShapePrimitives.Circle(new Vector2(600, 200), 60, 10));
            world.Insert(ShapePrimitives.BezelRectangle(new Vector2(60, 60), new Vector2(160, 220), 15));


            world.Insert(ShapePrimitives.Rectangle(new Vector2(620, 200), new Vector2(660, 230)));

            world.Insert(ShapePrimitives.Rectangle(new Vector2(0, 400), new Vector2(30, 430)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(30, 400), new Vector2(60, 430)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(60, 400), new Vector2(90, 430)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(90, 400), new Vector2(120, 430)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(120, 400), new Vector2(150, 430)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(150, 400), new Vector2(180, 430)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(0, 430), new Vector2(30, 460)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(30, 430), new Vector2(60, 460)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(60, 430), new Vector2(90, 460)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(90, 430), new Vector2(120, 460)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(120, 430), new Vector2(150, 460)));
            world.Insert(ShapePrimitives.Rectangle(new Vector2(150, 430), new Vector2(180, 460)));

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

            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
            {
                world.Insert(ShapePrimitives.BezelRectangle(new Vector2(mouse.X, mouse.Y), new Vector2(mouse.X + 20, mouse.Y + 20), 5));
            }
            lastMouse = mouse;

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

            world.ClearDebugTag();
            List<Polygon> polys = world.Retrieve(new RectangleF(poly1.Origin.X + poly1.BoundingBox.Left, poly1.Origin.Y + poly1.BoundingBox.Top, poly1.BoundingBox.Width, poly1.BoundingBox.Height));

            foreach (IPolygon poly in polys)
            {
                poly.AddTag("debug");
            }

            Window.Title = polys.Count.ToString();
            poly1.Move(polys, velocity);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Action<Vector2, Vector2, Color> drawLine = (p1, p2, color) => { DrawLine(spriteBatch, p1, p2, color, 1); };
            Action<string, Vector2> drawString = (text, pos) => { };

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            world.Draw(drawLine, drawString);


            poly1.Draw(drawLine, drawString);

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
