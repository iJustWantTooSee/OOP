using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newLive
{
    public class Unit
    {
        private const int WASTE_SATIETY_PER_TURN = 1;
        private const int REQUIRED_OFFSET_UNIT = 1;
        private const int NUTRITIONAL_VALUE_GRASS = 100;

        public int UnitSatiety = 200;
        public int UnitGender;
        public int UnitNumber;
        public bool IsLife = true;
        private int _birthUnit = 500;
        private int intervalBetweenBirth = 0;

        public Point CurrentUnitCoordinate;
        public Point OldUnitCoordinate;

        private Point PointWithFood = new Point();
        private bool firstSearchFood = true;
        public Map map;
        public int AdditionlOffsetUnit { get; set; } = 0;
        public Random RandomValue { get; set; } = new Random();
        public Unit(int x, int y, int randomStep, Random random, Map map, int number, int unitGender)
        {
            this.map = map;
            CurrentUnitCoordinate.X = x;
            CurrentUnitCoordinate.Y = y;
            UnitNumber = number;
            UnitGender = unitGender;
            AdditionlOffsetUnit = randomStep;
            RandomValue = random;

        }
        public void GetNewUnitPosition(int cols, int rows)
        {
            OldUnitCoordinate = CurrentUnitCoordinate;
            if (UnitSatiety > 100)
            {
                UpdaeteSatietyUnit();
            }
            else if (UnitSatiety > 0)
            {
                UpdateHungryUnit();
            }
            else
            {
                IsLife = false;
            }

            if (IsLife)
            {
                map.UpdateUnitPosition(OldUnitCoordinate, CurrentUnitCoordinate);
            }

        }

        private void UpdaeteSatietyUnit()
        {
            if (intervalBetweenBirth >= _birthUnit)
            {
                movedToCouple(SearchPointTheNearestNeighbours());
            }
            MovementWhenSatiety();
            UnitSatiety -= (AdditionlOffsetUnit + WASTE_SATIETY_PER_TURN);
            intervalBetweenBirth += WASTE_SATIETY_PER_TURN;
        }
        private void UpdateHungryUnit()
        {
            if (map.ListGrassOnMap.Count > 0)
            {
                if (firstSearchFood)
                {
                    PointWithFood = SearchPointTheNearestFood();
                    firstSearchFood = false;
                    movedToGrass(PointWithFood);
                }
                else
                {
                    movedToGrass(PointWithFood);
                    if (map.MapGridWithInfotmation[PointWithFood.X, PointWithFood.Y] != 2)
                    {
                        firstSearchFood = true;
                    }
                }

                UnitSatiety -= (AdditionlOffsetUnit + WASTE_SATIETY_PER_TURN);
                intervalBetweenBirth += WASTE_SATIETY_PER_TURN;
            }
            else
            {
                MovementWhenSatiety();
                UnitSatiety--;
                intervalBetweenBirth += WASTE_SATIETY_PER_TURN;
            }
        }

        private void MovementWhenSatiety()
        {
            Point offset = new Point();

            for (int i = 0; i < 1;)
            {
                Point additionalOffset = new Point();
                offset.X = RandomValue.Next(-1, 2);
                offset.Y = RandomValue.Next(-1, 2);
                if (offset.X < 0)
                {
                    additionalOffset.X = -AdditionlOffsetUnit;
                }
                if (offset.Y < 0)
                {
                    additionalOffset.Y = -AdditionlOffsetUnit;
                }
                if (!map.checkGoingOutTheMap(CurrentUnitCoordinate.X, CurrentUnitCoordinate.Y,
                    offset.X + additionalOffset.X, offset.Y + additionalOffset.Y))
                {
                    CurrentUnitCoordinate.X += (offset.X + additionalOffset.X);
                    CurrentUnitCoordinate.Y += (offset.Y + additionalOffset.Y);
                    i++;
                }
            }
            //int side = RandomValue.Next(8);
            //switch (side)
            //{
            //    case 0:
            //        if (CurrentUnitCoordinate.Y - AdditionlOffsetUnit > 1)
            //        {
            //            CurrentUnitCoordinate.Y -= (AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT);
            //        }
            //        break;
            //    case 1:
            //        if (CurrentUnitCoordinate.Y + AdditionlOffsetUnit < cols - 1)
            //        {
            //            CurrentUnitCoordinate.Y += AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT;
            //        }
            //        break;
            //    case 2:
            //        if (CurrentUnitCoordinate.X - AdditionlOffsetUnit > 1)
            //        {
            //            CurrentUnitCoordinate.X -= (AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT);
            //        }
            //        break;
            //    case 3:
            //        if (CurrentUnitCoordinate.X + AdditionlOffsetUnit < rows - 1)
            //        {
            //            CurrentUnitCoordinate.X += AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT;
            //        }
            //        break;
            //    case 4:
            //        if (CurrentUnitCoordinate.X + AdditionlOffsetUnit < rows - 1 && CurrentUnitCoordinate.Y + AdditionlOffsetUnit < cols - 1)
            //        {
            //            CurrentUnitCoordinate.X += AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT;
            //            CurrentUnitCoordinate.Y += AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT;
            //        }
            //        break;
            //    case 5:
            //        if (CurrentUnitCoordinate.X + AdditionlOffsetUnit < rows - 1 && CurrentUnitCoordinate.Y - AdditionlOffsetUnit > 1)
            //        {
            //            CurrentUnitCoordinate.X += AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT;
            //            CurrentUnitCoordinate.Y -= (AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT);
            //        }
            //        break;
            //    case 6:
            //        if (CurrentUnitCoordinate.X - AdditionlOffsetUnit > 1 && CurrentUnitCoordinate.Y + AdditionlOffsetUnit < cols - 1)
            //        {
            //            CurrentUnitCoordinate.X -= (AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT);
            //            CurrentUnitCoordinate.Y += AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT;
            //        }
            //        break;
            //    case 7:
            //        if (CurrentUnitCoordinate.X - AdditionlOffsetUnit > 1 && CurrentUnitCoordinate.Y - AdditionlOffsetUnit > 1)
            //        {
            //            CurrentUnitCoordinate.X -= (AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT);
            //            CurrentUnitCoordinate.Y -= (AdditionlOffsetUnit + REQUIRED_OFFSET_UNIT);
            //        }
            //        break;
            //    default:
            //        break;
            //}
        }

        private void movedToCouple(Point currentPurpose)
        {
            if (CurrentUnitCoordinate.X == currentPurpose.X && CurrentUnitCoordinate.Y == currentPurpose.Y)
            {
                Unit currentUnit = getBaby();
                map.AddUnitToMap(currentUnit);
                intervalBetweenBirth = 0;
            }
            MovementToPurpose(currentPurpose);
        }

        private void movedToGrass(Point currentPurpose)
        {
            if (CurrentUnitCoordinate.X == currentPurpose.X && CurrentUnitCoordinate.Y == currentPurpose.Y)
            {
                map.UppdateInformationGrass(CurrentUnitCoordinate, Map.StateOfPoint.EmptyPoint);
                UnitSatiety += NUTRITIONAL_VALUE_GRASS;
                firstSearchFood = true;
            }
            MovementToPurpose(currentPurpose);
        }

        private void MovementToPurpose(Point currentPurpose)
        {
            if (CurrentUnitCoordinate.X > currentPurpose.X)
            {
                CurrentUnitCoordinate.X--;
            }
            else if (CurrentUnitCoordinate.X < currentPurpose.X)
            {
                CurrentUnitCoordinate.X++;
            }

            if (CurrentUnitCoordinate.Y > currentPurpose.Y)
            {
                CurrentUnitCoordinate.Y--;
            }
            else if (CurrentUnitCoordinate.Y < currentPurpose.Y)
            {
                CurrentUnitCoordinate.Y++;
            }
        }

        private Unit getBaby()
        {
            return new Unit(CurrentUnitCoordinate.X, CurrentUnitCoordinate.Y, RandomValue.Next(0, 4), RandomValue, map, ++map.currentValuePopulation, UnitGender);
        }

        private Point SearchPointTheNearestNeighbours()
        {
            double distance = 0;
            double minDistance = map.SizeMap * map.SizeMap;
            Point nearestNeighbours = new Point();
            foreach (var unit in map.CurrentGenerationOfUnits)
            {
                if (unit.UnitGender != UnitGender)
                {
                    distance = calculationDistance(CurrentUnitCoordinate, unit.CurrentUnitCoordinate);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestNeighbours = unit.CurrentUnitCoordinate;
                    }
                }
            }
            return nearestNeighbours;
        }

        private Point SearchPointTheNearestFood()
        {
            double distance;
            Point pointWithFood = map.ListGrassOnMap.First();
            double minDistance = calculationDistance(CurrentUnitCoordinate, map.ListGrassOnMap.First());
            foreach (var grassPoint in map.ListGrassOnMap)
            {
                distance = calculationDistance(CurrentUnitCoordinate, grassPoint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    pointWithFood = grassPoint;
                }
            }
            return pointWithFood;
        }

        private double calculationDistance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow((end.X - start.X), 2) + Math.Pow((end.Y - start.Y), 2));
        }

    }
}
