using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightBike.Src
{
    public class AggroAIController : AIController
    {
        public AggroAIController(List<Bike> activeBikes) : base(activeBikes) { }

        protected override Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid)
        {
            Bike nearestBike = null;
            foreach(var b in bikes)
            {
                if (nearestBike == null)
                {
                    nearestBike = b;
                }
                else
                {
                    var dist = MathF.Abs(b.CellPos.X - bike.CellPos.X) + MathF.Abs(b.CellPos.Y - bike.CellPos.Y);
                    var distNear = MathF.Abs(nearestBike.CellPos.X - bike.CellPos.X) + MathF.Abs(nearestBike.CellPos.Y - bike.CellPos.Y);

                    if (dist < distNear)
                    {
                        nearestBike = b;
                    }
                }
            }

            var goalSpot = nearestBike.CellPos + 2 * nearestBike.Speed;

            return new Location((int)goalSpot.X, (int)goalSpot.Y);
        }
    }

    public class PassiveAIController : AIController
    {
        public PassiveAIController(List<Bike> activeBikes) : base(activeBikes) { }

        protected override Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid)
        {
            return new Location(0, 0);
        }
    }

    public class StupidAIController : AIController
    {
        private int counter = 0;

        private Location location = new Location();

        public StupidAIController(List<Bike> activeBikes) : base(activeBikes) { }

        protected override Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid)
        {
            if (counter == 0 || location == new Location((int)bike.CellPos.X, (int)bike.CellPos.Y))
            {
                var randX = new Random().Next(0, grid.GetCellNum());
                var randY = new Random().Next(0, grid.GetCellNum());

                location = new Location(randX, randY);

                counter = 7;
            }
            else
            {
                counter--;
            }

            return location;
        }
    }
}
