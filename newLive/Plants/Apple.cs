using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Apple: Grass, IEdibleForMoose, IEdibleForMouse, IEdibleForRabbit, IEdibleForBear, IEdibleForPig,
        IEdibleForRaccoon, IEdibleForHuman, IExtractionForWoman
    {
        public Apple(int x, int y, Map map)
            : base(x, y, map)
        {

        }
    }
}
