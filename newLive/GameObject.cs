using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public abstract class GameObject
    {

        public Point CurrentCoordinate { get; protected set; }
        public Point PreviousCoordinate { get; protected set; }
        public bool IsExists { get; set; } = true;
        public bool IsEaten { get; set; } = false;
        
        protected Map _map { get; private set; }
        
        protected GameObject(int x, int y, Map map)
        {
            CurrentCoordinate = new Point(x, y);
            _map = map;
        }

    }
}
