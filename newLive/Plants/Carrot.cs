using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Carrot: Grass, IEdibleForMoose, IEdibleForMouse, IEdibleForRabbit, IEdibleForBear, IEdibleForPig,
        IEdibleForHuman, IExtractionForWoman
    {
        public Carrot(int x, int y, Map map)
            : base(x, y, map)
        {

        }
    }
}
