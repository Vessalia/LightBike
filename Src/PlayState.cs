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
        private List<AIController> aiControllers;

        private Bike player;
        private List<Bike> enemies;

        private Grid grid;

        private bool indicator;
        private bool aiIndicator;

        private int counter;
        private float timer;

        public PlayState(IGameStateSwitcher switcher, Input input) : base(switcher, input)
        {
            int cellNum = 32;
            grid = new Grid(cellNum);

            player = new Bike(new Vector2(cellNum, cellNum) / 4, new Color(20, 120, 185), new Vector2(1, 0));

            aiControllers = new List<AIController>();

            enemies = new List<Bike>();

            Bike redEnemy = new Bike(new Vector2(cellNum, cellNum) / 4 + new Vector2(cellNum, 0) / 2, new Color(205, 50, 50), new Vector2(0, 1));
            Bike yellowEnemy = new Bike(new Vector2(cellNum, cellNum) / 4 + new Vector2(0, cellNum) / 2, new Color(175, 190, 50), new Vector2(0, -1));
            Bike greenEnemy = new Bike(3 * new Vector2(cellNum, cellNum) / 4, new Color(50, 150, 50), new Vector2(-1, 0));

            AddEnemies(redEnemy);
            AddEnemies(yellowEnemy);
            AddEnemies(greenEnemy);

            playerController = new PlayerController(player, input);

            indicator = true;
            aiIndicator = true;
            counter = 0;
            timer = 5;
        }

        public override void HandleInput()
        {
            if (indicator && (input.IsKeyJustPressed(Keys.Left) || input.IsKeyJustPressed(Keys.Right)))
            {
                playerController.HandleInput();

                indicator = false;
            }

            if (aiIndicator)
            {
                foreach (var c in aiControllers)
                {
                    c.HandleInput();
                }

                aiIndicator = false;
            }
        }

        public override void Update(float timeStep)
        {
            if (enemies.Count == 0)
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
                player.Update(grid);

                int i = 0;

                var bikesToDelete = new List<Bike>();

                foreach (var b in enemies)
                {
                    if (aiControllers[i].BikeKilled(b) == true)
                    {
                        bikesToDelete.Add(b);
                        aiControllers.Remove(aiControllers[i]);
                    }
                    else
                    {
                        b.Update(grid);
                        i++;
                    }
                }

                for(int j = 0; j < bikesToDelete.Count; j++)
                {
                    enemies.Remove(bikesToDelete[j]);
                }

                counter = 0;
                indicator = true;
                aiIndicator = true;
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

        private void AddEnemies(Bike bike)
        {
            enemies.Add(bike);
            aiControllers.Add(new AIController(bike, grid));
        }
    }
}
