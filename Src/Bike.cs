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

        private Color colour;

        public Bike(Vector2 cellPos, Color colour, Vector2 speed)
        {
            this.CellPos = cellPos;
            this.Speed = speed;
            this.colour = colour;
        }

        public void Update(Grid grid)
        {
            grid.SetCell((int)CellPos.X, (int)CellPos.Y, CellMembers.wall, 0.4f * colour);
            CellPos += Speed;
            grid.SetCell((int)CellPos.X, (int)CellPos.Y, CellMembers.bike, colour);
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
    }
}
