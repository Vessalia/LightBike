using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PlayerController : Controller
    {
        private int dir;
        private Input input;

        public PlayerController(Bike bike, Input input) : base(bike)
        {
            this.input = input;

            dir = 0;
        }

        public override void HandleInput()
        {
            if (input.IsKeyJustPressed(Keys.Left)) dir -= 1;
            if (input.IsKeyJustPressed(Keys.Right)) dir += 1;
            bike.RotateBike(dir);
        }
    }
}
