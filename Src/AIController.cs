using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class AIController : Controller
    {
        private int counter = 0;

        private int counterThreshold = 0;

        public AIController(int counterThreshold)
        {
            this.counterThreshold = counterThreshold;
        }

        protected override void DoInput(Bike bike, Grid grid)
        {
            var dirs = new List<int>();

            if (IsAdjactentCellEmpty(bike, grid, 0))
            {
                counter++;
                if (counter < counterThreshold)
                {
                    return;
                }
                else
                {
                    counter = 0;
                    dirs.Add(0);
                }
            }
            else
            {
                counter = 0;
            }
            if (IsAdjactentCellEmpty(bike, grid, -1))
            {
                dirs.Add(-1);
            }
            if (IsAdjactentCellEmpty(bike, grid, 1))
            {
                dirs.Add(1);
            }

            if (dirs.Count > 0)
            {
                var dirIdx = new Random().Next(0, dirs.Count);

                bike.RotateBike(dirs[dirIdx]);
            }
        }

        private bool IsAdjactentCellEmpty(Bike bike, Grid grid, int dir)
        {
            var checkDist = bike.Speed.Rotate(dir * MathF.PI / 2);
            checkDist = new Vector2(MathF.Round(checkDist.X), MathF.Round(checkDist.Y));

            var checkCell = bike.CellPos + checkDist;

            if (checkCell.X < 0 || checkCell.Y < 0 || checkCell.X > grid.GetCellNum() - 1 || checkCell.Y > grid.GetCellNum() - 1)
            {
                return false;
            }

            var member = grid.GetCell((int)checkCell.X, (int)checkCell.Y);
            return member == CellMembers.empty;
        }
    }
}
