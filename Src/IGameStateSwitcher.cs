using System;
using System.Collections.Generic;
using System.Text;

namespace LightBike.Src
{
    public interface IGameStateSwitcher
    {
        public void SetNextState(GameState gameState);
    }
}
