using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using System.Drawing;

namespace Invaders
{
   abstract class Ship
    {
        public Point Location { get; protected set; }

        public Size Size { get; private set; }

        public Rectangle Area { get { return new Rectangle(Location, Size); } }

        public Ship(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public abstract void Move(Direction direction);
   
    }
}
