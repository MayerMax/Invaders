using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class Player
    {
        private const int interval = 10;

        public Point Location { get; set; }
        public Bitmap image = Properties.Resources.player;
        public Rectangle Area
        {
            get { return new Rectangle(Location, image.Size);}

        }

        private DateTime deathAwaiting;
        private float deathHeight;
        private bool isAlive;
        public bool Alive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
                if (!value)
                    deathAwaiting = DateTime.Now;

            }
        }

        public Rectangle Boundaries { get; set; }

        public Player(Rectangle boundaries, Point location)
        {
            Location = location;
            Boundaries = boundaries;
            isAlive = true;
            deathHeight = 1.0f;
        }

        public void Move(Direction direction)
        {
            if (isAlive)
            {
                if (direction == Direction.Left)
                {
                    Point newPlace = new Point(Location.X - interval, Location.Y);
                    if ((newPlace.X < (Boundaries.Width - 100)) && (newPlace.X > 50))
                        Location = newPlace;
                }
                else if (direction == Direction.Right)
                {
                    Point newPlace = new Point(Location.X + interval, Location.Y);
                    if ((newPlace.X < (Boundaries.Width - 100)) && (newPlace.X > 50))
                        Location = newPlace;
                }
            }
        }

        public void Draw(Graphics g)
        {
            if (!Alive)
            {
                if ((DateTime.Now - deathAwaiting) > TimeSpan.FromSeconds(1.5))
                {
                    deathHeight = 0.0f;
                    isAlive = true;
                }
                else if ((DateTime.Now - deathAwaiting) > TimeSpan.FromSeconds(1))
                {
                    deathHeight = 0.25f;
                }
                else if ((DateTime.Now - deathAwaiting) > TimeSpan.FromSeconds(0.5))
                {
                    deathHeight = 0.75f;
                }
                else if ((DateTime.Now - deathAwaiting) > TimeSpan.FromSeconds(0))
                {
                    deathHeight = 0.9f;
                }
                g.DrawImage(image, Location.X, Location.Y, image.Width, (image.Height * deathHeight));
            }
            else
            {
                g.DrawImage(image, Location);

            }
        }

        
    }
}
