using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PlayerController : Controller
    {
        private IInput input;

        public PlayerController(IInput input)
        {
            this.input = input;
        }

        public override void HandleInput(Bike bike, Grid grid)
        {
            if(input.IsKeyJustPressed(Keys.Left) || input.IsKeyJustPressed(Keys.Right))
            {
                base.HandleInput(bike, grid);
            }

            if (!IsNextCellEmpty(bike, grid))
            {
                bike.Speed = Vector2.Zero;
                grid.KillBike(bike.GetColour());
            }
        }

        protected override void DoInput(Bike bike, Grid grid)
        {
            var dir = 0;
            if (input.IsKeyJustPressed(Keys.Left)) dir -= 1;
            if (input.IsKeyJustPressed(Keys.Right)) dir += 1;
            bike.RotateBike(dir);
        }

        private bool IsNextCellEmpty(Bike bike, Grid grid)
        {
            var checkDist = bike.Speed;
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
