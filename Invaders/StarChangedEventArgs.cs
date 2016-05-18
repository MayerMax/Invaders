using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Invaders
{
    class StarChangedEventArgs: EventArgs
    {
       public Point Point { get; private set; }
       public bool Dissapeared { get; private set; }

        public StarChangedEventArgs(Point point, bool disappeared)
        {
            Point = point;
            Dissapeared = disappeared;

        }


    }
}
