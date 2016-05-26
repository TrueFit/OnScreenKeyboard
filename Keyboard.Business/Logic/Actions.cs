using Keyboard.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.Business.Logic
{
    public class Actions
    {
        public Actions()
        {
            Right = new PathAction("R", 1, 0);
            Left = new PathAction("L", -1, 0);
            Down = new PathAction("D", 0, 1);
            Up = new PathAction("U", 0, -1);
            Click = new PathAction("#", 0, 0);
            Space = new PathAction("S", 0, 0);
        }
        
        public PathAction Right
        {
            get;

        }
        
        public PathAction Left
        {
            get;
        }
        
        public PathAction Down
        {
            get;
        }

        public PathAction Up
        {
            get;
        }

        public PathAction Click
        {
            get;
        }

        public PathAction Space
        {
            get;
        }

    }
}
