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
        }

        protected override void DoInput(Bike bike, Grid grid)
        {
            var dir = 0;
            if (input.IsKeyJustPressed(Keys.Left)) dir -= 1;
            if (input.IsKeyJustPressed(Keys.Right)) dir += 1;
            bike.RotateBike(dir);
        }
    }
}
