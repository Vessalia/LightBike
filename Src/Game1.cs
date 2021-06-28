using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace LightBike.Src
{
    public class Game1 : Game, IGameStateSwitcher
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Dictionary<string, SpriteFont> fonts;

        private GameState gameState;

        private Input input;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = (int)Constants.Screen.X;
            _graphics.PreferredBackBufferHeight = (int)Constants.Screen.Y;
            _graphics.ApplyChanges();

            input = new Input();

            gameState = new MainMenuState(this, input);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            fonts = new Dictionary<string, SpriteFont>();
            fonts["default"] = Content.Load<SpriteFont>("Arial32");
            fonts["title"] = Content.Load<SpriteFont>("TitleFont");
            fonts["score"] = Content.Load<SpriteFont>("ScoreFont");
        }

        protected override void Update(GameTime gameTime)
        {
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            input.Update();
            
            gameState.HandleInput();

            if (gameState == null)
            {
                Exit();
                return;
            }

            gameState.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            
            gameState.DrawToScreen(_spriteBatch, fonts);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetNextState(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}
