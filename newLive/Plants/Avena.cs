using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Avena: Grass, IEdibleForMoose, IEdibleForMouse, IEdibleForRabbit,
        IEdibleForRaccoon, IEdibleForHuman, IExtractionForWoman
    {
        public Avena(int x, int y, Map map)
            : base(x, y, map)
        {

        }
    }
}
