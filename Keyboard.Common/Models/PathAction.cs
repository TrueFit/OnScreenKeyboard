using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.Common.Models
{
    public class PathAction
    {
        public PathAction(string name, int x, int y)
        {
            Name = name;
            XMovement = x;
            YMovement = y;
        }

        public string Name { get; set; }

        public int XMovement { get; set; }

        public int YMovement { get; set; }
    }
}
