using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class AIController : Controller
    {
        protected override void DoInput(Bike bike, Grid grid)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                var x = 1;
            }

            var location = new Location((int)bike.CellPos.X, (int)bike.CellPos.Y);
            var nextCell = new PathFinding(grid).GetNextCell(location, new Location(0, 0));

            var speed = new Vector2(nextCell.x - location.x, nextCell.y - location.y);
            speed.Normalize();

            bike.Speed = speed;
        }
    }
}
