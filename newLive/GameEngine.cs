using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newLive
{
    public class Unit 
    {
        Random random = new Random();
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public int Step { get; set; } = 0;

        public Unit(int x, int y, int step)
        {
            X = x;
            Y = y;
            Step = step;
        }
        public Pair<int, int> GetNewUnitPosition(int cols, int rows)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            int side = random.Next(8);
            switch (side)
            {
                case 0:
                    if (Y - Step > 1)
                    {
                        Y = Y - Step - 1;
                    }
                    break;
                case 1:
                    if (Y + Step < cols - 1)
                    {
                        Y += Step + 1;
                    }
                    break;
                case 2:
                    if (X - Step > 1)
                    {
                        X = X - Step - 1;
                    }
                    break;
                case 3:
                    if (X + Step < rows - 1)
                    {
                        X += Step + 1;
                    }
                    break;
                case 4:
                    if (X + Step < rows - 1 && Y + Step < cols - 1)
                    {
                        X += Step + 1;
                        Y += Step + 1;
                    }
                    break;
                case 5:
                    if (X + Step < rows - 1 && Y - Step > 1)
                    {
                        X += Step + 1;
                        Y = Y - Step - 1; ;
                    }
                    break;
                case 6:
                    if (X - Step > 1 && Y + Step < cols - 1)
                    {
                        X = X - Step - 1;
                        Y += Step + 1;
                    }
                    break;
                case 7:
                    if (X - Step > 1 && Y - Step > 1)
                    {
                        X = X - Step - 1;
                        Y = Y - Step - 1;
                    }
                    break;


                default:
                    break;
            }
            return new Pair<int, int>(X, Y);
        }

    }
    public class GameEngine
    {
        private int _rows;
        private int _cols;
        private List<Unit> people = new List<Unit>();
        Random random = new Random((int)DateTime.Now.Ticks);
        public GameEngine(int rows, int cols)
        {
            this._cols = cols;
            this._rows = rows;
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
                Pair<int, int> newLocation = item.GetNewUnitPosition(_cols, _rows);
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
