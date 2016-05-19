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
        private const int interval = 15;
        private const int width = 3;
        private const int height = 10;


        public Point Location { get; private set; }

        
        public Direction Direction { get; set; }
        public Rectangle Boundaries { get; set;}

        ///private DateTime _lastMoved;

        public Shot(Point location, Direction direction, Rectangle boundaries)
        {
            Location = location;
            Direction = direction;
            Boundaries = boundaries;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, Location.X, Location.Y, width, height);
        }

        public bool Move()
        {
            Point newPlace;
            if (Direction == Direction.Down)
                newPlace = new Point(Location.X, Location.Y + interval);
            else
                newPlace = new Point(Location.X, Location.Y - interval);

            if (newPlace.Y < Boundaries.Height && newPlace.Y > 0)
            {
                Location = newPlace;
                return true;
            }
            return false;
        }



    }
}
