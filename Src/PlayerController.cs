using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PlayerController : Controller
    {
        private IInput input;

        public PlayerController(Bike bike, IInput input) : base(bike)
        {
            this.input = input;
        }

        public override void HandleInput()
        {
            var dir = 0;
            if (input.IsKeyJustPressed(Keys.Left)) dir -= 1;
            if (input.IsKeyJustPressed(Keys.Right)) dir += 1;
            bike.RotateBike(dir);
        }
    }
}
