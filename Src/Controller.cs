using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public abstract class Controller
    {
        protected Bike bike;

        public Controller(Bike bike)
        {
            this.bike = bike;
        }

        public abstract void HandleInput();
    }
}
