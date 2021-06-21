using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PlayState : GameState
    {
        private PlayerController playerController;
        private AIController aiController;

        private Bike player;
        private Bike enemy;

        private Grid grid;

        private bool indicator;

        private int counter;

        public PlayState(IGameStateSwitcher switcher, Input input) : base(switcher, input)
        {
            int cellNum = 32;
            grid = new Grid(cellNum);

            player = new Bike(new Vector2(cellNum, cellNum) / 2 - new Vector2(1, 1), new Color(20, 120, 185), new Vector2(1, 0));
            enemy = new Bike(new Vector2(cellNum, cellNum) / 2 + new Vector2(1, 1), new Color(205, 50, 50), new Vector2(-1, 0));

            playerController = new PlayerController(player, input);

            indicator = true;
            counter = 0;
        }

        public override void HandleInput()
        {
            if (indicator && (input.IsKeyJustPressed(Keys.Left) || input.IsKeyJustPressed(Keys.Right)))
            {
                playerController.HandleInput();

                indicator = false;
            }

            //aiController.HandleInput();
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switcher.SetNextState(new PauseState(switcher, input, this));
            }

            if (counter > 4)
            {
                player.Update(grid);
                enemy.Update(grid);

                counter = 0;
                indicator = true;
            }
            else
            {
                counter++;
            }
        }

        public override void DrawToScreen(SpriteBatch sb, SpriteFont font)
        {
            grid.DrawGrid(sb);
        }
    }
}
