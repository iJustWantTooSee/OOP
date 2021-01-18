using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public abstract class Grass:GameObject
    {
        public Grass(int x, int y, Map map)
            : base(x,y,map)
        {

        }
    }
}
