using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    struct Star
    {
        public Point Point { get; set; }
        public Brush Brush { get; set; }

        public Star(Point point, Brush brush)
        {
            Point = point;
            Brush = brush;
        }
    }

    
}
