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
        private List<Bike> bikes;
        private Bike player;

        private Grid grid;

        private int counter;
        private float timer;

        public PlayState(IGameStateSwitcher switcher, Input input) : base(switcher, input)
        {
            int cellNum = 32;
            grid = new Grid(cellNum);

            player = new Bike(new Vector2(cellNum, cellNum) / 4, new Color(20, 120, 185), new Vector2(1, 0), new PlayerController(input));

            Bike redEnemy = new Bike(new Vector2(cellNum, cellNum) / 4 + new Vector2(cellNum, 0) / 2, new Color(205, 50, 50), new Vector2(0, 1), new AIController());
            Bike yellowEnemy = new Bike(new Vector2(cellNum, cellNum) / 4 + new Vector2(0, cellNum) / 2, new Color(175, 190, 50), new Vector2(0, -1), new AIController());
            Bike greenEnemy = new Bike(3 * new Vector2(cellNum, cellNum) / 4, new Color(50, 150, 50), new Vector2(-1, 0), new AIController());

            bikes = new List<Bike>();

            bikes.Add(player);
            bikes.Add(redEnemy);
            bikes.Add(yellowEnemy);
            bikes.Add(greenEnemy);

            counter = 0;
            timer = 5;
        }

        public override void HandleInput()
        {
            foreach (var b in bikes)
            {
                b.HandleInput(grid);
            }
        }

        public override void Update(float timeStep)
        {
            if (bikes.Count == 1)
            {
                if (timer >= 0)
                {
                    timer -= timeStep;
                }
                else
                {
                    switcher.SetNextState(new MainMenuState(switcher, input));
                }
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                switcher.SetNextState(new PauseState(switcher, input, this));
            }

            if (counter > 4)
            {
                var bikesToDelete = new List<Bike>();

                foreach (var b in bikes)
                {
                    if (b.IsBikeKilled())
                    {
                        bikesToDelete.Add(b);
                    }
                    else
                    {
                        b.Update(grid);
                    }
                }

                foreach (var b in bikesToDelete)
                {
                    bikes.Remove(b);
                }

                counter = 0;
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
