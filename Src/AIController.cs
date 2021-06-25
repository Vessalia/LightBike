﻿using Microsoft.Xna.Framework;
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
                bike.Speed = Vector2.Zero;
                grid.KillBike(bike.GetColour());
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
            checkDist = new Vector2((int)checkDist.X, (int)checkDist.Y);

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
