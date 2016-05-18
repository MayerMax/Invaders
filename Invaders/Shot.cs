using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class Shot
    {
        public const double ShotPixelsPerSecond = 90;

        public Point Location { get; private set; }

        public static Size ShotSize = new Size(2, 10);

        private Direction _direction;
        public Direction Direction { get; private set;}

        private DateTime _lastMoved;

        public Shot(Point location, Direction direction)
        {
            Location = location;
            _direction = direction;
            _lastMoved = DateTime.Now;
        }

        public void Move()
        {
            TimeSpan timeSinceLastMove = DateTime.Now - _lastMoved;
            double distanceToPass = timeSinceLastMove.Milliseconds * ShotPixelsPerSecond / 1000;
            if (Direction == Direction.Up)
                distanceToPass = distanceToPass * -1;
            Location = new Point(Location.X, Location.Y + (int)distanceToPass);
            _lastMoved = DateTime.Now;
        }



    }
}
