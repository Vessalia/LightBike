﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightBike.Src
{
    class PlayState : GameState
    {
        private List<Bike> bikes;
        private List<Bike> activeBikes;
        private List<Bike> inactiveBikes;
        private Bike player;

        private Grid grid;
        private int cellNum;

        private int maxScore;
        private int counter;
        private float startTimer;
        private float endTimer;

        private bool isGameOver;

        public PlayState(IGameStateSwitcher switcher, Input input) : base(switcher, input)
        {
            cellNum = 128;

            grid = new Grid(cellNum);

            player = new Bike(new Vector2(cellNum, cellNum) / 4, new Color(20, 120, 185), new Vector2(1, 0), new PlayerController(input));

            Bike redEnemy = new Bike(new Vector2(cellNum, cellNum) / 4 + new Vector2(cellNum, 0) / 2, new Color(205, 50, 50), new Vector2(0, 1), new AIController(16));
            Bike yellowEnemy = new Bike(new Vector2(cellNum, cellNum) / 4 + new Vector2(0, cellNum) / 2, new Color(175, 190, 50), new Vector2(0, -1), new AIController(32));
            Bike greenEnemy = new Bike(3 * new Vector2(cellNum, cellNum) / 4, new Color(50, 150, 50), new Vector2(-1, 0), new AIController(64));

            bikes = new List<Bike>
            {
                player,
                redEnemy,
                yellowEnemy,
                greenEnemy
            };

            activeBikes = bikes.Where(a => true).ToList();
            inactiveBikes = new List<Bike>();

            maxScore = 3;

            counter = 0;
            startTimer = 4;
            endTimer = 3;

            isGameOver = false;
        }

        public override void HandleInput()
        {
            foreach (var b in activeBikes)
            {
                b.HandleInput(grid);
            }
        }

        public override void Update(float timeStep)
        {
            if (player.GetScore() == maxScore || AiBestScore() == maxScore)
            {
                isGameOver = true;

                if (endTimer >= 0)
                {
                    endTimer -= timeStep;
                }
                else
                {
                    switcher.SetNextState(new MainMenuState(switcher, input));
                }
                return;
            }

            if (startTimer >= 0)
            {
                startTimer -= timeStep;
                if (startTimer >= 1)
                {
                    return;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                switcher.SetNextState(new PauseState(switcher, input, this));
            }

            if (counter > 2)
            {
                foreach (var b in activeBikes)
                {
                    if (b.IsBikeKilled())
                    {
                        inactiveBikes.Add(b);
                    }
                    else
                    {
                        b.Update(grid);
                    }
                }

                foreach (var b in inactiveBikes)
                {
                    activeBikes.Remove(b);
                }

                counter = 0;
            }
            else
            {
                counter++;
            }

            if (activeBikes.Count == 1)
            {
                activeBikes[0].AddToScore(1);

                foreach (var b in inactiveBikes)
                {
                    activeBikes.Add(b);
                }

                foreach (var b in activeBikes)
                {
                    inactiveBikes.Remove(b);
                }

                ResetGame();
            }
        }

        public override void DrawToScreen(SpriteBatch sb, Dictionary<string, SpriteFont> fonts)
        {
            grid.DrawGrid(sb);

            var font = fonts["default"];

            if (startTimer >= 1 && !isGameOver)
            {
                var text = $"{(int)startTimer}";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, Constants.Screen / 2 - textSize / 2, Color.Purple);
            }
            else if (startTimer >= 0 && !isGameOver)
            {
                var text = "GO";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, Constants.Screen / 2 - textSize / 2, Color.Purple);
            }

            if (player.GetScore() == maxScore)
            {
                var text = "YOU FUCKING WIN YOU FUCKING WINNER ASS BITCH";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, Constants.Screen / 2 - textSize / 2, Color.Purple);
                return;
            }
            else if (AiBestScore() == maxScore)
            {
                var text = "YOU FUCKING LOSE YOU FUCKING LOSER ASS BITCH";
                var textSize = font.MeasureString(text);

                sb.DrawString(font, text, Constants.Screen / 2 - textSize / 2, Color.Purple);
                return;
            }

            foreach (var b in bikes)
            {
                var coords = Constants.GridToScreenCoords(b.GetInitialCellPos(), cellNum);

                var dirVec = coords - Constants.Screen / 2;
                dirVec.Normalize();
                dirVec.X *= Constants.Screen.X / 20;
                dirVec.Y *= Constants.Screen.Y / 4;

                var text = $"{(int)b.GetScore()}";
                var textSize = font.MeasureString(text);

                sb.DrawString(fonts["score"], text, coords + dirVec, b.GetColour());
            }
        }

        public int AiBestScore()
        {
            int bestScore = 0;

            foreach (var b in activeBikes)
            {
                bestScore = (int)MathF.Max(bestScore, b.GetScore());
            }

            return bestScore;
        }

        public void ResetGame()
        {
            foreach (var b in activeBikes)
            {
                b.ResetBike();
            }

            grid = new Grid(cellNum);

            startTimer = 4;
        }
    }
}
