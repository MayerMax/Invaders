using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class AllStars
    {
        private List<Star> stars;
        private Rectangle window;
        private Random rnd;

        public AllStars(Rectangle formArea)
        {
            stars = new List<Star>();

            for (int i = 0; i < 300; i++)
            {
                stars.Add(AddStar());
            }

        }

        private Star AddStar()
        {
            int height = window.Height;
            int width = window.Width;
            Random random = new Random();
            Point location = new Point(random.Next(0, width), random.Next(0, height));
            return new Star(location, Brushes.Aquamarine);

        }

        public Graphics Draw(Graphics g)
        {
            Graphics starGraphics = g;
            foreach (var star in stars)
            {
                starGraphics.FillRectangle(star.Brush, star.Point.X, star.Point.Y, 1, 1);

            }
            return starGraphics;
        }
        // просто мерцание
        public void Twinkle()
        {
            stars.RemoveRange(0, 4);
            // можно как-то получше
            // удаляет 4, а потом смещает весь лист ?
            for (int i = 0; i < 4; i++)
            {
                stars.Add(AddStar());
            }
        }

    }
}
