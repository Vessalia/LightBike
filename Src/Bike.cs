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
        private Vector2 dim;
        private Vector2 cellPos;

        private Color colour;

        public Bike(Vector2 cellPos, Vector2 dim, Color colour)
        {
            this.dim = dim;
            this.cellPos = cellPos;
            this.colour = colour;
        }

        public void Update(Grid grid, Vector2 speed)
        {
            grid.SetCell((int)cellPos.X, (int)cellPos.Y, CellMembers.wall);
            cellPos += speed;
            grid.SetCell((int)cellPos.X, (int)cellPos.Y, CellMembers.bike);
        }

        public void Draw(SpriteBatch sb, Grid grid)
        {
            var screenPos = Constants.GridToScreenCoords(cellPos, grid.GetCellNum());

            sb.FillRectangle(screenPos.X, screenPos.Y, grid.GetCellLen(), grid.GetCellLen(), colour);
        }
    }
}
