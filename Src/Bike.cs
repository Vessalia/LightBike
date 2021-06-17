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

        private Color colour;

        public Bike(Vector2 cellPos, Color colour)
        {
            this.cellPos = cellPos;
            this.colour = colour;
        }

        public void Update(Grid grid, Vector2 speed)
        {
            grid.SetCell((int)cellPos.X, (int)cellPos.Y, CellMembers.wall, 0.8f * colour);
            cellPos += speed;
            grid.SetCell((int)cellPos.X, (int)cellPos.Y, CellMembers.bike, colour);
        }
    }
}
