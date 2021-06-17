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
        private Vector2 cellPos;
        private Vector2 speed;

        private int initialSpeed;

        private Color colour;

        public Bike(Vector2 cellPos, Color colour, Vector2 speed)
        {
            this.cellPos = cellPos;
            this.speed = speed;
            this.colour = colour;

            initialSpeed = (int)MathF.Max(speed.X, speed.Y);
        }

        public void Update(Grid grid)
        {
            grid.SetCell((int)cellPos.X, (int)cellPos.Y, CellMembers.wall, 0.8f * colour);
            cellPos += speed;
            grid.SetCell((int)cellPos.X, (int)cellPos.Y, CellMembers.bike, colour);
        }

        public void RotateBike(int dir)
        {
            speed.X = initialSpeed * MathF.Cos(dir * MathF.PI / 2);
            speed.Y = initialSpeed * MathF.Sin(dir * MathF.PI / 2);
        }
    }
}
