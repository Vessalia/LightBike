using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public class Bike
    {
        public Vector2 CellPos { get; private set; }

        public Vector2 Speed { get; set; }

        private Vector2 initialCellPos;
        private Vector2 initialSpeed;

        private Color colour;

        private Controller controller;

        private int score;

        public Bike(Vector2 cellPos, Color colour, Vector2 speed, Controller controller)
        {
            this.colour = colour;
            this.controller = controller;

            CellPos = cellPos;
            Speed = speed;

            score = 0;

            initialCellPos = cellPos;
            initialSpeed = speed;
        }

        public void HandleInput(Grid grid)
        {
            controller.HandleInput(this, grid);
        }

        public void Update(Grid grid)
        {
            grid.SetCell((int)CellPos.X, (int)CellPos.Y, CellMembers.wall, 0.4f * colour);
            
            var nextPos = CellPos + Speed;
            
            if (grid.GetCell((int)nextPos.X, (int)nextPos.Y) != CellMembers.empty)
            {
                Speed = Vector2.Zero;
                grid.KillBike(colour);
            }
            else
            {
                CellPos += Speed;
                grid.SetCell((int)CellPos.X, (int)CellPos.Y, CellMembers.bike, colour);
                controller.ResetIndicator();
            }
        }

        public void RotateBike(int dir)
        {
            Speed = Speed.Rotate(dir * MathF.PI / 2);
            Speed = new Vector2((int)Speed.X, (int)Speed.Y);
        }

        public Color GetColour()
        {
            return colour;
        }

        public bool IsBikeKilled()
        {
            return Speed == Vector2.Zero;
        }

        public void AddToScore(int point)
        {
            score += point;
        }

        public int GetScore()
        {
            return score;
        }

        public void ResetBike()
        {
            CellPos = initialCellPos;
            Speed = initialSpeed;
        }

        public Vector2 GetInitialCellPos()
        {
            return initialCellPos;
        }
    }
}
