using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Map
    {
        //public int Size { get; private set; } = 1000;
        private int _size = 1000;
        public int Size
        {
            get
            {
                return _size;
            }
        }
        private int _detailing = 5;
        public season Season { get; set; }
        public int CurrentValuePopulation { get; set; } = 0;

        public List<GameObject> ListUnitAndGrass { get; private set; } = new List<GameObject>();
        private List<GameObject> ListDeadUnit { get; set; } = new List<GameObject>();
        private List<GameObject> _listConstruction { get; set; } = new List<GameObject>();
        public List<GameObject> ListResouces { get; set; } = new List<GameObject>();
        public Meal MealOnMap { get; private set; }

        public Cell[,] CellOnMap;

        private PerlinNoise _perlinNoise;

        public Map(int sizeMap)
        {
            _size = sizeMap;
            CellOnMap = new Cell[_size, _size];
            _perlinNoise = new PerlinNoise();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    CellOnMap[x, y] = new Cell(_perlinNoise.Noise(x, y, _detailing));
                    SetStatePoint(x, y);
                }
            }

        }

        private void SetStatePoint(int x, int y)
        {
            if (CellOnMap[x, y].NoiseValue < -0.15)
            {
                CellOnMap[x, y].Colors = new List<Brush> { Brushes.LightSkyBlue, Brushes.LightSkyBlue, Brushes.Aqua, Brushes.Aqua };
                CellOnMap[x, y].State = StateOfPoint.Water;
            }
            else if (CellOnMap[x, y].NoiseValue < -0.05)
            {
                CellOnMap[x, y].Colors = new List<Brush> { Brushes.Yellow, Brushes.Goldenrod, Brushes.Gold, Brushes.Goldenrod };
                CellOnMap[x, y].State = StateOfPoint.Sand;
            }
            else if (CellOnMap[x, y].NoiseValue < 0.2)
            {
                CellOnMap[x, y].Colors = new List<Brush> { Brushes.Green, Brushes.GreenYellow, Brushes.YellowGreen, Brushes.Lime };
                CellOnMap[x, y].State = StateOfPoint.Grass;
            }
            else if (CellOnMap[x, y].NoiseValue < 0.31)
            {
                CellOnMap[x, y].Colors = new List<Brush> { Brushes.DarkSlateGray, Brushes.DarkSlateGray, Brushes.DarkSlateGray, Brushes.DarkSlateGray };
                CellOnMap[x, y].State = StateOfPoint.TopHill;
            }
            else
            {
                CellOnMap[x, y].Colors = new List<Brush> { Brushes.Gray, Brushes.Gainsboro, Brushes.White, Brushes.Gainsboro };
                CellOnMap[x, y].State = StateOfPoint.Snow;
            }
        }

        public void SetNewMapColor()
        {
            switch (Season)
            {
                case season.summer:
                    for (int y = 0; y < _size; y++)
                    {
                        for (int x = 0; x < _size; x++)
                        {

                        }
                    }
                    break;
            }
        }

        public bool SetMealOnMap(Meal meal)
        {
            if (meal != null)
            {
                MealOnMap = meal;
                return true;
            }
            return false;
        }

        public void AddChildren(UnitWithoutGeneric unit)
        {
            ListUnitAndGrass.Add(unit);
        }

        public IEnumerable<Unit<T>> GetListWhithPossiblePartners<T>(Type type) where T : IEdible
        {
            foreach (var currentUnit in ListUnitAndGrass)
            {
                if (currentUnit.GetType() == type)
                {
                    yield return (Unit<T>)currentUnit;
                }

            }

        }

        public IEnumerable<GameObject> GetFood<TFood>() where TFood : IEdible
        {
            foreach (GameObject food in ListUnitAndGrass)
            {
                if (food is TFood)
                {
                    yield return food;
                }

            }
        }
        public void AddGrassToMap(GameObject pointGrassOnMap)
        {
            ListUnitAndGrass.Add(pointGrassOnMap);
        }


        public void RemoveEatenFood(GameObject currentObject)
        {
            if (ListUnitAndGrass.Any())
            {
                ListUnitAndGrass.Remove(currentObject);
            }
        }

        public void AddTree(GameObject obj)
        {
            ListResouces.Add(obj);
        }

        public void ClearDeadUnit()
        {
            ListDeadUnit = new List<GameObject>();
        }

        public List<GameObject> GetListDeadUnit()
        {
            List<GameObject> copyListDeadUnit = ListDeadUnit.ToList<GameObject>();
            ClearDeadUnit();
            return copyListDeadUnit;
        }

        public List<GameObject> GetListWithResouces()
        {
            return ListResouces;
        }

        public Point GetRandomPointWithOffset(int range, Point searchCenter, Random rand)
        {
            bool isNewPoint = false;

            int offsetX = rand.Next(-range / 2, range / 2 + 1);
            int offsetY = rand.Next(-range / 2, range / 2 + 1);

            Point newPoint = new Point(searchCenter.X,
                searchCenter.Y);
            if (CellOnMap[searchCenter.X, searchCenter.Y].State == StateOfPoint.Snow
                || CellOnMap[searchCenter.X, searchCenter.Y].State == StateOfPoint.Water)
            {
                range *= 5;
            }
            while (isNewPoint == false)
            {
                if (IsGoingOutTheMap(newPoint.X, newPoint.Y, offsetX, offsetY) == true
                    || Math.Abs(offsetX) < 3
                    || Math.Abs(offsetY) < 3)
                {
                    offsetX = rand.Next(-range / 2, range / 2 + 1);
                    offsetY = rand.Next(-range / 2, range / 2 + 1);
                }
                else
                {
                    if (IsMayAppear(newPoint.X + offsetX, newPoint.Y + offsetY))
                    {
                        newPoint = new Point(newPoint.X + offsetX, newPoint.Y + offsetY);
                        isNewPoint = true;
                    }
                    else
                    {
                        offsetX = rand.Next(-range / 2, range / 2 + 1);
                        offsetY = rand.Next(-range / 2, range / 2 + 1);
                    }
                }
            }
            return newPoint;
        }


        public void AddHouse(House house)
        {
            if (house != null)
            {
                _listConstruction.Add(house);
            }
        }

        public void AddBarn(Barn barn)
        {
            if (barn != null)
            {
                _listConstruction.Add(barn);
            }
        }

        public List<GameObject> GetListHouses()
        {
            return _listConstruction.OfType<House>().ToList<GameObject>();
        }

        public List<GameObject> GetListBarn()
        {
            return _listConstruction.OfType<Barn>().ToList<GameObject>();
        }

        public List<GameObject> GetListConstruction()
        {
            return _listConstruction.Where(house => ((Construction)house).IsBuilt == true).ToList();
        }

        public GameObject GetHouseByCoordinate(Point pointHouse)
        {
            return _listConstruction.Find(obj => obj.CurrentCoordinate == pointHouse);
        }

        public bool IsGoingOutTheMap(int currentX, int currentY, int offsetX, int offsetY)
        {
            if ((currentX + offsetX > _size - 1) || (currentX + offsetX < 1)
                || (currentY + offsetY > _size - 1) || (currentY + offsetY < 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsMayAppear(int x, int y)
        {
            if (CellOnMap[x, y].State == StateOfPoint.Water || CellOnMap[x, y].State == StateOfPoint.Snow
                || CellOnMap[x, y].State == StateOfPoint.House || CellOnMap[x, y].State == StateOfPoint.Barn)
            {
                return false;
            }
            return true;
        }

    }
}
