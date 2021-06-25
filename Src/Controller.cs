using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public abstract class Controller
    {
        private bool indicator = true;

        public virtual void HandleInput(Bike bike, Grid grid)
        {
            if (indicator)
            {
                DoInput(bike, grid);
                indicator = false;
            }
        }

        public void ResetIndicator()
        {
            indicator = true;
        }

        protected abstract void DoInput(Bike bike, Grid grid);
    }
}
