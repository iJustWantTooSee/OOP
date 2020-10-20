using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newLive
{
    public class GameEngine
    {
        public const int _rows = 1000;
        public const int _cols = 1000;
        public int speedLife = 500;
        public const int INCREASED_SPEED_LIFE = 20;
        private bool isClickPause = false;
        public long PeriodOfLife = 0;
        public const int startUnitCout = 555;
        public int nameOfUnit = 0;
        private int _x;
        private int _y;


        public Map map;
        public Meal GrassOnMap;

        Random random = new Random();
        public GameEngine(Map map)
        {
            nameOfUnit = 0;
            this.map = map;
            for (int i = 0; i < startUnitCout; i++)
            {
                _x = random.Next(_cols);
                _y = random.Next(_rows);
                map.CurrentGenerationOfUnits.Add(new Unit(_x, _y, random.Next(3), random, map, nameOfUnit++, setUnitGender()));
            }

            GrassOnMap = new Meal(map);
            map.currentValuePopulation = startUnitCout;

        }

        public List<Unit> NextUpdateUnit()
        {
            List<Unit> CurrentUnit = new List<Unit>();
            List<Unit> deadUnit = new List<Unit>();

            foreach (var unit in map.CurrentGenerationOfUnits)
            {
                unit.GetNewUnitPosition(_cols, _rows);
                CurrentUnit.Add(unit);
                if (!unit.IsLife)
                {
                    deadUnit.Add(unit);
                }

            }
            foreach (var child in map.CurrentGenerationOfChildUnits)
            {
                map.CurrentGenerationOfUnits.Add(child);
                CurrentUnit.Add(child);
            }
            map.CurrentGenerationOfChildUnits.Clear();
            while (deadUnit.Count > 0)
            {
                map.CurrentGenerationOfUnits.Remove(deadUnit.First());
                map.ListGrassOnMap.Add(deadUnit.First().CurrentUnitCoordinate);
                map.MapGridWithInfotmation[deadUnit.First().CurrentUnitCoordinate.X, deadUnit.First().CurrentUnitCoordinate.X]
                        = (int)Map.StateOfPoint.PointWithGrass;
                deadUnit.RemoveAt(0);
            }
            PeriodOfLife++;
            return CurrentUnit;
        }

        public List<Point> NextUpdateGrass()
        {
            GrassOnMap.AppereanceFood();
            List<Point> temp = new List<Point>();
            foreach (var item in map.ListGrassOnMap)
            {
                Point newLocation = item;
                temp.Add(newLocation);
            }
            return temp;
        }

        public bool CheakClickPause()
        {
            if (!isClickPause)
            {
                isClickPause = true;
                return isClickPause;
            }
            else
            {
                isClickPause = false;
                return isClickPause;
            }
        }
        public int SetSpeedLife(int currentSpeedLife)
        {
            if (currentSpeedLife > 10)
            {
                return speedLife + (-INCREASED_SPEED_LIFE * currentSpeedLife);
            }
            else if (currentSpeedLife < 10)
            {
                return speedLife + (INCREASED_SPEED_LIFE * currentSpeedLife);
            }
            else
            {
                return speedLife;
            }
        }

        public Unit GetUnitInfo(int x, int y)
        {
            Unit foundUnit = map.CurrentGenerationOfUnits[0];
            foreach(var item in map.CurrentGenerationOfUnits)
            {
                if (item.CurrentUnitCoordinate.X ==x && item.CurrentUnitCoordinate.Y == y)
                {
                    foundUnit = item;
                }
            }
            return foundUnit;
        }

        public void UpdateInfoUsingTheMause(int x, int y, int state)
        {
            Point currentPoint = new Point();
            currentPoint.X = x;
            currentPoint.Y = y;
            switch (state)
            {
                case (int)StateButton.createUnit:
                    map.CurrentGenerationOfUnits.Add(new Unit(x, y, random.Next(0, 4), random, map, ++map.currentValuePopulation, setUnitGender()));
                    break;
                case (int)StateButton.delUnit:
                    UnitToDelet(currentPoint);
                    break;
                case (int)StateButton.createGrass:
                    map.AddGrassToMap(currentPoint);
                    break;
                case (int)StateButton.delGrass:
                    map.UppdateInformationGrass(currentPoint, (int)Map.StateOfPoint.EmptyPoint);
                    break;
                default:
                    break;
            }
           
        }
        public int setUnitGender()
        {
            int value = random.Next(1, 4);
            int gender = 0;
            switch (value)
            {
                case 1:
                    gender = (int)Gender.Man;
                    break;
                case 2:
                    gender = (int)Gender.Woman;
                    break;
                case 3:
                    gender = (int)Gender.Super_Creature;
                    break;
                default:
                    gender = (int)Gender.Super_Creature;
                    break;
            }
            return gender;
        }

        public void UnitToDelet(Point unitToDel)
        {
            Unit delUnit = map.CurrentGenerationOfUnits.First();
            foreach (var unit in map.CurrentGenerationOfUnits)
            {
                if (unit.CurrentUnitCoordinate.X == unitToDel.X && unit.CurrentUnitCoordinate.Y == unitToDel.Y)
                {
                    delUnit = unit;
                }
            }
            map.AddGrassToMap(delUnit.CurrentUnitCoordinate);
            map.CurrentGenerationOfUnits.Remove(delUnit);
        }
        public enum Gender
        {
            Man = 10,
            Woman = 20,
            Super_Creature = 30,
        }

        public enum StateButton
        {
            createUnit,
            delUnit,
            createGrass,
            delGrass
        }
    }
}
