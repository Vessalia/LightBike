using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace LightBike.Src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Bike player;
        Bike enemy;

        Input input;
        PlayerController playerController;

        Grid grid;

        int counter = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            int cellNum = 32;
            grid = new Grid(cellNum);

            player = new Bike(new Vector2(cellNum, cellNum) / 2 - new Vector2(1,1), new Color(20, 120, 185), new Vector2(1, 0));
            enemy = new Bike(new Vector2(cellNum, cellNum) / 2 + new Vector2(1, 1), new Color(205, 50, 50), new Vector2(-1, 0));

            input = new Input();
            playerController = new PlayerController(player, input);

            _graphics.PreferredBackBufferWidth = (int)Constants.Screen.X;
            _graphics.PreferredBackBufferHeight = (int)Constants.Screen.Y;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            playerController.HandleInput();

            if (counter > 4)
            {
                player.Update(grid);
                enemy.Update(grid);

                counter = 0;
            }
            else
            {
                counter++;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            
            //enemy.Draw(_spriteBatch);

            grid.DrawGrid(_spriteBatch);

            // TODO: Add your drawing code here

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
