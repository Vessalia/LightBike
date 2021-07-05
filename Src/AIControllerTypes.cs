using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightBike.Src
{
    public class AggroAIController : AIController
    {
        private int counter = 0;

        private Bike nearestBike = null;

        public AggroAIController(List<Bike> activeBikes) : base(activeBikes) { }

        protected override Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid)
        {
            if (counter == 0)
            {
                nearestBike = null;

                foreach (var b in bikes)
                {
                    if (nearestBike == null)
                    {
                        nearestBike = b;
                    }
                    else
                    {
                        var dist = (int)MathF.Abs(b.CellPos.X - bike.CellPos.X) + (int)MathF.Abs(b.CellPos.Y - bike.CellPos.Y);
                        var distNear = (int)MathF.Abs(nearestBike.CellPos.X - bike.CellPos.X) + (int)MathF.Abs(nearestBike.CellPos.Y - bike.CellPos.Y);

                        if (dist < distNear)
                        {
                            nearestBike = b;
                        }
                    }
                }
                counter = 3;
            }
            else
            {
                counter--;
            }

            var goalSpot = nearestBike.CellPos + 4 * nearestBike.Speed;

            return new Location((int)goalSpot.X, (int)goalSpot.Y);
        }
    }

    public class SmartAIController : AIController
    {
        private int counter = 0;

        private Bike farthestBike = null;

        public SmartAIController(List<Bike> activeBikes) : base(activeBikes) { }

        protected override Location GoalLocation(List<Bike> bikes, Bike bike, Grid grid)
        {
            if (counter == 0)
            {
                farthestBike = null;

                foreach (var b in bikes)
                {
                    if (farthestBike == null)
                    {
                        farthestBike = b;
                    }
                    else
                    {
                        var dist = (int)MathF.Abs(b.CellPos.X - bike.CellPos.X) + (int)MathF.Abs(b.CellPos.Y - bike.CellPos.Y);
                        var distFar = (int)MathF.Abs(farthestBike.CellPos.X - bike.CellPos.X) + (int)MathF.Abs(farthestBike.CellPos.Y - bike.CellPos.Y);

                        if (dist > distFar)
                        {
                            farthestBike = b;
                        }
                    }
                }
                counter = 3;
            }
            else
            {
                counter--;
            }

            var goalSpot = farthestBike.CellPos + 4 * farthestBike.Speed;

            return new Location((int)goalSpot.X, (int)goalSpot.Y);
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
                var randX = (int)new Random().Next(0, grid.GetCellNum());
                var randY = (int)new Random().Next(0, grid.GetCellNum());

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
