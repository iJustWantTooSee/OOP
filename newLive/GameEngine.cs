using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newLive
{
     class Unit
    {
        Random random = new Random();
        public int x { get; set; } = 0;
        public int y { get; set; } = 0;
        public int size { get; set; } = 0;

        public Unit(int x, int y, int size)
        {
            this.x = x;
            this.y = y;
            this.size = size;
        }
        public Pair<int, int> moveUnit(int cols, int rows)
        {
            Random random = new Random();
            int side = random.Next(8);
            switch (side)
            {
                case 0:
                    if (y - size > 1)
                    {
                        y = y - size - 1;
                    }
                    break;
                case 1:
                    if (y + size < cols - 1)
                    {
                        y += size + 1;
                    }
                    break;
                case 2:
                    if (x - size > 1)
                    {
                        x = x - size - 1;
                    }
                    break;
                case 3:
                    if (x + size < rows - 1)
                    {
                        x += size + 1;
                    }
                    break;
                case 4:
                    if (x + size < rows - 1 && y + size < cols - 1)
                    {
                        x += size + 1;
                        y += size + 1;
                    }
                    break;
                case 5:
                    if (x + size < rows - 1 && y - size > 1)
                    {
                        x += size + 1;
                        y = y - size - 1; ;
                    }
                    break;
                case 6:
                    if (x - size > 1 && y + size < cols - 1)
                    {
                        x = x - size - 1;
                        y += size + 1;
                    }
                    break;
                case 7:
                    if (x - size > 1 && y - size > 1)
                    {
                        x = x - size - 1;
                        y = y - size - 1;
                    }
                    break;


                default:
                    break;
            }
            return new Pair<int, int>(x, y);
        }

    }
    public class GameEngine
    {
        private int rows;
        private int cols;
        private List<Unit> people = new List<Unit>();
        Random random = new Random();
        public GameEngine(int rows, int cols)
        {
            this.cols = cols;
            this.rows = rows;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    if (random.Next(500) == 0)
                    {
                        people.Add(new Unit(i, j,random.Next(4))); 
                    }
                }
            }

        }

        public  List<Pair<int, int>> NextUpdate()
        {
            List<Pair<int, int>> temp = new List<Pair<int, int> > ();
            foreach (var item in people)
            {
                Pair<int, int> newLocation = item.moveUnit(cols, rows);
                temp.Add(newLocation);
            }
            return temp;
        }
    }

    public class Pair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }
        public Pair(T first, K second)
        {
            First = first;
            Second = second;
        }
    }
}
