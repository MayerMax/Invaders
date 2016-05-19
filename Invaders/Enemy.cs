using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class Enemy
    {
        private const int horizontalInterval = 10;
        private const int verticalInterval = 25;

        private Bitmap image;
        private Bitmap[] imageAnimation;

        public Point Location { get; private set; }
        public InvaderType InvaderType{ get; private set;}
        public int Score { get; private set; }
        public Rectangle Area
        {
            get
            {
                return new Rectangle(Location, imageAnimation[0].Size);
            }
                
        }

        

        public Enemy(InvaderType invader, Point location, int score)
        {
            InvaderType = invader;
            Location = location;
            Score = score;
            CreateInvader();
            image = imageAnimation[0];
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    Location = new Point((Location.X + horizontalInterval), Location.Y);
                    break;
                case Direction.Left:
                    Location = new Point((Location.X - horizontalInterval), Location.Y);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, (Location.Y + verticalInterval));
                    break;
            }
        }

        public Graphics Draw(Graphics g, int current)
        {
            Graphics invaderGraphic = g;
            image = imageAnimation[current];
            invaderGraphic.DrawImage(image, Location);
            return invaderGraphic;
        }


        private void CreateInvader()
        {
            imageAnimation = new Bitmap[4];
            switch (InvaderType)
            {
                case InvaderType.Bug:
                    imageAnimation[0] = Properties.Resources.bug1;
                    imageAnimation[1] = Properties.Resources.bug2;
                    imageAnimation[2] = Properties.Resources.bug3;
                    imageAnimation[3] = Properties.Resources.bug4;
                    break;
                case InvaderType.Satellite:
                    imageAnimation[0] = Properties.Resources.satellite1;
                    imageAnimation[1] = Properties.Resources.satellite2;
                    imageAnimation[2] = Properties.Resources.satellite3;
                    imageAnimation[3] = Properties.Resources.satellite4;
                    break;
                case InvaderType.Star:
                    imageAnimation[0] = Properties.Resources.star1;
                    imageAnimation[1] = Properties.Resources.star2;
                    imageAnimation[2] = Properties.Resources.star3;
                    imageAnimation[3] = Properties.Resources.star4;
                    break;
                case InvaderType.Dont_Know_In_English:
                    imageAnimation[0] = Properties.Resources.flyingsaucer1;
                    imageAnimation[1] = Properties.Resources.flyingsaucer2;
                    imageAnimation[2] = Properties.Resources.flyingsaucer3;
                    imageAnimation[3] = Properties.Resources.flyingsaucer4;
                    break;
                case InvaderType.SpaceShip:
                    imageAnimation[0] = Properties.Resources.spaceship1;
                    imageAnimation[1] = Properties.Resources.spaceship2;
                    imageAnimation[2] = Properties.Resources.spaceship3;
                    imageAnimation[3] = Properties.Resources.spaceship4;
                    break;

            }
        }

    }
}
