using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class AIController : Controller
    {
        private Grid grid;

        public AIController(Bike bike, Grid grid) : base(bike)
        {
            this.grid = grid;
        }
        
        public override void HandleInput()
        {
            bool isScrewed = true;

            var dirs = new List<int>();

            if (IsAdjactentCellEmpty(bike, grid, -1))
            {
                dirs.Add(-1);

                isScrewed = false;
            }
            if (IsAdjactentCellEmpty(bike, grid, 0))
            {
                dirs.Add(0);

                isScrewed = false;
            }
            if (IsAdjactentCellEmpty(bike, grid, 1))
            {
                dirs.Add(1);

                isScrewed = false;
            }
            if (isScrewed)
            {
                bike.Speed = new Vector2(0, 0);
                grid.KillBike(bike.GetColour());
                BikeKilled(bike);
            }

            if (dirs.Count > 0)
            {
                var dirIdx = new Random().Next(0, dirs.Count);

                bike.RotateBike(dirs[dirIdx]);
            }
        }

        public bool IsAdjactentCellEmpty(Bike bike, Grid grid, int dir)
        {
            var checkDist = bike.Speed.Rotate(dir * MathF.PI / 2);
            checkDist = new Vector2((int)checkDist.X, (int)checkDist.Y);

            var checkCell = bike.CellPos + checkDist;

            if (checkCell.X < 0 || checkCell.Y < 0 || checkCell.X > grid.GetCellNum() - 1 || checkCell.Y > grid.GetCellNum() - 1)
            {
                return false;
            }

            var member = grid.GetCell((int)checkCell.X, (int)checkCell.Y);
            return member == CellMembers.empty;
        }

        public bool BikeKilled(Bike bike)
        {
            if (bike.Speed == new Vector2(0, 0))
            {
                return true;
            }
            return false;
        }
    }
}
