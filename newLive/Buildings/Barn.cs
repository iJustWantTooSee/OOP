using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Barn : Construction
    {
        public Barn(int x, int y, Map map)
            : base(x,y,map)
        {
            MaxCapacity = 20;
        }
    }
}
