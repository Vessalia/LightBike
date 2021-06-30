using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoGame.Extended;

namespace LightBike.Src
{
    class PlayState : GameState
    {
        private readonly List<Bike> bikes;
        private readonly List<Bike> activeBikes;
        private readonly List<Bike> inactiveBikes;
        private readonly Bike player;

        private Grid grid;
        private readonly int cellNum;

        private readonly int maxScore;
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
                greenEnemy,
                yellowEnemy
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

            float ang = 5 * MathF.PI / 4;

            foreach (var b in bikes)
            {
                var text = $"{(int)b.GetScore()}";
                var textSize = fonts["score"].MeasureString(text);

                var length = MathF.Sqrt(MathF.Pow(grid.GetCellLen() * grid.GetCellNum(), 2) / 2) + grid.GetCellLen();

                var vec = new Vector2(MathF.Cos(ang), MathF.Sin(ang)) * length + Constants.Screen / 2;

                vec.Y = MathF.Max(MathF.Min(Constants.Screen.Y, vec.Y), 0);

                sb.DrawString(fonts["score"], text, vec - textSize / 2, b.GetColour());
                ang += MathF.PI / 2;
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
