using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightBike.Src
{
    public abstract class AIController : Controller
    {
        protected List<Bike> activeBikes;

        public AIController (List<Bike> activeBikes)
        {
            this.activeBikes = activeBikes;
        }

        protected async override void DoInput(Bike bike, Grid grid)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                var x = 1;
            }

            var bikes = activeBikes.Where(b => b != bike).ToList();

            var location = new Location((int)bike.CellPos.X, (int)bike.CellPos.Y);

            var goal = GoalLocation(bikes, bike, grid);

            await Task.Run(() =>
            {
                var nextCell = new PathFinding(grid).GetNextCell(location, goal);

                var speed = new Vector2(nextCell.x - location.x, nextCell.y - location.y);
                speed.Normalize();

                bike.Speed = speed;
            });
        }

        protected abstract Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid);
    }
}
