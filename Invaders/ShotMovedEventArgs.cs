using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class ShotMovedEventArgs: EventArgs
    {
        public Point Point { get; private set;}
        public bool Disappeared { get; private set; }

        public ShotMovedEventArgs(Point point, bool disappered)
        {
            Point = point;
            Disappeared = disappered;

        }

    }
}
