using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    class PlayerController : Controller
    {
        private int dir;

        public PlayerController(Bike bike) : base(bike)
        {
            dir = 0;
        }

        public override void HandleInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) dir -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) dir += 1;
            bike.RotateBike(dir);
        }
    }
}
