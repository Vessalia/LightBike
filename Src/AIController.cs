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
            var bikes = activeBikes.Where(b => b != bike).ToList();

            var location = new Location((int)bike.CellPos.X, (int)bike.CellPos.Y);

            var goal = GoalLocation(bikes, bike, grid);

            await Task.Run(() =>
            {
                var nextCell = new PathFinding(grid).GetNextCell(location, goal);

                while (nextCell == goal)
                {
                    var randX = new Random().Next(0, grid.GetCellNum() - 1);
                    var randY = new Random().Next(0, grid.GetCellNum() - 1);
                    nextCell = new PathFinding(grid).GetNextCell(location, new Location(randX, randY));
                }

                var speed = new Vector2(nextCell.x - location.x, nextCell.y - location.y);
                speed.Normalize();

                speed.X = (int)MathF.Round(speed.X);
                speed.Y = (int)MathF.Round(speed.Y);

                if((int)MathF.Abs(speed.X) + (int)MathF.Abs(speed.Y) > 1)
                {
                    var picker = new Random().Next(0, 1);
                    speed.Y *= picker;
                    speed.X *= (int)MathF.Abs(speed.X) - (int)MathF.Abs(speed.Y);
                }

                bike.Speed = speed;
            });
        }

        protected abstract Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid);
    }
}
