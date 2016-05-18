using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class ShipChangedEventArgs: EventArgs
    {
        public Ship ShipUpdated { get; private set; }
        public bool Dead { get; private set;}

        public ShipChangedEventArgs(Ship ship, bool dead)
        {
            ShipUpdated = ship;
            Dead = dead;
        }
    }
}
